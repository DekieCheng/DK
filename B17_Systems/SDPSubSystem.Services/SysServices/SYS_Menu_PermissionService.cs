﻿using SDPSubSystem.Common.Opt;
using SDPSubSystem.Data;
using SDPSubSystem.Model.AssistModels;
using SDPSubSystem.Model.SysModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SDPSubSystem.Services.SysServices
{
    public class SYS_Menu_PermissionService:BaseService
    {
        private string _DBContext = SDPSubSystem.Common.Security.GetConnUtil.GetConn(System.Configuration.ConfigurationManager.AppSettings["SDPURL"]);
        //System.Configuration.ConfigurationManager.ConnectionStrings["SYSDbContext"].ConnectionString)
        /// <summary>
        /// 判断用户是否有当前功能页的权限
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsHavePagePermission(string key)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Action", "IsPermission");
            dic.Add("PermissionKey", key);
            return GetPermission(dic).Tables[0].Rows.Count > 0;
        }
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        public DataSet GetAccessPages()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Action", "GetAccessPages");
            return GetPermission(dic);
        }
        /// <summary>
        /// 获取有权限的Button列表
        /// </summary>
        /// <param name="pagePermissionKey"></param>
        /// <returns></returns>
        public DataSet GetAccessButtons(string pagePermissionKey)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Action", "GetAccessButtons");
            dic.Add("PermissionKey", pagePermissionKey);
            return GetPermission(dic);
        }

        /// <summary>
        /// 添加使用记录
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ip"></param>
        public void SaveUsageLogs(string key,string ip)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Action", "UsageLogs");
            dic.Add("IPAddress", ip);
            dic.Add("PermissionKey", key);
            try
            {
                int code = 0;
                SavePermission(out code, dic);
            }
            catch { }
        }

        protected DataSet GetPermission(Dictionary<string,object> dic = null)
        {
            dic = base.GetUserInfoDic(dic);
            return base.Query("usp_SYS_SubSystem_GetPermission", XmlParams.CreateSqlXml(dic), _DBContext);
        }

        protected string SavePermission(out int code,Dictionary<string,object> dic = null)
        {
            dic = base.GetUserInfoDic(dic);
            return base.RunProcedure("usp_SYS_SubSystem_SavePermission", XmlParams.CreateSqlXml(dic), out code,_DBContext);
        }

        public bool SetLogin(HttpResponseBase response,string param)
        {
            Dictionary<string, object> dic = base.GetDecryptionInfo(param);//empinfo = string.Empty;
            string _empInfo = dic["SiteCode"].ToString() + "|*|" + dic["Building"].ToString() + "|*|" + dic["EmployeeNO"].ToString() + "|*|" + dic["SysCode"].ToString() + "|*|" + dic["LangID"].ToString();
            DateTime time = DateTime.Parse(dic["Time"].ToString());
            var ts = DateTime.Now - time;
            //var ts = 31;
            if (ts.TotalSeconds > 30 || ts.TotalSeconds < 0)
            //if (ts > 30 || ts < 0)
            {
                throw new Exception("The link has expired");
            }
            if (CurrentUser != null)
            {
                CurrentUser.SysCode = dic["SysCode"].ToString();//更新用户的链接
                if (CurrentUser.SiteCode!= dic["SiteCode"].ToString() || CurrentUser.Building!= dic["Building"].ToString())
                {
                    throw new Exception("You have already opened a system in another building and must quit before you can open a new system.");
                }
            }
            else
            {
                Users user = base.GetLoginUser(_empInfo);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(_empInfo, false);
                    HttpCookie cookie = FormsAuthentication.GetAuthCookie(_empInfo, false);
                    //FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(cookie.Value);
                    //FormsAuthenticationTicket nTicket = new FormsAuthenticationTicket(1, oldTicket.Name, oldTicket.IssueDate, DateTime.Now.AddMinutes(120), oldTicket.IsPersistent, oldTicket.UserData, FormsAuthentication.FormsCookiePath);
                    //cookie.Domain = FormsAuthentication.CookieDomain;
                    //cookie.Value = FormsAuthentication.Encrypt(nTicket);
                    //HttpContext.Current.Response.Cookies.Add(cookie);
                    base.CurrentUser = user;
                }
                else
                {
                    throw new Exception("User information is incorrect");
                }
            }
            return true;
        }

        //public void SetLogOff()
        //{
        //    HttpCookie cookie = HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName];
        //    cookie.Domain = FormsAuthentication.CookieDomain;
        //    cookie.Value = null;
        //    cookie.Expires = DateTime.Now.AddDays(-1);
        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //    FormsAuthentication.SignOut();
        //}

        public string GetSDPUrl()
        {
            return System.Configuration.ConfigurationManager.AppSettings["SDPURL"];
        }

       

    }
}
