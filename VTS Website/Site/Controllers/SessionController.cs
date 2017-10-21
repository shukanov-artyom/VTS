using System;
using System.Web.Mvc;
using System.Web.Security;
using Html.Models;
using Site.Common;
using VTS.Shared;
using VTS.Site.WebService.Assemblers;
using VTS.Site.WebService.VtsWebService;

namespace Html.Controllers
{
    public class SessionController : ControllerBase
    {
        public ActionResult ChangeCurrentCulture(int culture)
        {
            // Change the current culture for this user.
            SiteSession.CurrentUICulture = culture;
            // Cache the new current culture into the user HTTP session. 
            Session["CurrentUICulture"] = culture;
            // Redirect to the same page from where the request was made! 
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ViewResult LogOn()
        {
            //
            // Create the model.
            //
            LogOnModel model = new LogOnModel();
            model.Username = String.Empty;
            model.Password = String.Empty;
            //
            // Activate the view.
            //
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                VtsWebServiceClient service = new VtsWebServiceClient();
                UserDto userDto = service.AuthenticateUser(
                    model.Username, Sha256Hash.Calculate(model.Password));
                if (userDto != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    SiteSession siteSession = new SiteSession();
                    siteSession.User = UserAssembler.FromDtoToDomainObject(userDto);
                    Session["SiteSession"] = siteSession;
                    return RedirectToAction("Index", "AdminConsole");
                }
                ModelState.AddModelError("", Resource.LogOnErrorMessage);
                return View(model);
            }
            return View(model);
        }

        /// <summary>
        /// Log off.
        /// </summary>
        /// <returns>The action result.</returns>
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            SiteSession siteSession = this.CurrentSiteSession;
            //
            // Clear the user session!
            //
            int culture = SiteSession.CurrentUICulture;
            this.Session["SiteSession"] = null;
            //
            return RedirectToAction("Index", "Home");
        }
    }
}
