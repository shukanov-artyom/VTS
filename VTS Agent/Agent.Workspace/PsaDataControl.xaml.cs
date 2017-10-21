using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Agent.Common.Instance;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Characteristics;
using Agent.Common.Presentation.ProgressBar;
using Agent.Common.Presentation.Vehicles;
using Agent.Localization;
using Agent.Workspace.EventArguments;
using Agent.Workspace.Views;
using Agent.Workspace.Views.Chrono;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace
{
    /// <summary>
    /// Interaction logic for CitroenDataControl.xaml
    /// </summary>
    public partial class PsaDataControl
    {
        private readonly BackgroundWorker characteristicsGetterWorker = new BackgroundWorker();
        private readonly BackgroundWorker dataItemPresentationCreationWorker = 
            new BackgroundWorker();
        private CultureInfo info;

        public PsaDataControl()
        {
            InitializeComponent();
            treeViewControlPsaTraces.OnTreeSelectionChanged += OnSelectionChanged;
            characteristicsGetterWorker.WorkerSupportsCancellation = true;
            characteristicsGetterWorker.WorkerReportsProgress = false;
            dataItemPresentationCreationWorker.WorkerSupportsCancellation = true;
            dataItemPresentationCreationWorker.WorkerReportsProgress = false;
            dataItemPresentationCreationWorker.DoWork += CreateDataItemPresentationAsync;
            dataItemPresentationCreationWorker.RunWorkerCompleted += OnDataitemPresentationCreated;
            if (LoggedUserContext.LoggedUser == null)
            {
                DisplayNoConnectionStub();
            }
            LoggedUserContext.UserLoggedOff += OnUserLoggedOff;
            LoggedUserContext.UserLoggedOn += OnUserLoggedOn;
            if (ViewModelBase.AppDebugMode)
            {
                //tabItemParameters.DataContext = new ParametersDebugViewModel();
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            PsaTreeSelectionChangedEventArgs ea =
                e as PsaTreeSelectionChangedEventArgs;
            if (ea == null)
            {
                throw new ArgumentException("wrong event args type");
            }
            DataViewControlFactory factory = new DataViewControlFactory(ea.Arg as ViewModelBase);
            Control settingsControl = factory.CreateDataSettingsView();
            Control dataViewControl = factory.CreateDataView(settingsControl);
            contentControlParameterVisual.Content = dataViewControl;
            controlParametersSetDetails.Content = settingsControl;
        }

        private void SelectedVehicleChanged(object sender, SelectionChangedEventArgs e)
        {
            if (characteristicsGetterWorker.IsBusy)
            {
                characteristicsGetterWorker.CancelAsync();
                characteristicsGetterWorker.DoWork -= GetCharacteristicsInBackground;
                characteristicsGetterWorker.RunWorkerCompleted -= OnCharacteristicsRetrieved;
                contentControlVehicleCharacteristics.Content = null;
                return;
            }
            if (LoggedUserContext.LoggedUser == null)
            {
                contentControlVehicleCharacteristics.Content =
                    new CannotDisplayVehicleCharacteristicsControl();
                return;
            }
            contentControlVehicleCharacteristics.Content = new CircularProgressBar();
            string vin = ((VehicleViewModel)e.AddedItems[0]).Vin;
            info = TranslationManager.Instance.CurrentLanguage;
            characteristicsGetterWorker.DoWork += GetCharacteristicsInBackground;
            characteristicsGetterWorker.RunWorkerCompleted += OnCharacteristicsRetrieved;
            characteristicsGetterWorker.RunWorkerAsync(vin);
        }

        private void OnCharacteristicsRetrieved(object sender, 
            RunWorkerCompletedEventArgs e)
        {
            characteristicsGetterWorker.DoWork -= GetCharacteristicsInBackground;
            characteristicsGetterWorker.RunWorkerCompleted -= OnCharacteristicsRetrieved;
            if (e.Cancelled)
            {
                contentControlVehicleCharacteristics.Content = null;
                return;
            }
            VehicleCharacteristics chars = e.Result as VehicleCharacteristics;
            if (chars == null)
            {
                contentControlVehicleCharacteristics.Content =
                    CodeBehindStringResolver.Resolve("CanNotGetVehicleCharacteristics");
                return;
            }
            VehicleCharacteristicsViewModel vm = new VehicleCharacteristicsViewModel(chars);
            VehicleCharacteristicsControl control = new VehicleCharacteristicsControl(vm);
            contentControlVehicleCharacteristics.Content = control;
        }

        private void GetCharacteristicsInBackground(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = info;
            string vin = (string) doWorkEventArgs.Argument;
            VehicleCharacteristics chars = VehicleCharacteristicsCache.Get(vin);
            doWorkEventArgs.Result = chars;
        }

        private void DataTreeSelectedItemChanged(object sender, 
            RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue == null)
            {
                contentControlDataItemDetails.Content = null;
                return;
            }
            DataItemViewFactory factory = new DataItemViewFactory(e.NewValue);
            contentControlDataItemDetails.Content = factory.Create();
        }

        private void ChronologicalDataTreeSelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue == null)
            {
                contentControlDataItemDetails.Content = null;
                return;
            }
            ChronologicalDataItemViewFactory factory = 
                new ChronologicalDataItemViewFactory(e.NewValue as ViewModelBase);
            IChronologicalDataItemView view = factory.Create();
            view.DataContext = e.NewValue;
            contentControlDataItemDetailsChronology.Content = view;
        }

        private void CreateDataItemPresentationAsync(object sender, DoWorkEventArgs ea)
        {
            DataItemViewFactory factory = new DataItemViewFactory(ea.Argument);
            IDataItemView view = factory.Create();
            ea.Result = view;
        }

        private void OnDataitemPresentationCreated(object sender, RunWorkerCompletedEventArgs ea)
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(new ParameterizedThreadStart(SetView), ea.Result);
        }

        private void SetView(object view)
        {
            contentControlDataItemDetails.Content = view;
        }

        private void OnUserLoggedOn(object sender, EventArgs eventArgs)
        {
            contentControlDataItemDetails.Content = null;
            contentControlDataItemDetailsChronology.Content = null;
            controlDataItemDetailsEvaluation.contentControlDataItemDetailsEvaluation.Content = null;
        }

        private void OnUserLoggedOff(object sender, EventArgs eventArgs)
        {
            DisplayNoConnectionStub();
        }

        private void DisplayNoConnectionStub()
        {
            contentControlDataItemDetails.Content = CreateNoConnectionStubTextBlock();
            contentControlDataItemDetailsChronology.Content = CreateNoConnectionStubTextBlock();
            controlDataItemDetailsEvaluation.contentControlDataItemDetailsEvaluation.Content = CreateNoConnectionStubTextBlock();
        }

        private static TextBlock CreateNoConnectionStubTextBlock()
        {
            TextBlock tb = new TextBlock();
            tb.Margin = new Thickness(10);
            tb.TextWrapping = TextWrapping.Wrap;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.Text = CodeBehindStringResolver.Resolve(
                "CannotGetDataWithoutNetworkConnectionAndUserLogged");
            return tb;
        }
    }
}
