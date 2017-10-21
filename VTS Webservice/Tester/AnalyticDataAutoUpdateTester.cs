using System;
using System.Collections.Generic;
using System.IO;
using Tester.DataImport;
using VTS.Shared.DomainObjects;
using VTSWebService;
using VTSWebService.DataContracts;
using VTSWebService.DomainObjects.Assemblers;

namespace Tester
{
    public class AnalyticDataAutoUpdateTester
    {
        public void Test()
        {
            CreateTestData();
            TestPrivate();
            DeleteTestData();
        }

        private void CreateTestData()
        {
            FileStream stream = new FileStream(@"c:\Users\Yama\Desktop\pegAll.vts", FileMode.Open);
            PortableDataFactory factory = new PortableDataFactory(stream);
            PortableData data = factory.Create();
            List<PsaDataset> datasets = FormDatasets(data);
            List<string> vins = new List<string>();
            foreach (PsaDataset dataset in datasets)
            {
                vins.Add(dataset.GetVin());
            }
            VtsWebService service = new VtsWebService();
            List<string> unsupportedVins = service.CheckVinsReturnUnsupported(vins);
            List<string> supportedvins = new List<string>();
            foreach (string vin in vins)
            {
                if (!unsupportedVins.Contains(vin))
                {
                    supportedvins.Add(vin);
                }
            }
            service.RegisterVehicles(supportedvins);
            List<PsaDatasetDto> dtos = new List<PsaDatasetDto>();
            foreach (PsaDataset dataset in datasets)
            {
                dtos.Add(PsaDatasetAssembler.FromDomainObjectToDto(dataset));
            }
            service.UploadDatasets(dtos);
        }

        private void TestPrivate()
        {
            
        }

        private void DeleteTestData()
        {
            
        }

        private static List<PsaDataset> FormDatasets(PortableData data)
        {
            IDictionary<string, List<PsaTrace>> sortedTraces =
                new Dictionary<string, List<PsaTrace>>();
            foreach (PsaTrace psaTrace in data.PsaTraces)
            {
                string vin = psaTrace.Vin;
                if (sortedTraces.ContainsKey(vin))
                {
                    sortedTraces[vin].Add(psaTrace);
                }
                else
                {
                    sortedTraces[vin] = new List<PsaTrace>() { psaTrace };
                }
            }
            List<PsaDataset> result = new List<PsaDataset>();
            foreach (KeyValuePair<string, List<PsaTrace>> pair in sortedTraces)
            {
                PsaDataset dataset = new PsaDataset();
                dataset.ExportedDate = data.Date;
                dataset.Guid = data.Guid;
                dataset.VehicleId = 0; // Service will fill it 
                dataset.SavedDate = DateTime.Now;
                foreach (PsaTrace trace in pair.Value)
                {
                    dataset.Traces.Add(trace);
                }
                result.Add(dataset);
            }
            return result;
        }

        private static void Persist(List<PsaDataset> datasets)
        {
            foreach (PsaDataset dataset in datasets)
            {
                DatasetPersister persister = 
                    new DatasetPersister();
                persister.Persist(PsaDatasetAssembler.FromDomainObjectToDto(dataset));
            }
        }
    }
}
