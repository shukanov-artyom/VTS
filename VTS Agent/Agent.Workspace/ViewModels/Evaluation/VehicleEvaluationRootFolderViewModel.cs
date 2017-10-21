using System;
using System.Collections.Generic;
using Agent.Common.Instance;
using Agent.Common.Presentation.Vehicles;
using Agent.Evaluation;
using VTS.Shared;

namespace Agent.Workspace.ViewModels.Evaluation
{
    public class VehicleEvaluationRootFolderViewModel : EvaluationFolderViewModelBase
    {
        private readonly VehicleViewModel vehicle;
        private readonly VehicleEvaluationGrid grid;

        public VehicleEvaluationRootFolderViewModel(
            VehicleEvaluationGrid grid,
            VehicleViewModel vehicle)
            : base("vehicle-root", vehicle.Vin)
        {
            this.grid = grid;
            this.vehicle = vehicle;
            InitializeChildren();
        }

        public new string Summary
        {
            get
            {
                return vehicle.Summary;
            }
        }

        private void InitializeChildren()
        {
            List<AnalyticRuleType> types = AnalyticRuleSettingsCache.GetAvailableTypes(vehicle.Vin);
            foreach (AnalyticRuleType ruleType in types)
            {
                RuleTypeTreePathResolver resolver =
                    new RuleTypeTreePathResolver(ruleType);
                PutParameter(resolver.GetSplitPath(), ruleType);
            }
        }
    }
}
