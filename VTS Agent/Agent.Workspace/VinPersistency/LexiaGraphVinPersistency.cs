using System;
using System.IO;
using System.Xml.Linq;
using Agent.Connector;
using Agent.Metadata.Psa;

namespace Agent.Workspace.VinPersistency
{
    internal class LexiaGraphVinPersistency : ITraceVinPersistency
    {
        private readonly PsaTraceInfo traceInfo;

        public LexiaGraphVinPersistency(PsaTraceInfo traceInfo)
        {
            if (traceInfo == null)
            {
                throw new ArgumentNullException("traceInfo");
            }
            this.traceInfo = traceInfo;
        }

        public void PersistNewVin(string vin)
        {
            string mainXml = traceInfo.Metadata.SourceXmlPath;
            WriteVinToMainXml(mainXml, vin);
            RenameFile(mainXml, vin);
            foreach (string additionalFilePath in traceInfo.Metadata.AdditionalFilePaths)
            {
                RenameFile(additionalFilePath, vin);
            }
        }

        private static void WriteVinToMainXml(string filePath, string vin)
        {
            using (FileStream stream = File.Open(
                filePath, 
                FileMode.Open, 
                FileAccess.ReadWrite, 
                FileShare.None))
            {
                XDocument doc = XDocument.Load(stream);
                XElement infSauve = doc.Root.Element("InfosSauvegarde");
                infSauve.Element("Vin").SetValue(vin);
                string nomFichierSave = infSauve.Element("NomFichierSave").Value;
                string[] split = nomFichierSave.Split(' ');
                if (split.Length != 0 && split[0].Length == 17)
                {
                    string newNom = nomFichierSave.Replace(split[0], vin);
                    infSauve.Element("NomFichierSave").SetValue(newNom);
                }
                else if (split.Length != 0)
                {
                    infSauve.Element("NomFichierSave").SetValue(
                        String.Format("{0} {1}", vin, nomFichierSave));
                }
                stream.Position = 0;
                doc.Save(stream);
            }
        }

        private void RenameFile(string fileName, string vin)
        {
            string folderName = Path.GetDirectoryName(fileName);
            string fileNameWoExt = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName);
            string[] split = fileNameWoExt.Split('&');
            string newFileName = String.Format("{0}&{1}&{2}&{3}&{4}{5}",
                split[0],
                split[1],
                vin,
                split[3],
                split[4],
                ext);
            string fullNewFilepath = Path.Combine(folderName, newFileName);
            File.Move(fileName, fullNewFilepath);
        }
    }
}
