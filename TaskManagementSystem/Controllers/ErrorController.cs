using System.Web.Mvc;

namespace TaskManagementSystem.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Error404");
        }

        public ActionResult NotFound()
        {
            return View("NotFound");
        }
    }
}