using System;
using System.Web.Mvc;
using VTS.Shared.DomainObjects;

namespace Portal.WebUI.Infrastructure.Binders
{
    public class UserModelBinder : IModelBinder
    {
        private const string SessionKey = "User";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            User user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                return controllerContext.HttpContext.Session[SessionKey] as User;
            }
            if (user == null)
            {
                throw new InfrastructureException("Cannot find logged user for model binding.");
            }
            return user;
        }
    }
}