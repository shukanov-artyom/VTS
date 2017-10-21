using System;
using VTS.AnalysisCore.Common;

namespace VTSWebService.AnalysisCore.Recognition
{
    public interface IEngineRecognizer
    {
        Engine Recognize();
    }
}
