using System;
using System.Reflection;
using System.Windows.Controls;
using VTSWeb.Presentation.Workspace;

namespace VTS
{
    public partial class MainPage : UserControl
    {
        private UserWorkspaceInitializer workspaceInitializer;

        public MainPage()
        {
            InitializeComponent();
            workspaceInitializer = new UserWorkspaceInitializer(
                stackPanelPlaceholder);
            AssemblyName assemblyName = 
                new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            textBlockVersion.Text = assemblyName.Version.ToString();
        }
    }
}
