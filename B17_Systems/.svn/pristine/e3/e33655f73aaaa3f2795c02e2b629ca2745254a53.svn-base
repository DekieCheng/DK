using SDPSubSystem.Model.AssistModels;
using SDPSubSystem.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SDPSubSystem.Web.Filter
{
    public class AuthorizeLoginAttribute: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                MvcEntity mvc = MvcFunctions.GetMvcEntity(httpContext);
                //判断当前URL是否在免授权列表上
                if (Services.SysServices.SYS_PubService.IsExistsNoAuthorize(mvc)) return true;
                else
                {
                    httpContext.Response.StatusCode = 401;
                    return false;
                }
            }
            else return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult { Data = 401, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    if (string.IsNullOrEmpty(filterContext.HttpContext.Request.Url.OriginalString))
                    {
                        filterContext.Result = new RedirectResult(System.Configuration.ConfigurationManager.AppSettings["SDPURL"]);
                    }
                    else
                    {
                       if (filterContext.Result is HttpUnauthorizedResult)
                        {
                            filterContext.Result = new RedirectResult(
                                string.Concat(System.Configuration.ConfigurationManager.AppSettings["SDPURL"],
                                             "?ReturnUrl=",
                                             filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri)));
                        }
                    }
                }
            }
        }
    }
}