using System;

namespace VTS.Shared.DomainObjects
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
