using System;
using System.ComponentModel;
using System.Threading;
using Agent.Common;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public class LastVersionChecker : IDisposable
    {
        public delegate void NewVersionAvailableCallbackDelegate(AgentVersion newVersion);

        private readonly BackgroundWorker worker = new BackgroundWorker();
        private readonly NewVersionAvailableCallbackDelegate newVersionCallback;
        private readonly ErrorCallbackDelegate errorCallback;

        public LastVersionChecker(
            NewVersionAvailableCallbackDelegate newVersionCallback, 
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.newVersionCallback = newVersionCallback;
            worker.DoWork += GetLastVersion;
            worker.RunWorkerCompleted += OnWorkerCompleted;
        }

        public void CheckAsync()
        {
            worker.RunWorkerAsync();
        }

        private void GetLastVersion(object w, DoWorkEventArgs e)
        {
            Thread.Sleep(10000);
            try
            {
                using (VtsWebServiceClient service = new VtsWebServiceClient())
                {
                    AgentVersionDto lastAgentVersionDto =
                        service.GetLastAgentVersion();
                    AgentVersion lastVersion = AgentVersionAssembler.
                        ToDomainObjectFromDto(lastAgentVersionDto);
                    e.Result = lastVersion;
                }
            }
            catch (Exception ex)
            {
                if (errorCallback != null)
                {
                    errorCallback.Invoke(ex, ex.Message);
                }
            }
        }

        private void OnWorkerCompleted(object w, RunWorkerCompletedEventArgs e)
        {
            AgentVersion lastVersion = e.Result as AgentVersion;
            if (lastVersion != null && ApplicationVersion.Current.IsOlderThan(lastVersion))
            {
                if (newVersionCallback != null)
                {
                    newVersionCallback.Invoke(lastVersion);
                }
            }
        }

        public void Dispose()
        {
            worker.Dispose();
        }
    }
}
