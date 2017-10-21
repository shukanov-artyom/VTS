using System;
using System.Collections.ObjectModel;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;

using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public static class PsaParametersSetAssembler
    {
        public static PsaParametersSetDto FromDomainObjectToDto(PsaParametersSet source)
        {
            PsaParametersSetDto target = new PsaParametersSetDto();
            target.Parameters = new ObservableCollection<PsaParameterDataDto>();
            target.Id = source.Id;
            target.PsaTraceId = source.PsaTraceId;
            target.Type = (int)source.Type;
            foreach (PsaParameterData parameter in source.Parameters)
            {
                target.Parameters.Add(PsaParameterDataAssembler.FromDomainObjectToDto(parameter));
            }
            return target;
        }

        public static PsaParametersSet FromDtoToDomainObject(PsaParametersSetDto source)
        {
            PsaParametersSet target = new PsaParametersSet();
            target.Id = source.Id;
            target.PsaTraceId = source.PsaTraceId;
            target.Type = (PsaParametersSetType)source.Type;
            foreach (PsaParameterDataDto parameter in source.Parameters)
            {
                target.Parameters.Add(PsaParameterDataAssembler.FromDtoToDomainObject(parameter));
            }
            return target;
        }
    }
}
