using VTS.Shared.DomainObjects;

namespace VTS.Site.DomainObjects.VendorData
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
    }
}
