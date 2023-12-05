using SDPSubSystem.Web.Filter;
using System.Web;
using System.Web.Mvc;

namespace SDPSubSystem.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionLogAttribute());
            filters.Add(new AuthorizeLoginAttribute());
        }
    }
}
