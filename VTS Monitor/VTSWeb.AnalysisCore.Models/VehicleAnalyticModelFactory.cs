using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.AnalysisCore.Models.CommonRail;
using VTSWeb.AnalysisCore.Models.ElectricSystem;
using VTSWeb.AnalysisCore.Models.PetrolEngineIgnition;
using VTSWeb.AnalysisCore.Models.PetrolEngineInjection;
using VTSWeb.AnalysisCore.Models.PetrolEnginePurification;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Models
{
    public class VehicleAnalyticModelFactory
    {
        public delegate void
            AnalyticModelCreatedCallback(IVehicleAnalyticModel result);

        private readonly VehicleInformation vehicleInformation;
        private readonly IList<IAnalyticModelFactory> subfactories = 
            new List<IAnalyticModelFactory>();
        private readonly VehicleAnalyticModel result = new VehicleAnalyticModel();
        private readonly Vehicle vehicle;
        private readonly AnalyticModelCreatedCallback successCallback;
        private readonly ErrorCallbackDelegate errorCallback;

        public VehicleAnalyticModelFactory(Vehicle vehicle,
            VehicleInformation vehicleInformation,
            AnalyticModelCreatedCallback successCallback,
            ErrorCallbackDelegate errorCallback)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException("vehicle");
            }
            this.vehicleInformation = vehicleInformation;
            this.errorCallback = errorCallback;
            this.successCallback = successCallback;
            this.vehicle = vehicle;
        }

        public void CreateAsync()
        {
            result.VehicleInfo = vehicleInformation;
            InitializeSubfactories(vehicleInformation);
            StartSubfactories();
        }

        /// <summary>
        /// Initializez subfactories list, place your subfactory here.
        /// </summary>
        private void InitializeSubfactories(VehicleInformation information)
        {
            if (information.Engine.InjectionType == InjectionType.CommonRail)
            {
                IAnalyticModelFactory commonRailFactory = 
                    new AnalyticModelFactoryCommonRail(
                        information, SubfactoryHasFinished);
                subfactories.Add(commonRailFactory);
            }
            if (information.Engine.FuelType == FuelType.Petrol)
            {
                subfactories.Add(new AnalyticModelFactoryPetrolEnginePurification(
                        information, SubfactoryHasFinished));
                subfactories.Add(new AnalyticModelFactoryPetrolEngineIgnition(
                    information, SubfactoryHasFinished));
                subfactories.Add(new AnalyticModelFactoryPetrolEngineInjection(
                    information, SubfactoryHasFinished));
            }
            // universal parameters for all engine types
            subfactories.Add(new AnalyticModelFactoryElectricSystem(
                information, SubfactoryHasFinished));
        }

        private void StartSubfactories()
        {
            if (subfactories.Count == 0)
            {
                successCallback.Invoke(result);
            }
            foreach (IAnalyticModelFactory factory in subfactories)
            {
                factory.CreateAsync();
            }
        }

        private void SubfactoryHasFinished(IAnalyticModelFactory sender)
        {
            if (!sender.HasError)
            {
                IAnalyticModel createdSubmodel = sender.Result;
                if (result == null)
                {
                    throw new Exception("Should be not null!");
                }
                result.Models.Add(createdSubmodel);
            }
            else
            {
                errorCallback.Invoke(sender.Error, sender.Error.Message);
            }
            if (!subfactories.Any(sf => !sf.Finished)) // all finished
            {
                successCallback.Invoke(result);
            }
        }
    }
}
