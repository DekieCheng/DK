using SDPSubSystem.Common.Security;
using SDPSubSystem.Data;
using SDPSubSystem.Model.AssistModels;
using SDPSubSystem.Model.SysModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace SDPSubSystem.Services
{
    public class BaseService
    {
        public new HttpSessionState Session { get { return System.Web.HttpContext.Current.Session; } }

        public Users CurrentUser
        {
            get
            {
                if (Session["CurrentUser"] == null && HttpContext.Current.Request.IsAuthenticated)
                {
                    Session["CurrentUser"] = GetLoginUser(HttpContext.Current.User.Identity.Name);
                }
                return Session["CurrentUser"] == null ? null : Session["CurrentUser"] as Users;
            }
            set
            {
                if (value == null) { Session["CurrentUser"] = null; }
                else
                {
                    Session["CurrentUser"] = value;
                }
            }
        }

        /// <summary>
        /// 获取登录用户的基本资料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Users GetLoginUser(string userInfo)
        {
            UnitOfWork uw = new UnitOfWork(new EFSYSContext());
            string[] info = userInfo.Split(new string[] { "|*|" }, StringSplitOptions.None);
            Repository<SYS_Users> resp = uw.Repository<SYS_Users>();
            string empno = info[2];
            var query = from e in resp.Entities
                        select new Users
                        {
                            ID = e.ID,
                            SiteCode = e.SiteCode,
                            EmployeeNO = e.EmpNO,
                            Account = e.Account,
                            CName = e.CName,
                            EName = e.EName,
                            LangID = e.LangID,
                            Building = e.BuildingCode
                        };
            Users u = query.FirstOrDefault(d => d.EmployeeNO == empno);
            if (u != null)
            {
                u.SiteCode = info[0];
                u.Building = info[1];
                u.SysCode = info[3];
            }
            return u;
        }

        public Users GetUserInfoByEName(string EName)
        {
            UnitOfWork uw = new UnitOfWork(new EFSYSContext());

            Repository<SYS_Users> resp = uw.Repository<SYS_Users>();

            var query = from e in resp.Entities
                        select new Users
                        {
                            ID = e.ID,
                            SiteCode = e.SiteCode,
                            EmployeeNO = e.EmpNO,
                            Account = e.Account,
                            CName = e.CName,
                            EName = e.EName,
                            LangID = e.LangID,
                            Building = e.BuildingCode,
                            MailAddress = e.MailAddress,
                        };
            Users u = query.FirstOrDefault(d => d.EName.ToLower() == EName.ToLower());
            return u;
        }

        public Users GetUserInfoByID(int ID)
        {
            UnitOfWork uw = new UnitOfWork(new EFSYSContext());

            Repository<SYS_Users> resp = uw.Repository<SYS_Users>();

            var query = from e in resp.Entities
                        select new Users
                        {
                            ID = e.ID,
                            SiteCode = e.SiteCode,
                            EmployeeNO = e.EmpNO,
                            Account = e.Account,
                            CName = e.CName,
                            EName = e.EName,
                            LangID = e.LangID,
                            Building = e.BuildingCode,
                            MailAddress = e.MailAddress,
                        };
            Users u = query.FirstOrDefault(d => d.ID == ID);
            return u;
        }

        /// <summary>
        /// 
        /// 通过工号查询用户信息
        /// </summary>
        /// <param name="EmpNO"></param>
        /// <returns></returns>
        public static Users GetUserInfoByEmpNO(string EmpNO)
        {
            UnitOfWork uw = new UnitOfWork(new EFSYSContext());

            Repository<SYS_Users> resp = uw.Repository<SYS_Users>();

            var query = from e in resp.Entities
                        select new Users
                        {
                            ID = e.ID,
                            SiteCode = e.SiteCode,
                            EmployeeNO = e.EmpNO,
                            Account = e.Account,
                            CName = e.CName,
                            EName = e.EName,
                            LangID = e.LangID,
                            Building = e.BuildingCode,
                            MailAddress = e.MailAddress,
                        };
            Users u = query.FirstOrDefault(d => d.EmployeeNO == EmpNO);
            return u;
        }


        /// <summary>
        /// 
        /// 通过用户电子邮件地址查询用户信息
        /// </summary>
        /// <param name="EmpNO"></param>
        /// <returns></returns>
        public static Users GetUserInfoByEmpEmailAddress(string EmailAddress)
        {
            UnitOfWork uw = new UnitOfWork(new EFSYSContext());

            Repository<SYS_Users> resp = uw.Repository<SYS_Users>();

            var query = from e in resp.Entities
                        select new Users
                        {
                            ID = e.ID,
                            SiteCode = e.SiteCode,
                            EmployeeNO = e.EmpNO,
                            Account = e.Account,
                            CName = e.CName,
                            EName = e.EName,
                            LangID = e.LangID,
                            Building = e.BuildingCode,
                            MailAddress = e.MailAddress,
                            ReportTo=e.ReportTo,
                        };
            Users u = query.FirstOrDefault(d => d.MailAddress == EmailAddress);
            return u;
        }

        /// <summary>
        /// 
        /// 通过dmn账号查询用户信息
        /// </summary>
        /// <param name="EmpNO"></param>
        /// <returns></returns>
        public static Users GetUserInfoByDMNAccount(string account)
        {
            UnitOfWork uw = new UnitOfWork(new EFSYSContext());

            Repository<SYS_Users> resp = uw.Repository<SYS_Users>();

            var query = from e in resp.Entities
                        select new Users
                        {
                            ID = e.ID,
                            SiteCode = e.SiteCode,
                            EmployeeNO = e.EmpNO,
                            Account = e.Account,
                            CName = e.CName,
                            EName = e.EName,
                            LangID = e.LangID,
                            Building = e.BuildingCode,
                            MailAddress = e.MailAddress,
                            ReportTo = e.ReportTo,
                        };
            Users u = query.FirstOrDefault(d => d.Account == account);
            return u;
        }

        /// <summary>
        /// 获取语言列表
        /// </summary>
        /// <returns></returns>
        public List<Language> GetLanguageList()
        {
            return Data.CacheData.DataCache.Languages;
        }

        public string GetMessage(string code,int LandID,params string[] args)
        {
            return Data.CacheData.DataCache.GetMessage(LandID, code, args);
        }

        /// <summary>
        /// 根据编号代码共取当前语言信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetMessage(string code, params string[] args)
        {
            return Data.CacheData.DataCache.GetMessage(CurrentUser == null ? 1 : CurrentUser.LangID, code, args);
        }
        /// <summary>
        /// 根据编号代码获取语言信息列表
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public List<LangMsg> GetMessages(List<string> codes)
        {
            return Data.CacheData.DataCache.GetMessages(CurrentUser == null ? 1 : CurrentUser.LangID, codes);
        }
        /// <summary>
        /// 根据数据库连接的Code获取连接池信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetDbConn(string code)
        {
            return Data.CacheData.DataCache.GetDbConn(code);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="code"></param>
        public void RemoveCache(string code)
        {
            Data.CacheData.DataCache.RemoveCache(code);
        }

        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public string RunProcedure(string procName, string sqlXml, out int code)
        {
            return MsSql.RunProcedure(procName, sqlXml, out code);
        }


        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public string RunProcedure(string procName, string sqlXml, out int code,string conn)
        {
            return MsSql.RunProcedure(procName, sqlXml, out code,conn);
        }

        /// <summary>
        /// 执行存储过程返回错误信息
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlXml">参数</param>
        /// <param name="code">错误代码</param>
        /// <returns></returns>
        public DataSet RunProcedure(string procName, IDataParameter[] parameters, string tablename, string conn = null)
        {
            return MsSql.RunProcedure(procName, parameters, tablename, conn);
        }

        /// <summary>
        /// 执行储存过程返回查询结果，该储存过程必须只有两个参数分别为sqlxml和msg,其中msg为output 参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sqlXml"></param>
        /// <returns></returns>
        public System.Data.DataSet Query(string procName, string sqlXml,string conn=null)
        {
            SqlParameter[] para ={
                                  new SqlParameter("@sqlxml", SqlDbType.Xml),
                                  new SqlParameter("@msg", SqlDbType.VarChar, 1000)
                              };
            para[0].Value = sqlXml;
            para[1].Direction = ParameterDirection.Output;
            return MsSql.RunProcedure(procName, para, "t1", conn);
        }


        /// <summary>
        /// 执行储存过程返回查询结果，该储存过程必须只有两个参数分别为sqlxml和msg,其中msg为output 参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="sqlXml"></param>
        /// <returns></returns>
        public System.Data.DataSet Query(string procName, string sqlXml, string msg, string conn = null)
        {
            SqlParameter[] para ={
                                  new SqlParameter("@sqlxml", SqlDbType.Xml),
                                  new SqlParameter("@msg", SqlDbType.VarChar, 1000)
                              };
            para[0].Value = sqlXml;
            para[1].Direction = ParameterDirection.Output;
            return MsSql.RunProcedure(procName, para, "t1", conn);
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string SQLString, string conn = null)
        {
            return MsSql.ExecuteSql(SQLString, conn);
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataSet QueryBySql(string SQLString, string conn = null)
        {
            return MsSql.Query(SQLString, conn);
        }
        /// <summary>
        /// 获取用户当前登录用户的基本信息（用于存储过程参数）
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetUserInfoDic(Dictionary<string, object> dic = null)
        {
            if (dic == null)
            {
                dic = new Dictionary<string, object>();
            }
            dic.Add("EmployeeNO", CurrentUser.EmployeeNO);
            dic.Add("SiteCode", CurrentUser.SiteCode);
            dic.Add("Building", CurrentUser.Building);
            dic.Add("LangID", CurrentUser.LangID);
            dic.Add("SysCode", CurrentUser.SysCode);
            return dic;
        }
        /// <summary>
        /// URL参数加密
        /// </summary>
        /// <returns></returns>
        public string GetEncryptionUserInfo(Dictionary<string,object> dic=null)
        {
            dic = GetUserInfoDic(dic);
            dic.Add("Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            int index = 0;
            StringBuilder sb = new StringBuilder();
            foreach(var obj in dic)
            {
                sb.AppendFormat("{0}{1}={2}", index == 0 ? "" : "&", obj.Key, obj.Value.ToString());
                index++;
            }
            return DESSecurity.Encrypt(sb.ToString());
        }
        /// <summary>
        /// URL参数解密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetDecryptionInfo(string encryptString)
        {
            string Decryptstr = DESSecurity.Decrypt(encryptString);
            string[] strs = Decryptstr.Split('&');
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach(var str in strs)
            {
                string[] s = str.Split('=');
                if (!result.ContainsKey(s[0]))
                {
                    result.Add(s[0], s[1]);
                }
            }
            return result;
        }
    }
}
