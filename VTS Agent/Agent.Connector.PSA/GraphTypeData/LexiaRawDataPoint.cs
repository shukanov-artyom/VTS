using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.GraphTypeData
{
    [XmlRoot("DataPoint")]
    public class LexiaRawDataPoint
    {
        [XmlAttribute("Value")]
        public string Value
        {
            get;
            set;
        }

        [XmlAttribute("Timestamp")]
        public string TimeStamp
        {
            get;
            set;
        }
    }
}
