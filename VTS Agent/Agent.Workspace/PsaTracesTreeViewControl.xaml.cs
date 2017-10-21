using System;
using System.Windows;
using System.Windows.Controls;
using Agent.Workspace.EventArguments;

namespace Agent.Workspace
{
    /// <summary>
    /// Interaction logic for PsaTracesTreeViewControl.xaml
    /// </summary>
    public partial class PsaTracesTreeViewControl : UserControl
    {
        public event EventHandler OnTreeSelectionChanged;

        public PsaTracesTreeViewControl()
        {
            InitializeComponent();
        }

        private void SelectedTreeItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            if (OnTreeSelectionChanged != null)
            {
                OnTreeSelectionChanged.Invoke(this,
                    new PsaTreeSelectionChangedEventArgs(e.NewValue));
            }
        }
    }
}
