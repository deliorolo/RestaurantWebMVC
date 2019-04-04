using CodeLibrary;
using CodeLibrary.DataAccess;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Utils;

namespace RestaurantWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            TypeOfAccess.Access = Connection.WebApi;

            ModelBinders.Binders.Add(typeof(decimal), new ModelBinder.DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new ModelBinder.DecimalModelBinder());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
            if (TypeOfAccess.Access == Connection.WebApi)
            {
                APIClientConfig.InitializeClient();
            }
        }
    }
}
