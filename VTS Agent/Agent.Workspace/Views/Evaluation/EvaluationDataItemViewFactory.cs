using System;
using Agent.Common.Presentation;
using Agent.Workspace.ViewModels.Evaluation;

namespace Agent.Workspace.Views.Evaluation
{
    internal class EvaluationDataItemViewFactory
    {
        public static IEvaluationDataItemView Create(ViewModelBase viewModel)
        {
            if (viewModel is VehicleEvaluationRootFolderViewModel)
            {
                return null;
            }
            if (viewModel is EvaluationFolderViewModel)
            {
                return null;
            }
            if (viewModel is RuleEvaluationChronologyViewModel)
            {
                IEvaluationDataItemView result = new VehicleEvaluationDataItemControl();
                result.DataContext = viewModel;
                return result;
            }
            throw new NotSupportedException("item type is not supported.");
        }
    }
}
