using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerXud : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerXud(EngineFamily family)
            : base(family)
        {

        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.FuelType = FuelType.Diesel;
            result.InjectionType = InjectionType.Indirect;
            result.Family = Family;
            string engineModelValue = characteristics.GetEngineModelString();
            string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (engineModelValue.Contains("XUD7"))
            {
                RecognizeXud7(engineModelValue, result);
            }
            else if (engineModelValue.Contains("XUD9"))
            {
                RecognizeXud9(engineModelValue, generalInfoValue, result);
            }
            else if (engineModelValue.Contains("XUD11"))
            {
                RecognizeXud11(engineModelValue, result);
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }

        private void RecognizeXud7(string val, Engine eng)
        {
            // Order is critical!
            if (val.Contains("TE"))
            {
                eng.DisplayName = "XUD7 TE";
                eng.Type = EngineType.XUD7TE;
            }
            else if (val.Contains("T"))
            {
                eng.DisplayName = "XUD7 T/K";
                eng.Type = EngineType.XUD7TK;
            }
            else if (val.Contains("K"))
            {
                eng.DisplayName = "XUD7/K";
                eng.Type = EngineType.XUD7K;
            }
            else if (val.Contains("Z"))
            {
                eng.DisplayName = "XUD7/Z";
                eng.Type = EngineType.XUD7Z;
            }
            else
            {
                throw new NotSupportedException(val);
            }
        }

        private void RecognizeXud9(string val, string general, Engine eng)
        {
            if (val.Contains("A"))
            {
                eng.DisplayName = "XUD9 A";
                eng.Type = EngineType.XUD9A;
            }
            else if (val.Contains("TE"))
            {
                if (general.Contains("90"))
                {
                    eng.DisplayName = "XUD9 TE/Y";
                    eng.Type = EngineType.XUD9TEY;
                }
                else
                {
                    eng.DisplayName = "XUD9 TE/L";
                    eng.Type = EngineType.XUD9TEL;
                }
            }
            else if (val.Contains("SD"))
            {
                eng.DisplayName = "XUD9 SD";
                eng.Type = EngineType.XUD9SD;
            }
            else if (val.Contains("Z"))
            {
                eng.DisplayName = "XUD9/Z";
                eng.Type = EngineType.XUD9Z;
            }
            else
            {
                eng.DisplayName = "XUD9";
                eng.Type = EngineType.XUD9;
            }
        }

        private void RecognizeXud11(string val, Engine eng)
        {
            if (val.Contains("ATE"))
            {
                eng.DisplayName = "XUD11 ATE";
                eng.Type = EngineType.XUD11ATE;
            }
            else if (val.Contains("BTE"))
            {
                eng.DisplayName = "XUD11 BTE";
                eng.Type = EngineType.XUD11BTE;
            }
            else if (val.Contains("A"))
            {
                eng.DisplayName = "XUD11 A";
                eng.Type = EngineType.XUD11A;
            }
            else
            {
                throw new NotSupportedException(val);
            }
        }
    }
}
