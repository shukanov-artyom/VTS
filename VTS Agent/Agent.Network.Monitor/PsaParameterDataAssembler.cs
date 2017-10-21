using System;
using System.Collections.Generic;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    /// <summary>
    /// ATTENTION - IT NOT ONLY ASSEMBLES BUT REPLACES UNITS
    /// </summary>
    public static class PsaParameterDataAssembler
    {
        public static PsaParameterDataDto FromDomainObjectToDto(PsaParameterData source)
        {
            var target = new PsaParameterDataDto();
            target.Id = source.Id;
            target.HasTimestamps = source.HasTimestamps;
            target.PsaParametersSetId = source.PsaParametersSetId;
            if (source.HasTimestamps)
            {
                List<int> timestamps = new List<int>();
                foreach (int ts in source.Timestamps)
                {
                    timestamps.Add(ts);
                }
                target.Timestamps = timestamps.ToArray();
            }
            target.OriginalTypeId = source.OriginalTypeId;
            target.Type = (int)source.Type;
            target.Units = (int)source.Units;
            List<string> values = new List<string>();
            foreach (string s in source.Values)
            {
                values.Add(s);
            }
            target.Values = values.ToArray();
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            return target;
        }

        public static PsaParameterData FromDtoToDomainObject(PsaParameterDataDto source)
        {
            var target = new PsaParameterData(source.OriginalTypeId);
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
            target.Type = (PsaParameterType)source.Type;
            target.Units = (Unit)source.Units;
            foreach (string s in source.Values)
            {
                target.Values.Add(s);
            }
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            return target;
        }
    }
}
