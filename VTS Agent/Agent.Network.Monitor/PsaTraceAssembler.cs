using System;
using System.Collections.Generic;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public class PsaTraceAssembler
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
            List<PsaParametersSetDto> tracesDtoList = new List<PsaParametersSetDto>();
            foreach (PsaParametersSet parametersSet in source.ParametersSets)
            {
                tracesDtoList.Add(PsaParametersSetAssembler.FromDomainObjectToDto(parametersSet));
            }
            target.ParametersSets = tracesDtoList.ToArray();
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
                target.ParametersSets.Add(PsaParametersSetAssembler.FromDtoToDomainObject(parametersSet));
            }
            return target;
        }
    }
}
