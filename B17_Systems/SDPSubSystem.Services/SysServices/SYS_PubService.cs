using SDPSubSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Services.SysServices
{
    public class SYS_PubService: BaseService
    {
        public static string GetNextNumber(string Code,string SiteCode)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SiteCode",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@Code",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@NextNumber",System.Data.SqlDbType.VarChar,100)
            };
            parameters[0].Value = SiteCode;
            parameters[1].Value = Code;
            parameters[2].Direction = System.Data.ParameterDirection.Output;
            MsSql.RunProcedure("usp_SYS_NextNumber_Generation", parameters);
            return Convert.IsDBNull(parameters[2].Value) ? "" : parameters[2].Value.ToString();
        }

        public static List<string> GetNextNumber_Batch(string Code,string SiteCode,int Counts)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SiteCode",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@Code",System.Data.SqlDbType.NVarChar,50),
                new SqlParameter("@Counts",System.Data.SqlDbType.Int,4)
            };
            parameters[0].Value = SiteCode;
            parameters[1].Value = Code;
            parameters[2].Value = Counts;
            DataTable dt = MsSql.RunProcedure("usp_SYS_NextNumber_Generation_Batch", parameters, "t1", null).Tables[0];
            List<string> result = (from d in dt.AsEnumerable() select d.Field<string>("NextNumber")).OrderBy(d => d).ToList();
            return result;
        }

        public static bool IsExistsNoAuthorize(Model.AssistModels.MvcEntity entity)
        {
            return Data.CacheData.DataCache.IsExistsNoAuthorize(entity);
        }
    }
}
