using System;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Statistics.Tools
{
    public static class ModelRelatedPsaParameters
    {
        public static PsaParameterType Get(int cylinderNumber)
        {
            switch (cylinderNumber)
            {
                case 1:
                    return PsaParameterType.CylinderCoilChargeTime1;
                case 2:
                    return PsaParameterType.CylinderCoilChargeTime2;
                case 3:
                    return PsaParameterType.CylinderCoilChargeTime3;
                case 4:
                    return PsaParameterType.CylinderCoilChargeTime4;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
