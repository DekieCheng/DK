using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Data
{
    public class OracleSql
    {
        private static string _connectstring;

        public static string Connectstring
        {
            get
            {
                return _connectstring;
            }

            set
            {
                lock (_connectstring)
                {
                    _connectstring = value;
                }
            }
        }
        static OracleSql()
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings["AM3Context"] != null)
            {
                _connectstring = ConDB.AM3Context;
            }
        }
        public static DataTable ExecuteSql(string Sqlstring, string Connectstr, params OracleParameter[] parameters)
        {
            using (OracleConnection con = new OracleConnection(string.IsNullOrEmpty(Connectstr) ? _connectstring : Connectstr))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand(Sqlstring, con))
                {
                    com.Parameters.AddRange(parameters);
                    OracleDataAdapter oda = new OracleDataAdapter(com);
                    DataTable dt = new DataTable();
                    oda.Fill(dt);
                    return dt;
                }
            }
        }
        public static DataTable ExecuteSql(string Sqlstring, string Connectstr)
        {
            using (OracleConnection con = new OracleConnection(string.IsNullOrEmpty(Connectstr) ? _connectstring : Connectstr))
            {
                con.Open();
                using (OracleCommand com = new OracleCommand(Sqlstring, con))
                {

                    OracleDataAdapter oda = new OracleDataAdapter(com);
                    DataTable dt = new DataTable();
                    oda.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
