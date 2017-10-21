using System;
using System.Xml.Serialization;

namespace Agent.Connector.PSA.GraphTypeData
{
    public class LexiaScanMetadata
    {
        [XmlElement("NomFichierSave")]
        public string Summary
        {
            get;
            set;
        }

        [XmlElement("Date")]
        public string Date
        {
            get;
            set;
        }

        [XmlElement("Time")]
        public string Time
        {
            get;
            set;
        }

        [XmlElement("Vehicule")]
        public string Vehicle
        {
            get;
            set;
        }

        [XmlElement("Calculateur")]
        public string Calculator
        {
            get;
            set;
        }

        [XmlElement("Vin")]
        public string Vin
        {
            get;
            set;
        }
    }
}
