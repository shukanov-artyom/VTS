using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Agent.Common.Presentation;
using Agent.Evaluation;
using Agent.Localization;
using VTS.Shared;

namespace Agent.Workspace.ViewModels.Evaluation
{
    public abstract class EvaluationFolderViewModelBase : ViewModelBase
    {
        private readonly string folderPathKey;
        private readonly string vin;
        private readonly ObservableCollection<ViewModelBase> children =
            new ObservableCollection<ViewModelBase>();
        private double summaryMark;

        public EvaluationFolderViewModelBase(string folderPathKey, string vin)
        {
            this.vin = vin.ToUpper();
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

        public double SummaryMark
        {
            get
            {
                return summaryMark;
            }
        }

        protected virtual void RefreshSummaryMark()
        {
            List<double> marks = new List<double>();
            foreach (ViewModelBase child in Children)
            {
                EvaluationFolderViewModel folder = child as EvaluationFolderViewModel;
                if (null != folder)
                {
                    marks.Add(folder.summaryMark);
                }
                RuleEvaluationChronologyViewModel rule = child as RuleEvaluationChronologyViewModel;
                if (rule != null)
                {
                    marks.Add(rule.CurrentMark);
                }
            }
            summaryMark = marks.Sum() / marks.Count;
            OnPropertyChanged("SummaryMark");
        }

        public void PutParameter(string[] path, AnalyticRuleType type)
        {
            if (path.Length == 0)
            {
                children.Add(new RuleEvaluationChronologyViewModel(
                    EvaluationCache.Get(vin).GetRuleEvaluationChronology(type)));
            }
            else
            {
                if (!children.Any(f => 
                    f is EvaluationFolderViewModel &&
                    ((EvaluationFolderViewModel)f).Key == path[0]))
                {
                    EvaluationFolderViewModel newFolder =
                        new EvaluationFolderViewModel(path[0], vin);
                    newFolder.PutParameter(GetSubPath(path), type);
                    children.Add(newFolder);
                }
                else
                {
                    EvaluationFolderViewModel folder =
                        children.First(f => 
                            f is EvaluationFolderViewModel && 
                            ((EvaluationFolderViewModel)f).Key == path[0])
                            as EvaluationFolderViewModel;
                    folder.PutParameter(GetSubPath(path), type);
                }
            }
            OnPropertyChanged("Children");
            RefreshSummaryMark();
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
