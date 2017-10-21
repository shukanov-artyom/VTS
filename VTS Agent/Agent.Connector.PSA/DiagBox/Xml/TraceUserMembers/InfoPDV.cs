using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.DiagBox.Xml.TraceUserMembers
{
    public class InfoPDV
    {
        [XmlAttribute("date")]
        public string DateAttribute { get; set; }

        [XmlElement("Profil")]
        public string Profil { get; set; }

        [XmlElement("CodeRRDI")]
        public string CodeRRDI { get; set; }

        [XmlElement("Social")]
        public string Social { get; set; }

        [XmlElement("TechnicianName")]
        public string TechnicianName { get; set; }
    }
}
