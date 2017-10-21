using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.GraphTypeData
{
    public class LexiaChannelHeader
    {
        [XmlAttribute("Id")]
        public int Id 
        {
            get;
            set; 
        }

        [XmlAttribute("Mnemo")]
        public string Mnemocode 
        {
            get;
            set; 
        }

        [XmlAttribute("Label")]
        public string Label 
        {
            get;
            set; 
        }

        [XmlAttribute("_Type")]
        public string DataType 
        {
            get;
            set; 
        }

        [XmlAttribute("Unit")]
        public string DataUnit 
        {
            get;
            set; 
        }
    }
}
