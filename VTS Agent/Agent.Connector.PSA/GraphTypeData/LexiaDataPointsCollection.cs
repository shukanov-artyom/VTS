using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.GraphTypeData
{
    public class LexiaDataPointsCollection
    {
        [XmlElement("Point")]
        public List<LexiaRawDataPoint> Points
        {
            get;
            set;
        }
    }
}
