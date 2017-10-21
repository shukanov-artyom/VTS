using System;
using System.Collections.Generic;
using System.Linq;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Import
{
    public abstract class ImportableViewModel : ViewModelBase
    {
        private bool isSelectedForExport = false;
        private IList<ImportableViewModel> children =
            new List<ImportableViewModel>();

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

        public abstract bool CanBeSelectedForExport
        {
            get;
        }

        protected void RegisterExportableChild(ImportableViewModel child)
        {
            child.StateChanged += OnChildStateChanged;
            children.Add(child);
        }

        protected void UnregisterExportableChild(ImportableViewModel child)
        {
            child.StateChanged -= OnChildStateChanged;
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

        public void UpdateStateWithoutUpNotification(bool newState)
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
                foreach (ImportableViewModel child in children)
                {
                    child.UpdateStateWithoutUpNotification(isSelectedForExport);
                }
            }
        }
    }
}
