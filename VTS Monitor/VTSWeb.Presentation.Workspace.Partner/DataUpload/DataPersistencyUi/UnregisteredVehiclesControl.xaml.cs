using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Workspace.Partner.DataUpload.DataPersistencyUi
{
    public partial class UnregisteredVehiclesControl : UserControl
    {
        private ObservableCollection<string> vinsDisplayed = 
            new ObservableCollection<string>();

        public event EventHandler OkClick;

        public UnregisteredVehiclesControl()
        {
            InitializeComponent();
            dataGridVins.ItemsSource = vinsDisplayed;
        }

        public void DisplayUnregisteredVehiclesData(IList<string> vins)
        {
            vinsDisplayed.Clear();
            foreach (string vin in vins)
            {
                vinsDisplayed.Add(vin);
            }
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if (OkClick != null)
            {
                OkClick.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
