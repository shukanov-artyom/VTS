using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.Chrono.Common;
using VTSWeb.Chrono.Psa;
using VTSWeb.DomainObjects;
using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.Chrono.Factories.Psa.EngineRpm
{
    public class ChronoParamEngineRpmFactory : IChronologicalParameterFactory
    {
        private const double InitialRpmLineMinLenghtPercent = 3.0;

        private ChronoParamIdleEngineRpm result;

        public bool CanGenerateFrom(DomainObject source)
        {
            PsaDataset dataset = source as PsaDataset;
            if (dataset == null)
            {
                throw new ArgumentException("Wrong object type.");
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
                throw new ArgumentException("Wrong object type.");
            }
            if (result == null)
            {
                result = new ChronoParamIdleEngineRpm();
            }
            foreach (PsaTrace trace in dataset.Traces)
            {
                if (TraceHasRequiredDataTypes(trace))
                {
                    ExtractDataFromTrace(trace);
                }
            }
        }

        public IChronologicalParameter GetResult()
        {
            return result;
        }

        private bool ParameterSetHasRequiredDataTypes(PsaParametersSet set)
        {
            return set.Parameters.
                Any(p => p.Type == PsaParameterType.EngineRpm) &&
                   set.Parameters.Any(p =>
                p.Type == PsaParameterType.WaterTemperature || // Not in use actually!
                p.Type == PsaParameterType.EngineCoolantTemperature);
        }

        private bool TraceHasRequiredDataTypes(PsaTrace trace)
        {
            return trace.ParametersSets.Any(
                p => ParameterSetHasRequiredDataTypes(p));
        }

        private void ExtractDataFromTrace(PsaTrace trace)
        {
            DateTime date = trace.Date;
            foreach (PsaParametersSet set in trace.ParametersSets)
            {
                PsaParameterData rpmData =
                        set.GetParameterOfType(PsaParameterType.EngineRpm);
                PsaParameterData waterData =
                    set.GetParameterOfType(
                        PsaParameterType.WaterTemperature);
                if (waterData == null)
                {
                    waterData = set.GetParameterOfType(
                        PsaParameterType.EngineCoolantTemperature);
                }

                if (rpmData != null && waterData != null)
                {
                    if (RpmDataFits(rpmData))
                    {
                        HelperRpmWaterDate result = new
                            HelperRpmWaterDate(rpmData, waterData, date);
                        ProcessHelper(result);
                    }
                }
            }
        }

        private bool RpmDataFits(PsaParameterData data)
        {
            if (data.Type != PsaParameterType.EngineRpm)
            {
                throw new ArgumentException("Wrong data type!");
            }
            IList<double> doubles = ExtractInitialIdleLine(data);
            int idlePointsCount = doubles.Count;
            int totalPointsCount = data.Values.Count;

            double ratio = ((double)idlePointsCount / totalPointsCount) * 100;
            if (ratio >= InitialRpmLineMinLenghtPercent)
            {
                return true;
            }
            return false;
        }

        private static IList<double> ExtractInitialIdleLine(
            PsaParameterData data)
        {
            IList<double> doubles = new List<double>();
            foreach (string s in data.Values)
            {
                doubles.Add(Double.Parse(s));
            }
            return IdleRpmValueExtractor.ExtractInitialIdleLine(doubles);
        }

        private void ProcessHelper(HelperRpmWaterDate helper)
        {
            if (helper.TemperatureClass == TemperatureClass.Cold)
            {
                result.ColdStartRpmDates.Add(helper.Date);
                result.ColdStartRpmValues.Add(helper.Value);
            }
            else if (helper.TemperatureClass == TemperatureClass.Medium)
            {
                result.MiddleStartRpmDates.Add(helper.Date);
                result.MiddleStartRpmValues.Add(helper.Value);
            }
            else
            {
                result.HotStartRpmDates.Add(helper.Date);
                result.HotStartRpmValues.Add(helper.Value);
            }
        }
    }
}
