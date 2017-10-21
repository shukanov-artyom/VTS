using System;
using System.Reflection;
using System.ServiceModel.DomainServices.Client;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTS
{
    public partial class App : Application
    {
        public App()
        {
            Application.Current.ApplicationLifetimeObjects.Add(new WebContext());
            this.Startup += ApplicationStartup;
            this.Exit += ApplicationExit;
            this.UnhandledException += ApplicationUnhandledException;

            SmartDispatcher.Initialize(Deployment.Current.Dispatcher);
            Current.Host.Content.Resized += 
                ApplicationSizeKeeper.OnApplicationResize;
            InitializeComponent();
            /*WebContext context = new WebContext();*/
            Application.Current.ApplicationLifetimeObjects.Add(WebContextBase.Current);
            WebContextBase.Current.Authentication = new FormsAuthentication();
        }

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            TranslationManager.Instance.TranslationProvider =
                new ResxTranslationProvider(
                    "VTSWeb.Localization.Resources", 
                    Assembly.Load("VTSWeb.Localization, Version=1.0.0.0, Culture=neutral"));
            RootVisual = new MainPage();
        }

        private void ApplicationExit(object sender, EventArgs e)
        {

        }

        private void ApplicationUnhandledException(object sender,
            ApplicationUnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is DomainOperationException)
            {
                e.Handled = true;
            }
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
