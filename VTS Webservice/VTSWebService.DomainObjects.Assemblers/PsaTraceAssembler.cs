using System;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using PsaTraceEntity = VTSWebService.DataAccess.PsaTrace;
using PsaParametersSetEntity = VTSWebService.DataAccess.PsaParametersSet;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class PsaTraceAssembler
    {
        public static PsaTraceDto FromDomainObjectToDto(PsaTrace source)
        {
            PsaTraceDto target = new PsaTraceDto();
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

        public static PsaTrace FromEntityToDomainObject(PsaTraceEntity source)
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
            target.PsaDatasetId = source.PsaDatasetEntityId;
            target.SavesetId = source.SavesetId;
            target.ToolSerialNumber = source.ToolSerialNumber;
            target.VehicleModelName = source.VehicleModelName;
            target.Vin = source.Vin;
            foreach (PsaParametersSetEntity parametersSet in source.PsaParametersSet)
            {
                target.ParametersSets.Add(
                    PsaParametersSetAssembler.FromEntityToDomainObject(parametersSet));
            }
            return target;
        }

        public static PsaTraceEntity FromDtoToEntity(PsaTraceDto source)
        {
            PsaTraceEntity target = new PsaTraceEntity();
            target.Id = source.Id;
            target.Address = source.Address;
            target.Application = source.Application;
            target.Date = source.Date;
            target.Format = source.Format;
            target.Manufacturer = source.Manufacturer;
            target.Mileage = source.Mileage;
            target.Phone = source.Phone;
            target.PhoneChannel = source.PhoneChannel;
            target.PsaDatasetEntityId = source.PsaDatasetId;
            target.SavesetId = source.SavesetId;
            target.ToolSerialNumber = source.ToolSerialNumber;
            target.VehicleModelName = source.VehicleModelName;
            target.Vin = source.Vin;
            foreach (PsaParametersSetDto parametersSet in source.ParametersSets)
            {
                target.PsaParametersSet.Add(
                    PsaParametersSetAssembler.FromDtoToEntity(parametersSet));
            }
            return target;
        }

        public static PsaTraceDto FromEntityToDto(PsaTraceEntity source)
        {
            PsaTraceDto target = new PsaTraceDto();
            target.Id = source.Id;
            target.Address = source.Address;
            target.Application = source.Application;
            target.Date = source.Date;
            target.Format = source.Format;
            target.Manufacturer = source.Manufacturer;
            target.Mileage = source.Mileage;
            target.Phone = source.Phone;
            target.PhoneChannel = source.PhoneChannel;
            target.PsaDatasetId = source.PsaDatasetEntityId;
            target.SavesetId = source.SavesetId;
            target.ToolSerialNumber = source.ToolSerialNumber;
            target.VehicleModelName = source.VehicleModelName;
            target.Vin = source.Vin;
            foreach (PsaParametersSetEntity parametersSet in source.PsaParametersSet)
            {
                target.ParametersSets.Add(
                    PsaParametersSetAssembler.FromEntityToDto(parametersSet));
            }
            return target;
        }
    }
}
