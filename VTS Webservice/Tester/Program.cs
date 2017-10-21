using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tester.DataImport;
using VTSWebService;
using VTSWebService.DataAccess;
using VTSWebService.DataContracts;
using VTSWebService.DomainObjects.Assemblers;
using PsaDataset = VTS.Shared.DomainObjects.PsaDataset;
using PsaTrace = VTS.Shared.DomainObjects.PsaTrace;
using AnalyticRuleSettingsEntity = VTSWebService.DataAccess.AnalyticRuleSettings;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            VtsWebService svc = new VtsWebService();
            VehicleInformationDto d = svc.GetVehicleInformationByVin("VF7CH9HZC39864291");
            d.ToString();
            /*List<string> vins = new List<string>()
            {
                "VF7LCKFUC74631358",
                "VF7DERHZB76139931",
                "VF7EB4HWG13182473",
                "VF3321PA212372875",
                "VF7AFRHWB12806391"
            };
            svc.AssociateVehiclesWithUser(vins, "dummy", Sha256Hash.Calculate("dummy"));*/
            //svc.ProvideAccessToVehicleForClientUsingEmail(4, "shuk.anov.artyom@gmail.com", "dummy", Sha256Hash.Calculate("dummy"));
            //Emailer.NotifyClientAboutRegistration("shuk.anov.artyom@gmail.com", "newClient", "newPassword");

            /*AnalyticDataAutoUpdateTester tester = new AnalyticDataAutoUpdateTester();
            tester.Test();*/

            /*VtsWebService s = new VtsWebService();
            List<int> res = s.GetAvailableAnalyticStatisticsTypesForVehicle("VF33CNFUB82601182");
            List<AnalyticStatisticsValueDto> vals = s.GetAnalyticStatisticsPerVehicle("VF33CNFUB82601182", 1034);*/

            /*List<SystemNewsDto> result = new List<SystemNewsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (SystemNews newsEntity in database.SystemNews.
                    OrderByDescending(s => s.DatePublished).Take(5))
                {
                    result.Add(SystemNewsAssembler.FromEntityToDto(newsEntity));
                }
            }

            VtsWebServiceClient cl = new VtsWebServiceClient();
            List<SystemNewsDto> n = cl.NewsGetAll().ToList();
            n.ToArray();*/


            /*string filename = @"g:\tmp\11_12_2013_good.uvts.zip";
            FileStream fs = File.Open(filename, FileMode.Open);
            byte[] bytez = ReadFully(fs);


            MemoryStream ms1 = new MemoryStream(bytez);
            ms1.Position = 0;

            ZipInputStream stream = new ZipInputStream(ms1);
            MemoryStream output = new MemoryStream();
            byte[] buffer = new byte[4096];
            StreamUtils.Copy(stream, output, buffer);
            byte[] bytes = ReadFully(stream);
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;*/


            /*string vin = "VF7SA5FV8BW634809"; //VF34C5FWF55360488 VF7LCKFUC74631358
            VehicleCharacteristicsManager manager = new VehicleCharacteristicsManager();
            VTS.Shared.DomainObjects.VehicleCharacteristics chars = manager.GetVehicleCharacteristicsForVin(vin, SupportedLanguage.English);
            VehicleInfoRecognizer rec = new VehicleInfoRecognizer(vin, chars);
            VehicleInformation info = rec.Recognize();*/

            /*FileStream stream = new FileStream(@"c:\Users\Yama\Desktop\pegAll.vts", FileMode.Open);
            PortableDataFactory factory = new PortableDataFactory(stream);
            PortableData data = factory.Create();
            List<PsaDataset> datasets = FormDatasets(data);
            VtsWebServiceClient client = new VtsWebServiceClient();
            //client.UploadDataset(PsaDatasetAssembler.FromDomainObjectToDto(datasets[0]));
            Persist(datasets);*/

            /*List<AnalyticRuleSettingsDto> result = GetAnalyticRuleSettingsDefaultByTypes(
                new List<int>() { 1001, 1034 }, 2, 216);*/
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsDefaultByTypes(
            List<int> types, int engineFamilyType, int engineType)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();

            foreach (int type in types)
            {
                result.Add(GetDefaultSettingsBySignature(type,
                    engineFamilyType, engineType));
            }
            return result;
        }

        private static AnalyticRuleSettingsDto GetDefaultSettingsBySignature(
            int ruleType, int engineFamilyType, int engineType)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                AnalyticRuleSettingsEntity exactMatch =
                    database.AnalyticRuleSettings.FirstOrDefault(
                        s => s.RuleType == ruleType &&
                             s.EngineFamilyType == engineFamilyType &&
                             s.EngineType == engineType);
                if (exactMatch != null)
                {
                    return AnalyticRuleSettingsAssembler.
                        FromEntityToDto(exactMatch);
                }
                AnalyticRuleSettingsEntity engineFamilyMatch =
                    database.AnalyticRuleSettings.FirstOrDefault(
                        s => s.RuleType == ruleType &&
                            s.EngineFamilyType == engineFamilyType &&
                            s.EngineType == null);
                if (engineFamilyMatch != null)
                {
                    return AnalyticRuleSettingsAssembler.
                        FromEntityToDto(engineFamilyMatch);
                }
                AnalyticRuleSettingsEntity typeFallbackMatch =
                    database.AnalyticRuleSettings.First(
                    s => s.RuleType == ruleType &&
                    s.EngineFamilyType == null &&
                    s.EngineType == null);
                return AnalyticRuleSettingsAssembler.
                        FromEntityToDto(typeFallbackMatch);
            }
        }

        private static AnalyticRuleSettingsEntity Get(int ruleType, int engineFamilyType, int engineType)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                AnalyticRuleSettingsEntity exactMatch =
                    database.AnalyticRuleSettings.FirstOrDefault(
                        s => s.RuleType == ruleType &&
                             s.EngineFamilyType == engineFamilyType &&
                             s.EngineType == engineType);
                if (exactMatch != null)
                {
                    return exactMatch;
                }
                AnalyticRuleSettingsEntity engineFamilyMatch =
                    database.AnalyticRuleSettings.FirstOrDefault(
                        s => s.RuleType == ruleType &&
                            s.EngineFamilyType == engineFamilyType &&
                            s.EngineType == null);
                if (engineFamilyMatch != null)
                {
                    return engineFamilyMatch;
                }
                AnalyticRuleSettingsEntity typeFallbackMatch =
                    database.AnalyticRuleSettings.First(
                    s => s.RuleType == ruleType &&
                    s.EngineFamilyType == null &&
                    s.EngineType == null);
                return typeFallbackMatch;
            }
        }

        private static void Persist(List<PsaDataset> datasets)
        {
            foreach (PsaDataset psaDataset in datasets)
            {
                PsaDatasetDto dataset = PsaDatasetAssembler.FromDomainObjectToDto(psaDataset);
                if (IsKnownDataset(dataset))
                {
                    foreach (PsaTraceDto traceDto in dataset.Traces)
                    {
                        // skip known traces
                        if (!IsKnownTrace(traceDto))
                        {
                            // persist trace for existing dataset
                            using (VTSDatabase database = new VTSDatabase())
                            {
                                Guid guid = dataset.Guid;
                                long datasetId = database.PsaDataset.First(d => d.Guid == guid).Id;
                                traceDto.PsaDatasetId = datasetId;
                                VTSWebService.DataAccess.PsaTrace traceEntity = PsaTraceAssembler.FromDtoToEntity(traceDto);
                                database.PsaTrace.Add(traceEntity);
                                database.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    List<PsaTraceDto> bufferList = new List<PsaTraceDto>(dataset.Traces);
                    foreach (PsaTraceDto traceDto in bufferList) // to remove known traces
                    {
                        if (IsKnownTrace(traceDto))
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
                        VTSWebService.DataAccess.PsaDataset datasetEntity = 
                            PsaDatasetAssembler.FromDtoToEntity(dataset);
                        using (VTSDatabase database = new VTSDatabase())
                        {
                            string vin = dataset.Traces.First().Vin;
                            datasetEntity.VehicleEntityId =
                                database.Vehicle.First(v => v.Vin == vin).Id;
                            database.PsaDataset.Add(datasetEntity);
                            database.SaveChanges();
                        }
                    }
                }
            }
        }

        private static bool IsKnownDataset(PsaDatasetDto dataset)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                Guid guid = dataset.Guid;
                return database.PsaDataset.Any(pd => pd.Guid == guid);
            }
        }

        private static bool IsKnownTrace(PsaTraceDto dto)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                string vin = dto.Vin;
                DateTime date = dto.Date;
                return database.PsaTrace.Any(t => t.Date == date && t.Vin == vin);
            }
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
    }
}
