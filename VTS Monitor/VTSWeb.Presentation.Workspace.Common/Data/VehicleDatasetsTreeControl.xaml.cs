using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.DomainObjects.Psa.Persistency;

using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Presentation.Psa;

namespace VTSWeb.Presentation.Workspace.Common.Data
{
    public partial class VehicleDatasetsTreeControl : UserControl
    {
        public event 
            RoutedPropertyChangedEventHandler<object> SelectionChanged;
        public event EventHandler UpdateComplete;

        private Vehicle vehicle;
        private ObservableCollection<PsaDatasetViewModel> data =
            new ObservableCollection<PsaDatasetViewModel>();

        public VehicleDatasetsTreeControl()
        {
            InitializeComponent();
            mainTreeView.ItemsSource = data;
        }

        private void OnGridSelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged.Invoke(this, e);
            }
        }

        public void UpdateForVehicle(Vehicle veh)
        {
            this.vehicle = veh;
            if (vehicle == null)
            {
                data.Clear();
                return;
            }
            if (vehicle is Vehicle)
            {
                PsaDatasetPersistency retriever =
                new PsaDatasetPersistency(DataRetrieved, ErrorCallback);
                retriever.GetAllForVehicle(vehicle.Vin);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void DataRetrieved(IList<PsaDataset> dataRetrieved)
        {
            data.Clear();
            foreach (PsaDataset dataset in dataRetrieved)
            {
                data.Add(new PsaDatasetViewModel(dataset));
            }
            if (UpdateComplete != null)
            {
                UpdateComplete.Invoke(this, EventArgs.Empty);
            }
        }

        private void ErrorCallback(Exception e, string msg)
        {
            if (UpdateComplete != null)
            {
                UpdateComplete.Invoke(this, EventArgs.Empty);
            }
            ErrorWindow wnd = new ErrorWindow(e, msg);
            wnd.Show();
        }
    }
}
