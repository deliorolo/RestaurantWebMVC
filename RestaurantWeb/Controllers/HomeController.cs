using System.Web.Mvc;

namespace RestaurantWeb.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // It gets the main page with info about application
        public ActionResult Index()
        {
            return View();
        }
    }
}