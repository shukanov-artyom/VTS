using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.DiagBox.Xml.TraceUserMembers
{
    public class InfoTrace
    {
        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlElement("Version")]
        public string Version { get; set; }

        [XmlElement("Date")]
        public string DateElement { get; set; }
    }
}
