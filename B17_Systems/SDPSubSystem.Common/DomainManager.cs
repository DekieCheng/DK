using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Common
{
    public static class DomainManager
    {
        
        public static bool fnCheckDomainUser(string txtUser, string txtPassword, out string msg)
        {
            msg = string.Empty;
            DirectoryEntry de = new DirectoryEntry("", txtUser, txtPassword);
            try
            {
                DirectorySearcher ds = new DirectorySearcher(de);
                ds.Filter = "(SAMAccountName=" + txtUser + ")";
                ds.PropertiesToLoad.Add("cn");
                SearchResult sr = ds.FindOne();
                if (sr != null)
                {
                    de = sr.GetDirectoryEntry();
                    string strFirstName = de.Properties["GivenName"].Value.ToString();  // 名
                    string strLastName = de.Properties["sn"].Value.ToString();  // 姓
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
    }
}
