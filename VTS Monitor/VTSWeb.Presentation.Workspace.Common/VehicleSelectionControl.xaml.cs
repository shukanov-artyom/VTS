using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.DomainObjects;

using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Presentation.Common.Vehicles;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Presentation.Workspace.Common
{
    public partial class VehicleSelectionControl : UserControl
    {
        public event SelectionChangedEventHandler VehicleSelected;

        private ObservableCollection<VehicleViewModel> vehicleViewModels =
            new ObservableCollection<VehicleViewModel>();
        private VehicleViewModel selectedVehicle;

        public VehicleSelectionControl()
        {
            InitializeComponent();
            comboBoxVehicles.ItemsSource = vehicleViewModels;
            comboBoxVehicles.SelectedItem = SelectedVehicle;
            InitializeVehicles();
        }

        private VehicleViewModel SelectedVehicle
        {
            get
            {
                return selectedVehicle;
            }
            set
            {
                selectedVehicle = value;
            }
        }

        private void InitializeVehicles()
        {
            User targetUser = LoggedUserContext.LoggedUser;
            if (targetUser.Role == UserRole.Administrator)
            {
                // no vehicles for admin usually
                return;
            }
            InitializeVehiclesPrivate(targetUser);
        }

        private void InitializeVehiclesPrivate(User user)
        {
            VtsWebServiceClient client = new VtsWebServiceClient();
            client.GetVehiclesForUserCompleted += VehiclesRetrievedCallback;
            client.GetVehiclesForUserAsync(user.Login, user.PasswordHash);
        }

        private void VehiclesRetrievedCallback(object s, 
            GetVehiclesForUserCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ErrorCallback(e.Error, e.Error.Message);
            }
            else
            {
                vehicleViewModels.Clear();
                foreach (VehicleDto vd in e.Result)
                {
                    Vehicle v = VehicleAssembler.FromDtoToDomainObject(vd);
                    vehicleViewModels.Add(new VehicleViewModel(v));
                }
            }
        }

        private void ErrorCallback(Exception e, string msg)
        {
            ErrorWindow wnd = new ErrorWindow(e, msg);
            wnd.Closed += DialogWindowStatus.OnDialogClosed;
            wnd.Show();
        }

        private void SelectedVehicleChanged(object sender,
            SelectionChangedEventArgs e)
        {
            if (VehicleSelected != null)
            {
                VehicleSelected.Invoke(sender, e);
            }
        }

        /// <summary>
        /// If we need to explicitly initialize vehicles for a particular user
        /// </summary>
        /// <param name="targetUser"></param>
        public void InitializeVehicles(User targetUser)
        {
            if (targetUser == null)
            {
                throw new ArgumentNullException("targetUser");
            }
            InitializeVehiclesPrivate(targetUser);
        }
    }
}
