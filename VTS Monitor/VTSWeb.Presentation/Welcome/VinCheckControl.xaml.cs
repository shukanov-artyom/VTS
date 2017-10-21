using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VTS.Shared;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.Common;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;
using VTSWeb.VendorData.Extensions;

namespace VTSWeb.Presentation.Welcome
{
    public partial class VinCheckControl : UserControl
    {
        private string vinOnCheck;

        public VinCheckControl()
        {
            InitializeComponent();
        }

        private void CheckClicked(object sender, RoutedEventArgs e)
        {
            Disable();
            vinOnCheck = textBoxVin.Text;
            if (!VinValidator.Validate(vinOnCheck))
            {
                textBlockCheckResult.Foreground = 
                    new SolidColorBrush(Colors.Red);
                textBlockCheckResult.Text = CodeBehindStringResolver.Resolve(
                    "IncorrectVinErrorText");
                Enable();
                return;
            }
            Manufacturer? manuf = VinValidator.GetManufacturer(vinOnCheck);
            if (manuf == null)
            {
                textBlockCheckResult.Foreground =
                    new SolidColorBrush(Colors.Red);
                textBlockCheckResult.Text = CodeBehindStringResolver.Resolve(
                    "UnsupportedManufacturerErrorText");
                Enable();
            }
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.GetVehicleInformationByVinCompleted +=
                ServiceOnGetVehicleInformationByVinCompleted;
            service.GetVehicleInformationByVinAsync(vinOnCheck);
        }

        private void ServiceOnGetVehicleInformationByVinCompleted(object sender, 
            GetVehicleInformationByVinCompletedEventArgs ea)
        {
            VehicleInformation info = VehicleInformationAssembler.
                FromDtoToDomainObject(ea.Result);
            if (ea.Error is NotSupportedException)
            {
                textBlockCheckResult.Foreground =
                    new SolidColorBrush(Colors.Red);
                textBlockCheckResult.Text = CodeBehindStringResolver.Resolve(
                    "UnfortunatelyVehicleIsNotSupportedMessage");
                Enable();
                return;
            }
            if (ea.Error is NoInfoForVinException)
            {
                textBlockCheckResult.Foreground =
                    new SolidColorBrush(Colors.Red);
                textBlockCheckResult.Text = CodeBehindStringResolver.Resolve(
                    "NoInformationForThisVin");
                Enable();
                return;
            }
            if (ea.Error != null)
            {
                OnError(ea.Error, ea.Error.Message);
            }
            string recognizedInfo = String.Format("{0}: {1}, {2}: {3}",
                CodeBehindStringResolver.Resolve("VehicleRecognizedText"),
                info.VehicleModel,
                CodeBehindStringResolver.Resolve("EngineText"),
                info.Engine.DisplayName);
            textBlockCheckResult.Foreground =
                    new SolidColorBrush(Colors.Green);
            textBlockCheckResult.Text = recognizedInfo;
            Enable();
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Show();
            Enable();
        }

        private void Disable()
        {
            textBoxVin.IsEnabled = false;
            buttonCheck.IsEnabled = false;
        }

        private void Enable()
        {
            textBoxVin.IsEnabled = true;
            buttonCheck.IsEnabled = true;
        }
    }
}
