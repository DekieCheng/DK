using SDPSubSystem.Common.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Common.Opt
{
    public class UrlParams
    {
        /// <summary>
        /// 根据URL密文获取解密URL的键值对
        /// </summary>
        /// <param name="EncUrl"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetDecryptUrl(string EncUrl)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            string DecUrl = DESSecurity.Decrypt(EncUrl);
            string[] groups = DecUrl.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string group in groups)
            {
                string[] keyValue = group.Split(new char[] { '=' }, StringSplitOptions.None);
                if (keyValue.Count() == 2 && !dicResult.Keys.Contains(keyValue[0].Trim()))
                {
                    dicResult.Add(keyValue[0], keyValue[1]);
                }
            }
            return dicResult;
        }


       

    }
}
