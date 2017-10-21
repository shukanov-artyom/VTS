using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.VehicleEvents.Persistency;

namespace VTSWeb.VehicleEvents.Presentation
{
    public partial class VehicleEventWindow : ChildWindow
    {
        public VehicleEventWindow()
        {
            InitializeComponent();
        }

        public VehicleEventWindow(VehicleEventViewModel vm)
            : this()
        {
            DataContext = vm;
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            VehicleEventViewModel vm = DataContext as VehicleEventViewModel;
            vm.CheckingInProgress = true;
            if (vm.CheckDataConsistency())
            {
                VehicleEventsPersistency po = new VehicleEventsPersistency(
                    EventPersisted, Error);
                if (vm.Model.Id == 0)
                {
                    po.Persist(vm.Model);
                }
                else
                {
                    po.Persist(vm.Model);
                }
            }
        }

        private void EventPersisted()
        {
            VehicleEventViewModel vm = DataContext as VehicleEventViewModel;
            vm.CheckingInProgress = false;
            DialogResult = true;
        }

        private void Error(Exception e, string msg)
        {
            VehicleEventViewModel vm = DataContext as VehicleEventViewModel;
            vm.CheckingInProgress = false;
            ErrorWindow wnd = new ErrorWindow(e, msg);
            wnd.Closed += DialogWindowStatus.OnDialogClosed;
            wnd.Show();
        }
    }
}

