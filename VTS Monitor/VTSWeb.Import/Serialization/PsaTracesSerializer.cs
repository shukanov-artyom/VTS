using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace VTSWeb.Import.Serialization
{
    public class PsaTracesSerializer
    {
        private IEnumerable<PsaTrace> source;

        private const string IdName = "Id";
        private const string PsaDatasetIdName = "PsaDatasetId";
        private const string RootName = "Root";
        private const string VersionName = "Version";
        private const string HeaderName = "Header";
        private const string DateName = "Date";
        private const string EnvHash = "EnvironmentHash";
        private const string GuidName = "Guid";
        private const string TraceName = "Trace";
        private const string PsaTracesName = "PsaTraces";
        private const string ApplicationName = "Application";
        private const string FormatName = "Format";
        private const string PhoneName = "Phone";
        private const string ParameterSetsName = "ParameterSets";
        private const string PhoneChannelName = "PhoneChannel";
        private const string VinName = "Vin";
        private const string AddressName = "Address";
        private const string ToolSerialNumberName = "ToolSerialNumber";
        private const string ToolNameName = "ToolName";
        private const string SavesetIdName = "SavesetId";
        private const string VehicleModelNameName = "VehicleModelName";
        private const string MileageName = "Mileage";
        private const string ManufacturerName = "Manufacturer";
        private const string SourceParamSetIdName = "SourceParamSetId";
        private const string PsaTraceIdName = "PsaTraceId";
        private const string TypeName = "Type";
        private const string ParametersName = "Parameters";
        private const string ParameterName = "Parameter";
        private const string HasTimestampsName = "HasTimestamps";
        private const string ParametersSetIdName = "ParametersSetId";
        private const string UnitsName = "Units";
        private const string ValuesName = "Values";
        private const string ValueName = "Value";
        private const string TimestampsName = "Timestamps";
        private const string TimestampName = "Timestamp";
        private const string ParametersSetName = "ParametersSet";
        private const string OriginalTypeId = "OriginalTypeId";
        private const string OriginalName = "OriginalName";

        public PsaTracesSerializer(IEnumerable<PsaTrace> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            this.source = source;
        }

        public PsaTracesSerializer()
        {
        }

        public void Serialize(Stream stream)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement(RootName);
            AddHeader(root);
            AddTraces(root);
            doc.Add(root);
            doc.Save(stream);
            stream.Position = 0;
        }

        public PortableData Deserialize(Stream stream)
        {
            PortableData result = new PortableData();
            XDocument doc = XDocument.Load(stream);
            XElement root = doc.Root;
            XElement header = root.Element(HeaderName);

            DateTime dt;
            DateTime.TryParse(header.Element(DateName).Value, 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            result.Date = dt;
            result.Guid = Guid.Parse(header.Element(GuidName).Value);
            result.Version = new Version(header.Element(VersionName).Value);

            ReadTraces(root, result);
            return result;
        }

        /// <summary>
        /// Deserialization
        /// </summary>
        /// <param name="root"></param>
        /// <param name="result"></param>
        private void ReadTraces(XElement root, PortableData result)
        {
            IEnumerable<XElement> traceElements =
                root.Element(PsaTracesName).Elements(TraceName);
            foreach (XElement traceElement in traceElements)
            {
                PsaTrace trace = ReadTraceElement(traceElement);
                result.PsaTraces.Add(trace);
            }
        }

        /// <summary>
        /// Deserialization
        /// </summary>
        /// <param name="traceElement"></param>
        /// <returns></returns>
        private PsaTrace ReadTraceElement(XElement traceElement)
        {
            PsaTrace result = new PsaTrace();

            // on resources
            XElement id = traceElement.Element(IdName);
            XElement psaDatasetId = traceElement.Element(PsaDatasetIdName);

            // not on resources yet
            XElement date = traceElement.Element(DateName);
            XElement application = traceElement.Element(ApplicationName);
            XElement format = traceElement.Element(FormatName);
            XElement phone = traceElement.Element(PhoneName);
            XElement phoneChannel = traceElement.Element(PhoneChannelName);
            XElement vin = traceElement.Element(VinName);
            XElement address = traceElement.Element(AddressName);
            XElement toolSn = traceElement.Element(ToolSerialNumberName);
            XElement toolName = traceElement.Element(ToolNameName);
            XElement ssid = traceElement.Element(SavesetIdName);
            XElement vehModelName = traceElement.Element(VehicleModelNameName);
            XElement mileage = traceElement.Element(MileageName);
            XElement manufacturer = traceElement.Element(ManufacturerName);

            result.Id = long.Parse(id.Value);
            result.PsaDatasetId = long.Parse(psaDatasetId.Value);
            DateTime dt;
            DateTime.TryParse(date.Value, 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            result.Date = dt;
            result.Application = application.Value;
            result.Format = format.Value;
            result.Phone = phone.Value;
            result.PhoneChannel = phoneChannel.Value;
            result.Vin = vin.Value;
            result.Address = address.Value;
            result.ToolSerialNumber = toolSn.Value;
            result.ToolName = toolName.Value;
            result.SavesetId = ssid.Value;
            result.VehicleModelName = vehModelName.Value;
            result.Mileage = int.Parse(mileage.Value);
            result.Manufacturer = (Manufacturer) (Int32.
                Parse(manufacturer.Value));

            XElement parameterSetsElement =
                traceElement.Element(ParameterSetsName);
            IEnumerable<XElement> parameterSetElements =
                parameterSetsElement.Elements(ParametersSetName);
            foreach (XElement paramSetElement in parameterSetElements)
            {
                PsaParametersSet set = new PsaParametersSet();
                DeserializeParametersSet(paramSetElement, set);
                result.ParametersSets.Add(set);
            }

            return result;
        }

        /// <summary>
        /// Deserialization
        /// </summary>
        /// <param name="paramSetElement"></param>
        /// <param name="set"></param>
        private void DeserializeParametersSet(XElement paramSetElement,
            PsaParametersSet set)
        {

            set.PsaTraceId = long.Parse(paramSetElement.
                Element(PsaTraceIdName).Value);
            set.Type = (PsaParametersSetType) Int32.Parse(paramSetElement.Element(TypeName).Value);

            XElement parametersElement =
                paramSetElement.Element(ParametersName);
            IEnumerable<XElement> parameterElements =
                parametersElement.Elements(ParameterName);
            foreach (XElement element in parameterElements)
            {
                PsaParameterData data = DeserializeParameterData(element);
                set.Parameters.Add(data);
            }
        }

        /// <summary>
        /// Deserialization
        /// </summary>
        /// <param name="parameterElement"></param>
        private PsaParameterData DeserializeParameterData(XElement parameterElement)
        {
            //mgr.GetString(SourceParamSetIdName);
            var data = new PsaParameterData(parameterElement.Element(OriginalTypeId).Value);
            data.Id = long.Parse(
                parameterElement.Element(IdName).Value);
            data.HasTimestamps = Boolean.Parse(
                parameterElement.Element(HasTimestampsName).Value);
            data.PsaParametersSetId = long.Parse(
                parameterElement.Element(ParametersSetIdName).Value);
            data.Type = (PsaParameterType)
                        Int32.Parse(parameterElement.Element(TypeName).Value);
            data.Units = (Unit)
                         Int32.Parse(parameterElement.Element(UnitsName).Value);
            data.OriginalName = parameterElement.Element(OriginalName).Value;
            XElement valuesElement = parameterElement.Element(ValuesName);
            foreach (XElement valueElement in valuesElement.Elements(ValueName))
            {
                data.Values.Add(valueElement.Value);
            }

            if (data.HasTimestamps)
            {
                XElement timestampsElement =
                    parameterElement.Element(TimestampsName);
                foreach (XElement timestampElement in
                    timestampsElement.Elements(TimestampName))
                {
                    data.Timestamps.Add(Int32.Parse(timestampElement.Value));
                }
            }
            return data;
        }

        /// <summary>
        /// Serialization
        /// </summary>
        /// <param name="root"></param>
        private void AddHeader(XElement root)
        {
            XElement header = new XElement(HeaderName);

            // date
            XElement date = new XElement(DateName);
            date.Value = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            header.Add(date);

            // env hash
            XElement envHash = new XElement(EnvHash);
            envHash.Value = String.Empty;
            header.Add(envHash);

            // guid
            XElement guid = new XElement(GuidName);
            guid.Value = Guid.NewGuid().ToString();
            header.Add(guid);

            // version
            XElement version = new XElement(VersionName);
            version.Value = "0.0.0.1";
            header.Add(version);

            root.Add(header);
        }

        /// <summary>
        /// Serialization
        /// </summary>
        /// <param name="root"></param>
        private void AddTraces(XElement root)
        {
            XElement psaTraceSection = new XElement(PsaTracesName);
            foreach (PsaTrace trace in source)
            {
                XElement traceElement = new XElement(TraceName);
                FillTrace(traceElement, trace);
                psaTraceSection.Add(traceElement);
            }
            root.Add(psaTraceSection);
        }

        /// <summary>
        /// Serialization
        /// </summary>
        /// <param name="traceElement"></param>
        /// <param name="sourceTrace"></param>
        private void FillTrace(XElement traceElement, PsaTrace sourceTrace)
        {
            XElement id = new XElement(IdName);
            id.Value = sourceTrace.Id.ToString(CultureInfo.InvariantCulture);
            traceElement.Add(id);

            XElement psaDatasetId = new XElement(PsaDatasetIdName);
            psaDatasetId.Value =
                sourceTrace.PsaDatasetId.
                    ToString(CultureInfo.InvariantCulture);
            traceElement.Add(psaDatasetId);

            XElement date = new XElement(DateName);
            date.Value = sourceTrace.Date.
                ToString(CultureInfo.InvariantCulture);
            traceElement.Add(date);

            XElement application = new XElement(ApplicationName);
            application.Value = sourceTrace.Application;
            traceElement.Add(application);

            XElement format = new XElement(FormatName);
            format.Value = sourceTrace.Format ?? String.Empty;
            traceElement.Add(format);

            XElement phone = new XElement(PhoneName);
            phone.Value = sourceTrace.Phone ?? String.Empty;
            traceElement.Add(phone);

            XElement phoneChannel = new XElement(PhoneChannelName);
            phoneChannel.Value = sourceTrace.PhoneChannel ?? String.Empty;
            traceElement.Add(phoneChannel);

            XElement vin = new XElement(VinName);
            vin.Value = sourceTrace.Vin ?? String.Empty;
            traceElement.Add(vin);

            XElement address = new XElement(AddressName);
            address.Value = sourceTrace.Address ?? String.Empty;
            traceElement.Add(address);

            XElement toolSn = new XElement(ToolSerialNumberName);
            toolSn.Value = sourceTrace.ToolSerialNumber ?? String.Empty;
            traceElement.Add(toolSn);

            XElement toolName = new XElement(ToolNameName);
            toolName.Value = sourceTrace.ToolName ?? String.Empty;
            traceElement.Add(toolName);

            XElement ssid = new XElement(SavesetIdName);
            ssid.Value = sourceTrace.SavesetId ?? String.Empty;
            traceElement.Add(ssid);

            XElement vehModelName = new XElement(VehicleModelNameName);
            vehModelName.Value = sourceTrace.VehicleModelName ?? String.Empty;
            traceElement.Add(vehModelName);

            XElement mileage = new XElement(MileageName);
            mileage.Value = sourceTrace.Mileage.ToString(
                CultureInfo.InvariantCulture);
            traceElement.Add(mileage);

            XElement manufacturer = new XElement(ManufacturerName);
            manufacturer.Value = ((int) sourceTrace.Manufacturer).ToString(
                CultureInfo.InvariantCulture);
            traceElement.Add(manufacturer);

            XElement parameterSets = new XElement(ParameterSetsName);
            foreach (PsaParametersSet set in sourceTrace.ParametersSets)
            {
                XElement parametersSet = new XElement(ParametersSetName);
                FillParametersSet(parametersSet, set);
                parameterSets.Add(parametersSet);
            }
            traceElement.Add(parameterSets);
        }

        /// <summary>
        /// Serialization
        /// </summary>
        /// <param name="set"></param>
        /// <param name="sourceParamSet"></param>
        private void FillParametersSet(XElement set,
                                       PsaParametersSet sourceParamSet)
        {
            XElement sourceParamSetId = new XElement(SourceParamSetIdName);
            sourceParamSetId.Value = sourceParamSet.Id.ToString();
            set.Add(sourceParamSetId);

            XElement psaTraceId = new XElement(PsaTraceIdName);
            psaTraceId.Value = sourceParamSet.PsaTraceId.ToString();
            set.Add(psaTraceId);

            XElement type = new XElement(TypeName);
            type.Value = ((int) sourceParamSet.Type).ToString();
            set.Add(type);

            XElement parameters = new XElement(ParametersName);
            foreach (PsaParameterData data in sourceParamSet.Parameters)
            {
                XElement parameterElement = new XElement(ParameterName);

                XElement hasTimestampsElement = new XElement(HasTimestampsName);
                hasTimestampsElement.Value = data.HasTimestamps.ToString();
                parameterElement.Add(hasTimestampsElement);

                XElement paramIdElement = new XElement(IdName);
                paramIdElement.Value = data.Id.ToString();
                parameterElement.Add(paramIdElement);

                XElement parameterSetIdElement =
                    new XElement(ParametersSetIdName);
                parameterSetIdElement.Value =
                    data.PsaParametersSetId.ToString();
                parameterElement.Add(parameterSetIdElement);

                XElement typeElement = new XElement(TypeName);
                typeElement.Value = ((int) data.Type).ToString();
                parameterElement.Add(typeElement);

                XElement unitsElement = new XElement(UnitsName);
                unitsElement.Value = ((int) data.Units).ToString();
                parameterElement.Add(unitsElement);

                XElement valuesElement = new XElement(ValuesName);
                foreach (string val in data.Values)
                {
                    XElement valElement = new XElement(ValueName);
                    valElement.Value = val;
                    valuesElement.Add(valElement);
                }
                parameterElement.Add(valuesElement);

                if (data.HasTimestamps)
                {
                    XElement timestampsElement = new XElement(TimestampsName);
                    foreach (int ts in data.Timestamps)
                    {
                        XElement tsElement = new XElement(TimestampName);
                        tsElement.Value = ts.ToString();
                        timestampsElement.Add(tsElement);
                    }
                    parameterElement.Add(timestampsElement);
                }
                parameters.Add(parameterElement);
            }
            set.Add(parameters);
        }
    }
}
