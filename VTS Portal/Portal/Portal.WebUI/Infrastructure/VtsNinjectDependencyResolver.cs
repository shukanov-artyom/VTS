using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Portal.Service.Service;

namespace Portal.WebUI.Infrastructure
{
    /// <summary>
    /// VTS Website dependency resolver.
    /// </summary>
    public class VtsNinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public VtsNinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IVtsWebService>().To<VtsWebServiceClient>();
        }
    }
}