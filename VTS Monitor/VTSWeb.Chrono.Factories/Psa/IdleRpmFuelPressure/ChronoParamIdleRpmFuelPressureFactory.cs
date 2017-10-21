using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.Chrono.Common;
using VTSWeb.Chrono.Psa;
using VTSWeb.DomainObjects;
using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.Chrono.Factories.Psa.IdleRpmFuelPressure
{
    public class ChronoParamIdleRpmFuelPressureFactory :
        IPsaChronologicalParameterFactory
    {
        private const int RatioMultiplier = 100;
        private ChronoParamIdleRpmFuelPressure result;

        private int idleRpmValuesCountLowerThreshold = 5;

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
            if (result == null)
            {
                 result = new ChronoParamIdleRpmFuelPressure();
            }
            PsaDataset dataset = source as PsaDataset;
            if (dataset == null)
            {
                throw new ArgumentException("PsaDataset should be here ");
            }
            foreach (PsaTrace trace in dataset.Traces)
            {
                PickDataFromTrace(trace);
            }
        }

        public IChronologicalParameter GetResult()
        {
            return result;
        }

        private bool TraceHasRequiredDataTypes(PsaTrace trace)
        {
            foreach (PsaParametersSet set in trace.ParametersSets)
            {
                if (SetHasRequiredData(set))
                {
                    return true;
                }
            }
            return false;
        }

        private bool SetHasRequiredData(PsaParametersSet set)
        {
            if (set.Parameters.Any(
                    p => p.Type == PsaParameterType.EngineRpm)
                    &&
                    set.Parameters.Any(
                    p => p.Type == PsaParameterType.FuelSystemPressure))
            {
                return true;
            }
            return false;
        }

        private void PickDataFromTrace(PsaTrace trace)
        {
            foreach (PsaParametersSet set in trace.ParametersSets)
            {
                if (SetHasRequiredData(set))
                {
                    PickDataFromSet(set, trace.Date);
                }
            }
        }

        private void PickDataFromSet(PsaParametersSet set, DateTime date)
        {
            PsaParameterData rpmData =
                set.GetParameterOfType(PsaParameterType.EngineRpm);
            PsaParameterData pressureData =
                set.GetParameterOfType(PsaParameterType.FuelSystemPressure);

            IList<int> idleIndexes = IdleRpmValueExtractor.
                ExtractIdleIndexes(rpmData.GetDoubles());
            if (idleIndexes.Count < idleRpmValuesCountLowerThreshold)
            {
                return;
            }
            IList<double> idlePressureValues = 
                ExtractIdlePressureValues(idleIndexes, pressureData.GetDoubles());
            result.Values.Add(
                new KeyValuePair<DateTime, double>(
                    date, idlePressureValues.Average()));

            // Let's get OcrToIdleRpmRatio
            PsaParameterData ocrData =
                set.GetParameterOfType(PsaParameterType.
                    DieselPressureRegulatorRatio);
            if (ocrData != null)
            {
                IList<double> ocrValues = 
                    ocrData.ExtractByIndexes(idleIndexes).ToList();
                IList<double> rpmValues =
                    rpmData.ExtractByIndexes(idleIndexes).ToList();
                if (ocrValues.Count != rpmValues.Count)
                {
                    throw new Exception("Counts should be equal!");
                }
                IList<double> ratioValues = new List<double>();
                for (int i = 0; i < rpmValues.Count; i++)
                {
                    double ratio = ocrValues[i]/rpmValues[i];
                    if (double.IsInfinity(ratio))
                    {
                        continue;
                    }
                    ratioValues.Add(RatioMultiplier*ocrValues[i]/rpmValues[i]);
                }
                result.OcrToRpmRatio[date] = ratioValues.Average();
            }
        }

        private IList<double> ExtractIdlePressureValues(
            IList<int> indexes, IList<double> values)
        {
            IList<double> extractedValues = new List<double>();
            foreach (int i in indexes)
            {
                extractedValues.Add(values[i]);
            }
            return extractedValues;
        }
    }
}
