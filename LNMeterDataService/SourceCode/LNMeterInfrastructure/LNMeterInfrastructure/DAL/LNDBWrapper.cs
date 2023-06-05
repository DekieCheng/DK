using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using LNInterfaces;
using System.Configuration;
using LNMeterInfrastructure.Common;

public abstract class DbConnectionBase
{
    protected string GetConnectionString(string server, string database, string username, string password, bool integratedSecurity)
    {
        SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
        {
            var withBlock = sqlConnectionStringBuilder;
            withBlock.DataSource = server;
            withBlock.InitialCatalog = database;
            withBlock.ApplicationName = "Lean Service";
            withBlock.IntegratedSecurity = integratedSecurity;
            withBlock.UserID = (string)Interaction.IIf(!integratedSecurity, username, string.Empty);
            withBlock.Password = (string)Interaction.IIf(!integratedSecurity, password, string.Empty);
        }

        return sqlConnectionStringBuilder.ConnectionString;
    }
}

public class LNDBWrapper : DbConnectionBase
{
    private string m_Server;
    private string m_DataBase;
    private string m_UserName;
    private string m_Pwd;
    private bool m_ConnStatus;
    private SqlConnection m_Con;

    private const string DB_CALL_LOG = "LNMeterDBCall.log";
    private const string DB_CALL_ARCHIVE_LOG = "LNMeterDBCall-Archive.log";
    private const long FIVEMB = 5242880;
    private const int DB_CUSTOM_ERROR_START = 50000;
    private const int DB_ERROR_CLASS = 18;
    private const int DB_ERROR_STATE = 127;

    private const string DYN_SQL_EXECUTION = "udpLNExecuteDynamicQuery";

    private int m_CommandTimeOut = 120;
    private Hashtable m_CmdCollection = new Hashtable();
    private bool m_IntegratedSecurity = false;
    private string m_DetailErrorLog = "";
    private Impersonator m_impersonatorContext;
    private Impersonator m_impersonatorContextLocal;

    public LNDBWrapper()
    {
        AssignDBInfo();
    }

    private void AssignDBInfo()
    {
        try
        {
            string[] dbInfoArr = ConfigurationManager.AppSettings.Get("ConnectionString").Split(new char[] { ';' });
            m_Server = GetValByIndex(dbInfoArr[0]);
            m_UserName = GetValByIndex(dbInfoArr[1]);
            m_Pwd = LNGlobal.DecryptString(GetValByIndex(dbInfoArr[2])).TrimEnd(new char[] { '=' });
            m_DataBase = GetValByIndex(dbInfoArr[3]);
            m_IntegratedSecurity = GetValByIndex(dbInfoArr[4]).ToUpper().Equals("YES");

        }
        catch (Exception ex)
        {

        }
    }

    private string GetValByIndex(string orgStr)
    {
        return orgStr.Substring(orgStr.IndexOf('=') + 1);
    }

    public string Server
    {
        get
        {
            return m_Server;
        }
        set
        {
            m_Server = value;
        }
    }

    public string DBName
    {
        get
        {
            return m_DataBase;
        }
        set
        {
            m_DataBase = value;
        }
    }

    public string UserName
    {
        get
        {
            return m_UserName;
        }
        set
        {
            m_UserName = value;
        }
    }

    public string Pwd
    {
        get
        {
            return m_Pwd;
        }
        set
        {
            m_Pwd = value;
        }
    }

    public int CommandTimeOut
    {
        get
        {
            return m_CommandTimeOut;
        }
        set
        {
            m_CommandTimeOut = value;
        }
    }

    public bool IntegratedSecurity
    {
        get
        {
            return m_IntegratedSecurity;
        }
        set
        {
            m_IntegratedSecurity = value;
        }
    }

