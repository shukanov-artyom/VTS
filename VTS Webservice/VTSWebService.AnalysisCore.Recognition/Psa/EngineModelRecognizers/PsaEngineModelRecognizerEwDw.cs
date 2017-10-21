using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerEwDw : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerEwDw(EngineFamily family)
            : base(family)
        {

        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            string engineModelValue = characteristics.GetEngineModelString();
            string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (Ew7J4(engineModelValue))
            {
                result.Type = EngineType.EW7J4;
            }
            else if (engineModelValue.ToUpper().Contains("EW") &&
                engineModelValue.ToUpper().Contains("7A"))
            {
                result.Type = EngineType.EW7A;
            }
            else if (Ew10D(engineModelValue))
            {
                result.Type = EngineType.EW10D;
            }
            else if (Ew10J4Rfn(engineModelValue, generalInfoValue))
            {
                result.Type = EngineType.EW10J4RFN;
            }
            else if (Ew10J4Rfr(engineModelValue, generalInfoValue))
            {
                result.Type = EngineType.EW10J4RFR;
            }
            else if (Ew10J4S(engineModelValue))
            {
                result.Type = EngineType.EW10J4S;
            }
            else if (Ew10A(engineModelValue))
            {
                result.Type = EngineType.EW10A;
            }
            else if (Ew12J4(engineModelValue))
            {
                result.Type = EngineType.EW12J4;
            }
            else if (Dw8B(engineModelValue))
            {
                result.Type = EngineType.DW8B;
            }
            else if (Dw8(engineModelValue))
            {
                result.Type = EngineType.DW8;
            }
            else if (Dw10Ated4(engineModelValue))
            {
                // FIRST BEFORE ATED!!
                result.Type = EngineType.DW10ATED4;
            }
            else if (Dw10Ated(engineModelValue))
            {
                result.Type = EngineType.DW10ATED;
            }
            else if (Dw10Td(engineModelValue))
            {
                result.Type = EngineType.DW10TD;
            }
            else if (Dw10Bted4(engineModelValue))
            {
                result.Type = EngineType.DW10BTED4;
            }
            else if (engineModelValue.ToUpper().Contains("BTED"))
            {
                result.Type = EngineType.DW10BTED;
                result.DisplayName = "DW10 BTED";
                result.InjectionType = InjectionType.CommonRail;
                result.FuelType = FuelType.Diesel;
            }
            else if (engineModelValue.Contains("CTED4"))
            {
                result.Type = EngineType.DW10CTED4;
                result.DisplayName = "DW10 CTED4";
            }
            else if (Dw12Uted(engineModelValue))
            {
                result.Type = EngineType.DW12UTED;
            }
            else if (Dw12Mted4(engineModelValue))
            {
                result.Type = EngineType.DW12MTED4;
            }
            else if (Dw12Ted4(engineModelValue))
            {
                result.Type = EngineType.DW12TED4;
            }
            else if (engineModelValue.Contains("DW12C"))
            {
                result.Type = EngineType.DW12C;
                result.FuelType = FuelType.Diesel;
                result.InjectionType = InjectionType.CommonRail;
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            UpdateEngineDetails(result);
            return result;
        }

        private bool Dw12Mted4(string value)
        {
            return value.Contains("DW12") &&
                   value.Contains("MTED4");
        }

        private bool Dw12Ted4(string value)
        {
            return value.Contains("DW12") && value.Contains("TED4");
        }

        private bool Dw12Uted(string value)
        {
            return value.Contains("DW12") &&
                   value.Contains("UTED");
        }

        private bool Dw10Bted4(string value)
        {
            return value.Contains("DW10") &&
                value.Contains("BTED4");
        }

        private bool Dw10Td(string value)
        {
            return value.Contains("DW10TD");
        }

        private bool Dw10Ated4(string value)
        {
            return value.Contains("DW10") &&
                   value.Contains("ATED4");
        }
        private bool Dw10Ated(string value)
        {
            return value.Contains("DW10") &&
                   value.Contains("ATED");
        }

        private bool Dw8B(string value)
        {
            return value.Contains("DW8B");
        }

        private bool Dw8(string value)
        {
            return value.Contains("DW8");
        }

        private bool Ew7J4(string engineModelValue)
        {
            return engineModelValue.Contains("EW7") &&
                   engineModelValue.Contains("J4");
        }

        private bool Ew10D(string value)
        {
            return value.Contains("EW10D");
        }

        private bool Ew10J4Rfn(string engineModelValue, string generalInfo)
        {
            return engineModelValue.Contains("EW10") &&
                engineModelValue.Contains("J4") &&
                (generalInfo.Contains("136") || generalInfo.Contains("138"));
        }

        private bool Ew10J4Rfr(string engineModelValue, string generalInfo)
        {
            return engineModelValue.Contains("EW10") &&
                engineModelValue.Contains("J4") &&
                (generalInfo.Contains("140") || generalInfo.Contains("143"));
        }

        private bool Ew10J4S(string engineModelValue)
        {
            return engineModelValue.Contains("EW10J4S");
        }

        private bool Ew10A(string engineModelValue)
        {
            return engineModelValue.Contains("EW10A");
        }

        private bool Ew12J4(string modelValue)
        {
            return modelValue.Contains("EW12J4");
        }

        private void UpdateEngineDetails(Engine engine)
        {
            if (engine.Type == EngineType.EW7J4)
            {
                engine.DisplayName = "EW7 J4";
                engine.FuelType = FuelType.Petrol;
                engine.InjectionType = InjectionType.Injector;
            }
            else if (engine.Type == EngineType.EW10D)
            {
                engine.DisplayName = "EW10 D";
                engine.FuelType = FuelType.Petrol;
                engine.InjectionType = InjectionType.Direct;
            }
            else if (engine.Type == EngineType.EW10J4RFN ||
                engine.Type == EngineType.EW10J4RFR)
            {
                engine.DisplayName = engine.Type ==
                    EngineType.EW10J4RFN ? "EW10 J4 (RFN)" : "EW10 J4 (RFR)";
                engine.FuelType = FuelType.Petrol;
                engine.InjectionType = InjectionType.Injector;
            }
            else if (engine.Type == EngineType.EW10J4S)
            {
                engine.DisplayName = "EW10 J4 S";
                engine.FuelType = FuelType.Petrol;
                engine.InjectionType = InjectionType.Injector;
            }

            else if (engine.Type == EngineType.EW10A)
            {
                engine.DisplayName = "EW10 A";
                engine.FuelType = FuelType.Petrol;
                engine.InjectionType = InjectionType.Injector;
            }

            else if (engine.Type == EngineType.EW12J4)
            {
                engine.DisplayName = "EW12 J4";
                engine.FuelType = FuelType.Petrol;
                engine.InjectionType = InjectionType.Injector;
            }

            else if (engine.Type == EngineType.DW8B)
            {
                engine.DisplayName = "DW8 B";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.Indirect;
            }

            else if (engine.Type == EngineType.DW8)
            {
                engine.DisplayName = "DW8";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.Indirect;
            }
            else if (engine.Type == EngineType.DW10ATED4)
            {
                engine.DisplayName = "DW10 ATED4";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
            else if (engine.Type == EngineType.DW10ATED)
            {
                engine.DisplayName = "DW10 ATED";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
            else if (engine.Type == EngineType.DW10TD)
            {
                engine.DisplayName = "DW10 TD";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
            else if (engine.Type == EngineType.DW10C)
            {
                engine.DisplayName = "DW10 C";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
            else if (engine.Type == EngineType.DW10BTED4)
            {
                engine.DisplayName = "DW10 BTED4";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
            else if (engine.Type == EngineType.DW12UTED)
            {
                engine.DisplayName = "DW12 UTED";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
            else if (engine.Type == EngineType.DW12TED4)
            {
                engine.DisplayName = "DW12 TED4";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
            else if (engine.Type == EngineType.DW12MTED4)
            {
                engine.DisplayName = "DW12 MTED4";
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
            else if (engine.Type == EngineType.DW12C)
            {
                engine.DisplayName = "DW12 C";
            }
            else if (engine.Type == EngineType.DW10CTED4)
            {
                engine.FuelType = FuelType.Diesel;
                engine.InjectionType = InjectionType.CommonRail;
            }
        }
    }
}
