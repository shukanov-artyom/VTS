using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.GraphTypeData
{
    [XmlRoot("ECUDataLogFile")]
    public class LexiaGraphSessionRawData
    {
        /// <summary>
        /// not XML tag, just for metadata passing through
        /// </summary>
        [XmlIgnore]
        public string SourceFileName
        {
            get;
            set;
        }

        [XmlIgnore]
        public int Mileage
        {
            get;
            set;
        }

        [XmlElement("XML_InfosGenerales")]
        public LexiaScanGeneralInfo GeneralInformation
        {
            get;
            set;
        }

        [XmlElement("InfosSauvegarde")]
        public LexiaScanMetadata SessionInformation
        {
            get;
            set;
        }

        [XmlElement("Header")]
        public LexiaScanDataHeader DataHeader
        {
            get;
            set;
        }

        [XmlElement("Channel")]
        public List<LexiaChannelRawData> Channels
        {
            get;
            set;
        }
    }
}
