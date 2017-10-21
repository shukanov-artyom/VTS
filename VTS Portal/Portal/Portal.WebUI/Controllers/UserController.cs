using System;
using System.Web.Mvc;
using Portal.Assemblers;
using Portal.Service.Service;
using Portal.WebUI.Utils;
using VTS.Shared;

namespace Portal.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IVtsWebService service;

        public UserController(IVtsWebService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Entry point for a portal.
        /// </summary>
        public ActionResult Logon()
        {
            return View();
        }

        /// <summary>
        /// Action uset lo log in a user after he has entered his credentials.
        /// </summary>
        public ActionResult Signin(string emailOrLogin, string password, string language)
        {
            string passwordHash = Sha256Hash.Calculate(password);
            UserDto user = service.AuthenticateUser(emailOrLogin, passwordHash);
            if (user == null)
            {
                TempData["LogonFailed"] = true;
                return View("Logon");
            }
            Session["User"] = UserAssembler.FromDtoToDomainObject(user);
            Session["Lang"] = LangConverter.Convert(language);
            return RedirectToAction("Index", "Vehicles");
        }
    }
}