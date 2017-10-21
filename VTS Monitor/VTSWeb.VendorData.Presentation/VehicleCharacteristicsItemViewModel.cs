using System;
using VTSWeb.Presentation.Common;

namespace VTSWeb.VendorData.Presentation
{
    public class VehicleCharacteristicsItemViewModel : ViewModelBase
    {
        private VehicleCharacteristicsItem item;

        public VehicleCharacteristicsItemViewModel(
            VehicleCharacteristicsItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            this.item = item;
        }

        public string Name
        {
            get
            {
                return item.Name;
            }
        }

        public string Value
        {
            get
            {
                return item.Value;
            }
        }
    }
}
