using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantWeb.Startup))]
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace RestaurantWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