    public string ConnectionString
    {
        get
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = m_Server;
            builder.InitialCatalog = m_DataBase;
            builder.IntegratedSecurity = m_IntegratedSecurity;

            if (m_IntegratedSecurity)
            {
                if (!string.IsNullOrEmpty(m_UserName))
                    builder.UserID = m_UserName;

                if (!(m_Pwd == null || string.IsNullOrEmpty(m_Pwd)))
                    builder.Password = m_Pwd;
            }

            return builder.ConnectionString;
        }
    }

    private SqlConnection Connection
    {
        get
        {
            if (m_Con == null)
                m_Con = new SqlConnection();
            if (m_Con.State == ConnectionState.Closed)
            {
                if (m_Server == "")
                {
                    ConnectionStatus = false;
                    return null;
                }
                try
                {
                    m_Con.ConnectionString = GetConnectionString();
                    Impersonate();
                    m_Con.Open();
                    ConnectionStatus = true;
                }
                catch (Exception ex)
                {
                    ConnectionStatus = false;
                    throw ex;
                }
            }
            return m_Con;
        }
    }

    public bool ConnectionStatus
    {
        get
        {
            return m_ConnStatus;
        }
        set
        {
            m_ConnStatus = value;
        }
    }

    private void CloseConnection()
    {
        if (m_Con != null)
        {
            if (m_Con.State == ConnectionState.Open)
            {
                try
                {
                    m_Con.Close();
                }
                catch (Exception ex)
                {
                    ConnectionStatus = false;
                    throw ex;
                }
            }
        }

        UnImpersonate();
    }

    public bool IsIPAddress(string sString)
    {
        try
        {
            string[] sNumbers;

            sNumbers = sString.Split(new char[] { '.' });

            if (sNumbers.Length != 4)
                return false;
            foreach (string s in sNumbers)
            {
                if (!Information.IsNumeric(s))
                    return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public DataSet ExecuteSPWithResults(string SPName, ref int returnValue, CommandBehavior CommandBehaviour = CommandBehavior.Default)
    {
        DataSet DS = new DataSet();
        SqlCommand Cmd = new SqlCommand(SPName);

        try
        {
            Cmd.Connection = this.Connection;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandTimeout = m_CommandTimeOut;

            SqlParameter RetParam = new SqlParameter();
            {
                var withBlock = RetParam;
                withBlock.ParameterName = "@RETURN_VALUE";
                withBlock.Direction = ParameterDirection.ReturnValue;
                withBlock.SqlDbType = SqlDbType.Int;
            }

            Cmd.Parameters.Add(RetParam);

            SqlDataAdapter DA = new SqlDataAdapter(Cmd);
            string sExceptionMsg = "";
            try
            {
                DA.Fill(DS);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }

            WriteSPToFile(SPName);
            if (!string.IsNullOrEmpty(sExceptionMsg))
                WriteErrorToFile(sExceptionMsg);
            returnValue = (int)Cmd.Parameters["@RETURN_VALUE"].Value;

            WriteReturnValueToFile(returnValue);
        }
        catch (Exception ex)
        {
            this.CloseConnection();
            throw (ex);
        }

        return DS;
    }

    public DataSet ExecuteSPWithResults(string SPName, LNParameterCollection Parameters, ref int returnValue, CommandBehavior CommandBehaviour = CommandBehavior.Default)
    {
        DataSet DS = new DataSet();
        SqlCommand Cmd = null;

        try
        {
            if (m_CmdCollection.ContainsKey(SPName))
            {
                Cmd = (SqlCommand)m_CmdCollection[SPName];
                Cmd.Connection = this.Connection;
                Cmd.CommandTimeout = m_CommandTimeOut;
            }
            else
            {
                Cmd = new SqlCommand(SPName);
                Cmd.Connection = this.Connection;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = m_CommandTimeOut;

                SqlCommandBuilder.DeriveParameters(Cmd);

                m_CmdCollection.Add(SPName, Cmd);
            }

            foreach (SqlParameter spParam in Cmd.Parameters)
                spParam.Value = null;

            foreach (LNParameter Param in Parameters)
            {
                try
                {
                    Cmd.Parameters[Param.Name].Value = Param.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            SqlDataAdapter DA = new SqlDataAdapter(Cmd);
            string sExceptionMsg = "";
            try
            {
                DA.Fill(DS);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }

            WriteSPToFile(SPName, Parameters);
            if (!string.IsNullOrEmpty(sExceptionMsg))
                WriteErrorToFile(sExceptionMsg);
            returnValue = (int)Cmd.Parameters["@RETURN_VALUE"].Value;

            WriteReturnValueToFile(returnValue);

            return DS;
        }
        catch (Exception ex)
        {
            this.CloseConnection();
            throw (ex);
        }
    }

    public DataSet ExecuteSPWithResults(string SPName, LNParameterCollection Parameters, ref int returnValue, bool bCreateNewConnection, CommandBehavior CommandBehaviour = CommandBehavior.Default)
    {
        DataSet DS = new DataSet();
        SqlCommand Cmd;

        SqlConnection objConnection = new SqlConnection();
        try
        {
            try
            {
                objConnection.ConnectionString = GetConnectionString();
                Impersonate();
                objConnection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Cmd = new SqlCommand(SPName);
            Cmd.Connection = objConnection;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandTimeout = m_CommandTimeOut;
            SqlCommandBuilder.DeriveParameters(Cmd);

            foreach (SqlParameter spParam in Cmd.Parameters)
                spParam.Value = null;

            foreach (LNParameter Param in Parameters)
            {
                try
                {
                    Cmd.Parameters[Param.Name].Value = Param.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            SqlDataAdapter DA = new SqlDataAdapter(Cmd);
            string sExceptionMsg = "";
            try
            {
                DA.Fill(DS);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }

            objConnection.Close();
            UnImpersonate();
            WriteSPToFile(SPName, Parameters);
            if (!string.IsNullOrEmpty(sExceptionMsg))
                WriteErrorToFile(sExceptionMsg);
            returnValue = (int)Cmd.Parameters["@RETURN_VALUE"].Value;

            WriteReturnValueToFile(returnValue);

            // Return the populated dataset
            return DS;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (objConnection != null)
            {
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();
                objConnection.Dispose();
            }
        }
    }
    // Author         : Vivek SARAFF
    // CreationDate   : 8/15/2003
    // Description    : This executes an SP that has NO Parameters and Returns
    // NO results (Just inserts into or updates the DB)
    // Modification   : Eugene 20060525   To support the use of custom RaiseError from stored procedure, the
    // method would log the message to error file but not to throw (display) it
    // Modification   : Daphne 20060824   TO let COMA error code for those >50000 and stage =2 to throw exception

    public int ExecuteSPWithNoResults(string SPName, ref int returnValue)
    {
        SqlCommand Cmd = new SqlCommand(SPName);
        int Count = 0;

        try
        {
            Cmd.Connection = this.Connection;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandTimeout = m_CommandTimeOut;

            SqlParameter RetParam = new SqlParameter();
            {
                var withBlock = RetParam;
                withBlock.ParameterName = "@RETURN_VALUE";
                withBlock.Direction = ParameterDirection.ReturnValue;
                withBlock.SqlDbType = SqlDbType.Int;
            }

            // WriteSPToFile(SPName)

            Cmd.Parameters.Add(RetParam);
            string sExceptionMsg = "";
            try
            {
                Count = Cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                sExceptionMsg = ex.Message;
            }

            this.CloseConnection();
            WriteSPToFile(SPName);
            if (!string.IsNullOrEmpty(sExceptionMsg))
                WriteErrorToFile(sExceptionMsg);
            returnValue = (int)Cmd.Parameters["@RETURN_VALUE"].Value;

            WriteReturnValueToFile(returnValue);
            return Count;
        }
        catch (Exception ex)
        {
            this.CloseConnection();
            throw (ex);
        }
    }

    public int ExecuteSPWithNoResults(string SPName, LNParameterCollection Parameters, ref int returnValue)
    {
        SqlCommand Cmd = new SqlCommand(SPName);
        int Count = 0;

        try
        {
            // if the command object exists in cache get it from there
            // else create it from the database signature
            if (m_CmdCollection.ContainsKey(SPName))
            {
                Cmd = (SqlCommand)m_CmdCollection[SPName];
                Cmd.Connection = this.Connection;
                Cmd.CommandTimeout = m_CommandTimeOut;
            }
            else
            {
                Cmd = new SqlCommand(SPName);
                Cmd.Connection = this.Connection;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = m_CommandTimeOut;

                // Refresh parameters from the database
                SqlCommandBuilder.DeriveParameters(Cmd);
                // Add it to the global cache of Stored Procedure Signatures
                m_CmdCollection.Add(SPName, Cmd);
            }

            // reset parameter values still being held from a previous run
            foreach (SqlParameter spParam in Cmd.Parameters)
                spParam.Value = null;
            // Set the parameters to what was passed in
            foreach (LNParameter Param in Parameters)
            {
                try
                {
                    Cmd.Parameters[Param.Name].Value = Param.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            string sExceptionMsg = "";
            try
            {
                Count = Cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                sExceptionMsg = ex.Message;
            }


            this.CloseConnection();
            WriteSPToFile(SPName, Parameters);
            if (!string.IsNullOrEmpty(sExceptionMsg))
                WriteErrorToFile(sExceptionMsg);
            returnValue = (int)Cmd.Parameters["@RETURN_VALUE"].Value;

            WriteReturnValueToFile(returnValue);

            return Count;
        }
        catch (Exception ex)
        {
            this.CloseConnection();
            throw (ex);
        }
    }

    // Author         : CHTee
    // CreationDate   : 20130815 FF2.8.19 Devboard 69313.01 -- Concurrent Connection Issue
    // Description    : Add an Overload function to have accept sql connection
    // This executes an SP that has NO Parameters and Returns
    // a return value and NO results
    public int ExecuteSPWithNoResults(string SPName, LNParameterCollection Parameters, ref int returnValue, bool bCreateNewConnection)
    {
        SqlCommand Cmd = new SqlCommand(SPName);
        int Count = 0;
        SqlConnection objConnection = new SqlConnection();

        try
        {
            try
            {
                objConnection.ConnectionString = GetConnectionString();
                Impersonate();
                objConnection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Cmd = new SqlCommand(SPName);
            Cmd.Connection = objConnection;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandTimeout = m_CommandTimeOut;

            SqlCommandBuilder.DeriveParameters(Cmd);

            foreach (SqlParameter spParam in Cmd.Parameters)
                spParam.Value = null;

            foreach (LNParameter Param in Parameters)
            {
                try
                {
                    Cmd.Parameters[Param.Name].Value = Param.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            string sExceptionMsg = "";
            try
            {
                Count = Cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                sExceptionMsg = ex.Message;
            }

            objConnection.Close();
            UnImpersonate();
            WriteSPToFile(SPName, Parameters);
            if (!string.IsNullOrEmpty(sExceptionMsg))
                WriteErrorToFile(sExceptionMsg);
            if (Cmd.Parameters["@RETURN_VALUE"].Value != null)
            {
                returnValue = (int)Cmd.Parameters["@RETURN_VALUE"].Value;

                WriteReturnValueToFile(returnValue);
            }

            return Count;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (objConnection != null)
            {
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();
                objConnection.Dispose();
            }
        }
    }

    public DataSet ExecuteSPWithOutPutParamsAndResults(string SPName, LNParameterCollection InputParameters, ref LNParameterCollection OutputParameters, ref int returnValue)
    {
        SqlCommand Cmd;

        try
        {
            if (m_CmdCollection.ContainsKey(SPName))
            {
                Cmd = (SqlCommand)m_CmdCollection[SPName];
                Cmd.Connection = this.Connection;
                Cmd.CommandTimeout = m_CommandTimeOut;
            }
            else
            {
                Cmd = new SqlCommand(SPName);
                Cmd.Connection = this.Connection;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = m_CommandTimeOut;

                SqlCommandBuilder.DeriveParameters(Cmd);
                m_CmdCollection.Add(SPName, Cmd);
            }

            foreach (SqlParameter spParam in Cmd.Parameters)
            {
                if (spParam.Direction == ParameterDirection.Input | spParam.Direction == ParameterDirection.InputOutput)
                    spParam.Value = null;
            }
            foreach (LNParameter Param in InputParameters)
            {
                try
                {
                    Cmd.Parameters[Param.Name].Value = Param.Value;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

            DataSet DS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(Cmd);
            string sExceptionMsg = "";
            try
            {
                DA.Fill(DS);
            }
            catch (SqlException ex)
            {
                sExceptionMsg = ex.Message;
            }

            this.CloseConnection();
            WriteSPToFile(SPName, InputParameters);
            if (!string.IsNullOrEmpty(sExceptionMsg))
                WriteErrorToFile(sExceptionMsg);
            returnValue = (int)Cmd.Parameters["@RETURN_VALUE"].Value;

            WriteReturnValueToFile(returnValue);

            LNParameter outPutParam;
            foreach (SqlParameter spParam in Cmd.Parameters)
            {
                if (spParam.Direction == ParameterDirection.InputOutput | spParam.Direction == ParameterDirection.Output)
                {
                    outPutParam = new LNParameter();
                    outPutParam.Name = spParam.ParameterName;
                    outPutParam.Value = spParam.Value;
                    OutputParameters.Add(spParam.ParameterName, outPutParam);
                }
            }

            return DS;
        }
        catch (Exception ex)
        {
            this.CloseConnection();
            throw (ex);
        }
    }

    public void ExecuteSPWithOutPutParameters(string SPName, LNParameterCollection InputParameters, ref LNParameterCollection OutputParameters, ref int returnValue)
    {
        SqlCommand Cmd;

        try
        {
            // if the command object exists in cache get it from there
            // else create it from the database signature
            if (m_CmdCollection.ContainsKey(SPName))
            {
                Cmd = (SqlCommand)m_CmdCollection[SPName];
                Cmd.Connection = this.Connection;
                Cmd.CommandTimeout = m_CommandTimeOut;
            }
            else
            {
                Cmd = new SqlCommand(SPName);
                Cmd.Connection = this.Connection;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = m_CommandTimeOut;

                SqlCommandBuilder.DeriveParameters(Cmd);
                m_CmdCollection.Add(SPName, Cmd);
            }

            foreach (SqlParameter spParam in Cmd.Parameters)
            {
                if (spParam.Direction == ParameterDirection.Input | spParam.Direction == ParameterDirection.InputOutput)
                    spParam.Value = null;
            }

            foreach (LNParameter Param in InputParameters)
            {
                try
                {
                    Cmd.Parameters[Param.Name].Value = Param.Value;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            string sExceptionMsg = "";
            try
            {
                Cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                sExceptionMsg = ex.Message;
            }

            this.CloseConnection();
            WriteSPToFile(SPName, InputParameters);
            if (!string.IsNullOrEmpty(sExceptionMsg))
                WriteErrorToFile(sExceptionMsg);
            returnValue = (int)Cmd.Parameters["@RETURN_VALUE"].Value;

            WriteReturnValueToFile(returnValue);

            LNParameter outPutParam;
            foreach (SqlParameter spParam in Cmd.Parameters)
            {
                if (spParam.Direction == ParameterDirection.InputOutput | spParam.Direction == ParameterDirection.Output)
                {
                    outPutParam = new LNParameter();
                    outPutParam.Name = spParam.ParameterName;
                    outPutParam.Value = spParam.Value;
                    OutputParameters.Add(spParam.ParameterName, outPutParam);
                }
            }
        }
        catch (Exception ex)
        {
            this.CloseConnection();
            throw (ex);
        }
    }

    public DataTable GetTable(string tableName, string whereClause = "", string fieldNames = "*")
    {
        if ((Strings.Len(whereClause) >= 1) & Strings.Len(whereClause) <= 5)
            whereClause = " where " + whereClause;
        else if ((Strings.Len(whereClause) > 5))
        {
            if (whereClause.ToUpper().Trim().Substring(0, 5) == "WHERE")
                whereClause = " " + whereClause.Trim();
            else
                whereClause = " where " + whereClause;
        }
        else if ((Strings.Len(whereClause) == 0))
        {
        }
        else
            throw new Exception("Error in sql query filter");
        return ExecuteDynamicSQL(tableName, whereClause, fieldNames);
    }

    public void Impersonate()
    {
        if (m_IntegratedSecurity)
        {
            if (!string.IsNullOrEmpty(m_UserName))
            {
                if (!m_UserName.Equals($@"{Environment.UserDomainName}\{Environment.UserName}", StringComparison.OrdinalIgnoreCase))
                {
                    char[] splitter = new char[1] { '\\' };
                    m_impersonatorContext = new Impersonator(m_UserName.Split(splitter)[1], m_UserName.Split(splitter)[0], m_Pwd);
                    if (m_impersonatorContextLocal == null)
                        m_impersonatorContextLocal = m_impersonatorContext;
                }
            }
        }
    }

    public void UnImpersonate()
    {
        if (m_impersonatorContext != null)
            m_impersonatorContext.IDisposable_Dispose();
        else if (m_impersonatorContextLocal != null)
        {
            m_impersonatorContext = m_impersonatorContextLocal;
            m_impersonatorContext.IDisposable_Dispose();
        }
        m_impersonatorContext = null;
    }

    private void WriteSPToFile(string spName, LNParameterCollection @params = null)
    {
        FileStream fs;
        string TextToWrite;
        string fileName;
        try
        {
            if (!LNGlobal.g_SPLoggingEnabled) return;

            fileName = System.Environment.CurrentDirectory + @"\" + DB_CALL_LOG;

            ArchiveLogFile();

            fs = File.Open(fileName, FileMode.Append);
            byte[] StringToWrite;

            StringToWrite = new UTF8Encoding(true).GetBytes(DateTime.Now.ToString() + " : " + Constants.vbCrLf + Constants.vbTab + Strings.UCase(spName) + Constants.vbCrLf);
            fs.Write(StringToWrite, 0, StringToWrite.Length);
            m_DetailErrorLog = "";

            m_DetailErrorLog = Constants.vbCrLf + Constants.vbTab + Strings.UCase(spName) + Constants.vbCrLf + "|";

            if (@params != null)
            {
                foreach (LNParameter param in @params)
                {
                    if (param.Value != null)
                    {
                        if (!(param.Value is byte[]))
                        {
                            if (param.Value is System.String)
                                TextToWrite = param.Name + " = '" + System.Convert.ToString(StripNull(param.Value, "NULL")) + "', -- (" + param.Value.GetType().ToString() + ")";
                            else if (param.Value is System.Guid)
                                TextToWrite = param.Name + " = '" + param.Value.ToString() + "', -- (" + param.Value.GetType().ToString() + ")";
                            else
                                TextToWrite = param.Name + " = " + System.Convert.ToString(StripNull(param.Value, "NULL")) + ", -- (" + param.Value.GetType().ToString() + ")";
                        }
                        else
                            TextToWrite = param.Name + " = " + param.Value.GetType().ToString() + ", -- (ByteArray)";
                    }
                    else
                        TextToWrite = param.Name + " = null,";

                    m_DetailErrorLog = m_DetailErrorLog + Constants.vbTab + TextToWrite + Constants.vbCrLf;

                    StringToWrite = new UTF8Encoding(true).GetBytes(Constants.vbTab + TextToWrite + Constants.vbCrLf);
                    fs.Write(StringToWrite, 0, StringToWrite.Length);
                }
            }

            m_DetailErrorLog = m_DetailErrorLog + "|";

            fs.Close();
        }
        catch (Exception ex)
        {
        }
    }

    private void WriteReturnValueToFile(int retval)
    {
        FileStream fs;
        string TextToWrite;
        string fileName;
        try
        {
            if (!LNGlobal.g_SPLoggingEnabled) return;

            fileName = System.Environment.CurrentDirectory + @"\" + DB_CALL_LOG;
            TextToWrite = Constants.vbTab + "RETURN VALUE = " + retval.ToString() + Constants.vbCrLf + Constants.vbCrLf;

            if ((retval > 0))
            {
                m_DetailErrorLog = m_DetailErrorLog + Constants.vbTab + TextToWrite + Constants.vbCrLf + "|";
            }

            if (!File.Exists(fileName))
            {
                fs = File.Create(fileName);
                fs.Close();
            }

            fs = File.Open(fileName, FileMode.Append);
            byte[] StringToWrite = new UTF8Encoding(true).GetBytes(TextToWrite);
            fs.Write(StringToWrite, 0, StringToWrite.Length);
            fs.Close();
        }
        catch (Exception ex)
        {
        }
    }

    private void WriteErrorToFile(string message)
    {
        FileStream fs;
        string TextToWrite;
        string fileName;
        try
        {
            if (!LNGlobal.g_SPLoggingEnabled) return;

            fileName = System.Environment.CurrentDirectory + @"\" + DB_CALL_LOG;
            TextToWrite = Constants.vbTab + "ERROR MESSAGE = " + message + Constants.vbCrLf;

            if (!File.Exists(fileName))
            {
                fs = File.Create(fileName);
                fs.Close();
            }

            fs = File.Open(fileName, FileMode.Append);
            byte[] StringToWrite = new UTF8Encoding(true).GetBytes(TextToWrite);
            fs.Write(StringToWrite, 0, StringToWrite.Length);
            fs.Close();
        }
        catch (Exception ex)
        {
        }
    }

    private void ArchiveLogFile()
    {
        try
        {
            // Create a FileInfo object for the log file
            FileInfo f = new FileInfo(System.Environment.CurrentDirectory + @"\" + DB_CALL_LOG);
            if (f.Exists)
            {
                if (f.Length > FIVEMB)
                {
                    // if archive file exists then delete it first
                    if (File.Exists(System.Environment.CurrentDirectory + @"\" + DB_CALL_ARCHIVE_LOG))
                        File.Delete(System.Environment.CurrentDirectory + @"\" + DB_CALL_ARCHIVE_LOG);
                    // Move the logfile to Archive
                    f.MoveTo(System.Environment.CurrentDirectory + @"\" + DB_CALL_ARCHIVE_LOG);
                    // Create the logfile and close it
                    File.Create(System.Environment.CurrentDirectory + @"\" + DB_CALL_LOG).Close();
                }
            }
            else
                // Create the logfile and close it
                f.Create().Close();
        }
        catch (Exception ex)
        {
        }
    }

    private object StripNull(object content, object ReplaceNullWith)
    {
        if (content == System.DBNull.Value)
            return ReplaceNullWith;
        else
            return content;
    }

    private DataTable ExecuteDynamicSQL(string tableName, string whereClause = "", string fieldNames = "*")
    {
        int RetVal = 0;
        DataSet DS;
        DataTable DT = null;
        LNParameterCollection @params = new LNParameterCollection();

        try
        {
            @params.Add("@tableName", new LNParameter("@tableName", tableName));
            @params.Add("@restriction", new LNParameter("@restriction", whereClause));
            @params.Add("@fieldName", new LNParameter("@fieldName", fieldNames));

            DS = ExecuteSPWithResults(DYN_SQL_EXECUTION, @params, ref RetVal, true);

            if (RetVal == 0)
            {
                if (DS != null)
                {
                    if (DS.Tables.Count > 0)
                    {
                        foreach (DataTable dte in DS.Tables)
                        {
                            if (DT == null)
                                DT = dte;
                            else
                                foreach (DataRow row in dte.Rows)
                                    DT.ImportRow(row);
                        }
                    }
                    else
                        return null;
                }
            }

            DS.Tables.Remove(DT);
            DT.TableName = tableName;
            return DT;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.CloseConnection();
        }
    }

    private string GetConnectionString()
    {
        return base.GetConnectionString(m_Server, m_DataBase, m_UserName, m_Pwd, m_IntegratedSecurity);
    }
}

public class LNParameter
{
    private string m_Name;
    private object m_Value;

    public LNParameter()
    {
    }

    public LNParameter(string name, object value)
    {
        m_Name = name;
        m_Value = value;
    }
    public string Name
    {
        get
        {
            return m_Name;
        }
        set
        {
            m_Name = value;
        }
    }

    public object Value
    {
        get
        {
            return m_Value;
        }
        set
        {
            m_Value = Value;
        }
    }
}

public class LNParameterCollection : LNCollectionBase
{
    public LNParameter Add(string key, LNParameter value)
    {
        return (LNParameter)base.Add(key, value);
    }


    public LNParameter Add(LNParameter value)
    {
        return (LNParameter)base.Add(value);
    }
}

public abstract class LNCollectionBase : LNCollection
{
    private Collection m_oCol = new Collection();

    public object Add(string Key, object Value)
    {
        m_oCol.Add(Value, Key);
        return m_oCol[m_oCol.Count];
    }

    public object Add(object Value)
    {
        m_oCol.Add(Value);
        return m_oCol[m_oCol.Count];
    }

    public void Clear()
    {
        m_oCol.Clear();

        int count = m_oCol.Count;
        for (int i = 1; i <= count; i++)
        {
            m_oCol.Remove(i);
        }
    }

    public int Count()
    {
        return m_oCol.Count;
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
        return m_oCol.GetEnumerator();
    }

    public object Item(int index)
    {
        return m_oCol[index];
    }

    public object Item(string key)
    {
        return m_oCol[key];
    }

    public void Remove(string Key)
    {
        m_oCol.Remove(Key);
    }

    public void Remove(int index)
    {
        m_oCol.Remove(index);
    }
}

