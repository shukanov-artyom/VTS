using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Tools;
using VTSWeb.Chrono.Psa;
using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.Chrono.Factories.Psa.InjectorsCorrections
{
    internal class MedianDataFactory
    {
        // TODO : Refactor for CorrelatedMedianExtractor
        private const int LowRpm = 1000;
        private const int MediumRpm = 2000;
        private const int HighRpm = 3000;
        private const int RpmDifferencePercentage = 5;

        private PsaParameterData rpmData;
        private PsaParameterData injectorData;
        private int injectorNumber;

        public MedianDataFactory(
            PsaParameterData rpmData,
            PsaParameterData injectorData,
            int injectorNumber)
        {
            this.rpmData = rpmData;
            this.injectorData = injectorData;
            this.injectorNumber = injectorNumber;
        }

        public void Generate(
            ChronoParamInjectorCorrections result, DateTime date)
        {
            IList<int> lowRpmIndexes = ExtractRpmIndexes(rpmData, LowRpm);
            IList<int> mediumRpmIndexes = ExtractRpmIndexes(rpmData, MediumRpm);
            IList<int> highRpmIndexes = ExtractRpmIndexes(rpmData, HighRpm);

            double lowRpmCorrectionValue = double.NaN;
            if (lowRpmIndexes.Count != 0)
            {
                lowRpmCorrectionValue =
                ExtractMedianCorrectionValueForRpm(
                injectorData, lowRpmIndexes);
            }
            
            double mediumRpmCorrectionValue = double.NaN;
            if (mediumRpmIndexes.Count != 0)
            {
                mediumRpmCorrectionValue =
                ExtractMedianCorrectionValueForRpm(
                injectorData, mediumRpmIndexes);
            }
            double highRpmCorrectionValue = double.NaN;
            if (highRpmIndexes.Count != 0)
            {
                highRpmCorrectionValue =
                 ExtractMedianCorrectionValueForRpm(
                 injectorData, highRpmIndexes); 
            }

            double totalMediumValue = double.NaN;
            if (lowRpmIndexes.Count != 0 || mediumRpmIndexes.Count != 0 ||
                highRpmIndexes.Count != 0)
            {
                totalMediumValue = (lowRpmCorrectionValue + 
                    mediumRpmCorrectionValue + highRpmCorrectionValue)/3;
            }

            // check whether any data has been generated and push it to result
            // for low rpm
            if (!double.IsNaN(lowRpmCorrectionValue))
            {
                MedianInjectorCorrectionsChronoData medianResultComponent =
                    result.LowRpmMedianData.FirstOrDefault(
                    m => m.InjectorNumber == injectorNumber);
                if (medianResultComponent == null)
                {
                    medianResultComponent =
                        new MedianInjectorCorrectionsChronoData();
                    medianResultComponent.InjectorNumber = injectorNumber;
                    result.LowRpmMedianData.Add(medianResultComponent);
                }
                medianResultComponent.Dates.Add(date);
                medianResultComponent.Values.Add(lowRpmCorrectionValue);
            }

            // for medium rpm
            if (!double.IsNaN(mediumRpmCorrectionValue))
            {
                MedianInjectorCorrectionsChronoData medianResultComponent =
                    result.MediumRpmMedianData.FirstOrDefault(
                    m => m.InjectorNumber == injectorNumber);
                if (medianResultComponent == null)
                {
                    medianResultComponent =
                        new MedianInjectorCorrectionsChronoData();
                    result.MediumRpmMedianData.Add(medianResultComponent);
                    medianResultComponent.InjectorNumber = injectorNumber;
                }
                medianResultComponent.Dates.Add(date);
                medianResultComponent.Values.Add(mediumRpmCorrectionValue);
            }

            // for high rpm
            if (!double.IsNaN(highRpmCorrectionValue))
            {
                MedianInjectorCorrectionsChronoData medianResultComponent =
                    result.HighRpmMedianData.FirstOrDefault(
                    m => m.InjectorNumber == injectorNumber);
                if (medianResultComponent == null)
                {
                    medianResultComponent =
                        new MedianInjectorCorrectionsChronoData();
                    medianResultComponent.InjectorNumber = injectorNumber;
                    result.HighRpmMedianData.Add(medianResultComponent);
                }
                medianResultComponent.Dates.Add(date);
                medianResultComponent.Values.Add(highRpmCorrectionValue);
            }

            // for total medium value
            if (!double.IsNaN(totalMediumValue))
            {
                MedianInjectorCorrectionsChronoData medianResultComponent =
                    result.MedianData.FirstOrDefault(
                    m => m.InjectorNumber == injectorNumber);
                if (medianResultComponent == null)
                {
                    medianResultComponent =
                        new MedianInjectorCorrectionsChronoData();
                    result.MedianData.Add(medianResultComponent);
                    medianResultComponent.InjectorNumber = injectorNumber;
                }
                medianResultComponent.Dates.Add(date);
                medianResultComponent.Values.Add(totalMediumValue);
            }
        }

        private double ExtractMedianCorrectionValueForRpm(
            PsaParameterData injectorDataParam, IList<int> indexes)
        {
            IList<double> line = new List<double>();
            foreach (int index in indexes)
            {
                double d;
                double.TryParse(injectorDataParam.Values[index],
                    NumberStyles.Float, 
                    CultureInfo.InvariantCulture, out d);
                line.Add(d);
            }
            double result;
            if (line.Count != 0)
            {
                result = line.Average();
            }
            else
            {
                result = double.NaN;
            }
            return result;
        }

        private IList<int> ExtractRpmIndexes(
            PsaParameterData rpmDataValues, int valueBase)
        {
            IList<int> result = new List<int>();
            for (int i = 0; i < rpmDataValues.Values.Count; i++)
            {
                double value;
                double.TryParse(rpmDataValues.Values[i], NumberStyles.Float, 
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
