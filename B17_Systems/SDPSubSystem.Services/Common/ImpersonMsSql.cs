using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SDPSubSystem.Common.Security;

namespace SDPSubSystem.Services.Common
{
    public class ImpersonMsSql
    {
        private static string _connectionString;
        private static string _userName;
        private static string _passWord;
        private static string _domain;
        /// <summary>
        /// Ms数据连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                lock (_connectionString)
                {
                    _connectionString = value;
                }
            }
        }
        /// <summary>
        /// 从配置文件中读取连接字符串
        /// </summary>
        static ImpersonMsSql()
        {
            try 
            {

                _connectionString = DESSecurity.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["PPApp"].ToString());
                
                _userName = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString();
                _passWord = DESSecurity.Decrypt(System.Configuration.ConfigurationManager.AppSettings["PassWord"].ToString());
                _domain = System.Configuration.ConfigurationManager.AppSettings["DomainName"].ToString();
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString,string connStr =  null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        using (new Impersonator(_userName,_domain,_passWord))
                        {
                            connection.Open();
                        }
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(ArrayList SQLStringList,string connStr=null)
        {
            using (SqlConnection conn = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content,string connStr=null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    using (new Impersonator(_userName, _domain, _passWord))
                    {
                        connection.Open();
                    }
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString,string connStr=null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        using (new Impersonator(_userName, _domain, _passWord))
                        {
                            connection.Open();
                        }
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL, string connStr = null)
        {
            SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    connection.Open();
                }
                SqlDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString,string connStr=null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                DataSet ds = new DataSet();
                try
                {
                    using (new Impersonator(_userName, _domain, _passWord))
                    {
                        connection.Open();
                    }
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "table");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }


        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString,string connStr=null, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList,string connStr=null)
        {
            using (SqlConnection conn = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString,string connStr=null, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString,string connStr=null, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString,string connStr=null, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "table");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            using (new Impersonator(_userName, _domain, _passWord))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
            }
            
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters,string connStr=null)
        {
            SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }

        public static void RunProcedure(string storedProcName, SqlParameter[] param, string connStr = null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    connection.Open();
                }
                SqlCommand sqlCmd = BuildQueryCommand(connection, storedProcName, param);
                sqlCmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName,string connStr=null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                DataSet dataSet = new DataSet();
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    connection.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, string sqlXml,string tablename, out int code, string connStr = null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                DataSet dataSet = new DataSet();
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    connection.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                IDataParameter[] parameters =
                {
                    new SqlParameter("@sqlxml",sqlXml),
                    new SqlParameter("@msg",SqlDbType.NVarChar,600)
                };
                parameters[1].Direction = ParameterDirection.Output;
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                code =sqlDA.Fill(dataSet, tablename);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, string sqlXml, string tablename, out int code,out string Msg, string connStr = null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                DataSet dataSet = new DataSet();
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    connection.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                IDataParameter[] parameters =
                {
                    new SqlParameter("@sqlxml",sqlXml),
                    new SqlParameter("@msg",SqlDbType.NVarChar,600),
                    new SqlParameter("@ReturnCode ",SqlDbType.Int),
                };
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.ReturnValue;
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                code = sqlDA.Fill(dataSet, tablename);
                code = (int)parameters[2].Value;
                Msg = parameters[1].Value.ToString();
                connection.Close();
                return dataSet;
            }
        }

        public static string RunProcedure(string ProcName, string sqlXml, out int code,string connStr=null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = ProcName;
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@sqlxml", SqlDbType.Xml).Value = sqlXml;
                cmd.Parameters.Add("@msg", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    connection.Open();
                }
                cmd.ExecuteNonQuery();
                int.TryParse(cmd.Parameters[2].Value.ToString(), out code);
                return cmd.Parameters[1].Value.ToString();
            }
        }


        public static object RunProcedure(string ProcName, string sqlXml, out string msg, out int code,string connStr=null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = ProcName;
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@sqlxml", SqlDbType.Xml).Value = sqlXml;
                cmd.Parameters.Add("@msg", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    connection.Open();
                }
                object obj = cmd.ExecuteScalar();
                int.TryParse(cmd.Parameters[2].Value.ToString(), out code);
                msg = cmd.Parameters[1].Value.ToString();
                return obj;
            }
        }


        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 120;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected,string connStr=null)
        {
            using (SqlConnection connection = new SqlConnection(string.IsNullOrEmpty(connStr) ? _connectionString : connStr))
            {
                int result;
                using (new Impersonator(_userName, _domain, _passWord))
                {
                    connection.Open();
                }
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion
    }
}
