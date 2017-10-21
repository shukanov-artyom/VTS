using System;
using System.Web.Mvc;
using System.Web.Routing;
using Portal.WebUI.Infrastructure.Binders;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Portal.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(User), new UserModelBinder());
            ModelBinders.Binders.Add(typeof(SupportedLanguage), new LanguageModelBinder());
        }
    }
}
