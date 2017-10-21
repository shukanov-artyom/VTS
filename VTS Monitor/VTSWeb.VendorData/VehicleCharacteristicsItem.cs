using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects;

namespace VTSWeb.VendorData
{
    public class VehicleCharacteristicsItem : DomainObject
    {
        public long GroupId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public KeyVehicleCharacteristics? Code
        {
            get;
            set;
        }
    }
}
