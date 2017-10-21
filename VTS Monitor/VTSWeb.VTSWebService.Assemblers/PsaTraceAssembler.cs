using System;
using System.Collections.ObjectModel;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public static class PsaTraceAssembler
    {
        public static PsaTraceDto FromDomainObjectToDto(PsaTrace source)
        {
            PsaTraceDto target = new PsaTraceDto();
            target.ParametersSets = new ObservableCollection<PsaParametersSetDto>();
            target.Id = source.Id;
            target.Address = source.Address;
            target.Application = source.Application;
            target.Date = source.Date;
            target.Format = source.Format;
            target.Manufacturer = (int)source.Manufacturer;
            target.Mileage = source.Mileage;
            target.Phone = source.Phone;
            target.PhoneChannel = source.PhoneChannel;
            target.PsaDatasetId = source.PsaDatasetId;
            target.SavesetId = source.SavesetId;
            target.ToolSerialNumber = source.ToolSerialNumber;
            target.VehicleModelName = source.VehicleModelName;
            target.Vin = source.Vin;
            foreach (PsaParametersSet parametersSet in source.ParametersSets)
            {
                target.ParametersSets.Add(
                    PsaParametersSetAssembler.FromDomainObjectToDto(parametersSet));
            }
            return target;
        }

        public static PsaTrace FromDtoToDomainObject(PsaTraceDto source)
        {
            PsaTrace target = new PsaTrace();
            target.Id = source.Id;
            target.Address = source.Address;
            target.Application = source.Application;
            target.Date = source.Date;
            target.Format = source.Format;
            target.Manufacturer = (Manufacturer)source.Manufacturer;
            target.Mileage = source.Mileage;
            target.Phone = source.Phone;
            target.PhoneChannel = source.PhoneChannel;
            target.PsaDatasetId = source.PsaDatasetId;
            target.SavesetId = source.SavesetId;
            target.ToolSerialNumber = source.ToolSerialNumber;
            target.VehicleModelName = source.VehicleModelName;
            target.Vin = source.Vin;
            foreach (PsaParametersSetDto parametersSet in source.ParametersSets)
            {
                target.ParametersSets.Add(
                    PsaParametersSetAssembler.FromDtoToDomainObject(parametersSet));
            }
            return target;
        }
    }
}
