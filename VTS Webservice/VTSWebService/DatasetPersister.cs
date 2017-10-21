using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using VTS.Shared;
using VTSWebService.DataAccess;
using VTSWebService.DataContracts;
using VTSWebService.DomainObjects.Assemblers;
using PsaDatasetEntity = VTSWebService.DataAccess.PsaDataset;

namespace VTSWebService
{
    public class DatasetPersister
    {
        /// <summary>
        /// Persists dataset with all duplicate protections.
        /// </summary>
        /// <returns>Persisted dataset guid.</returns>
        public Guid Persist(PsaDatasetDto dataset)
        {
            Guid updatedDatasetGuid = Guid.Empty;
            if (IsKnownDataset(dataset))
            {
                foreach (PsaTraceDto traceDto in dataset.Traces)
                {
                    // skip known traces, skip empty traces
                    if (!IsKnownTrace(traceDto) && traceDto.ParametersSets.Count > 0)
                    {
                        // persist trace for existing dataset
                        using (VTSDatabase database = new VTSDatabase())
                        {
                            Guid guid = dataset.Guid;
                            PsaDatasetEntity entity = database.PsaDataset.FirstOrDefault(d => d.Guid == guid);
                            if (entity != null)
                            {
                                traceDto.PsaDatasetId = entity.Id;
                            }
                            PsaTrace traceEntity = PsaTraceAssembler.FromDtoToEntity(traceDto);
                            database.PsaTrace.Add(traceEntity);
                            database.SaveChanges();
                            updatedDatasetGuid = dataset.Guid; // to update analytic data based on updated dataset
                        }
                    }
                }
            }
            else
            {
                List<PsaTraceDto> bufferList = new List<PsaTraceDto>(dataset.Traces);
                foreach (PsaTraceDto traceDto in bufferList) // to remove known traces
                {
                    if (IsKnownTrace(traceDto) && traceDto.ParametersSets.Count > 0)
                    {
                        DateTime date = traceDto.Date;
                        string vin = traceDto.Vin;
                        PsaTraceDto dtoToRemove = dataset.Traces.FirstOrDefault(t =>
                            t.Date == date && t.Vin == vin);
                        if (dtoToRemove == null)
                        {
                            throw new Exception("Trace not found here!");
                        }
                        dataset.Traces.Remove(dtoToRemove);
                    }
                }
                if (dataset.Traces.Count > 0) // if there are still some new
                {
                    string vin = dataset.Traces.First().Vin;
                    if (!VinChecker.IsValid(vin))
                    {
                        throw new FaultException<VtsWebServiceException>(
                            new VtsWebServiceException(String.Format("Cannot save data for invalid vin {0}", vin)));
                    }
                    PsaDatasetEntity datasetEntity = PsaDatasetAssembler.FromDtoToEntity(dataset);
                    using (VTSDatabase database = new VTSDatabase())
                    {
                        datasetEntity.VehicleEntityId =
                            database.Vehicle.First(v => v.Vin == vin).Id;
                        database.PsaDataset.Add(datasetEntity);
                        database.SaveChanges();
                        updatedDatasetGuid = dataset.Guid; // to update analytic data based on updated dataset
                    }
                }
            }
            return updatedDatasetGuid;
        }

        private bool IsKnownDataset(PsaDatasetDto dataset)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                Guid guid = dataset.Guid;
                return database.PsaDataset.Any(pd => pd.Guid == guid);
            }
        }

        private bool IsKnownTrace(PsaTraceDto dto)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                string vin = dto.Vin;
                DateTime date = dto.Date;
                return database.PsaTrace.Any(t => t.Date == date && t.Vin == vin);
            }
        }
    }
}