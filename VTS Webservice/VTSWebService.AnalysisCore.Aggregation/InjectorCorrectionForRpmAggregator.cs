using System;
using System.Linq;
using VTS.AnalysisCore.Common;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Aggregation
{
    public class InjectorCorrectionForRpmAggregator : IAggregator
    {
        private AnalyticStatisticsItem item;

        public InjectorCorrectionForRpmAggregator(AnalyticStatisticsItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            this.item = item;
        }

        public AnalyticRuleSettings Aggregate()
        {
            ReliabilityEvaluator evaluator = new ReliabilityEvaluator(item);
            AnalyticItemSettingsReliability reliability = evaluator.Evaluate();
            AnalyticRuleSettings result =
                new AnalyticRuleSettings(item.Type, reliability);
            result.EngineType = item.TargetEngineType;
            result.EngineFamilyType = item.TargetEngineFamilyType;

            // 1. Get math expectation
            double m = item.GetDoubleValues().Average();
            // 2. Get Sigma
            double sigma = Sigma.Get(item.GetDoubleValues().ToList());
            float a = StandardAggregatorSettings.OptimalBound;
            float b = StandardAggregatorSettings.MaxAcceptableBound;

            result.SettingsMolecule.StatisticalAtom.MinOptimal = 0;
            // 3. Get optimal bound as M + A*Sigma
            result.SettingsMolecule.StatisticalAtom.
                MaxOptimal = m + a * sigma;
            // 4. Get acceptable bound as M + B*Sigma
            result.SettingsMolecule.StatisticalAtom.
                MaxAcceptable = m + b * sigma;
            result.SettingsMolecule.StatisticalAtom.MinAcceptable = 0;
            return result;
        }
    }
}
