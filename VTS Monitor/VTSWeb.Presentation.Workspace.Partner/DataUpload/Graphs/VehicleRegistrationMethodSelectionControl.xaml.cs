using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Workspace.Partner.DataUpload.Graphs
{
    public partial class VehicleRegistrationMethodSelectionControl : UserControl
    {
        public event EventHandler RegisterByDeliveredKeyClicked;
        public event EventHandler RegisterByNewKeyCreationClicked;

        public VehicleRegistrationMethodSelectionControl()
        {
            InitializeComponent();
        }

        private void RegisterVehicleButtonClick(
            object sender, RoutedEventArgs e)
        {
            if (radioButtonUseDeliveredKey.IsChecked != null &&
                radioButtonUseDeliveredKey.IsChecked.Value == true)
            {
                if (RegisterByDeliveredKeyClicked != null)
                {
                    RegisterByDeliveredKeyClicked.Invoke(this, 
                        EventArgs.Empty);
                }
            }
            else if (radioButtonCreateNewKeyAndSave.IsChecked != null &&
                radioButtonCreateNewKeyAndSave.IsChecked.Value == true)
            {
                if (RegisterByNewKeyCreationClicked != null)
                {
                    RegisterByNewKeyCreationClicked.Invoke(
                        this, EventArgs.Empty);
                }
            }
            else
            {
                MessageBox.Show("Registration way is not supported yet.");
            }
        }
    }
}
