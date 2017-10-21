using System;
using System.Web.Mvc;

namespace Html.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetBanner(int number)
        {
            string partialViewName = String.Format("_Banner{0}PartialView", number);
            return PartialView(partialViewName);
        }
    }
}
