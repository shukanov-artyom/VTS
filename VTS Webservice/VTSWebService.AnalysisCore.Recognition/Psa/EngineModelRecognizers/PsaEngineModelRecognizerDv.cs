using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerDv : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerDv(EngineFamily family)
            : base(family)
        {
        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            result.FuelType = FuelType.Diesel;
            result.InjectionType = InjectionType.CommonRail;
            string engineModelValue = characteristics.GetEngineModelString();
            if (Dv4Td(engineModelValue))
            {
                result.Type = EngineType.DV4TD;
                result.DisplayName = "DV4 TD (DLD-414)";
            }
            else if (Dv4Ted4(engineModelValue))
            {
                result.Type = EngineType.DV4TED4;
                result.DisplayName = "DV4 TED4 (DLD-414)";
            }
            else if (Dv6Ated4(engineModelValue))
            {
                result.Type = EngineType.DV6ATED4;
                result.DisplayName = "DV6 ATED4 (DLD-416)";
            }
            else if (engineModelValue.ToUpper().Contains("DV4") &&
                     engineModelValue.ToUpper().Contains("TED"))
            {
                result.Type = EngineType.DV4TED;
                result.DisplayName = "DV4TED (8HS)";
            }
            else if (Dv6b(engineModelValue))
            {
                result.Type = EngineType.DV6B;
                result.DisplayName = "DV6 B (DLD-416)";
            }
            else if (Dv6c(engineModelValue))
            {
                result.Type = EngineType.DV6C;
                result.DisplayName = "DV6 C (DLD-416)";
            }
            else if (Dv6d(engineModelValue))
            {
                result.Type = EngineType.DV6D;
                result.DisplayName = "DV6 D (DLD-416)";
            }
            else if (Dv6Ted4(engineModelValue))
            {
                result.Type = EngineType.DV6TED4;
                result.DisplayName = "DV6 TED4 (DLD-416)";
            }
            else if (engineModelValue.ToUpper().Contains("DV4C"))
            {
                result.Type = EngineType.DV4C;
                result.DisplayName = "DV4C";
            }
            else if (engineModelValue.ToUpper().Contains("PUMA"))
            {
                result.InjectionType = InjectionType.Direct;
                if (engineModelValue.IndexOf("20DT",
                    StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    result.Type = EngineType.ZSD420;
                    result.DisplayName = "ZSD-420 (Duratorq TDCi)";
                }
                else if (engineModelValue.IndexOf("22DT",
                    StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    result.Type = EngineType.ZSD422;
                    result.DisplayName = "ZSD-422 (Duratorq TDCi)";
                }
                else if (engineModelValue.IndexOf("24DT",
                    StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    result.Type = EngineType.ZSD424;
                    result.DisplayName = "ZSD-424 (Duratorq TDCi)";
                }
            }
            return result;
        }

        private bool Dv4Td(string modelValue)
        {
            return modelValue.Contains("DV4") &&
                    modelValue.Contains("TD");
        }

        private bool Dv4Ted4(string modelValue)
        {
            return modelValue.Contains("DV4") &&
                    modelValue.Contains("TED4");
        }

        private bool Dv6Ated4(string modelValue)
        {
            return modelValue.Contains("DV6") &&
                   modelValue.Contains("ATED4");
        }

        private bool Dv6Ted4(string modelValue)
        {
            return modelValue.Contains("DV6") &&
                   modelValue.Contains("TED4");
        }

        private bool Dv6b(string modelValue)
        {
            return modelValue.Contains("DV6B");
        }

        private bool Dv6c(string modelValue)
        {
            return modelValue.Contains("DV6C");
        }

        private bool Dv6d(string modelValue)
        {
            return modelValue.Contains("DV6D");
        }
    }
}
