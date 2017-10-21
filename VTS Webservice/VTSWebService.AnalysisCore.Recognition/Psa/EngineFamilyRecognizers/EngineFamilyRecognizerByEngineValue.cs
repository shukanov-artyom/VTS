using System;
using VTS.Shared;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineFamilyRecognizers
{
    internal class EngineFamilyRecognizerByEngineValue
    {
        private string value;

        public EngineFamilyRecognizerByEngineValue(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }
            this.value = value;
        }

        public EngineFamilyType Recognize()
        {
            // --- Known strings: 
            // XUD11ATE
            // CARBURETTOR TU9
            // DIESEL XUD11A
            // INJECTION TU3M
            // XU9J2
            // XU52C

            if (value.Contains("XU9") ||
                value.Contains("XU5") ||
                value.Contains("XU7") ||
                value.Contains("XU8") ||
                value.Contains("XU10"))
            {
                return EngineFamilyType.XU;
            }
            if (value.Contains("XUD7") ||
                value.Contains("XUD9") ||
                value.Contains("XUD11"))
            {
                return EngineFamilyType.XUD;
            }
            if (value.Contains("TU1") ||
                value.Contains("TU2") ||
                value.Contains("TU3") ||
                value.Contains("TU5") ||
                value.Contains("ET3") ||
                value.Contains("TU9"))
            {
                return EngineFamilyType.TU;
            }

            // ========================================
            // --- Unknown is unknown, but we can guess

            if (value.Contains("EW7") ||
                value.Contains("EW10") ||
                value.Contains("EW12") ||
                value.Contains("DW8") ||
                value.Contains("DW10") ||
                value.Contains("DW12"))
            {
                return EngineFamilyType.EWDW;
            }

            // Toyota small engines
            if (value.ToUpper().Contains("384F"))
            {
                return EngineFamilyType.KR;
            }
            if (value.ToUpper().Contains("MMC"))
            {
                return EngineFamilyType.MMC;
            }
            if (value.ToUpper().Contains("DV"))
            {
                return EngineFamilyType.DV;
            }

            // TODO: throw custom exception 
            throw new NotSupportedException(String.Format(
                "The following Engine Family value has not been recognized: {0}",
                value));
        }
    }
}
