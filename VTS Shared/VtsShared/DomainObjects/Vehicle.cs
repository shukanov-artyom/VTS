using System;
using VTS.Shared;

namespace VTS.Shared.DomainObjects
{
    public class Vehicle : DomainObject
    {
        public string Vin
        {
            get;
            set;
        }

        public int ProductionYear
        {
            get;
            set;
        }

        public Manufacturer Manufacturer
        {
            get;
            set;
        }

        public string Model
        {
            get;
            set;
        }

        public System.DateTime RegisteredDate
        {
            get;
            set;
        }

        public int Mileage
        {
            get;
            set;
        }
    }
}
