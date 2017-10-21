using System;
using System.Web.Mvc;
using Html.Models;

namespace Html.Controllers
{
    public class ControllerBase : Controller
    {
        /// <summary>
        /// Gets the current site session.
        /// </summary>
        public SiteSession CurrentSiteSession
        {
            get
            {
                SiteSession vtsSiteSession = (SiteSession)this.Session["SiteSession"];
                return vtsSiteSession;
            }
        }
    }
}