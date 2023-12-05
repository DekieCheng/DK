using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Common.Opt
{
    public class XmlParams
    {
        public static string CreateSqlXml<T>(Dictionary<string, object> parameters, List<T> list) where T : new()
        {
            if (parameters == null ) { return null; }
            StringBuilder sb = new StringBuilder();
            sb.Append("<sdp><data ");
            foreach (var parameter in parameters)
            {
                sb.AppendFormat("{0}=\"{1}\" ", parameter.Key, System.Security.SecurityElement.Escape(parameter.Value.ToString().Trim()));
            }
            sb.Append("></data>");
            if (list != null)
            {
                foreach (var m in list)
                {
                    sb.Append("<data1 ");
                    foreach (var item in m.GetType().GetProperties())
                    {
                        sb.AppendFormat("{0}=\"{1}\" ", item.Name, item.GetValue(m, null)==null?null: System.Security.SecurityElement.Escape(item.GetValue(m, null).ToString().Trim()));
                    }
                    sb.Append("></data1>");
                }
            }
           
            sb.Append("</sdp>");
            return sb.ToString();
        }

        public static string CreateSqlXml(Dictionary<string,object> parameters)
        {
            if (parameters == null) { return null; }
            StringBuilder sb = new StringBuilder();
            sb.Append("<sdp><data ");
            foreach (var parameter in parameters)
            {
                sb.AppendFormat("{0}=\"{1}\" ", parameter.Key, System.Security.SecurityElement.Escape(parameter.Value == null ? "" : parameter.Value.ToString().Trim()));
            }
            sb.Append("></data></sdp>");
            return sb.ToString();
        }

        public static string CreateSqlXml(DataTable dt)
        {
            if (dt == null) { return null; }
            StringBuilder sb = new StringBuilder();
            sb.Append("<sdp>");
            foreach(DataRow dr in dt.Rows)
            {
                sb.Append("<data ");
                foreach(DataColumn dc in dt.Columns)
                {
                    sb.AppendFormat("{0}=\"{1}\" ", dc.ColumnName, System.Security.SecurityElement.Escape(dr[dc.ColumnName].ToString()));
                }
                sb.Append("/>");
            }
            sb.Append("</sdp>");
            return sb.ToString();
        }

        public static string CreateSqlXml(Dictionary<string,object> parameters, DataTable dt)
        {
            if (dt == null) { return null; }
            StringBuilder sb = new StringBuilder();
            sb.Append("<sdp ");
            foreach(var parameter in parameters)
            {
                sb.AppendFormat("{0}=\"{1}\" ", parameter.Key, System.Security.SecurityElement.Escape(parameter.Value == null ? "" : parameter.Value.ToString().Trim()));
            }
            sb.Append(">");
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<data ");
                foreach (DataColumn dc in dt.Columns)
                {
                    sb.AppendFormat("{0}=\"{1}\" ", dc.ColumnName, System.Security.SecurityElement.Escape(dr[dc.ColumnName].ToString()));
                }
                sb.Append("/>");
            }
            sb.Append("</sdp>");
            return sb.ToString();
        }

        public static string CreateSqlXml(List<Dictionary<string, object>> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<sdp>");
            if (list.GetType() == typeof(List<Dictionary<string, object>>))
            {
                var data = list as List<Dictionary<string, object>>;
                foreach (Dictionary<string, object> d in data)
                {
                    sb.Append("<data ");
                    foreach (var x in d)
                    {
                        sb.AppendFormat(" {0}=\"{1}\" ", x.Key, System.Security.SecurityElement.Escape(x.Value.ToString().Trim()));
                    }
                    sb.Append(" ></data>");
                }
            }
            sb.Append("</sdp>");
            return sb.ToString();
        }
    }
}
