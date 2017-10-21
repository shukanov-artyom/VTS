using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.DiagBox.Xml.TraceUserMembers
{
    public class InfoVEH
    {
        [XmlAttribute("Date")]
        public string DateAttribute { get; set; }

        [XmlElement("Date")]
        public string Date { get; set; }

        [XmlElement("Engine")]
        public string Engine { get; set; }

        [XmlElement("Carrosserie")]
        public string Carrosserie { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Trademark")]
        public string Trademark { get; set; }

        [XmlElement("VariantName")]
        public string VariantName { get; set; }

        [XmlElement("VIN")]
        public string Vin { get; set; }

        [XmlElement("VinEnterMode")]
        public string VinEnterMode { get; set; }

        [XmlElement("DamOpr")]
        public string DamOpr { get; set; }

        [XmlElement("KM")]
        public int MileageKm { get; set; }
    }
}
