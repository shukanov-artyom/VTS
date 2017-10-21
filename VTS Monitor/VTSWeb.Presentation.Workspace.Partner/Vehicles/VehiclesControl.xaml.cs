using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Presentation.Common.Vehicles;
using VTSWeb.Presentation.Workspace.Common;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;
using VTSWeb.VendorData.Presentation;
using VehicleCharacteristics = VTSWeb.VendorData.VehicleCharacteristics;

namespace VTSWeb.Presentation.Workspace.Partner.Vehicles
{
    public partial class VehiclesControl : NavigatablePage
    {
        private VehicleViewModel selectedItem;
        private readonly ObservableCollection<VehicleViewModel> vehicleViewModels = 
            new ObservableCollection<VehicleViewModel>();

        public VehiclesControl()
        {
            InitializeComponent();
            InitializeHeader("Vehicles");
            progressBarVehiclesLoading.Visibility = Visibility.Visible;
            GetVehicles();
            dataGridVehicles.ItemsSource = vehicleViewModels;
            selectedItem = null;
            buttonShowDetails.IsEnabled = false;
        }

        private void GetVehicles()
        {
            VtsWebServiceClient client = new VtsWebServiceClient();
            client.GetUsersVehiclesCompleted += OnGetUsersVehiclesCompleted;
            client.GetUsersVehiclesAsync(LoggedUserContext.LoggedUser.Login,
                LoggedUserContext.LoggedUser.PasswordHash);
        }

        private void OnGetUsersVehiclesCompleted(object sender,
            GetUsersVehiclesCompletedEventArgs ea)
        {
            if (ea.Error != null)
            {
                OnError(ea.Error, ea.Error.Message);
            }
            else
            {
                foreach (VehicleDto dto in ea.Result)
                {
                    Vehicle domainObject = VehicleAssembler.FromDtoToDomainObject(dto);
                    vehicleViewModels.Add(new VehicleViewModel(domainObject));
                }
            }
            progressBarVehiclesLoading.Visibility = Visibility.Collapsed;
        }

        private void ShowVehicleDetails(object sender, RoutedEventArgs e)
        {
            buttonShowDetails.IsEnabled = false;
            dataGridVehicles.IsEnabled = false;
            VtsWebServiceClient client = new VtsWebServiceClient();
            client.GetVehicleCharacteristicsCompleted += VehicleCharacteristicsRetrieved;
            client.GetVehicleCharacteristicsAsync(selectedItem.Model.Vin, (int)LoggedUserContext.CurrentLanguageEnum);
        }

        private void VehicleCharacteristicsRetrieved(
            object sender, GetVehicleCharacteristicsCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                VehicleCharacteristics ch = VehicleCharacteristicsDtoAssembler.Assemble(e.Result);
                VehicleCharacteristicsWindow window =
                new VehicleCharacteristicsWindow();
                window.Content =
                    new VehicleCharacteristicsTabbedControl(
                        new VehicleCharacteristicsViewModel(
                            selectedItem.Model, ch));
                window.Closed += DialogWindowStatus.OnDialogClosed;
                window.Show();
                buttonShowDetails.IsEnabled = true;
                dataGridVehicles.IsEnabled = true;
            }
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow wnd = new ErrorWindow(e, msg);
            wnd.Show();
        }

        private void OnGridSelectionChanged(object sender, 
            SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                selectedItem = null;
                buttonShowDetails.IsEnabled = false;
            }
            else
            {
                selectedItem = e.AddedItems[0] as VehicleViewModel;
                buttonShowDetails.IsEnabled = true;
            }
        }
    }
}
