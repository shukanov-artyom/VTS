using System;
using System.Web.Mvc;
using VTS.Shared;

namespace Portal.WebUI.Infrastructure.Binders
{
    public class LanguageModelBinder : IModelBinder
    {
        private const string SessionKey = "Lang";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext.HttpContext.Session != null)
            {
                return (SupportedLanguage)controllerContext.HttpContext.Session[SessionKey];
            }
            throw new InfrastructureException("Cannot determine language.");
        }
    }
}