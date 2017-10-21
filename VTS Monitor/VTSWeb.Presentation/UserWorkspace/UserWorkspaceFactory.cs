using System;
using System.Windows.Controls;
using VTSWeb.DomainObjects;
using VTSWeb.DomainObjects.Enums;
using VTSWeb.Presentation.Workspace.Admin;
using VTSWeb.Presentation.Workspace.Partner;

namespace VTSWeb.Presentation.UserWorkspace
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
                case UserRole.EndUser:
                    return GetWorkspaceForEndUser();
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

        private UserControl GetWorkspaceForEndUser()
        {
            throw new NotImplementedException();
        }
    }
}
