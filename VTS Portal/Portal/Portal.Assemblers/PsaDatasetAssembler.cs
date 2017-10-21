using System;
using Portal.Service.Service;
using VTS.Shared.DomainObjects;

namespace Portal.Assemblers
{
    public static class PsaDatasetAssembler
    {
        public static PsaDataset FromDtoToDomainObject(PsaDatasetDto source)
        {
            var target = new PsaDataset
                         {
                             Id = source.Id,
                             ExportedDate = source.ExportedDate,
                             SavedDate = source.SavedDate,
                             Guid = source.Guid,
                             VehicleId = source.PsaVehicleId
                         };
            foreach (PsaTraceDto trace in source.Traces)
            {
                target.Traces.Add(PsaTraceAssembler.FromDtoToDomainObject(trace));
            }
            return target;
        }
    }
}
