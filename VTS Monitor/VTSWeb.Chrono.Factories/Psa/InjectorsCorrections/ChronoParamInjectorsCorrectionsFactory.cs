using System;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.Chrono.Psa;
using VTSWeb.DomainObjects;
using VTSWeb.DomainObjects.Psa;


namespace VTSWeb.Chrono.Factories.Psa.InjectorsCorrections
{
    public class ChronoParamInjectorsCorrectionsFactory 
         : IChronologicalParameterFactory
    {
        private ChronoParamInjectorCorrections result;

        public bool CanGenerateFrom(DomainObject source)
        {
            PsaDataset dataset = source as PsaDataset;
            if (dataset == null)
            {
                throw new ArgumentException("PsaDataset should be here ");
            }
            foreach (PsaTrace trace in dataset.Traces)
            {
                if (TraceHasRequiredDataTypes(trace))
                {
                    return true;
                }
            }
            return false;
        }

        public void PickUpFrom(DomainObject source)
        {
            PsaDataset dataset = source as PsaDataset;
            if (dataset == null)
            {
                throw new ArgumentException("PsaDataset should be here ");
            }
            foreach (PsaTrace trace in dataset.Traces)
            {
                PickUpFromTrace(trace);
            }
        }

        public IChronologicalParameter GetResult()
        {
            return result;
        }

        private void PickUpFromTrace(PsaTrace trace)
        {
            foreach (PsaParametersSet set in trace.ParametersSets)
            {
                PickUpFromSet(set, trace.Date);
            }
        }

        private void PickUpFromSet(PsaParametersSet set, DateTime date)
        {
            if (!ParametersSetHasRequiredDataTypes(set))
            {
                return;
            }
            PsaParameterData rpmData = set.Parameters.FirstOrDefault(
                p => p.Type == PsaParameterType.EngineRpm);
            PsaParameterData inj1Data =
                set.Parameters.FirstOrDefault(
                p => p.Type == PsaParameterType.Injector1Correction);
            PsaParameterData inj2Data =
                set.Parameters.FirstOrDefault(
                p => p.Type == PsaParameterType.Injector2Correction);
            PsaParameterData inj3Data =
                set.Parameters.FirstOrDefault(
                p => p.Type == PsaParameterType.Injector3Correction);
            PsaParameterData inj4Data =
                set.Parameters.FirstOrDefault(
                p => p.Type == PsaParameterType.Injector4Correction);
            if (rpmData != null)
            {
                GenerateFromParameters(rpmData, 
                    inj1Data, inj2Data, inj3Data, inj4Data, date);
            }
        }

        private void GenerateFromParameters(
            PsaParameterData rpmData, 
            PsaParameterData inj1Data,
            PsaParameterData inj2Data,
            PsaParameterData inj3Data,
            PsaParameterData inj4Data, 
            DateTime date)
        {
            GenerateMedianData(rpmData, inj1Data, 1, date);
            GenerateMedianData(rpmData, inj2Data, 2, date);
            GenerateMedianData(rpmData, inj3Data, 3, date);
            GenerateMedianData(rpmData, inj4Data, 4, date);

            GenerateRangeData(rpmData, inj1Data, 1, date);
            GenerateRangeData(rpmData, inj2Data, 2, date);
            GenerateRangeData(rpmData, inj3Data, 3, date);
            GenerateRangeData(rpmData, inj4Data, 4, date);
        }

        private void GenerateRangeData(PsaParameterData rpmData, 
            PsaParameterData injectorData, int injectorNumber, DateTime date)
        {
            RangeDataFactory factory = 
                new RangeDataFactory(rpmData, injectorData, injectorNumber);
            factory.Generate(Result, date);
        }

        private void GenerateMedianData(PsaParameterData rpmData,
            PsaParameterData injectorData, int injectorNumber, DateTime date)
        {
            MedianDataFactory factory =
                new MedianDataFactory(rpmData, injectorData, injectorNumber);
            factory.Generate(Result, date);
        }

        private bool TraceHasRequiredDataTypes(PsaTrace trace)
        {
            foreach (PsaParametersSet set in trace.ParametersSets)
            {
                if (ParametersSetHasRequiredDataTypes(set))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ParametersSetHasRequiredDataTypes(PsaParametersSet set)
        {
            if (set.Parameters.Any(p =>
                p.Type == PsaParameterType.Injector1Correction ||
                p.Type == PsaParameterType.Injector2Correction ||
                p.Type == PsaParameterType.Injector3Correction ||
                p.Type == PsaParameterType.Injector4Correction) &&
                set.Parameters.Any(p =>
                p.Type == PsaParameterType.EngineRpm))
            {
                return true;
            }
            return false;
        }

        private ChronoParamInjectorCorrections Result
        {
            get
            {
                if (result == null)
                {
                    result = new ChronoParamInjectorCorrections();
                }
                return result;
            }
        }
    }
}
