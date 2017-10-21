using System;
using System.Linq;
using VTS.AnalysisCore.Common;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Aggregation
{
    public class StandardAggregator : IAggregator
    {
        private AnalyticStatisticsItem item;

        public StandardAggregator(AnalyticStatisticsItem item)
        {
            this.item = item;
        }

        public AnalyticRuleSettings Aggregate()
        {
            ReliabilityEvaluator evaluator = new ReliabilityEvaluator(item);
            AnalyticRuleSettings result =
                new AnalyticRuleSettings(item.Type, evaluator.Evaluate());
            result.EngineType = item.TargetEngineType;
            result.EngineFamilyType = item.TargetEngineFamilyType;

            // 1. get median
            double M = item.GetDoubleValues().Average();

            // 2. Get Sigma
            double sigma = Sigma.Get(item.GetDoubleValues().ToList());

            // 3. Get a and b values 
            float a = StandardAggregatorSettings.OptimalBound;
            float b = StandardAggregatorSettings.MaxAcceptableBound;

            // 4. Get optimal bounds as M +/- a*sigma
            result.SettingsMolecule.StatisticalAtom.MinOptimal = M - a * sigma;
            result.SettingsMolecule.StatisticalAtom.MaxOptimal = M + a * sigma;

            // 5. Get acceptable bounds as M +/- b*sigma
            result.SettingsMolecule.StatisticalAtom.MinAcceptable = M - b * sigma;
            result.SettingsMolecule.StatisticalAtom.MaxAcceptable = M + b * sigma;

            return result;
        }
    }
}
