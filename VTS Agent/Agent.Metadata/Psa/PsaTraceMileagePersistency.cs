using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Agent.Common;
using Agent.Common.Data;
using VTS.Shared;

namespace Agent.Metadata.Psa
{
    internal class PsaTraceMileagePersistency
    {
        private readonly string infoElementName = 
            Decode("2prb/SlNg/6ZH8AsGP/5JQ==");//"INFO";
        private readonly string vehicleElementName = 
            Decode("fB16XmQUlHMzswDgU34NTQ==");//"VEHICLE");
        private readonly string kmAttributeName = 
            Decode("kL9Cnbbf8KPdytein5hFfA==");//"KM");
        private readonly string chapitre = 
            Decode("tFE+zk+lz2Gs/6G4TZaGmg==");//"chapitre";
        private readonly string code =
            Decode("nAe5CUCiRpTGew70ywu+UQ==");//"code";
        private readonly string kilometrage =
            Decode("/C+EJIMt+AIMPbOR+6D9Sw=="); //"kilometrage";
        private readonly string vtsagentFormat =
            Decode("9z/D3sMSrXvaR09jpgFTMA=="); //"{0}.vtsagent.md";
        private readonly string oo1 =
            Decode("uXCDTiXehKMAjsMK97bUIw=="); //"001";
        private readonly string infoOutilString =
            Decode("o2wPgzbtf4A1eJOGrwpCJg==");//"infoOutil");

        private PsaTraceMetadata md;

        public PsaTraceMileagePersistency(PsaTraceMetadata md)
        {
            if (md == null)
            {
                throw new ArgumentNullException("md");
            }
            this.md = md;
        }

        public void Persist(int newMileage)
        {
            if (md.Connector == DiagnosticSystemType.Lexia)
            {
                PersistLexiaMileage(newMileage);
                return;
            }
            if (md.Connector == DiagnosticSystemType.PP2000)
            {
                PersistPP2000Mileage(newMileage);
                return;
            }
            throw new NotImplementedException();
        }

        private void PersistLexiaMileage(int newMileage)
        {
            if (md.Subtype == PsaConnectorSubtype.Trace)
            {
                using (FileStream stream = new FileStream(
                md.SourceXmlPath, FileMode.Open))
                {
                    int currentMileage = 0; 

                    XDocument xDoc = XDocument.Load(stream);
                    IEnumerable<XElement> chapter1s = xDoc.Root.Elements(chapitre)
                        .Where(e => e.Attribute(code).Value == oo1);
                    foreach (XElement ch1 in chapter1s)
                    {
                        XElement infoOutil = ch1.Element(infoOutilString);
                        if (infoOutil != null)
                        {
                            XAttribute attr = infoOutil.Attribute(
                                kilometrage);
                            if (attr != null && !String.IsNullOrEmpty(attr.Value))
                            {
                                int result = 0;
                                Int32.TryParse(attr.Value,
                                    NumberStyles.Integer,
                                    CultureInfo.InvariantCulture, out result);
                                currentMileage = result;
                                break;
                            }
                        }
                    }
                    if (currentMileage != 0 && currentMileage >= newMileage)
                    {
                        return;
                    }
                    if (chapter1s.Count() != 0)
                    {
                        XElement infoOutil = chapter1s.First().
                            Element(infoOutilString);
                        if (infoOutil != null)
                        {
                            XAttribute attr = infoOutil.Attribute(
                                kilometrage);
                            if (attr != null)
                            {
                                attr.Value = newMileage.ToString();
                                xDoc.Root.Elements(chapitre).Where(
                                    e => e.Attribute(code).Value == oo1).
                                    FirstOrDefault().Element(infoOutilString).
                                    Attribute(kilometrage).Value = 
                                    newMileage.ToString();
                                stream.Position = 0;
                                xDoc.Save(stream);
                            }
                        }
                    }
                }
            }
            else if (md.Subtype == PsaConnectorSubtype.Graph)
            {
                string xmlFilePath = md.SourceXmlPath;
                string baseFilePath = Path.Combine(
                    Path.GetDirectoryName(xmlFilePath),
                    Path.GetFileNameWithoutExtension(xmlFilePath));
                string metadataFilePath = 
                    String.Format(vtsagentFormat, baseFilePath);
                FileStream metadataFile = 
                    File.Open(metadataFilePath, FileMode.OpenOrCreate);
                PsaMetadataPersistencyObject po =
                    new PsaMetadataPersistencyObject(metadataFile);
                po.SetMileage(newMileage);
                metadataFile.Close();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void PersistPP2000Mileage(int newMileage)
        {
            if (md.Subtype == PsaConnectorSubtype.Trace)
            {
                using (FileStream stream = new FileStream(
                md.SourceXmlPath, FileMode.Open))
                {
                    XDocument xDoc = XDocument.Load(stream);
                    XElement mileageElement =
                        xDoc.Root.Element(infoElementName).
                            Element(vehicleElementName);
                    string baseMileage = newMileage.ToString();
                    while (baseMileage.Length < 30)
                    {
                        baseMileage = String.Format("{0} ", baseMileage);
                    }
                    mileageElement.Attribute(kmAttributeName).Value =
                        baseMileage;
                    stream.Position = 0;
                    xDoc.Save(stream);
                }
            }
            else if (md.Subtype == PsaConnectorSubtype.Graph)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static string Decode(string s)
        {
            return Cipher.Decrypt(s, "Int32");
        }
    }
}
