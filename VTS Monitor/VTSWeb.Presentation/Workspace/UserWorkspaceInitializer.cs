using System;
using System.Windows.Controls;
using VTSWeb.Common;
using VTSWeb.Presentation.Welcome;

namespace VTSWeb.Presentation.Workspace
{
    public class UserWorkspaceInitializer
    {
        private StackPanel placeholder;

        public UserWorkspaceInitializer(StackPanel placeholder)
        {
            this.placeholder = placeholder;
            placeholder.Children.Clear();
            placeholder.Children.Add(new WelcomeScreenControl());
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
            placeholder.Children.Add(new WelcomeScreenControl());
        }
    }
}
