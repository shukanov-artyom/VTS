using System.Windows.Controls;

namespace VTSWeb.VendorData.Presentation
{
    public partial class VehicleCharacteristicsTabbedControl : UserControl
    {
        public VehicleCharacteristicsTabbedControl()
        {
            InitializeComponent();
        }

        public VehicleCharacteristicsTabbedControl(
            VehicleCharacteristicsViewModel viewModel)
            : this()
        {
            foreach (VehicleCharacteristicsItemsGroupViewModel
                groupViewModel in viewModel.Groups)
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = 
                    new VehicleCharacteristicsGroupControl(groupViewModel);
                tabItem.Header = groupViewModel.Name;
                tabControlCharacteristics.Items.Add(tabItem);
            }
        }
    }
}
