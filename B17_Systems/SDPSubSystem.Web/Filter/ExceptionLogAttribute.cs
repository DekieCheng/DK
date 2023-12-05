using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDPSubSystem.Web.Filter
{
    public class ExceptionLogAttribute:HandleErrorAttribute
    {

        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       // protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override void OnException(ExceptionContext filterContext)
        {
            string message = string.Format("MessageType：{0} , Content：{1} , Function：{2} , Object：{3} , Controller：{4} , Action：{5} ; ", 
                    filterContext.Exception.GetType().Name, 
                    filterContext.Exception.Message, 
                    filterContext.Exception.TargetSite, 
                    filterContext.Exception.Source, 
                    filterContext.RouteData.GetRequiredString("controller"), 
                    filterContext.RouteData.GetRequiredString("action"));
            log.Error(message);
            base.OnException(filterContext);

        }
    }
}