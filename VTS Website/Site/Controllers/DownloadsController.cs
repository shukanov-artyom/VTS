using System;
using System.Web.Mvc;
using Html.Models;

namespace Html.Controllers
{
    public class DownloadsController : ControllerBase
    {
        public ActionResult Index()
        {
            return View(new DownloadAgentViewModel());
        }
    }
}
