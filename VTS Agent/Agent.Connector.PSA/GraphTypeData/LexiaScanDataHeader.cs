using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.GraphTypeData
{
    public class LexiaScanDataHeader
    {
        [XmlAttribute("LogName")]
        public string DataFileName
        {
            get;
            set;
        }

        [XmlAttribute("Duration")]
        public long Duration
        {
            get;
            set;
        }

        [XmlAttribute("_Type")]
        public string Type 
        { 
            get;
            set;
        }

        [XmlAttribute("CaptureTime")]
        public long CaptureTime
        {
            get;
            set;
        }

        [XmlAttribute("VehicleLabel")]
        public string VehicleLabel
        {
            get;
            set;
        }

        [XmlAttribute("ECUName")]
        public string EcuName
        {
            get;
            set;
        }

        [XmlAttribute("ECULabel")]
        public string EcuLabel
        {
            get;
            set;
        }
    }
}
