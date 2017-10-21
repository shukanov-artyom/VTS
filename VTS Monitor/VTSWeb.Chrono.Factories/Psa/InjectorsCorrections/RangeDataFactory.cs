using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Tools;
using VTSWeb.Chrono.Common;
using VTSWeb.Chrono.Psa;
using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.Chrono.Factories.Psa.InjectorsCorrections
{
    internal class RangeDataFactory
    {
        private const int LowRpm = 1000;
        private const int MediumRpm = 2000;
        private const int HighRpm = 3000;
        private const int RpmDifferencePercentage = 5;

        private PsaParameterData rpmData;
        private PsaParameterData injectorData;
        private int injectorNumber;

        public RangeDataFactory(
            PsaParameterData rpmData,
            PsaParameterData injectorData, 
            int injectorNumber)
        {
            this.rpmData = rpmData;
            this.injectorData = injectorData;
            this.injectorNumber = injectorNumber;
        }

        public void Generate(ChronoParamInjectorCorrections result, 
            DateTime date)
        {
            IList<int> lowRpmIndexes = ExtractRpmIndexes(rpmData, LowRpm);
            IList<int> mediumRpmIndexes = ExtractRpmIndexes(rpmData, MediumRpm);
            IList<int> highRpmIndexes = ExtractRpmIndexes(rpmData, HighRpm);
            /*if (lowRpmIndexes.Count == 0 || mediumRpmIndexes.Count == 0 ||
                highRpmIndexes.Count == 0)
            {
                return;
            }*/
            IList<double> lowRpmCorrectionValues = new List<double>();
            if (lowRpmIndexes.Count != 0)
            {
                lowRpmCorrectionValues =
                ExtractMedianCorrectionValuesForRpm(
                injectorData, lowRpmIndexes);
            }
            IList<double> mediumRpmCorrectionValues = new List<double>();
            if (mediumRpmIndexes.Count != 0)
            {
                mediumRpmCorrectionValues =
                ExtractMedianCorrectionValuesForRpm(
                injectorData, mediumRpmIndexes);
            }
            IList<double> highRpmCorrectionValues = new List<double>();
            if (highRpmIndexes.Count != 0)
            {
                highRpmCorrectionValues =
                ExtractMedianCorrectionValuesForRpm(
                injectorData, highRpmIndexes);
            }

            double totalMin = double.NaN;
            double totalMax = double.NaN;

            if (lowRpmCorrectionValues.Count != 0)
            {
                RangeInjectorCorrectionsChronoData resultComponent =
                    result.LowRpmRangeData.FirstOrDefault(
                    m => m.InjectorNumber == injectorNumber);
                if (resultComponent == null)
                {
                    resultComponent =
                        new RangeInjectorCorrectionsChronoData();
                    result.LowRpmRangeData.Add(resultComponent);
                }
                resultComponent.InjectorNumber = injectorNumber;
                resultComponent.Dates.Add(date);

                double min = lowRpmCorrectionValues.Min();
                double max = lowRpmCorrectionValues.Max();

                resultComponent.MaxValues.Add(max);
                resultComponent.MinValues.Add(min);

                totalMin = min;
                totalMax = max;
            }

            if (mediumRpmCorrectionValues.Count != 0)
            {
                RangeInjectorCorrectionsChronoData resultComponent =
                    result.MediumRpmRangeData.FirstOrDefault(
                    m => m.InjectorNumber == injectorNumber);
                if (resultComponent == null)
                {
                    resultComponent =
                        new RangeInjectorCorrectionsChronoData();
                    result.MediumRpmRangeData.Add(resultComponent);
                }
                resultComponent.InjectorNumber = injectorNumber;
                resultComponent.Dates.Add(date);
                double min = mediumRpmCorrectionValues.Min();
                double max = mediumRpmCorrectionValues.Max();
                resultComponent.MaxValues.Add(max);
                resultComponent.MinValues.Add(min);

                if (!double.IsNaN(totalMin))
                {
                    totalMin = Math.Min(totalMin, min);
                }
                else
                {
                    totalMin = min;
                }

                if (!double.IsNaN(totalMax))
                {
                    totalMax = Math.Max(totalMax, max);
                }
                else
                {
                    totalMax = max;
                }
            }

            if (highRpmCorrectionValues.Count != 0)
            {
                RangeInjectorCorrectionsChronoData resultComponent =
                    result.HighRpmRangeData.FirstOrDefault(
                    m => m.InjectorNumber == injectorNumber);
                if (resultComponent == null)
                {
                    resultComponent =
                        new RangeInjectorCorrectionsChronoData();
                    result.HighRpmRangeData.Add(resultComponent);
                }
                resultComponent.InjectorNumber = injectorNumber;
                resultComponent.Dates.Add(date);
                double max = highRpmCorrectionValues.Max();
                double min = highRpmCorrectionValues.Min();
                resultComponent.MaxValues.Add(max);
                resultComponent.MinValues.Add(min);

                if (!double.IsNaN(totalMin))
                {
                    totalMin = Math.Min(totalMin, min);
                }
                else
                {
                    totalMin = min;
                }

                if (!double.IsNaN(totalMax))
                {
                    totalMax = Math.Max(totalMax, max);
                }
                else
                {
                    totalMax = max;
                }
            }

            // get total range
            if (lowRpmIndexes.Count == 0 || mediumRpmIndexes.Count == 0 ||
                highRpmIndexes.Count == 0)
            {
                if (!double.IsNaN(totalMax) && !double.IsNaN(totalMin))
                {
                    RangeInjectorCorrectionsChronoData data =
                        result.RangeData.FirstOrDefault(
                        d => d.InjectorNumber == injectorNumber);
                    if (data == null)
                    {
                        data = new RangeInjectorCorrectionsChronoData();
                        result.RangeData.Add(data);
                    }
                    data.Dates.Add(date);
                    data.InjectorNumber = injectorNumber;
                    data.MaxValues.Add(totalMax);
                    data.MinValues.Add(totalMin);
                }
            }
        }

        private IList<double> ExtractMedianCorrectionValuesForRpm(
            PsaParameterData injectorDataset, IList<int> indexes)
        {
            IList<double> result = new List<double>();
            foreach (int index in indexes)
            {
                double d;
                double.TryParse(injectorDataset.Values[index],
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture, out d);
                result.Add(d);
            }
            return result;
        }

        private IList<int> ExtractRpmIndexes(
            PsaParameterData rpmDataSet, int valueBase)
        {
            IList<int> result = new List<int>();
            for (int i = 0; i < rpmDataSet.Values.Count; i++)
            {
                double value;
                double.TryParse(rpmDataSet.Values[i], NumberStyles.Float,
                    CultureInfo.InvariantCulture, out value);
                if (DeltaHelper.GetDeltaPercentage(value,
                    valueBase) <= RpmDifferencePercentage)
                {
                    result.Add(i);
                }
            }
            return result;
        }
    }
}
