using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Agent.Network.Monitor.VtsWebService;

namespace Agent.Common.Presentation.Network
{
    /// <summary>
    /// Interaction logic for ConnectionCheckWindow.xaml
    /// </summary>
    public partial class ConnectionCheckWindow : Window
    {
        private bool connectionIsOk = false;

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public ConnectionCheckWindow()
        {
            InitializeComponent();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += CheckConnectionInBackground;
            worker.WorkerSupportsCancellation = false;
            worker.WorkerReportsProgress = false;
            worker.RunWorkerCompleted += OnConnectionChecked;
            worker.RunWorkerAsync();
        }

        private void OnConnectionChecked(object sender, 
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (connectionIsOk)
            {
                DialogResult = true;
            }
            else
            {
                NoConnectionControl control = new NoConnectionControl();
                control.OkClicked += OnAgreeWinhNoConnection;
                Content = control;
            }
        }

        private void OnAgreeWinhNoConnection(object sender, EventArgs eventArgs)
        {
            DialogResult = false;
        }

        private void CheckConnectionInBackground(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            IVtsWebService service = Infrastructure.Container.GetInstance<IVtsWebService>();
            try
            {
                string connectionResult = service.CheckConnection();
                if (String.Equals(connectionResult, "ok", 
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    connectionIsOk = true;
                }
            }
            catch (Exception)
            {
                connectionIsOk = false;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
