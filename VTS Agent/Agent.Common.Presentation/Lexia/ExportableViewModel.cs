using System;
using System.Collections.Generic;
using System.Linq;

namespace Agent.Common.Presentation.Lexia
{
    /// <summary>
    /// Basic class for LexiaDataViewModel that provides some code for:
    /// - object selection to export
    /// </summary>
    public abstract class ExportableViewModel : ViewModelBase
    {
        private bool isSelectedForExport = false;
        private IList<ExportableViewModel> children = 
            new List<ExportableViewModel>();

        public event EventHandler Deleted;

        public bool IsSelectedForExport
        {
            get
            {
                return isSelectedForExport;
            }
            set
            {
                if (CanBeSelectedForExport)
                {
                    isSelectedForExport = value;
                    if (StateChanged != null)
                    {
                        StateChanged.Invoke(this, EventArgs.Empty);
                    }
                    UpdateChildren();
                    OnPropertyChanged("IsSelectedForExport");
                }
            }
        }

        public virtual bool CanBeSelectedForExport
        {
            get
            {
                return true;
            }
        }

        protected void RegisterExportableChild(ExportableViewModel child)
        {
            child.StateChanged += OnChildStateChanged;
            child.Deleted += OnChildDeleted;
            children.Add(child);
        }

        protected void UnregisterExportableChild(ExportableViewModel child)
        {
            child.StateChanged -= OnChildStateChanged;
            child.Deleted -= OnChildDeleted;
            children.Remove(child);
        }

        protected event EventHandler StateChanged;

        private void OnChildStateChanged(object sender, EventArgs e)
        {
            bool newState = GetCurrentState();
            if (CanBeSelectedForExport)
            {
                if (isSelectedForExport != newState)
                {
                    isSelectedForExport = newState;
                    OnPropertyChanged("IsSelectedForExport");
                    if (StateChanged != null)
                    {
                        StateChanged.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        private bool GetCurrentState()
        {
            bool thereAreSelectedChildren =
                children.Any(c => c.IsSelectedForExport);
            if (thereAreSelectedChildren)
            {
                return true;
            }
            return false;
        }

        public void UpdateStateWithoutUpwardsNotification(bool newState)
        {
            if (CanBeSelectedForExport)
            {
                if (isSelectedForExport == newState)
                {
                    return;
                }
                isSelectedForExport = newState;
                OnPropertyChanged("IsSelectedForExport");
                UpdateChildren();
            }
        }

        private void UpdateChildren()
        {
            if (CanBeSelectedForExport)
            {
                foreach (ExportableViewModel child in children)
                {
                    child.UpdateStateWithoutUpwardsNotification(isSelectedForExport);
                }
            }
        }

        protected virtual void OnChildDeleted(object sender, EventArgs e)
        {
            ExportableViewModel evm = sender as ExportableViewModel;
            if (evm == null)
            {
                throw new ArgumentException("ExportableViewModel expected!");
            }
            UnregisterExportableChild(evm);
        }

        protected void OnDeleted()
        {
            if (Deleted != null)
            {
                Deleted.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
