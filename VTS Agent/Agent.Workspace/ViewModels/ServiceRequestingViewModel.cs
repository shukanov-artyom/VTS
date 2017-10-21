using System;
using Agent.Common.Presentation;
using Agent.Network.Monitor.VtsWebService;

namespace Agent.Workspace.ViewModels
{
    public abstract class ServiceRequestingViewModel : ViewModelBase
    {
        protected IVtsWebService Service
        {
            get
            {
                return Infrastructure.Container.GetInstance<IVtsWebService>();
            }
        }
    }
}
