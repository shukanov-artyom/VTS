using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Agent.Logging;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Peugeot
{
    public class PeugeotTracePsaTraceFactory : PsaTraceFactoryBase
    {
        private readonly FileInfo file;

        public PeugeotTracePsaTraceFactory(FileInfo file)
        {
            this.file = file;
        }

        public PsaTrace Create()
        {
            PsaTrace trace = new PsaTrace();
            trace.Manufacturer = Manufacturer.Peugeot;
            try
            {
                using (FileStream stream = new FileStream(
                file.FullName, FileMode.Open))
                {
                    XDocument xDoc = XDocument.Load(stream);
                    FillGeneralTraceData(xDoc, trace);
                    foreach (PsaParametersSet set in GenerateParametersSets(xDoc))
                    {
                        trace.ParametersSets.Add(set);
                    }
                }
                return trace;
            }
            catch (XmlException)
            {
                Log.Info(String.Format("Found corrupted trace {0}, attempting to restore.", file.FullName));
                using (FileStream stream = new FileStream(
                    file.FullName, FileMode.Open))
                {
                    using (MemoryStream temp = new MemoryStream())
                    {
                        stream.CopyTo(temp);
                        using (StreamWriter writer = new StreamWriter(temp))
                        {
                            writer.WriteLine();
                            writer.WriteLine("</XMLTRACE>");
                            writer.Flush();
                            temp.Position = 0;
                            try
                            {
                                PsaTrace traceCorrected = new PsaTrace();
                                traceCorrected.Manufacturer = Manufacturer.Peugeot;
                                XDocument xDoc = XDocument.Load(temp);
                                FillGeneralTraceData(xDoc, traceCorrected);
                                foreach (PsaParametersSet set in GenerateParametersSets(xDoc))
                                {
                                    traceCorrected.ParametersSets.Add(set);
                                }
                                Log.Info("Trace restore successful.");
                                return traceCorrected;
                            }
                            catch (XmlException e)
                            {
                                Log.Error(e, "Appending closing </XMLTRACE> tag to a corrupted trace did not work.");
                            }
                        }
                    }
                }
                throw;
            }
        }

        private void FillGeneralTraceData(XDocument xDoc, PsaTrace trace)
        {
            trace.Address = xDoc.Root.Element(PeugeotTraceStrings.Info).
                Element(PeugeotTraceStrings.Workplace).
                Attribute(PeugeotTraceStrings.Address).Value.
                TrimEnd(new char[] { ' ' });
            trace.Application = String.Empty;

            string dateTimeString = xDoc.Root.Element(PeugeotTraceStrings.Info).
                Element(PeugeotTraceStrings.InfoMemo).
                Attribute(PeugeotTraceStrings.DateHour).
                Value.TrimEnd(new char[] { ' ' });
            if (!String.IsNullOrEmpty(dateTimeString))
            {
                DateTime result;
                DateTime.TryParse(dateTimeString, out result);
                trace.Date = result;
            }
            else
            {
                trace.Date = GetFirstCardDateTime(xDoc);
            }
            trace.Format = String.Empty;
            trace.Manufacturer = Manufacturer.Peugeot;
            trace.Mileage = 0;
            int resMileage = 0;
            Int32.TryParse(xDoc.Root.Element(PeugeotTraceStrings.Info).
                Element(PeugeotTraceStrings.Vehicle).
                Attribute(PeugeotTraceStrings.Km).Value.
                TrimEnd(new char[] { ' ' }), out resMileage);
            if (resMileage != 0)
            {
                trace.Mileage = resMileage;
            }
            else
            {
                trace.Mileage = GetMileageFromCommandSection(xDoc);
            }
            trace.Phone = xDoc.Root.Element(PeugeotTraceStrings.Info).
                Element(PeugeotTraceStrings.Workplace).
                Attribute(PeugeotTraceStrings.Tel).Value.
                TrimEnd(new char[] { ' ' });
            trace.PhoneChannel = String.Empty;
            trace.SavesetId = String.Empty;
            trace.ToolName = xDoc.Root.Element(PeugeotTraceStrings.Info).
                Element(PeugeotTraceStrings.Version).
                Attribute(PeugeotTraceStrings.VersionCd).Value.
                TrimEnd(new char[] { ' ' });
            trace.ToolSerialNumber =
                xDoc.Root.Element(PeugeotTraceStrings.Info).
                Element(PeugeotTraceStrings.Workplace).
                Attribute(PeugeotTraceStrings.Noserieppi).Value.
                TrimEnd(new char[] { ' ' });
            trace.VehicleModelName = xDoc.Root.Element(PeugeotTraceStrings.Info).
                Element(PeugeotTraceStrings.Vehicle).
                Attribute(PeugeotTraceStrings.Type).Value.
                TrimEnd(new char[] { ' ' });
            trace.Vin = xDoc.Root.Element(PeugeotTraceStrings.Info).
                Element(PeugeotTraceStrings.Vehicle).
                Attribute(PeugeotTraceStrings.Vin).Value.
                TrimEnd(new char[] { ' ' });
            if (String.IsNullOrEmpty(trace.Vin))
            {
                Log.Warn(String.Format("Trace {0} Info section does not contain a valid VIN code, attempting to locate in commands section.", file.FullName));
                trace.Vin = GetVinFromCommandsSection(xDoc);
            }
        }

        private IEnumerable<PsaParametersSet> 
            GenerateParametersSets(XDocument doc)
        {
            IList<PeugeotScreen> screens = new List<PeugeotScreen>();
            foreach (XElement command in doc.Root.Elements(PeugeotTraceStrings.Command))
            {
                bool isScreen = command.Element(PeugeotTraceStrings.Card).
                    Element(PeugeotTraceStrings.Param).
                    Elements(PeugeotTraceStrings.Attribut).
                    Any(e => e.Attribute(PeugeotTraceStrings.Name).
                        Value == PeugeotTraceStrings.Screentitle);
                if (isScreen)
                {
                    screens.Add(GenerateScreen(command));
                }
            }
            PeugeotScreensToPsaParametersSetConverter converter = 
                new PeugeotScreensToPsaParametersSetConverter(screens);
            return converter.Generate();
        }

        private PeugeotScreen GenerateScreen(XElement commandElement)
        {
            PeugeotScreen screen = new PeugeotScreen();
            screen.Name = commandElement.
                Element(PeugeotTraceStrings.Card).
                Element(PeugeotTraceStrings.Param).
                Elements(PeugeotTraceStrings.Attribut).First(e =>
                    e.Attribute(PeugeotTraceStrings.Name).Value ==
                    PeugeotTraceStrings.Screentitle).Value;

            IList<string> parameterIds = new List<string>();
            IList<string> values = new List<string>();
            IList<string> units = new List<string>();

            IEnumerable<XElement> attributes = commandElement.
                Element(PeugeotTraceStrings.Card).
                Element(PeugeotTraceStrings.Param).
                Elements(PeugeotTraceStrings.Attribut);
            foreach (XElement paramId in attributes.Where(
                a => a.Attribute(PeugeotTraceStrings.Name).Value ==
                     PeugeotTraceStrings.Title))
            {
                parameterIds.Add(paramId.Value);
            }
            foreach (XElement paramId in attributes.Where(
                a => a.Attribute(PeugeotTraceStrings.Name).Value ==
                     PeugeotTraceStrings.Measure))
            {
                values.Add(paramId.Value);
            }
            foreach (XElement paramId in attributes.Where(
                a => a.Attribute(PeugeotTraceStrings.Name).Value ==
                     PeugeotTraceStrings.Unit))
            {
                units.Add(paramId.Value);
            }

            if (parameterIds.Count != values.Count ||
                parameterIds.Count != units.Count)
            {
                throw new ApplicationException();
            }

            for (int i = 0; i < parameterIds.Count; i++)
            {
                PeugeotRawParameterPoint pt = new PeugeotRawParameterPoint();
                pt.ParameterName = parameterIds[i];
                pt.Value = values[i];
                pt.Units = units[i];
                screen.Points.Add(pt);
            }
            return screen;
        }

        private string GetVinFromCommandsSection(XDocument doc)
        {
            try
            {
                string result = GetValueFromCommandsSection(doc, PeugeotTraceStrings.Vin);
                if (String.IsNullOrWhiteSpace(result))
                {
                    Log.Warn("Unable to get vin from commads section");
                }
                else
                {
                    Log.Info(String.Format("Located vin in commands section: {0}", result));
                }
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed iterating through command sections.");
                return String.Empty;
            }
        }

        private int GetMileageFromCommandSection(XDocument doc)
        {
            try
            {
                string val = GetValueFromCommandsSection(doc, PeugeotTraceStrings.Km);
                if (String.IsNullOrEmpty(val))
                {
                    Log.Warn("Unable to raise Mileage from commands sction as it has an empty mileage value.");
                    return 0;
                }
                else
                {
                    int result = Int32.Parse(val);
                    if (result == 0)
                    {
                        Log.Warn("Unable to get Mileage from commands section");
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed iterating through command sections.");
                return 0;
            }
        }

        private string GetValueFromCommandsSection(XDocument doc, string attributName)
        {
            foreach (XElement element in doc.Root.Elements(PeugeotTraceStrings.Command))
            {
                if (element.HasElements)
                {
                    XElement card = element.Elements(PeugeotTraceStrings.Card).FirstOrDefault();
                    if (card != null)
                    {
                        XElement param = card.Element(PeugeotTraceStrings.Param);
                        if (param != null)
                        {
                            XElement parameter = param.Elements(PeugeotTraceStrings.Attribut).FirstOrDefault(
                                p => p.Attribute(PeugeotTraceStrings.Name).Value.Equals(attributName, StringComparison.OrdinalIgnoreCase));
                            if (parameter != null)
                            {
                                return parameter.Value;
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
