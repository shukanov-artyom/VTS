using System;
using System.Windows.Controls;
using VTSWeb.Common;
using VTSWeb.Presentation.Workspace.Admin;

namespace VTSWeb.Presentation.UserWorkspace
{
    public class UserWorkspaceInitializer
    {
        private StackPanel placeholder;

        public UserWorkspaceInitializer(StackPanel placeholder)
        {
            this.placeholder = placeholder;
            LoggedUserContext.UserLoggedIn += OnUserLogon;
            LoggedUserContext.UserLoggedOut += OnUserLogout;
        }

        private void OnUserLogon(object sender, EventArgs e)
        {
            UserLoggedOnEventArgs ea = e as UserLoggedOnEventArgs;
            if (ea == null)
            {
                throw new ArgumentNullException("e");
            }
            UserWorkspaceFactory factory = new UserWorkspaceFactory(ea.User);
            UserControl workspace = factory.GetWorkspace();
            placeholder.Children.Clear();
            placeholder.Children.Add(workspace);
        }

        private void OnUserLogout(object sender, EventArgs e)
        {
            placeholder.Children.Clear();
        }
    }
}
