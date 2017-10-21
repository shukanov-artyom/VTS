using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using PsaParametersSetEntity = VTSWebService.DataAccess.PsaParametersSet;
using PsaParameterDataEntity = VTSWebService.DataAccess.PsaParameterData;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class PsaParametersSetAssembler
    {
        public static PsaParametersSetDto FromDomainObjectToDto(PsaParametersSet source)
        {
            PsaParametersSetDto target = new PsaParametersSetDto();
            target.Parameters = new List<PsaParameterDataDto>();
            target.Id = source.Id;
            target.PsaTraceId = source.PsaTraceId;
            target.Type = (int)source.Type;
            target.EcuLabel = source.EcuLabel;
            target.EcuName = source.EcuName;
            target.OriginalTypeId = source.OriginalTypeId;
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            foreach (PsaParameterData parameter in source.Parameters)
            {
                target.Parameters.Add(PsaParameterDataAssembler.FromDomainObjectToDto(parameter));
            }
            return target;
        }

        public static PsaParametersSet FromEntityToDomainObject(PsaParametersSetEntity source)
        {
            PsaParametersSet target = new PsaParametersSet();
            target.Id = source.Id;
            target.PsaTraceId = source.PsaTraceEntityId;
            target.Type = (PsaParametersSetType)source.Type;
            target.EcuLabel = source.EcuLabel;
            target.EcuName = source.EcuName;
            target.OriginalTypeId = source.OriginalTypeId;
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            foreach (PsaParameterDataEntity parameter in source.PsaParameterData)
            {
                target.Parameters.Add(PsaParameterDataAssembler.FromEntityToDomainObject(parameter));
            }
            return target;
        }

        public static PsaParametersSetEntity FromDtoToEntity(PsaParametersSetDto source)
        {
            PsaParametersSetEntity target = new PsaParametersSetEntity();
            target.Id = source.Id;
            target.PsaTraceEntityId = source.PsaTraceId;
            target.Type = source.Type;
            target.EcuLabel = source.EcuLabel;
            target.EcuName = source.EcuName;
            target.OriginalTypeId = source.OriginalTypeId;
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            foreach (PsaParameterDataDto parameter in source.Parameters)
            {
                target.PsaParameterData.Add(PsaParameterDataAssembler.FromDtoToEntity(parameter));
            }
            return target;
        }

        public static PsaParametersSetDto FromEntityToDto(PsaParametersSetEntity source)
        {
            PsaParametersSetDto target = new PsaParametersSetDto();
            target.Parameters = new List<PsaParameterDataDto>();
            target.Id = source.Id;
            target.PsaTraceId = source.PsaTraceEntityId;
            target.Type = source.Type;
            target.EcuLabel = source.EcuLabel;
            target.EcuName = source.EcuName;
            target.OriginalTypeId = source.OriginalTypeId;
            target.AdditionalSourceInfo = source.AdditionalSourceInfo;
            foreach (PsaParameterDataEntity parameter in source.PsaParameterData)
            {
                target.Parameters.Add(PsaParameterDataAssembler.FromEntityToDto(parameter));
            }
            return target;
        }
    }
}
