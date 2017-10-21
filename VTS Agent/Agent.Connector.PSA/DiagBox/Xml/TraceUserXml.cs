using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Agent.Connector.PSA.DiagBox.Xml.TraceUserMembers;

namespace Agent.Connector.PSA.DiagBox.Xml
{
    [XmlRoot("TraceUser")]
    public class TraceUserXml
    {
        [XmlElement("InfoTrace")]
        public InfoTrace InfoTrace { get; set; }

        [XmlElement("InfoTool")]
        public InfoTool InfoTool { get; set; }

        [XmlElement("InfoPDV")]
        public InfoPDV InfoPDV { get; set; }

        [XmlElement("InfoVEH")]
        public InfoVEH InfoVeh { get; set; }

        [XmlElement("InfoMCC")]
        public List<InfoMCC> InfoMCC { get; set; }

        [XmlElement("InfoUserAction")]
        public List<InfoUserAction> InfoUserAction { get; set; }
    }
}
