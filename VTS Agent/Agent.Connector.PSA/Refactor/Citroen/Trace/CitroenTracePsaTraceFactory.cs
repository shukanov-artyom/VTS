using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Agent.Connector.PSA.Citroen;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    internal class CitroenTracePsaTraceFactory : PsaTraceFactoryBase
    {
        private string traceFile;
        private IList<string> relatedFiles = new List<string>();
        //
        private readonly string regexPattern = @"(?<DateTime>.*)&(?<Vin>.*)&.xml";
        private readonly string fileDoesNotExistText = "File does not exist!";
        //
        private readonly string traceFileText = "traceFile";
        private readonly string yyyyMMddHHmmss = "yyyyMMddHHmmss";
        //
        private readonly string chapitre = "chapitre";
        //code
        private readonly string code = "code";
        //infoOutil
        private readonly string infoOutilText = "infoOutil";
        private readonly string oo1 = "001";
        //
        private readonly string NumMiseJrCd = "NumMiseJrCd";
        //NumSerie
        private readonly string NumSerie = "NumSerie";
        //kilometrage
        private readonly string kilometrage = "kilometrage";
        private readonly string DateTimeString = "DateTime";

        public CitroenTracePsaTraceFactory(string traceFile)
        {
            if (String.IsNullOrEmpty(traceFile))
            {
                throw new ArgumentNullException(traceFileText);
            }
            if (!File.Exists(traceFile))
            {
                throw new Exception(fileDoesNotExistText);
            }
            this.traceFile = traceFile;

            relatedFiles.Add(traceFile);
            LexiaAdditionalFilePathSearcher searcher =
                new LexiaAdditionalFilePathSearcher(traceFile);
            foreach (string relatedFile in searcher.Search())
            {
                if (!relatedFiles.Contains(relatedFile))
                {
                    relatedFiles.Add(relatedFile);
                }
            }
        }

        public PsaTrace Create()
        {
            PsaTrace trace = new PsaTrace();
            GetGeneralTraceProperties(trace);
            foreach (PsaParametersSet set in LexiaScreensToPsaParametersSetConverter.Convert(
                LexiaScreensExtractor.GetScreens(traceFile)))
            {
                trace.ParametersSets.Add(set);
            }
            UnrecognizedDataKeeper.AnalyseTrace(trace, relatedFiles);
            return trace;
        }

        private void GetGeneralTraceProperties(PsaTrace trace)
        {
            PsaTraceVinExtractor vinExtractor = 
                new PsaTraceVinExtractor(traceFile);
            trace.Vin = vinExtractor.Get();
            trace.Date = GetDateTime();
            //trace.Address = GetAddress(); curse
            trace.ToolName = GetToolName();
            trace.ToolSerialNumber = GetToolSerialNumber();
            trace.Mileage = GetMileage();
            //trace.Application = GetApplication();
            PsaTraceVehicleNameExtractor nameExtractor = 
                new PsaTraceVehicleNameExtractor(traceFile);
            trace.VehicleModelName = nameExtractor.Get();
        }

        private DateTime GetDateTime()
        {
            FileInfo fi = new FileInfo(traceFile);
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(fi.Name);
            string parsedOff = String.Empty;
            if (match.Success)
            {
                parsedOff = match.Groups[DateTimeString].Value;
            }
            else
            {
                parsedOff = fi.Name.Substring(0, 14);
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime result = DateTime.
                ParseExact(parsedOff, yyyyMMddHHmmss, provider);
            if (result == DateTime.MinValue)
            {
                XDocument doc = XDocument.Load(traceFile);
                result = GetFirstCardDateTime(doc);
            }
            return result;
        }

        private string GetToolName()
        {
            using (FileStream stream = new FileStream(
                traceFile, FileMode.Open))
            {
                XDocument xDoc = XDocument.Load(stream);
                XElement chapter1 = xDoc.Root.Elements(chapitre).
                    Where(e => e.Attribute(code).Value == oo1).
                    FirstOrDefault();
                string toolName = chapter1.Element(infoOutilText)
                    .Attribute(NumMiseJrCd).Value;
                return toolName;
            }
        }

        private string GetToolSerialNumber()
        {
            using (FileStream stream = new FileStream(
                traceFile, FileMode.Open))
            {
                XDocument xDoc = XDocument.Load(stream);
                XElement chapter1 = xDoc.Root.Elements(chapitre).
                    Where(e => e.Attribute(code).Value == oo1).
                    FirstOrDefault();
                string toolName = chapter1.Element(infoOutilText)
                    .Attribute(NumSerie).Value;
                return toolName;
            }
        }

        private int GetMileage()
        {
            using (FileStream stream = new FileStream(
                traceFile, FileMode.Open))
            {
                XDocument xDoc = XDocument.Load(stream);
                IEnumerable<XElement> chapter1s = xDoc.Root.Elements(chapitre)
                    .Where(e => e.Attribute(code).Value == oo1);
                foreach (XElement ch1 in chapter1s)
                {
                    XElement infoOutil = ch1.Element(infoOutilText);
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
                            return result;
                        }
                    }
                }
            }
            return 0;
        }
    }
}
