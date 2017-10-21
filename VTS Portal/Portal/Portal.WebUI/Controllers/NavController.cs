using System;
using System.Web.Mvc;

namespace Portal.WebUI.Controllers
{
    public class NavController : Controller
    {
        /// <summary>
        /// Navigation menu wiidget.
        /// </summary>
        public PartialViewResult Menu()
        {
            return PartialView();
        }
    }
}