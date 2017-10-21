using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.DiagBox.Xml
{
    [XmlRoot("TraceSession")]
    public class TraceSessionXml
    {
        [XmlElement("Version")]
        public int Version { get; set; }

        [XmlElement("DATE")]
        public string Date { get; set; }

        [XmlElement("TRACE_ID")]
        public string TraceId { get; set; }

        [XmlElement("VEHICLE_NAME")]
        public string VehicleName { get; set; }

        [XmlElement("VEHICLE_ARCHI_NAME")]
        public string VehicleArchiName { get; set; }

        [XmlElement("VIN")]
        public string Vin { get; set; }

        [XmlElement("TRADEMARK")]
        public string Trademark { get; set; }
    }
}
