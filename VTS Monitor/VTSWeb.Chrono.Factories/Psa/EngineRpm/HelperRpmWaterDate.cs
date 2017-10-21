using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.Chrono.Common;
using VTSWeb.DomainObjects.Psa;


namespace VTSWeb.Chrono.Factories.Psa.EngineRpm
{
    /// <summary>
    /// Helper class to keep information;
    /// </summary>
    internal class HelperRpmWaterDate
    {
        /// <summary>
        /// По достижении этой температуры в градусах цельсия 
        /// двигатель уже не считается холодным.
        /// Это нижняя граница средней прогретости двигателя.
        /// </summary>
        public const double MediumTemperatureThreshold = 30.0;

        /// <summary>
        /// После этой температуры двигатель считается прогретым.
        /// </summary>
        public const double FullTemperatureThreshold = 55.0;

        private IList<double> rpmValues =
            new List<double>();
        public IList<double> waterTempValues =
            new List<double>();

        private TemperatureClass temperatureClass;

        public HelperRpmWaterDate(PsaParameterData rpmData,
            PsaParameterData waterTempData,
            DateTime date)
        {
            Date = date;
            if (rpmData.Type != PsaParameterType.EngineRpm)
            {
                throw new ArgumentException("Engine RPM parameter expected!");
            }
            if (waterTempData.Type != PsaParameterType.WaterTemperature &&
                waterTempData.Type != 
                PsaParameterType.EngineCoolantTemperature)
            {
                throw new ArgumentException(
                    "EngineCoolant temperature parameter expected!");
            }
            foreach (string s in rpmData.Values)
            {
                rpmValues.Add(Double.Parse(s, NumberStyles.Float,
                    CultureInfo.InvariantCulture));
            }
            foreach (string s in waterTempData.Values)
            {
                waterTempValues.Add(Double.Parse(s, NumberStyles.Float,
                    CultureInfo.InvariantCulture));
            }
            temperatureClass = DetermineTemperatureClass(waterTempValues);
        }

        public TemperatureClass TemperatureClass
        {
            get
            {
                return temperatureClass;
            }
        }

        public DateTime Date
        {
            get;
            set;
        }

        public double Value
        {
            get
            {
                return Math.Round(GenerateValue(rpmValues), 0);
            }
        }

        private static TemperatureClass DetermineTemperatureClass(
            IList<double> waterTempValues)
        {
            IList<double> tempList = new List<double>();
            for (int i = 0; i < waterTempValues.Count / 3; i++)
            {
                tempList.Add(waterTempValues[i]);
            }
            double median = GetMedian(tempList);
            if (median < MediumTemperatureThreshold)
            {
                return TemperatureClass.Cold;
            }
            if (median >= MediumTemperatureThreshold &&
                median < FullTemperatureThreshold)
            {
                return TemperatureClass.Medium;
            }
            if (median >= FullTemperatureThreshold)
            {
                return TemperatureClass.Hot;
            }
            throw new ArgumentException("Incorrect water temperature data");
        }

        private static double GetMedian(IList<double> vals)
        {
            double sum = 0;
            foreach (double val in vals)
            {
                sum += val;
            }
            return sum / vals.Count;
        }

        private double GenerateValue(IList<double> data)
        {
            IList<double> initialLine =
                IdleRpmValueExtractor.ExtractInitialIdleLine(data);
            if (initialLine.Count > 0)
            {
                return initialLine.Average();
            }
            return 0;
        }
    }
}
