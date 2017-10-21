using System;
using System.Linq;
using VTS.AnalysisCore.Common;
using VTS.Shared;
using VTSWebService.DataAccess;
using VehicleCharacteristics = VTS.Shared.DomainObjects.VehicleCharacteristics;

namespace VTSWebService.AnalysisCore.Recognition
{
    public class VehicleInfoRecognizer
    {
        private readonly VehicleCharacteristics characteristics;
        private readonly string vin;

        public VehicleInfoRecognizer(
            string vin,
            VehicleCharacteristics characteristics)
        {
            if (characteristics == null)
            {
                throw new ArgumentNullException("characteristics");
            }
            this.characteristics = characteristics;
            this.vin = vin;
        }

        public VehicleInformation Recognize()
        {
            VehicleInformation result = new VehicleInformation(vin);
            result.Engine = RecognizeEngine();
            result.VehicleModel = RecognizeVehicleModel();
            result.ProductionYear = RecognizeProductionYear();
            return result;
        }

        private Engine RecognizeEngine()
        {
            EngineRecognizerFactory factory =
                new EngineRecognizerFactory(vin, characteristics);
            try
            {
                IEngineRecognizer recognizer = factory.Create();
                return recognizer.Recognize();
            }
            catch (NotSupportedException)
            {
                LogUnrecognizedVin(vin);
                throw;
            }
        }

        private string RecognizeVehicleModel()
        {
            IVehicleModelRecognizer recognizer = VehicleModelRecognizerFactory.
                Create(VinChecker.GetManufacturer(vin));
            return recognizer.Recognize(characteristics);
        }

        private int RecognizeProductionYear()
        {
            IVehicleProductionYearRecognizer rec =
                VehicleProductionYearRecognizerFactory.Create(VinChecker.GetManufacturer(vin));
            return rec.Recognize(characteristics);
        }

        private void LogUnrecognizedVin(string vin)
        {
            if (vin.Length != 17)
            {
                return;
            }
            using (VTSDatabase database = new VTSDatabase())
            {
                if (!database.UnrecognizedVin.Any(
                    uv => 
                        uv.Vin.Equals(vin, StringComparison.OrdinalIgnoreCase)))
                {
                    database.UnrecognizedVin.Add(
                        new UnrecognizedVin
                        {
                            Date = DateTime.Now,
                            Vin = vin
                        });
                    database.SaveChanges();
                }
            }
        }
    }
}
