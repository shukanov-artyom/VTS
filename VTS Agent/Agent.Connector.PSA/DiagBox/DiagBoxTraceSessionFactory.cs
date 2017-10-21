using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Agent.Common;
using Agent.Connector.PSA.DiagBox.Xml;
using Agent.Connector.PSA.GraphTypeData;
using Agent.Logging;
using VTS.Shared;

namespace Agent.Connector.PSA.DiagBox
{
    internal static class DiagBoxTraceSessionFactory
    {
        public static bool IsMainTraceSessionFile(string xmlFilePath)
        {
            using (FileStream stream = new FileStream(xmlFilePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            {
                return XDocument.Load(stream).Element("TraceSession") != null;
            }
        }

        public static DiagBoxTraceSession CreateSession(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (!File.Exists(filePath))
            {
                throw new VtsAgentException("DiagBox trace session file does not exist.");
            }
            if (!IsMainTraceSessionFile(filePath))
            {
                throw new VtsAgentException("Provided file is not Main Trace session file.");
            }
            DiagBoxTraceSession result = new DiagBoxTraceSession();
            using (FileStream stream = new FileStream(filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TraceSessionXml));
                TraceSessionXml sessionXml = serializer.Deserialize(stream) as TraceSessionXml;
                if (sessionXml == null)
                {
                    throw new VtsAgentException("Failed to parse DiagBox session correctly.");
                }
                result.TraceSessionMainFilePath = filePath;
                result.Vin = sessionXml.Vin;
                result.Date = DateTime.Parse(sessionXml.Date);
                result.Manufacturer = ParseManufacturer(sessionXml.Trademark);
                result.TraceId = sessionXml.TraceId;
                result.VehicleArchiName = sessionXml.VehicleArchiName;
                result.VehicleName = sessionXml.VehicleName;
                result.Version = sessionXml.Version;
            }
            string traceUserDataFilePathName = GetTraceUserDataFilePathName(filePath);
            if (!String.IsNullOrWhiteSpace(traceUserDataFilePathName))
            {
                result.AdditionalFilePaths.Add(traceUserDataFilePathName);
                result.Mileage = DetermineMileage(traceUserDataFilePathName);
            }
            // get all channels
            string sessionId = Path.GetFileNameWithoutExtension(filePath);
            string sessionFolder = Path.GetDirectoryName(filePath);
            string searchPattern = String.Format("{0}.MPM.*.xml", sessionId);
            foreach (string file in Directory.GetFiles(sessionFolder, searchPattern))
            {
                result.AdditionalFilePaths.Add(file);
                LexiaGraphSessionRawData session = CreateSessionFromFile(file);
                if (session != null)
                {
                    result.Data.Add(session);
                }
            }
            return result;
        }

        private static Manufacturer ParseManufacturer(string trademark)
        {
            switch (trademark.ToUpper())
            {
                case "CITROEN":
                    return Manufacturer.Citroen;
                case "PEUGEOT":
                    return Manufacturer.Peugeot;
                default:
                    throw new VtsAgentException(String.
                        Format("Cannot recognize manufacturer: {0}", trademark));
            }
        }

        private static string GetTraceUserDataFilePathName(string mainXmlFile)
        {
            string sessionId = Path.GetFileNameWithoutExtension(mainXmlFile);
            string sessionFolder = Path.GetDirectoryName(mainXmlFile);
            string searchPattern = String.Format("{0}.TU.*.xml", sessionId);
            string traceUserFile = Directory.GetFiles(sessionFolder, searchPattern).FirstOrDefault();
            if (traceUserFile == null)
            {
                Log.Warn("Cannot deteramine Mileage for {0} DiagBox trace session.");
                return String.Empty;
            }
            return traceUserFile;
        }

        private static LexiaGraphSessionRawData CreateSessionFromFile(string file)
        {
            try
            {
                using (FileStream stream = new FileStream(
                    file,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite))
                {
                    XmlSerializer srl = new XmlSerializer(typeof(LexiaGraphSessionRawData));
                    LexiaGraphSessionRawData session = null;
                    try
                    {
                        session = srl.Deserialize(stream) as LexiaGraphSessionRawData;
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, String.Format("Cannot deserialize session by {0}", file));
                    }
                    return session;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, String.Format("Unable to process DiagBox trace additional filepath {0}", file));
            }
            return null;
        }

        private static int DetermineMileage(string tu)
        {
            int result = 0;
            bool serializerSuccess = false;
            if (!String.IsNullOrEmpty(tu))
            {
                try
                {
                    using (FileStream stream = new FileStream(tu,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(TraceUserXml));
                        var traceUser = serializer.Deserialize(stream) as TraceUserXml;
                        result = traceUser.InfoVeh.MileageKm;
                        serializerSuccess = true;
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, String.Format("Unable to determine Mileage for Trace User {0}", tu));
                }
                if (!serializerSuccess)
                {
                    try
                    {
                        using (FileStream stream = new FileStream(tu,
                            FileMode.Open,
                            FileAccess.Read,
                            FileShare.ReadWrite))
                        {
                            XDocument doc = XDocument.Load(stream);
                            string mileage = doc.Root.Element("InfoVEH").Element("KM").Value;
                            if (!String.IsNullOrWhiteSpace(mileage))
                            {
                                int res;
                                if (Int32.TryParse(mileage, out res))
                                {
                                    Log.Info("Got mileage from InfoVEH section.");
                                    return res;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, "Second attempt to get mileage is unsuccessful.");
                    }
                }
            }
            return result;
        }
    }
}