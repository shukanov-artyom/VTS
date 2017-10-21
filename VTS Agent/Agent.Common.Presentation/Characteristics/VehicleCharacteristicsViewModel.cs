using System;
using System.Collections.ObjectModel;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Presentation.Characteristics
{
    public class VehicleCharacteristicsViewModel : ViewModelBase
    {
        private ObservableCollection<VehicleCharacteristicsItemsGroupViewModel> groups =
            new ObservableCollection<VehicleCharacteristicsItemsGroupViewModel>();
        private VehicleCharacteristics model;

        public VehicleCharacteristicsViewModel(
            VehicleCharacteristics model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            foreach (VehicleCharacteristicsItemsGroup group
                in model.ItemsGroups)
            {
                groups.Add(new VehicleCharacteristicsItemsGroupViewModel(group));
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
