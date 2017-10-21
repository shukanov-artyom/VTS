using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerXu : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerXu(EngineFamily family)
            : base(family)
        {

        }

        public override Engine Recognize(
            VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            result.FuelType = FuelType.Petrol;
            result.InjectionType = InjectionType.Injector;
            string engineModelValue = characteristics.GetEngineModelString();
            string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (engineModelValue.Contains("XU5"))
            {
                RecognizeXu5(engineModelValue, result);
            }
            else if (engineModelValue.Contains("XU7"))
            {
                RecognizeXu7(engineModelValue, result);
            }
            else if (engineModelValue.Contains("XU8"))
            {
                RecognizeXu8(engineModelValue, result);
            }
            else if (engineModelValue.Contains("XU9"))
            {
                RecognizeXu9(engineModelValue, generalInfoValue, result);
            }
            else if (engineModelValue.Contains("XU10"))
            {
                RecognizeXu10(engineModelValue, generalInfoValue, result);
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }

        /// <summary>
        /// http://en.wikipedia.org/wiki/PSA_XU#XU5
        /// </summary>
        /// VF34BBDY270884206
        private void RecognizeXu5(string val, Engine result)
        {
            result.FuelType = FuelType.Petrol;
            result.InjectionType = InjectionType.Injector;
            if (val.Contains("J"))
            {
                result.DisplayName = "XU5J";
                result.Type = EngineType.XU5J;
            }
            else if (val.Contains("1C"))
            {
                result.DisplayName = "XU51C";
                result.Type = EngineType.XU51C;
            }
            else if (val.Contains("2C"))
            {
                result.DisplayName = "XU52C";
                result.Type = EngineType.XU52C;
            }
            else if (val.Contains("C"))
            {
                result.DisplayName = "XU5C";
                result.Type = EngineType.XU5C;
            }
            else if (val.Contains("M") || val.Contains("Z"))
            {
                result.DisplayName = @"XU5M3/Z";
                result.Type = EngineType.XU5M3Z;
            }
            else
            {
                throw new NotSupportedException(val);
            }
        }

        /// <summary>
        /// TO TEST: VF38ELFYE80861140
        /// </summary>
        private void RecognizeXu7(string val, Engine result)
        {
            result.FuelType = FuelType.Petrol;
            result.InjectionType = InjectionType.Injector;
            if (val.Contains("JB") || val.Contains("LFX"))
            {
                result.DisplayName = "XU7 JB/LFX";
                result.Type = EngineType.XU7JB;
            }
            else if (val.Contains("LFW"))
            {
                result.DisplayName = "XU7 LFW";
                result.Type = EngineType.XU7LFW;
            }
            else if (val.Contains("JP4") || val.Contains("LFZ"))
            {
                result.DisplayName = "XU7 JP4/LFZ";
                result.Type = EngineType.XU7JP4;
            }
            else if (val.Contains("JP") || val.Contains("LFY"))
            {
                result.DisplayName = "XU7 JP";
                result.Type = EngineType.XU7JP;
            }
            else
            {
                throw new NotSupportedException(val);
            }
        }

        private void RecognizeXu8(string val, Engine result)
        {
            if (val.Contains("T"))
            {
                result.DisplayName = "XU8 T300";
                result.Type = EngineType.XU8T300;
                // Have taken average
                // TODO: make it more strong
            }
            else
            {
                throw new NotSupportedException(val);
            }
        }

        /// <summary>
        /// http://en.wikipedia.org/wiki/PSA_XU#XU9
        /// </summary>
        private void RecognizeXu9(string val, string generalInfo, Engine result)
        {
            if (val.Contains("2C"))
            {
                if (generalInfo.Contains("110"))
                {
                    result.DisplayName = "XU9 2C (110)";
                    result.Type = EngineType.XU92C110;
                }
                else
                {
                    result.DisplayName = "XU9 2C (105)";
                    // TODO: make it more strict
                    result.Type = EngineType.XU92C105;
                }
            }
            else if (val.Contains("4C"))
            {
                result.DisplayName = "XU9 4C";
                result.Type = EngineType.XU94C;
            }
            else if (val.Contains("J1"))
            {
                result.DisplayName = "XU9 J1/Z";
                result.Type = EngineType.XU9J1Z;
            }
            else if (val.Contains("J2"))
            {
                result.DisplayName = "XU9 J2";
                result.Type = EngineType.XU9J2;
            }
            else if (val.Contains("JA"))
            {
                if (generalInfo.Contains("130"))
                {
                    result.DisplayName = "XU9 JA/K";
                    result.Type = EngineType.XU9JAK;
                }
                else
                {
                    result.DisplayName = "XU9 JA/Z";
                    result.Type = EngineType.XU9JAZ;
                }
            }
            else if (val.Contains("J4"))
            {
                if (generalInfo.Contains("160"))
                {
                    result.DisplayName = "XU9 J4 (D6C/L, Mi6)";
                    result.Type = EngineType.XU9J4D6CL;
                }
                else
                {
                    result.DisplayName = "XU9 J4/Z (DFW, Mi6)";
                    result.Type = EngineType.XU9J4ZDFW;
                }
            }
            else
            {
                throw new NotSupportedException(val);
            }
        }

        /// <summary>
        /// VF36DRFNB21203202
        /// </summary>
        private void RecognizeXu10(string val, string generalInfo, Engine result)
        {
            if (val.Contains("2C"))
            {
                result.DisplayName = "XU10 2C";
                result.Type = EngineType.XU102C;
            }
            else if (val.Contains("J2C"))
            {
                result.DisplayName = "XU10 J2C/L/RFX";
                result.Type = EngineType.XU102C;
            }
            else if (val.Contains("J2U"))
            {
                result.DisplayName = "XU10 J2U (RFW/RFL)";
                result.Type = EngineType.XU10J2U;
            }
            else if (val.Contains("J2"))
            {
                result.DisplayName = "XU10 J2";
                result.Type = EngineType.XU10J2;
            }
            else if (val.Contains("J2TE"))
            {
                if (generalInfo.Contains("141"))
                {
                    result.DisplayName = "XU10 J2TE/RGY";
                    result.Type = EngineType.XU10J2TERGY;
                }
                else
                {
                    result.DisplayName = "XU10 J2TE/RGX";
                    result.Type = EngineType.XU10J2TERGX;
                }
            }
            else if (val.Contains("J4D"))
            {
                result.DisplayName = "XU10 J4D/Z/RFT";
                result.Type = EngineType.XU10J4DZ;
            }
            else if (val.Contains("J4RS"))
            {
                result.DisplayName = "XU10 J4RS/L3/RFS";
                result.Type = EngineType.XU10J4RSL3FS;
            }
            else if (val.Contains("J4R"))
            {
                result.DisplayName = "XU10 J4R/L3/RFV";
                result.Type = EngineType.XU10J4RL3RFV;
            }
            else if (val.Contains("J4TE"))
            {
                result.DisplayName = "XU10 J4TE";
                result.Type = EngineType.XU10J4TE;
            }
            else if (val.Contains("J4"))
            {
                result.DisplayName = "XU10 J4/L/Z/RFY (Mi16)";
                result.Type = EngineType.XU10J4LZRFY;
            }
            else if (val.Contains("M"))
            {
                result.DisplayName = "XU10 M";
                result.Type = EngineType.XU10M;
            }
            else
            {
                throw new NotSupportedException(val);
            }
        }
    }
}
