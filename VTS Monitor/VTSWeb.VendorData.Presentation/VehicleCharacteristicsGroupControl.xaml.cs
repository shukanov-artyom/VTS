using System.Windows.Controls;

namespace VTSWeb.VendorData.Presentation
{
    public partial class VehicleCharacteristicsGroupControl : UserControl
    {
        public VehicleCharacteristicsGroupControl()
        {
            InitializeComponent();
        }

        public VehicleCharacteristicsGroupControl(
            VehicleCharacteristicsItemsGroupViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}
