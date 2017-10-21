using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.GraphTypeData
{
    public class LexiaChannelRawData
    {
        [XmlElement("ChannelHeader")]
        public LexiaChannelHeader Header
        {
            get;
            set;
        }

        [XmlElement("Points")]
        public LexiaDataPointsCollection ChannelDataPoints
        {
            get;
            set;
        }
    }
}
