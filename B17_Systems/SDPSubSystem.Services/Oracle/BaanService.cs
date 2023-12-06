using SDPSubSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Services.Oracle
{
    public class BaanService : OracleService
    {
        public DataTable ExecuteSqlbyPara()
        {
            DataTable dt = OracleSql.ExecuteSql(@"select 
                                A.cwar Whs,
                                A.loca Location_t,
                                A.item,
                                A.idat_loc inventorydate,
                                A.stks onhand,
                                B.mnum MagicNumber,
                                trim(B.cref) Ref2SKID
                                from
                                BO_read891.whinr140 A
                                inner join BO_read891.fxinh051 B on (A.item=B.item and A.idat=B.idat)
                                where
                                A.stks>0 and
                                A.idat_loc>to_date('1970-01-04','yyyy-MM-dd') and
                                (A.item<='         PMM           ' or
                                A.item>='         PMMZZZZZZZZZZZ')", ConDB.AM3Context
                        );
            return dt;
        }

        public DataTable ExecuteSqlbyParaInAM3(string sStr)
        {
            DataTable dt = OracleSql.ExecuteSql(@sStr, ConDB.AM3Context
                        );
            return dt;
        }
    }
}
