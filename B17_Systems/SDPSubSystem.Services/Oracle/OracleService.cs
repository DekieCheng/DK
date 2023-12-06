using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Services.Oracle
{
    public class OracleService
    {
        protected static string GetOracleSqlwhere(Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" (");
            foreach (var item in ht.Keys)
            {
                //sb.Append(item + "','");
                sb.AppendFormat("{0}" + item + "{1}", "'", "',");
            }
            string sqlwhere = sb.ToString();
            sqlwhere = sqlwhere.Substring(0, sqlwhere.Length - 1);
            sqlwhere = sqlwhere + " ) ";
            return sqlwhere;
        }

        protected static OracleParameter[] GetOracleParams(Hashtable ht)
        {
            OracleParameter[] op = new OracleParameter[ht.Count];
            for (int i = 0; i < op.Count(); i++)
            {
                op[i] = new OracleParameter(ht[i].ToString(), "ll");
            }
            return op;
        }
    }
}
