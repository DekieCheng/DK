using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SDPSubSystem.Web.Startup))]
namespace SDPSubSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
