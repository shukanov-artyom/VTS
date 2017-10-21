using System;
using Portal.Service.Service;
using VTS.Shared.DomainObjects;

namespace Portal.Assemblers
{
    public static class PsaParametersSetAssembler
    {
        public static PsaParametersSet FromDtoToDomainObject(PsaParametersSetDto source)
        {
            var target = new PsaParametersSet
                         {
                             Id = source.Id,
                             PsaTraceId = source.PsaTraceId,
                             Type = (PsaParametersSetType) source.Type,
                             EcuLabel = source.EcuLabel,
                             EcuName = source.EcuName,
                             OriginalTypeId = source.OriginalTypeId,
                             AdditionalSourceInfo = source.AdditionalSourceInfo
                         };
            foreach (PsaParameterDataDto parameter in source.Parameters)
            {
                target.Parameters.Add(PsaParameterDataAssembler.FromDtoToDomainObject(parameter));
            }
            return target;
        }
    }
}
