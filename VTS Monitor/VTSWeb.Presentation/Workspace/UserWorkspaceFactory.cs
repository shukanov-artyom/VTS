using System;
using System.Windows.Controls;
using VTS.Shared.DomainObjects;
using VTSWeb.Presentation.Workspace.Admin;
using VTSWeb.Presentation.Workspace.Client;
using VTSWeb.Presentation.Workspace.Partner;

namespace VTSWeb.Presentation.Workspace
{
    public class UserWorkspaceFactory
    {
        private User user;

        public UserWorkspaceFactory(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            this.user = user;
        }

        public UserControl GetWorkspace()
        {
            switch (user.Role)
            {
                case UserRole.Administrator:
                    return GetWorkspaceForAdministrator();
                case UserRole.Partner:
                    return GetWorkspaceForPartner();
                case UserRole.Client:
                    return GetWorkspaceForClient();
                default:
                    throw new ArgumentException("Wrong user role!");
            }
        }

        private UserControl GetWorkspaceForAdministrator()
        {
            AdministratorWorkspaceControl adminWorkspace = 
                new AdministratorWorkspaceControl();
            return adminWorkspace;
        }

        private UserControl GetWorkspaceForPartner()
        {
            return new PartnerWorkspaceControl();
        }

        private UserControl GetWorkspaceForClient()
        {
            return new ClientWorkspaceControl();
        }
    }
}
