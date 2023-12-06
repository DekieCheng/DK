using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Common.Customized
{
    public class DtToXml
    {
        /// <summary>
        /// 把DataTable转换Xml
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static string DtToXmlString(DataTable dt)
        {
            DataSet ds = new DataSet();
           
            ds.DataSetName = "d";
            dt.TableName = "t";
            ds.Tables.Add(dt);
            String xml = ds.GetXml();

            return xml;
        }

    }
}
