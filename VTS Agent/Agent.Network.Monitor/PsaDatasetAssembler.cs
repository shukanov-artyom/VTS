using System;
using System.Collections.Generic;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public static class PsaDatasetAssembler
    {
        public static PsaDatasetDto FromDomainobjectToDto(PsaDataset source)
        {
            PsaDatasetDto target = new PsaDatasetDto();
            target.ExportedDate = source.ExportedDate;
            target.Guid = source.Guid;
            target.Id = source.Id;
            target.PsaVehicleId = source.VehicleId;
            target.SavedDate = source.SavedDate;
            List<PsaTraceDto> traceDtos = new List<PsaTraceDto>();
            foreach (PsaTrace trace in source.Traces)
            {
                traceDtos.Add(PsaTraceAssembler.FromDomainObjectToDto(trace));
            }
            target.Traces = traceDtos.ToArray();
            return target;
        }

        public static PsaDataset FromDtoToDomainObject(PsaDatasetDto source)
        {
            PsaDataset target = new PsaDataset();
            target.ExportedDate = source.ExportedDate;
            target.Guid = source.Guid;
            target.Id = source.Id;
            target.VehicleId = source.PsaVehicleId;
            target.SavedDate = source.SavedDate;
            foreach (PsaTraceDto trace in source.Traces)
            {
                target.Traces.Add(PsaTraceAssembler.FromDtoToDomainObject(trace));
            }
            return target;
        }
    }
}
