using System;
using Agent.Common.Presentation;
using Agent.Workspace.ViewModels.Chronology;

namespace Agent.Workspace.Views.Chrono
{
    public class ChronologicalDataItemViewFactory
    {
        private readonly ViewModelBase item;

        public ChronologicalDataItemViewFactory(ViewModelBase item)
        {
            if (item is ChronoFolderViewModel || item is AnalyticRuleStatsPerVehicleViewModel)
            {
                this.item = item;
            }
            else
            {
                throw new NotSupportedException("This object type is not supported");
            }
        }

        public IChronologicalDataItemView Create()
        {
            ChronoFolderViewModel folder = item as ChronoFolderViewModel;
            if (folder != null)
            {
                return new ChronologicalFolderControl();
            }
            AnalyticRuleStatsPerVehicleViewModel statsItem = 
                item as AnalyticRuleStatsPerVehicleViewModel;
            if (statsItem != null)
            {
                return new ChronologicalItemControl();
            }
            throw new NotSupportedException("Item type is not supported.");
        }
    }
}
