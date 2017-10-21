using System;
using System.Web.Mvc;

namespace Html.Controllers
{
    public class TopicDetailsController : ControllerBase
    {
        public ActionResult GeneralOverview()
        {
            return View();
        }

        public ActionResult DataCapture()
        {
            return View();
        }

        public ActionResult DataAnalysis()
        {
            return View();
        }

        public ActionResult DataShare()
        {
            return View();
        }
    }
}
