using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.SqlServer;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using LNMeterInfrastructure.Common;

namespace LNMeterInfrastructure.DAL
{
    public class FFDALBase : IDisposable
    {
        private SqlConnection objConn = null;
        private SqlCommand objCmd = null;
        private SqlDataAdapter objAdp = null;
        private SqlTransaction objTrans = null;

        private string strErrMsg = "";
        private int intTransTimes = 0;
        private bool disposedValue;

        public string ErrMsg
        {
            get
            {
                return strErrMsg;
            }
            set
            {
                strErrMsg = value;
            }
        }

        protected SqlParameterCollection SQLParams
        {
            get
            {
                return objCmd.Parameters;
            }
        }



        public FFDALBase()
        {
            InitDatabase();
        }

        public FFDALBase(FFDALBase objDalBase)
        {
            InitDatabase(objDalBase);
        }

        private void InitDatabase()
        {
            string strConnString = ConfigurationManager.AppSettings.Get("ConnectionString");
            strConnString = LNGlobal.CommonParsePassword(strConnString, true);
            objConn = new SqlConnection(strConnString);
            objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objAdp = new SqlDataAdapter();
            objAdp.SelectCommand = objCmd;
        }

        private void InitDatabase(FFDALBase objDalBase)
        {
            objConn = objDalBase.objConn;
            objCmd = objDalBase.objCmd;
            objAdp = objDalBase.objAdp;
            objTrans = objDalBase.objTrans;
            intTransTimes = objDalBase.intTransTimes;
        }

        public void BeginTrans()
        {
            if (intTransTimes == 0)
            {
                objConn.Open();
                objTrans = objConn.BeginTransaction();
            }
            intTransTimes += 1;
        }

        public void CommnitTrans()
        {
            if (objTrans != null && intTransTimes == 1)
            {
                intTransTimes -= 1;
                objTrans.Commit();
                objConn.Close();
            }
            else
                intTransTimes -= 1;
        }

        public void RollbackTrans()
        {
            intTransTimes -= 1;
            if (objTrans != null && intTransTimes == 1)
            {
                objTrans.Rollback();
                objConn.Close();
            }
            else
                intTransTimes -= 1;
        }



        public DataTable DoQuery(string strSQL)
        {
            try
            {
                //objCmd = new SqlCommand(strSQL);

                if (intTransTimes == 0)
                    objConn.Open();
                //objCmd = objConn.CreateCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = strSQL;
                DataTable dtTemp = new DataTable();
                objAdp.Fill(dtTemp);
                this.objCmd.Parameters.Clear();
                if (intTransTimes == 0)
                    objConn.Close();
                return dtTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                }
            }
        }

        public int DoNonQuery(string strSQL)
        {
            if (intTransTimes == 0)
                objConn.Open();
            int intReturnValue = 0;
            objCmd.CommandText = strSQL;
            intReturnValue = objCmd.ExecuteNonQuery();
            this.objCmd.Parameters.Clear();
            if (intTransTimes == 0)
                this.objConn.Close();
            return intReturnValue;
        }

        public DataTable ExecuteSPByTable(string strSPName, SqlParameterCollection myParameters)
        {
            if (intTransTimes == 0)
                objConn.Open();
            objCmd.CommandType = System.Data.CommandType.StoredProcedure;
            objCmd.CommandText = strSPName;
            DataTable dtTemp = new DataTable();
            objAdp.Fill(dtTemp);
            this.objCmd.Parameters.Clear();
            if (intTransTimes == 0)
                objConn.Close();
            return dtTemp;
        }

        public DataSet ExecuteSPByDataSet(string strSPName, SqlParameterCollection myParameters)
        {
            if (intTransTimes == 0)
                objConn.Open();
            objCmd.CommandType = System.Data.CommandType.StoredProcedure;
            objCmd.CommandText = strSPName;
            DataSet dtTemp = new DataSet();
            objAdp.Fill(dtTemp);
            this.objCmd.Parameters.Clear();
            if (intTransTimes == 0)
                objConn.Close();
            return dtTemp;
        }

        public bool ExecuteSPWithoutResults(string strSPName)
        {
            try
            {
                if (intTransTimes == 0)
                    objConn.Open();

                objCmd.CommandType = System.Data.CommandType.StoredProcedure;
                objCmd.CommandText = strSPName;
                objCmd.ExecuteNonQuery();

                this.objCmd.Parameters.Clear();

                return true;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // If intTransTimes = 0 Then
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FFDALBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
