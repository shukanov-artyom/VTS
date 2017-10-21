using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Agent.Common.Presentation.Characteristics
{
    /// <summary>
    /// Interaction logic for VehicleCharacteristicsControl.xaml
    /// </summary>
    public partial class VehicleCharacteristicsControl : UserControl
    {
        public VehicleCharacteristicsControl()
        {
            InitializeComponent();
        }

        public VehicleCharacteristicsControl(VehicleCharacteristicsViewModel vm)
            : this()
        {
            Update(vm);
        }

        private void Update(VehicleCharacteristicsViewModel viewModel)
        {
            DataContext = null;
            List<TabItem> items = new List<TabItem>();
            foreach (VehicleCharacteristicsItemsGroupViewModel groupViewModel in viewModel.Groups)
            {
                TabItem item = new TabItem();
                item.Header = groupViewModel.Name;
                VehicleCharacteristicsItemsGroupControl control = 
                    new VehicleCharacteristicsItemsGroupControl();
                control.DataContext = groupViewModel;
                item.Content = control;
                items.Add(item);
            }
            DataContext = items;
            tabControlCharacteristics.SelectedItem = items[0];
        }
    }
}
