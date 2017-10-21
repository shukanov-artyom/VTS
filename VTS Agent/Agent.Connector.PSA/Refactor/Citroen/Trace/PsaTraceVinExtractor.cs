using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Agent.Common.Data;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    public class PsaTraceVinExtractor
    {
        private FileInfo traceFile;
        //chapitre
        private static readonly string chapitre = "chapitre";
        //code
        private static readonly string code = "code";
        //003
        private static readonly string oo3 = "003";
        //001
        private static readonly string oo1 = "001";
        //infoOutil
        private static readonly string infoOutilString = "infoOutil";
        //vin
        private static readonly string vin = Decode("VaWlrNA1/CLJo6nuRwUMpQ==");
        //Vin
        private static readonly string vinCapital = Decode("N4bUjTzvVZo0DpJ+aZt3ZA==");
        //num
        private static readonly string num = Decode("NGqYlqUWeaaJ5Us27TdlUg==");
        //VinSaisieClient
        private static readonly string vinSaisieClient = Decode("P8QsfY4ZlgLyCAmCR0EvgA==");
        //(?<DateTime>.*)&(?<Vin>.*)&.xml
        private static readonly string regexPattern = Decode("IJCR+hp43rXOPs2vMEEcVCvaQd9P7ZDv6Rgrh//SSWU=");

        public PsaTraceVinExtractor(string traceFile)
        {
            this.traceFile = new FileInfo(traceFile);
        }

        public string Get()
        {
            // Plan A - filename
            string name = traceFile.Name;
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(name);
            string result = match.Groups[vinCapital].Value;
            if (!String.IsNullOrEmpty(result))
            {
                return result;
            }

            // plan b - chapter1
            using (FileStream stream = new FileStream(
                traceFile.FullName, FileMode.Open))
            {
                XDocument xDoc = XDocument.Load(stream);
                IEnumerable<XElement> chapter1s = xDoc.Root.Elements(chapitre)
                    .Where(e => e.Attribute(code).Value == oo1);
                foreach (XElement ch1 in chapter1s)
                {
                    XElement infoOutil = ch1.Element(infoOutilString);
                    if (infoOutil != null)
                    {
                        XAttribute attr = infoOutil.Attribute(vinSaisieClient);
                        if (attr != null && !String.IsNullOrEmpty(attr.Value))
                        {
                            return attr.Value;
                        }
                    }
                }
            }

            // plan c - chapter3
            using (FileStream stream = new FileStream(
                traceFile.FullName, FileMode.Open))
            {
                XDocument xDoc = XDocument.Load(stream);
                IEnumerable<XElement> chapter3s = xDoc.Root.Elements(chapitre)
                    .Where(e => e.Attribute(code).Value == oo3);
                foreach (XElement chapter3 in chapter3s)
                {
                    XElement vinelement = chapter3.Element(vin);
                    if (vinelement != null)
                    {
                        XAttribute vinAttr = vinelement.Attribute(num);
                        if (vinAttr != null)
                        {
                            if (!String.IsNullOrEmpty(vinAttr.Value))
                            {
                                return vinAttr.Value;
                            }
                        }
                    }
                }
            }
            return String.Empty;
        }

        private static string Decode(string s)
        {
            return Cipher.Decrypt(s, "System.FileInfo");
        }
    }
}
