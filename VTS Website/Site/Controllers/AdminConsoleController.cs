using System;
using System.Web.Mvc;

namespace Html.Controllers
{
    public class AdminConsoleController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
