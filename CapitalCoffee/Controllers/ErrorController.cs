using System.Web.Mvc;

namespace CapitalCoffee.Controllers
{
    public class ErrorController : Controller
    {

        [HttpGet]
        [HandleError]
        public ActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        [HandleError]
        public ActionResult Forbidden()
        {
            return View();
        }

        [HttpGet]
        [HandleError]
        public ActionResult Internal()
        {
            return View();
        }
    }
}