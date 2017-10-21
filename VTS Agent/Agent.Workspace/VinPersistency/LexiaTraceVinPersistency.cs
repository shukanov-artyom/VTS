using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Agent.Common;
using Agent.Connector;
using Agent.Logging;
using Agent.Metadata.Psa;

namespace Agent.Workspace.VinPersistency
{
    internal class LexiaTraceVinPersistency : ITraceVinPersistency
    {
        private readonly PsaTraceInfo psaTraceInfo;

        public LexiaTraceVinPersistency(PsaTraceInfo psaTraceInfo)
        {
            if (psaTraceInfo == null)
            {
                throw new ArgumentNullException("psaTraceInfo");
            }
            this.psaTraceInfo = psaTraceInfo;
        }

        public void PersistNewVin(string vin)
        {
            string mainXmlFile = psaTraceInfo.Metadata.SourceXmlPath;
            if (!File.Exists(mainXmlFile))
            {
                throw new ArgumentException("File does not exist.");
            }
            XDocument doc;
            using (FileStream stream = new FileStream(mainXmlFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                doc = XDocument.Load(stream);
                ChangeVinSaisieClientAttribute(doc, vin);
                ChangeNomFichierSaveTag(doc, vin);
                ChangeChapitre3VinTag(doc, vin);
            }
            using (FileStream stream = new FileStream(mainXmlFile, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                doc.Save(stream);
            }
            ReplaceVinInTxtFiles(mainXmlFile, vin);
            RenameFiles(mainXmlFile, vin);
        }

        private void ChangeVinSaisieClientAttribute(XDocument doc, string vin)
        {
            XElement chapitre1 = GetChapitre1(doc);
            if (chapitre1 == null)
            {
                throw new VtsAgentException("Cannot find chapitre1 section to set vin");
            }
            XElement infoOutil = chapitre1.Element("infoOutil");
            if (infoOutil == null)
            {
                throw new VtsAgentException("Cannot find infoOutil section to set vin");
            }
            XAttribute vinClient = infoOutil.Attribute("VinSaisieClient");
            vinClient.SetValue(vin);
        }

        private void ChangeNomFichierSaveTag(XDocument doc, string vin)
        {
            XElement chapitre1 = GetChapitre1(doc);
            if (chapitre1 == null)
            {
                throw new VtsAgentException("Cannot find chapitre1 section to set vin");
            }
            XElement nomFichierSave = chapitre1.Element("NomFichierSave");
            if (nomFichierSave == null)
            {
                Log.Info("Could not find NomFichierSave to set vin.");
                return;
            }
            string nom = nomFichierSave.Value;
            string[] split = nom.Split(' ');
            if (split.Length == 0)
            {
                nomFichierSave.SetValue(String.Format("  {0}", vin));
                return;
            }
            string potentialVin = split[split.Length - 1];
            if (potentialVin.Length == 17)
            {
                nomFichierSave.SetValue(nom.Replace(potentialVin, vin));
                return;
            }
            string result = String.Format("{0} {1}", nom, vin);
            nomFichierSave.SetValue(result);
        }

        private void ChangeChapitre3VinTag(XDocument doc, string vin)
        {
            XElement chapitre3 = doc.Root.Elements("chapitre").FirstOrDefault(
                c => c.Attribute("code") != null &&
                     c.Attribute("code").Value.Equals("003", StringComparison.OrdinalIgnoreCase));
            if (chapitre3 == null)
            {
                Log.Warn("Was not able to change chapter 3 vin.");
                return;
            }
            XElement vinElement = chapitre3.Element("vin");
            if (vinElement == null)
            {
                Log.Warn("Unable to find VIN element within chapter 3");
                return;
            }
            vinElement.Attribute("num").SetValue(vin);
        }

        private void ReplaceVinInTxtFiles(string originalFilePath, string vin)
        {
            // TODO : Implement
            //throw new NotImplementedException();
        }

        private void RenameFiles(string originalFilePath, string vin)
        {
            // rename main file
            string fileName = Path.GetFileNameWithoutExtension(originalFilePath);
            string[] spl = fileName.Split('&');
            string newFileName = String.Format("{0}&{1}&{2}.{3}", spl[0], vin, spl[2], "xml");
            string fullNewPathName = Path.Combine(Path.GetDirectoryName(originalFilePath), newFileName);
            File.Move(originalFilePath, fullNewPathName);
            // rename additional files
            foreach (string add in psaTraceInfo.Metadata.AdditionalFilePaths)
            {
                string folder = Path.GetDirectoryName(add);
                string fileNameWoExt = Path.GetFileNameWithoutExtension(add);
                string ext = Path.GetExtension(add).Trim('.');
                string[] split = fileNameWoExt.Split('&');
                string newAddFileName = String.Format("{0}&{1}&{2}.{3}", split[0], vin, split[2], ext);
                string newFullPath = Path.Combine(folder, newAddFileName);
                File.Move(add, newFullPath);
            }
        }

        private XElement GetChapitre1(XDocument doc)
        {
            return doc.Root.Elements("chapitre").FirstOrDefault(
                e =>
                    e.Attribute("code") != null &&
                    e.Attribute("code").Value.Equals("001"));
        }
    }
}
