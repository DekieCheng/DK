using SDPSubSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDPSubSystem.Model.ChangeOver;

namespace SDPSubSystem.Services
{
    public class CommonService:BaseService
    {
        public DataTable ExecuteSqlbyPara(string constr,string sql )
        {
            DataTable dt = CommonSql.Query(sql, constr).Tables[0];
            return dt;
        }

        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public string RunProcedure(string procName, string sqlXml, string constr, out int code)
        {
            return CommonSql.RunProcedure(procName, sqlXml, out code,constr);
        }
        public udtChangeOver GetSingle(int ID ,string connstr)
        {
            DataTable dt=CommonSql.Query("select * from udtChangeOver with (nolock) where ID=" + ID, connstr).Tables[0];
            udtChangeOver uco = new udtChangeOver()
            {
                ID = Convert.ToInt32(dt.Rows[0][0].ToString()),
                Project = dt.Rows[0][1].ToString(),
                LineID = Convert.ToInt32(dt.Rows[0][2].ToString()),
                AmrNumberID = Convert.ToInt32(dt.Rows[0][3].ToString()),
                ActionID = Convert.ToInt32(dt.Rows[0][4].ToString()),
                EmployeeID = Convert.ToInt32(dt.Rows[0][5].ToString()),
                PlanTime =Convert.ToDateTime( dt.Rows[0][6].ToString()).ToUniversalTime(),
                ProductTime = Convert.ToDateTime(dt.Rows[0][7].ToString()).ToUniversalTime(),
                CreationTime  = Convert.ToDateTime(dt.Rows[0][8].ToString()).ToUniversalTime(),
                StatusID = Convert.ToInt32(dt.Rows[0][9].ToString())
            };
            return uco;
            //return obj;
        }

      
    }
}
