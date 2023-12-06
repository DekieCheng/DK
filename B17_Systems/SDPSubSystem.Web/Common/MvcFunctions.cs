using SDPSubSystem.Model.AssistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDPSubSystem.Web.Common
{
    public class MvcFunctions
    {
        /// <summary>
        /// 获取用户当前IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string userIP;
            // HttpRequest Request = HttpContext.Current.Request; 
            HttpRequest Request = HttpContext.Current.Request; // ForumContext.Current.Context.Request; 
                                                               // 如果使用代理，获取真实IP 
            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                userIP = Request.ServerVariables["REMOTE_ADDR"];
            else
                userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (userIP == null || userIP == "")
                userIP = Request.UserHostAddress;
            return userIP;
        }
        /// <summary>
        /// 获取当前用户请求的M V C
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static MvcEntity GetMvcEntity(HttpContextBase httpContext)
        {
            MvcEntity entity = new MvcEntity();
            entity.Controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            entity.Action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            if (httpContext.Request.RequestContext.RouteData.DataTokens.Keys.Contains("area"))
            {
                entity.Area = httpContext.Request.RequestContext.RouteData.DataTokens["area"].ToString();
            }
            else
            {
                entity.Area = "";
            }
            return entity;
        }
    }
}