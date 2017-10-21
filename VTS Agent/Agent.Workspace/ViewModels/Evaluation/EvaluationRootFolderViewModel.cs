using System;
using Agent.Common.Presentation.Vehicles;
using Agent.Evaluation;

namespace Agent.Workspace.ViewModels.Evaluation
{
    public class EvaluationRootFolderViewModel : EvaluationFolderViewModelBase
    {
        public EvaluationRootFolderViewModel(VehicleViewModel vehicle)
            : base("root", vehicle.Vin.ToUpper())
        {
            Children.Add(new VehicleEvaluationRootFolderViewModel(
                EvaluationCache.Get(vehicle.Vin), vehicle));
        }
    }
}
