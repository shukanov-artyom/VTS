using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using PsaDatasetEntity = VTSWebService.DataAccess.PsaDataset;
using PsaTraceEntity = VTSWebService.DataAccess.PsaTrace;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class PsaDatasetAssembler
    {
        public static PsaDatasetDto FromDomainObjectToDto(PsaDataset source)
        {
            PsaDatasetDto target = new PsaDatasetDto();
            target.Traces = new List<PsaTraceDto>();
            target.Id = source.Id;
            target.ExportedDate = source.ExportedDate;
            target.SavedDate = source.SavedDate;
            target.Guid = source.Guid;
            target.PsaVehicleId = source.VehicleId;
            foreach (PsaTrace trace in source.Traces)
            {
                target.Traces.Add(PsaTraceAssembler.FromDomainObjectToDto(trace));
            }
            return target;
        }

        public static PsaDataset FromEntityToDomainObject(PsaDatasetEntity source)
        {
            PsaDataset target = new PsaDataset();
            target.Id = source.Id;
            target.ExportedDate = source.DateExported;
            target.SavedDate = source.DateSaved;
            target.Guid = source.Guid;
            target.VehicleId = source.VehicleEntityId;
            foreach (PsaTraceEntity trace in source.PsaTrace)
            {
                target.Traces.Add(PsaTraceAssembler.FromEntityToDomainObject(trace));
            }
            return target;
        }

        public static PsaDatasetEntity FromDtoToEntity(PsaDatasetDto source)
        {
            PsaDatasetEntity target = new PsaDatasetEntity();
            target.Id = source.Id;
            target.DateExported = source.ExportedDate;
            target.DateSaved = source.SavedDate;
            target.Guid = source.Guid;
            target.VehicleEntityId = source.PsaVehicleId;
            foreach (PsaTraceDto trace in source.Traces)
            {
                target.PsaTrace.Add(PsaTraceAssembler.FromDtoToEntity(trace));
            }
            return target;
        }

        public static PsaDatasetDto FromEntityToDto(PsaDatasetEntity source)
        {
            PsaDatasetDto target = new PsaDatasetDto();
            target.Traces = new List<PsaTraceDto>();
            target.Id = source.Id;
            target.ExportedDate = source.DateExported;
            target.SavedDate = source.DateSaved;
            target.Guid = source.Guid;
            target.PsaVehicleId = source.VehicleEntityId;
            foreach (PsaTraceEntity trace in source.PsaTrace)
            {
                target.Traces.Add(PsaTraceAssembler.FromEntityToDto(trace));
            }
            return target;
        }
    }
}
