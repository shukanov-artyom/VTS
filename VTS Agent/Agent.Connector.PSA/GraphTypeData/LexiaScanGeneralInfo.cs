using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.GraphTypeData
{
    public class LexiaScanGeneralInfo
    {
        [XmlElement("application")]
        public string Application
        {
            get;
            set;
        }

        [XmlElement("formatSortie")]
        public string Format
        {
            get;
            set;
        }

        [XmlElement("typeFichier")]
        public string Type
        {
            get;
            set;
        }

        [XmlElement("version")]
        public string Version
        {
            get;
            set;
        }
    }
}
