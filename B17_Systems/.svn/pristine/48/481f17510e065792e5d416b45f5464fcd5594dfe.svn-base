using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Common.Security
{
    public class GetConnUtil
    {
        /// <summary>
        /// Get Conn By webapi
        /// </summary>
        /// <returns></returns>
        public static string  GetConn(string baseurl)
        {
            string conn = null;
            string json = HttpGet(baseurl+"/GetConnWebapi/GetConn");
            var jsonStatusTest = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonStatus>(json);
            if (jsonStatusTest != null)
             {
                conn = jsonStatusTest.conn.Trim();
             }
            return conn;
        }

        /*
           Web API Call Function
        */
        private static string HttpGet(string url)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,*/*";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
