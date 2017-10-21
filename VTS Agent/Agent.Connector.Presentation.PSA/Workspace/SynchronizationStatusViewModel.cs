using System;
using Agent.Common.Presentation;
using Agent.Network.DataSynchronization;

namespace Agent.Connector.Presentation.PSA.Workspace
{
    public class DataSynchronizationStatusViewModel : ViewModelBase
    {
        private DataSynchronizationStatus status;
        private bool isDataUnsupported;
        private bool isUnknown = true;
        private bool isFinished;
        private bool isFailed;
        private bool isNetworkNotAccessible;
        private bool isInProgress;
        private bool isVehicleUnsupported;

        public void Update(DataSynchronizationStatus status)
        {
            Reset();
            this.status = status;
            switch (status)
            {
                case DataSynchronizationStatus.DataUnsupported:
                    IsDataUnsupported = true;
                    break;
                case DataSynchronizationStatus.Failed:
                    IsFailed = true;
                    break;
                case DataSynchronizationStatus.Finished:
                    IsFinished = true;
                    break;
                case DataSynchronizationStatus.InProgress:
                    IsInProgress = true;
                    break;
                case DataSynchronizationStatus.NetworkNotAccessible:
                    IsNetworkNotAccessible = true;
                    break;
                case DataSynchronizationStatus.Unknown:
                    IsUnknown = true;
                    break;
                case DataSynchronizationStatus.VehicleUnsupported:
                    IsVehicleUnsupported = true;
                    break;
                default:
                    IsUnknown = true;
                    break;
            }
        }

        public bool IsDataUnsupported
        {
            get
            {
                return isDataUnsupported;
            }
            set
            { 
                Reset();
                isDataUnsupported = value;
                Check();
                OnPropertyChanged("IsDataUnsupported");
            }
        }

        public bool IsUnknown
        {
            get
            {
                return isUnknown;
            }
            set
            {
                Reset();
                isUnknown = value;
                Check();
                OnPropertyChanged("IsUnknown");
            }
        }

        public bool IsFinished
        {
            get
            {
                return isFinished;
            }
            set
            {
                Reset();
                isFinished = value;
                Check();
                OnPropertyChanged("IsFinished");
            }
        }

        public bool IsFailed
        {
            get
            {
                return isFailed;
            }
            set
            {
                Reset();
                isFailed = value;
                Check();
                OnPropertyChanged("IsFailed");
            }
        }

        public bool IsNetworkNotAccessible
        {
            get
            {
                return isNetworkNotAccessible;
            }
            set
            {
                Reset();
                isNetworkNotAccessible = value;
                Check();
                OnPropertyChanged("IsNetworkNotAccessible");
            }
        }

        public bool IsInProgress
        {
            get
            {
                return isInProgress;
            }
            set
            {
                Reset();
                isInProgress = value;
                Check();
                OnPropertyChanged("IsInProgress");
            }
        }

        public bool IsVehicleUnsupported
        {
            get
            {
                return isVehicleUnsupported;
            }
            set
            {
                Reset();
                isVehicleUnsupported = value;
                Check();
                OnPropertyChanged("IsVehicleUnsupported");
            }
        }

        private void Reset()
        {
            isDataUnsupported = false;
            isUnknown = false;
            isFinished = false;
            isFailed = false;
            isNetworkNotAccessible = false;
            isInProgress = false;
            isVehicleUnsupported = false;
            OnPropertyChanged("IsInProgress");
            OnPropertyChanged("IsNetworkNotAccessible");
            OnPropertyChanged("IsFailed");
            OnPropertyChanged("IsFinished");
            OnPropertyChanged("IsUnknown");
            OnPropertyChanged("IsDataUnsupported");
            OnPropertyChanged("IsVehicleUnsupported");
        }

        private void Check()
        {
            if (!IsDataUnsupported && 
                !IsUnknown && 
                !IsFailed && 
                !IsFinished && 
                !IsInProgress && 
                !IsNetworkNotAccessible &&
                !IsVehicleUnsupported)
            {
                isUnknown = true;
            }
        }
    }
}
