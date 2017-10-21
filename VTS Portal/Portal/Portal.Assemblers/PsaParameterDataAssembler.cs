using System;
using Portal.Service.Service;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Portal.Assemblers
{
    public static class PsaParameterDataAssembler
    {
        public static PsaParameterData FromDtoToDomainObject(PsaParameterDataDto source)
        {
            var target = new PsaParameterData(source.OriginalTypeId)
                         {
                             Id = source.Id,
                             HasTimestamps = source.HasTimestamps,
                             PsaParametersSetId = source.PsaParametersSetId,
                             OriginalTypeId = source.OriginalTypeId,
                             AdditionalSourceInfo = source.AdditionalSourceInfo,
                             Type = (PsaParameterType)source.Type,
                             Units = (Unit)source.Units
                         };
            if (source.HasTimestamps)
            {
                foreach (int ts in source.Timestamps)
                {
                    target.Timestamps.Add(ts);
                }
            }
            foreach (string s in source.Values)
            {
                target.Values.Add(s);
            }
            return target;
        }
    }
}
