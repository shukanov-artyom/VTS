using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using VTS.Shared.DomainObjects;

using VTSWeb.Presentation.Common;

namespace VTSWeb.VendorData.Presentation
{
    public class VehicleCharacteristicsViewModel : ViewModelBase
    {
        private Vehicle vehicle;
        private ObservableCollection<
            VehicleCharacteristicsItemsGroupViewModel> groups =
            new ObservableCollection<
                VehicleCharacteristicsItemsGroupViewModel>();
        
        private VehicleCharacteristics characteristics;
        private ObservableCollection<
            VehicleCharacteristicsItemViewModel> baseCollection = 
            new ObservableCollection<VehicleCharacteristicsItemViewModel>();
        private PagedCollectionView items;

        public VehicleCharacteristicsViewModel(
            Vehicle vehicle,
            VehicleCharacteristics characteristics)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException("vehicle");
            }
            if (characteristics == null)
            {
                throw new ArgumentNullException("characteristics");
            }
            this.vehicle = vehicle;
            this.characteristics = characteristics;

            // fill direct items collection
            foreach (VehicleCharacteristicsItemsGroup group 
                in characteristics.ItemsGroups)
            {
                foreach (VehicleCharacteristicsItem item in group.Items)
                {
                    baseCollection.Add(
                        new VehicleCharacteristicsItemViewModel(item));
                }
            }
            items = new PagedCollectionView(baseCollection);
            items.GroupDescriptions.Clear();
            items.GroupDescriptions.Add(
                new PropertyGroupDescription("Group"));

            // fill groups collection
            foreach (VehicleCharacteristicsItemsGroup group
                in characteristics.ItemsGroups)
            {
                groups.Add(
                    new VehicleCharacteristicsItemsGroupViewModel(group));
            }
        }

        public PagedCollectionView Items
        {
            get
            {
                return items;
            }
        }

        public ObservableCollection<VehicleCharacteristicsItemsGroupViewModel> Groups
        {
            get
            {
                return groups;
            }
        }
    }
}
