using System;
using System.Collections.Generic;
using System.Globalization;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using PsaParameterDataEntity = VTSWebService.DataAccess.PsaParameterData;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class PsaParameterDataAssembler
    {
        public static PsaParameterDataDto FromDomainObjectToDto(PsaParameterData source)
        {
            PsaParameterDataDto target = new PsaParameterDataDto();
            target.Values = new List<string>();
            target.Timestamps = new List<int>();
            target.Id = source.Id;
            target.HasTimestamps = source.HasTimestamps;
            target.PsaParametersSetId = source.PsaParametersSetId;
            if (source.HasTimestamps)
            {
                foreach (int ts in source.Timestamps)
                {
                    target.Timestamps.Add(ts);
                }
            }
            target.OriginalTypeId = source.OriginalTypeId;
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            target.Type = (int)source.Type;
            target.Units = (int)source.Units;
            foreach (string s in source.Values)
            {
                target.Values.Add(s);
            }
            return target;
        }

        public static PsaParameterData FromEntityToDomainObject(PsaParameterDataEntity source)
        {
            PsaParameterData target = new PsaParameterData(source.OriginalTypeId);
            target.Id = source.Id;
            target.HasTimestamps = source.HasTimestamps;
            target.PsaParametersSetId = source.PsaParametersSetEntityId;
            if (source.HasTimestamps)
            {
                foreach (int ts in TimestampsFromStringToList(source.Timestamps))
                {
                    target.Timestamps.Add(ts);
                }
            }
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            target.Type = (PsaParameterType)source.Type;
            target.Units = (Unit)source.Units;
            foreach (string s in ValuesFromStringToList(source.Values))
            {
                target.Values.Add(s);
            }
            return target;
        }

        public static PsaParameterDataEntity FromDtoToEntity(PsaParameterDataDto source)
        {
            PsaParameterDataEntity target = new PsaParameterDataEntity();
            target.Id = source.Id;
            target.HasTimestamps = source.HasTimestamps;
            target.PsaParametersSetEntityId = source.PsaParametersSetId;
            if (source.HasTimestamps)
            {
                target.Timestamps = TimestampsFromListToString(source.Timestamps);
            }
            target.OriginalTypeId = source.OriginalTypeId;
            target.Type = source.Type;
            target.Units = source.Units;
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            target.Values = ValuesFromListToString(source.Values);
            return target;
        }

        public static PsaParameterDataDto FromEntityToDto(PsaParameterDataEntity source)
        {
            PsaParameterDataDto target = new PsaParameterDataDto();
            target.Values = new List<string>();
            target.Timestamps = new List<int>();
            target.Id = source.Id;
            target.HasTimestamps = source.HasTimestamps;
            target.PsaParametersSetId = source.PsaParametersSetEntityId;
            if (source.HasTimestamps)
            {
                foreach (int ts in TimestampsFromStringToList(source.Timestamps))
                {
                    target.Timestamps.Add(ts);
                }
            }
            target.OriginalTypeId = source.OriginalTypeId;
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            target.Type = source.Type;
            target.Units = source.Units;
            foreach (string s in ValuesFromStringToList(source.Values))
            {
                target.Values.Add(s);
            }
            return target;
        }

        private static string ValuesFromListToString(List<string> values)
        {
            string val = String.Empty;
            foreach (string s in values)
            {
                if (String.IsNullOrEmpty(val))
                {
                    val = s;
                }
                else
                {
                    val = String.Format("{0};{1}", val, s);
                }
            }
            return val;
        }

        private static List<string> ValuesFromStringToList(string values)
        {
            List<string> result = new List<string>();
            foreach (string val in values.Split(';'))
            {
                result.Add(val);
            }
            return result;
        }

        private static string TimestampsFromListToString(List<int> timestamps)
        {
            string tst = String.Empty;
            foreach (int i in timestamps)
            {
                if (String.IsNullOrEmpty(tst))
                {
                    tst = i.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    tst = String.Format("{0};{1}", tst,
                        i.ToString(CultureInfo.InvariantCulture));
                }
            }
            return tst;
        }

        private static List<int> TimestampsFromStringToList(string timestamps)
        {
            List<int> result = new List<int>();
            foreach (string val in timestamps.Split(';'))
            {
                int converted = 0;
                Int32.TryParse(val, NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out converted);
                result.Add(converted);
            }
            return result;
        }
    }
}
