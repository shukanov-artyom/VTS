using System;
using System.Collections.ObjectModel;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public static class PsaDatasetAssembler
    {
        public static PsaDatasetDto FromDomainObjectToDto(PsaDataset source)
        {
            PsaDatasetDto target = new PsaDatasetDto();
            target.Traces = new ObservableCollection<PsaTraceDto>();
            target.Id = source.Id;
            target.ExportedDate = source.ExportedDate;
            target.Guid = source.Guid;
            target.PsaVehicleId = source.VehicleId;
            target.SavedDate = source.SavedDate;
            foreach (PsaTrace trace in source.Traces)
            {
                target.Traces.Add(PsaTraceAssembler.FromDomainObjectToDto(trace));
            }
            return target;
        }

        public static PsaDataset FromDtoToDomainObject(PsaDatasetDto source)
        {
            PsaDataset target = new PsaDataset();
            target.Id = source.Id;
            target.ExportedDate = source.ExportedDate;
            target.Guid = source.Guid;
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
