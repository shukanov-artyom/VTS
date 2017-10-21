using System;
using System.Windows.Controls;

namespace Agent.Workspace.Views
{
    /// <summary>
    /// Interaction logic for ServiceEventDetailsControl.xaml
    /// </summary>
    public partial class ServiceEventDetailsControl : UserControl
    {
        public ServiceEventDetailsControl()
        {
            InitializeComponent();
        }

        public ComboBox ComboBoxType {
            get
            {
                return comboBoxType;
            }
        }
    }
}
