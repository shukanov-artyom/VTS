using System;
using System.Collections.Generic;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public static class PsaParametersSetAssembler
    {
        public static PsaParametersSetDto FromDomainObjectToDto(PsaParametersSet source)
        {
            PsaParametersSetDto target = new PsaParametersSetDto();
            target.Id = source.Id;
            target.PsaTraceId = source.PsaTraceId;
            target.Type = (int)source.Type;
            target.EcuName = source.EcuName;
            target.EcuLabel = source.EcuLabel;
            List<PsaParameterDataDto> dtos = new List<PsaParameterDataDto>();
            foreach (PsaParameterData parameter in source.Parameters)
            {
                dtos.Add(PsaParameterDataAssembler.FromDomainObjectToDto(parameter));
            }
            target.Parameters = dtos.ToArray();
            return target;
        }

        public static PsaParametersSet FromDtoToDomainObject(PsaParametersSetDto source)
        {
            PsaParametersSet target = new PsaParametersSet();
            target.Id = source.Id;
            target.PsaTraceId = source.PsaTraceId;
            target.Type = (PsaParametersSetType)source.Type;
            target.EcuName = source.EcuName;
            target.EcuLabel = source.EcuLabel;
            foreach (PsaParameterDataDto parameter in source.Parameters)
            {
                target.Parameters.Add(PsaParameterDataAssembler.FromDtoToDomainObject(parameter));
            }
            return target;
        }
    }
}
