using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.DiagBox.Xml.TraceUserMembers
{
    public class InfoTool
    {
        [XmlAttribute("date")]
        public string DateAttribute { get; set; }

        [XmlElement("VCISerial")]
        public int VCISerial { get; set; }

        [XmlElement("VCIType")]
        public int VCIType { get; set; }

        [XmlElement("VCIFirmware")]
        public string VCIFirmware { get; set; }

        [XmlElement("Serial")]
        public string Serial { get; set; }

        [XmlElement("ToolVersion")]
        public string ToolVersion { get; set; }
    }
}
