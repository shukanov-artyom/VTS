using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.Chrono.Factories.Psa.EngineRpm;
using VTSWeb.Chrono.Factories.Psa.IdleRpmFuelPressure;
using VTSWeb.Chrono.Factories.Psa.InjectorsCorrections;
using VTSWeb.Chrono.Psa;
using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.Chrono.Factories.Psa
{
    public class PsaChronologicalDataFactory
    {
        private IEnumerable<PsaDataset> datasets;
        private IList<IChronologicalParameterFactory> factories = 
            new List<IChronologicalParameterFactory>();

        private IDictionary<ChronologicalParameterType, 
            ChronologicalParameterGroupType> parameterToGroupMapping = 
            new Dictionary<ChronologicalParameterType,
                ChronologicalParameterGroupType>(); 

        public PsaChronologicalDataFactory(IEnumerable<PsaDataset> datasets)
        {
            if (datasets == null)
            {
                throw new ArgumentNullException("datasets");
            }
            this.datasets = datasets;
            InitializeParamToGroupMapping();
            InitializeFactories();
        }

        private void InitializeParamToGroupMapping()
        {
            parameterToGroupMapping.Add(
                ChronologicalParameterType.IdleEngineRpm, 
                ChronologicalParameterGroupType.Engine);
            parameterToGroupMapping.Add(
                ChronologicalParameterType.InjectorCorrections, 
                ChronologicalParameterGroupType.EngineInjection);
            parameterToGroupMapping.Add(
                ChronologicalParameterType.IdleRpmFuelPressure,
                ChronologicalParameterGroupType.EngineInjection);
        }

        private void InitializeFactories()
        {
            factories.Add(new ChronoParamEngineRpmFactory());
            factories.Add(new ChronoParamInjectorsCorrectionsFactory());
            factories.Add(new ChronoParamIdleRpmFuelPressureFactory());
        }

        public ChronologicalData Create()
        {
            ChronologicalData result = new PsaChronologicalData();
            foreach (IChronologicalParameterFactory factory in factories)
            {
                foreach (PsaDataset dataset in datasets)
                {
                    if (factory.CanGenerateFrom(dataset))
                    {
                        factory.PickUpFrom(dataset);
                    }
                }
                IChronologicalParameter resultParameter = factory.GetResult();
                if (resultParameter != null)
                {
                    PlaceToGroup(result, resultParameter);
                }
            }
            return result;
        }

        private void PlaceToGroup(ChronologicalData data, 
            IChronologicalParameter parameter)
        {
            ChronologicalParameterGroupType groupType =
                parameterToGroupMapping[parameter.Type];
            if (!data.Groups.Any(g => g.Type == groupType))
            {
                ChronoParamGroupFactory groupFactory = 
                    new ChronoParamGroupFactory();
                IChronologicalParametersGroup group =
                    groupFactory.Create(groupType);
                data.Groups.Add(group);
            }
            data.Groups.FirstOrDefault(g => 
                    g.Type == groupType).Parameters.Add(parameter);
        }
    }
}
