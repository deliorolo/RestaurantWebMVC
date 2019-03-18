using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantWeb.Startup))]
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
