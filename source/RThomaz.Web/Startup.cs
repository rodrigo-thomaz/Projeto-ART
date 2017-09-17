using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RThomaz.Web.Startup))]

namespace RThomaz.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }        
    }
}
