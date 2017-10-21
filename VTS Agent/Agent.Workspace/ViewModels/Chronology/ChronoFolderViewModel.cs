using System;
using System.Collections.ObjectModel;
using System.Linq;
using Agent.Common.Presentation;
using Agent.Localization;
using VTS.Shared;

namespace Agent.Workspace.ViewModels.Chronology
{
    public class ChronoFolderViewModel : ViewModelBase
    {
        private readonly string folderPathKey;
        private readonly ObservableCollection<ViewModelBase> children = 
            new ObservableCollection<ViewModelBase>();

        private readonly string vin;

        public ChronoFolderViewModel(string folderPathKey, 
            string vin)
        {
            this.vin = vin;
            this.folderPathKey = folderPathKey;
        }

        public ObservableCollection<ViewModelBase> Children
        {
            get
            {
                return children;
            }
        }

        public string Key
        {
            get
            {
                return folderPathKey;
            }
        }

        public string Summary
        {
            get
            {
                return CodeBehindStringResolver.Resolve(
                    String.Format("AnalyticItemDisplayNameAnalyticModel{0}", Key));
            }
        }

        public void PutParameter(string[] path, AnalyticRuleType type)
        {
            if (path.Length == 0)
            {
                children.Add(new AnalyticRuleStatsPerVehicleViewModel(type, vin));
            }
            else
            {
                if (!children.Any(f => 
                    f is ChronoFolderViewModel &&
                    ((ChronoFolderViewModel)f).Key == path[0]))
                {
                    ChronoFolderViewModel newFolder =
                        new ChronoFolderViewModel(path[0], vin);
                    newFolder.PutParameter(GetSubPath(path), type);
                    children.Add(newFolder);
                }
                else
                {
                    ChronoFolderViewModel folder =
                        children.First(f => 
                            f is ChronoFolderViewModel && 
                            ((ChronoFolderViewModel)f).Key == path[0]) as ChronoFolderViewModel;
                    folder.PutParameter(GetSubPath(path), type);
                }
            }
            OnPropertyChanged("Children");
        }

        private string[] GetSubPath(string[] path)
        {
            string[] subpath = new string[path.Length - 1];
            Array.Copy(path, 1, subpath, 0, path.Length - 1);
            return subpath;
        }

        protected override void ChangeLanguage()
        {
            base.ChangeLanguage();
            OnPropertyChanged("Summary");
        }
    }
}
