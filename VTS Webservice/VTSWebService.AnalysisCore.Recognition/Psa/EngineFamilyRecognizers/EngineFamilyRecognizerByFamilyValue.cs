using System;
using VTS.Shared;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineFamilyRecognizers
{
    internal class EngineFamilyRecognizerByFamilyValue
    {
        private const string EwDwSign = @"EW/DW";
        private const string DvSign = @"EV/DV";
        private const string PrinceSign = "EP";
        private const string XuXudSign = @"XU/XUD";
        private const string XuSign = "XU";
        private const string XudSign = "XUD";
        private const string TuTudSign = @"TU/TUD";
        private const string TuSign = @"TU";
        private const string TudSign = @"TUD";
        private const string EsSign = "ES";
        private const string EtSign = "ET";
        private const string DkSign = "DK";
        private const string EbSign = "EB";
        private const string EcSign = "EC";

        private string familyValue;
        private string modelValue;

        public EngineFamilyRecognizerByFamilyValue(
            string familyValue, string modelValue)
        {
            if (string.IsNullOrEmpty(familyValue))
            {
                throw new ArgumentNullException("familyValue");
            }
            this.familyValue = familyValue;
            this.modelValue = modelValue;
        }

        public EngineFamilyType Recognize()
        {
            // EW/DW
            if (familyValue.Contains(EwDwSign))
            {
                return EngineFamilyType.EWDW;
            }

            // DV
            if (familyValue.Contains(DvSign) ||
                familyValue.ToUpper().Contains("ENGINE FORD") || 
                familyValue.ToUpper().Contains("DV TYPE ENGINE"))
            {
                return EngineFamilyType.DV;
            }

            // ET - parsed by TU engines
            if (familyValue.Contains(EtSign) ||
                familyValue.Contains(TuSign))
            {
                return EngineFamilyType.TU;
            }

            // Prince
            if (familyValue.Contains(PrinceSign))
            {
                return EngineFamilyType.Prince;
            }

            // XU/XUD
            if (familyValue.Contains(XuXudSign))
            {
                if (modelValue.Contains(XudSign))
                {
                    return EngineFamilyType.XUD;
                }
                if (modelValue.Contains(XuSign))
                {
                    return EngineFamilyType.XU;
                }
                throw new NotSupportedException();
            }

            // TU - EQUALS should be here!
            if (String.Equals(familyValue, TuSign))
            {
                return EngineFamilyType.TU;
            }

            // TU/TUD
            if (familyValue.Contains(TuTudSign))
            {
                if (modelValue.Contains(TudSign))
                {
                    return EngineFamilyType.TUD;
                }
                if (modelValue.Contains(TuSign))
                {
                    return EngineFamilyType.TU;
                }
                throw new NotSupportedException();
            }

            // ES
            if (String.Equals(familyValue, EsSign))
            {
                return EngineFamilyType.ES;
            }
            // DK
            if (familyValue.ToUpper().Contains(DkSign.ToUpper()))
            {
                return EngineFamilyType.DK;
            }

            // EB
            if (familyValue.ToUpper().Contains(EbSign.ToUpper()))
            {
                return EngineFamilyType.EB;
            }

            // EC
            if (familyValue.ToUpper().Contains(EcSign.ToUpper()))
            {
                return EngineFamilyType.EC;
            }

            //DT
            if (familyValue.ToUpper().Contains("DT"))
            {
                return EngineFamilyType.DT;
            }

            // -- old and not very much supported live here
            // Most likely we wil not support concrete engines, only their families

            // Douvrin,
            // X,
            // EC,
            // PRV
            // XB,
            // XC,
            // XD,
            // XM,
            // XN,

            throw new NotSupportedException(String.Format("Family {0} is unknown", familyValue));
        }
    }
}
