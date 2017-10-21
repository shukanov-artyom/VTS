using System;
using System.Collections.Generic;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Recognition;
using VTSWebService.VendorInfo;
using PsaDataset = VTS.Shared.DomainObjects.PsaDataset;

namespace VTSWebService.AnalysisCore.Statistics
{
    public class StatisticsEngine
    {
        public List<AnalyticStatisticsItem> ProcessDatasets(IList<PsaDataset> datasets)
        {
            List<AnalyticStatisticsItem> result = new List<AnalyticStatisticsItem>();
            foreach (PsaDataset dataset in datasets)
            {
                foreach (AnalyticStatisticsItem item in ProcessDataset(dataset))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<AnalyticStatisticsItem> ProcessDataset(PsaDataset dataset)
        {
            List<AnalyticStatisticsItem> result = new List<AnalyticStatisticsItem>();
            string vin = dataset.GetVin().ToUpper();
            VehicleInformation info = GetVehicleInformation(vin);
            foreach (PsaTrace trace in dataset.Traces)
            {
                StatisticsGenerationConveyour conveyor =
                    new StatisticsGenerationConveyour(info);
                foreach (AnalyticStatisticsItem item in conveyor.RollAlong(trace))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private VehicleInformation GetVehicleInformation(string vin)
        {
            VehicleCharacteristicsManager manager =
                new VehicleCharacteristicsManager();
            VehicleCharacteristics chars = manager.
                GetVehicleCharacteristicsForVin(vin, SupportedLanguage.English);
            VehicleInfoRecognizer recognizer = new VehicleInfoRecognizer(vin, chars);
            VehicleInformation vehicleInformation = recognizer.Recognize();
            return vehicleInformation;
        }
    }
}
