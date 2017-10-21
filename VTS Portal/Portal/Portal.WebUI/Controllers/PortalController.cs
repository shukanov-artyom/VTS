using System;
using System.Web.Mvc;

namespace Portal.WebUI.Controllers
{
    public class PortalController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}