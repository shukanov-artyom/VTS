using System;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.Common;

namespace VTSWeb.AnalysisCore.Models.Settings.CommonRail.Persistency
{
    public class AnalyticModelSettingsPersistencyObjectCommonRail
    {
        public delegate void SuccessfullyFetchedCommonRailModelSettings(
            AnalyticModelSettingsCommonRail settings);

        private SuccessfullyFetchedCommonRailModelSettings successCallback;
        private ErrorCallbackDelegate errorCallback;

        public AnalyticModelSettingsPersistencyObjectCommonRail(
            SuccessfullyFetchedCommonRailModelSettings successCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.successCallback = successCallback;
        }

        private void Retrieve(Engine engine)
        {
            
        }

        private void Persist()
        {
            
        }
    }
}
