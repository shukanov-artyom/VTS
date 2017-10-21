using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using Agent.Common;
using Agent.Common.Data;
using Agent.Infrastructure;
using Agent.Localization;
using Agent.Logging;
using Agent.Network.Monitor.VtsWebService;

namespace Agent
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
            base.OnStartup(e);
            TranslationManager.Instance.TranslationProvider =
                new ResxTranslationProvider("Agent.Localization.Resources.Resources",
                Assembly.GetAssembly(typeof (TranslateExtension)));
            RollingFileAppender.SetLogPath(Path.Combine(Directory.GetCurrentDirectory(), "Agent.log"));
            Log.Info(String.Format("VTS Agent {0} has been started.", ApplicationVersion.Current.VersionString));
            IDictionary<Type, Type> dict = new Dictionary<Type, Type>();
            dict.Add(typeof(IVtsWebService), typeof(VtsWebServiceClient));
            Container.Initialize(dict);
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error(e.ExceptionObject as Exception, "Cannot load application. AppDomain has crashed due to unhandled exception.");
        }
    }
}
