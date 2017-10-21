using System;
using Portal.Service.Service;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Portal.Assemblers
{
    public static class PsaTraceAssembler
    {
        public static PsaTrace FromDtoToDomainObject(PsaTraceDto source)
        {
            var target = new PsaTrace
                         {
                             Id = source.Id,
                             Address = source.Address,
                             Application = source.Application,
                             Date = source.Date,
                             Format = source.Format,
                             Manufacturer = (Manufacturer) source.Manufacturer,
                             Mileage = source.Mileage,
                             Phone = source.Phone,
                             PhoneChannel = source.PhoneChannel,
                             PsaDatasetId = source.PsaDatasetId,
                             SavesetId = source.SavesetId,
                             ToolSerialNumber = source.ToolSerialNumber,
                             VehicleModelName = source.VehicleModelName,
                             Vin = source.Vin
                         };
            foreach (PsaParametersSetDto parametersSet in source.ParametersSets)
            {
                target.ParametersSets.Add(
                    PsaParametersSetAssembler.FromDtoToDomainObject(parametersSet));
            }
            return target;
        }
    }
}
