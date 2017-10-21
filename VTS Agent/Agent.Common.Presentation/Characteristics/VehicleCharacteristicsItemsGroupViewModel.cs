using System;
using System.Collections.ObjectModel;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Presentation.Characteristics
{
    public class VehicleCharacteristicsItemsGroupViewModel
    {
        private VehicleCharacteristicsItemsGroup model;
        private ObservableCollection<VehicleCharacteristicsItemViewModel> items
            = new ObservableCollection<VehicleCharacteristicsItemViewModel>();

        public VehicleCharacteristicsItemsGroupViewModel(
            VehicleCharacteristicsItemsGroup model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            this.model = model;
            foreach (VehicleCharacteristicsItem item in model.Items)
            {
                items.Add(new VehicleCharacteristicsItemViewModel(item));
            }
        }

        public string Name
        {
            get
            {
                return model.Name;
            }
        }

        public ObservableCollection<VehicleCharacteristicsItemViewModel> Items
        {
            get
            {
                return items;
            }
        }
    }
}
