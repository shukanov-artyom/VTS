using System;

namespace VTS.Shared.DomainObjects
{
    public class VehicleEvent : DomainObject
    {
        public VehicleEvent()
        {
            Date = DateTime.Now;
        }

        public DateTime Date { get; set; }

        public int Mileage { get; set; }

        public VehicleEventType Type { get; set; }

        public int? YellowMileage { get; set; }

        public int? RedMileage { get; set; }

        public String Comment { get; set; }

        public long VehicleId { get; set; }

        /// <summary>
        /// If both replacement period and replacement mileage is set then what comes the first will be notified.
        /// </summary>
        public int NextReplacementPeriod { get; set; }
    }
}
