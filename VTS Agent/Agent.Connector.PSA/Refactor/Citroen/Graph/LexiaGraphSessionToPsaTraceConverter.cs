using System;
using System.Collections.Generic;
using System.Globalization;
using Agent.Connector.PSA.GraphTypeData;
using Agent.Connector.PSA.Refactor.Citroen.Trace;
using Agent.Connector.PSA.Refactor.Common;
using Agent.Connector.PSA.TypeConversion;
using Agent.Logging;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Citroen.Graph
{
    internal static class LexiaGraphSessionToPsaTraceConverter
    {
        private static readonly string dateTimeFormat = "yyyy:MM:dd HH:mm:ss";

        public static PsaTrace Convert(LexiaGraphSessionRawData data) 
        {
            if (data == null)
            {
                Log.Warn("Got null data into converter");
                return null;
            }
            PsaTrace result = new PsaTrace();
            result.Manufacturer = Manufacturer.Citroen;
            if (data.SessionInformation != null)
            {
                result.Vin = data.SessionInformation.Summary.Split(' ')[0];
                string datetime = String.Format("{0} {1}",
                       data.SessionInformation.Date,
                       data.SessionInformation.Time);
                result.Date = DateTime.ParseExact(datetime,
                dateTimeFormat, CultureInfo.InvariantCulture);
                if (data.SessionInformation.Vehicle.Contains("@"))
                {
                    result.VehicleModelName = 
                        KnownVehicleNames.Get(data.SessionInformation.Vehicle);
                }
                else
                {
                    result.VehicleModelName = data.SessionInformation.Vehicle;
                }
            }
            result.Mileage = data.Mileage;
            PsaParametersSet set = GenerateParametersSet(data.Channels);
            set.EcuName = data.DataHeader.EcuName;
            set.EcuLabel = data.DataHeader.EcuLabel;
            result.ParametersSets.Add(set);
            return result;
        }

        private static PsaParametersSet GenerateParametersSet(
            List<LexiaChannelRawData> channels)
        {
            PsaParametersSet set = new PsaParametersSet();
            set.Type = PsaParametersSetType.Mixed; // Mixed for channel data
            foreach (LexiaChannelRawData channel in channels)
            {
                set.Parameters.Add(Convert(channel));
            }
            return set;
        }

        private static PsaParameterData Convert(LexiaChannelRawData channel)
        {
            PsaParameterData parameter = new PsaParameterData(channel.Header.Mnemocode) // Passed original type id
                                         {
                                             HasTimestamps = true, // for Graph it is true
                                             AdditionalSourceInfo =
                                                 GenerateAdditionalSourceInfo(channel.Header.Mnemocode),
                                             Type = ConvertType(channel.Header.Mnemocode),
                                             OriginalName = channel.Header.Mnemocode,
                                             Units = UnitsConverter.Convert(channel.Header.DataUnit)
                                         };
            foreach (LexiaRawDataPoint p in channel.ChannelDataPoints.Points)
            {
                int ts;
                Int32.TryParse(p.TimeStamp, NumberStyles.Integer, 
                    CultureInfo.InvariantCulture, out ts);
                parameter.Timestamps.Add(ts);
                parameter.Values.Add(p.Value);
            }
            return parameter;
        }

        private static PsaParameterType ConvertType(string code)
        {
            PsaParameterType type = DataTypeResolver.GetType(new Mnemocode(code));
            if (type == PsaParameterType.Unsupported)
            {
                Log.Warn(String.Format("Mnemocode {0} is not supported yet.", code));
            }
            return type;
        }

        private static string GenerateAdditionalSourceInfo(string originalTypeId)
        {
            // need to generate additional data: french names, additional synonymic mnemocodes etc.
            return String.Empty;
        }
    }
}
