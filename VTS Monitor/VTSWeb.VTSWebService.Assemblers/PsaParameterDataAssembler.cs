using System;
using System.Collections.ObjectModel;
using VTS.Shared;
using VTS.Shared.DomainObjects;

using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public static class PsaParameterDataAssembler
    {
        public static PsaParameterDataDto FromDomainObjectToDto(PsaParameterData source)
        {
            var target = new PsaParameterDataDto();
            target.Timestamps = new ObservableCollection<int>();
            target.Values = new ObservableCollection<string>();
            target.Id = source.Id;
            target.HasTimestamps = source.HasTimestamps;
            target.PsaParametersSetId = source.PsaParametersSetId;
            target.Type = (int) source.Type;
            target.Units = (int) source.Units;
            target.OriginalTypeId = source.OriginalTypeId;
            //target.OriginalName = source.OriginalName;
            foreach (int timestamp in source.Timestamps)
            {
                target.Timestamps.Add(timestamp);
            }
            foreach (string s in source.Values)
            {
                target.Values.Add(s);
            }
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            return target;
        }

        public static PsaParameterData FromDtoToDomainObject(PsaParameterDataDto source)
        {
            PsaParameterData target = new PsaParameterData(source.OriginalTypeId);
            target.Id = source.Id;
            target.HasTimestamps = source.HasTimestamps;
            target.PsaParametersSetId = source.PsaParametersSetId;
            target.Type = (PsaParameterType)source.Type;
            target.Units = (Unit)source.Units;
            target.OriginalTypeId = source.OriginalTypeId;
            //target.OriginalName = source.OriginalName;
            foreach (int timestamp in source.Timestamps)
            {
                target.Timestamps.Add(timestamp);
            }
            foreach (string s in source.Values)
            {
                target.Values.Add(s);
            }
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            return target;
        }
    }
}
