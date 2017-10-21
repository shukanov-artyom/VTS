using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Agent.Common.Instance;
using Agent.Logging;
using Agent.Metadata.Psa;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Network.DataSynchronization.Psa
{
    public class PsaDataSynchronizer : IDisposable
    {
        public event DataSynchronizerStatusChangedEventHandler StatusUpdated;

        private readonly BackgroundWorker worker = new BackgroundWorker();

        private List<PsaTraceInfo> traceInfos;

        public PsaDataSynchronizer()
        {
            worker.DoWork += SynchronizeDataBackground;
            worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
            LoggedUserContext.UserLoggedOn += OnUserLoggedOn; 
        }

        private void OnUserLoggedOn(object sender, EventArgs eventArgs)
        {
            worker.RunWorkerAsync();
        }

        private void WorkerOnRunWorkerCompleted(object sender, 
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            Log.Info("Synchronization complete.");
            // StatusUpdated.Invoke(DataSynchronizationStatus.Finished);
        }

        public void StartSynchronizationAsync(List<PsaTraceInfo> traces)
        {
            traceInfos = traces;
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }

        private void SynchronizeDataBackground(object sender, 
            DoWorkEventArgs doWorkEventArgs)
        {
            if (traceInfos == null)
            {
                throw new ArgumentException("Traces are required to synchronize");
            }
            if (!ConnectionEstablished() || LoggedUserContext.LoggedUser == null)
            {
                MarkEverythingAsHalted();
                return;
            }
            else
            {
                MarkEverythingAsInProgress();
            }
            List<PsaTrace> tracesRequiringAttention = FilterTraces(traceInfos);
            if (tracesRequiringAttention.Count > 0)
            {
                List<PsaDataset> datasets = FormDatasets(tracesRequiringAttention);
                List<string> supportedVins = SelectSupportedVehicles(datasets.
                    Select(d => d.GetVin()).ToList());
                RegisterAndAssociateNewVehicles(supportedVins);
                SynchronizeDatasets(datasets);
            }
            // Make sure vehicles are associated, it may be a new user
            JustAssociateVehiclesToCurrentUser();
        }

        private List<string> SelectSupportedVehicles(List<string> allVins)
        {
            IVtsWebService service = Infrastructure.Container.GetInstance<IVtsWebService>();
            List<string> unsupportedVins = service.
                    CheckVinsReturnUnsupported(allVins.ToArray()).ToList();
            List<string> result = new List<string>();
            foreach (string vin in allVins)
            {
                if (unsupportedVins.Contains(vin,
                    StringComparer.InvariantCultureIgnoreCase))
                {
                    // report unsupported vehicle
                        new List<PsaTraceSignature>();
                    List<PsaTrace> tracesToGetSignatures = traceInfos.
                        Where(t => t.Trace.Vin == vin).
                        Select(t => t.Trace).ToList();
                    List<PsaTraceSignature> signaturesOfTracesForUnsupportedVehicle =
                        GetTraceSignaturesForTracesList(tracesToGetSignatures);
                    StatusUpdated.Invoke(DataSynchronizationStatus.VehicleUnsupported, 
                        signaturesOfTracesForUnsupportedVehicle);
                }
                else
                {
                    result.Add(vin);
                }
            }
            return result;
        }

        private void RegisterAndAssociateNewVehicles(List<string> vins)
        {
            IVtsWebService service = Infrastructure.Container.GetInstance<IVtsWebService>();
            service.RegisterVehicles(vins.ToArray());
            service.AssociateVehiclesWithUser(vins.ToArray(),
                LoggedUserContext.LoggedUser.Login,
                LoggedUserContext.LoggedUser.PasswordHash);
        }

        private bool SynchronizeDatasets(List<PsaDataset> datasetsToUpload)
        {
            foreach (PsaDataset dataset in datasetsToUpload)
            {
                PsaDatasetDto dto = PsaDatasetAssembler.FromDomainobjectToDto(dataset);
                try
                {
                    VtsWebServiceClient service = new VtsWebServiceClient();
                    service.UploadDataset(dto);
                    MarkTracesAsSynchronized(dataset.Traces);
                }
                catch (Exception e)
                {
                    StatusUpdated.Invoke(DataSynchronizationStatus.Failed,
                        GetTraceSignaturesForDataset(dataset));
                    Log.Error(e, e.Message);
                }
            }
            return true;
        }

        private void MarkTracesAsSynchronized(IList<PsaTrace> tracesToMark)
        {
            StatusUpdated.Invoke(DataSynchronizationStatus.Finished, 
                GetTraceSignaturesForTracesList(tracesToMark));
            foreach (PsaTrace psaTrace in tracesToMark)
            {
                traceInfos.First(ti => ti.Trace.Vin == psaTrace.Vin &&
                    ti.Trace.Date == psaTrace.Date).Metadata.IsSynchronized = true;
            }
        }

        /// <summary>
        /// Filter out already synchronized and unsupported data.
        /// </summary>
        private List<PsaTrace> FilterTraces(IEnumerable<PsaTraceInfo> traceInfs)
        {
            List<PsaTrace> result = new List<PsaTrace>();
            foreach (PsaTraceInfo traceInfo in traceInfs)
            {
                if (!traceInfo.Metadata.IsSynchronized && 
                    traceInfo.Trace.Date != DateTime.MinValue &&
                    traceInfo.Trace.ParametersSets.Count > 0)
                {
                    result.Add(traceInfo.Trace);
                }
                else if (traceInfo.Metadata.IsSynchronized)
                {
                    StatusUpdated.Invoke(DataSynchronizationStatus.Finished,
                        new List<PsaTraceSignature>()
                            {
                                new PsaTraceSignature()
                                    {
                                        TraceDate = traceInfo.Trace.Date,
                                        Vin = traceInfo.Trace.Vin
                                    }
                            });
                    Log.Info(String.Format("Finished sync for trace by {0}, vin: {1}", 
                        traceInfo.Trace.Date, traceInfo.Trace.Vin));
                }
                else if (traceInfo.Trace.Date == DateTime.MinValue ||
                    traceInfo.Trace.ParametersSets.Count == 0)
                {
                    StatusUpdated.Invoke(DataSynchronizationStatus.Failed,
                        new List<PsaTraceSignature>()
                            {
                                new PsaTraceSignature()
                                    {
                                        TraceDate = traceInfo.Trace.Date,
                                        Vin = traceInfo.Trace.Vin
                                    }
                            });
                    Log.Warn(String.Format(
                        "Cannot sync trace: Trace for {0} by path {1} has incorrect date or has no Parameter Sets.", 
                        traceInfo.Trace.Vin, traceInfo.Metadata.SourceXmlPath));
                }
            }
            return result;
        }

        private List<PsaDataset> FormDatasets(List<PsaTrace> tracesRequiringAttention)
        {
            List<PsaDataset> result = new List<PsaDataset>();
            List<string> vins = new List<string>();
            foreach (PsaTrace psaTrace in tracesRequiringAttention)
            {
                if (!vins.Contains(psaTrace.Vin.ToUpper()))
                {
                    vins.Add(psaTrace.Vin.ToUpper());
                }
            }
            foreach (string vin in vins)
            {
                PsaDataset dataset = new PsaDataset();
                dataset.ExportedDate = DateTime.Now;
                dataset.SavedDate = DateTime.Now;
                dataset.VehicleId = 0; // service will fill it
                foreach (PsaTrace psaTrace in tracesRequiringAttention.
                    Where(t => t.Vin == vin))
                {
                    dataset.Traces.Add(psaTrace);
                }
                result.Add(dataset);
            }
            return result;
        }

        private List<PsaTraceSignature> GetTraceSignaturesForDataset(PsaDataset dataset)
        {
            return GetTraceSignaturesForTracesList(dataset.Traces);
        }

        private List<PsaTraceSignature> GetTraceSignaturesForTracesList(IList<PsaTrace> traces)
        {
            List<PsaTraceSignature> result = new List<PsaTraceSignature>();
            foreach (PsaTrace trace in traces)
            {
                result.Add(new PsaTraceSignature()
                {
                    TraceDate = trace.Date,
                    Vin = trace.Vin
                });
            }
            return result;
        }

        private bool ConnectionEstablished()
        {
            try
            {
                VtsWebServiceClient service = new VtsWebServiceClient();
                string ok = service.CheckConnection();
                service.Close();
                return ok == "ok";
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void MarkEverythingAsHalted()
        {
            List<PsaTrace> allTraces = traceInfos.Select(ti => ti.Trace).ToList();
            List<PsaTraceSignature> sigs = GetTraceSignaturesForTracesList(allTraces);
            StatusUpdated.Invoke(DataSynchronizationStatus.NetworkNotAccessible, 
                sigs);
        }

        private void MarkEverythingAsInProgress()
        {
            List<PsaTrace> allTraces = traceInfos.Select(ti => ti.Trace).ToList();
            List<PsaTraceSignature> sigs = GetTraceSignaturesForTracesList(allTraces);
            StatusUpdated.Invoke(DataSynchronizationStatus.InProgress,
                sigs);
        }

        private void JustAssociateVehiclesToCurrentUser()
        {
            if (!ConnectionEstablished())
            {
                return;
            }
            List<string> vins = new List<string>();
            foreach (PsaTraceInfo info in traceInfos)
            {
                if (!vins.Contains(info.Trace.Vin.ToUpper()))
                {
                    vins.Add(info.Trace.Vin.ToUpper());
                }
            }
            VtsWebServiceClient service = new VtsWebServiceClient();
            try
            {
                service.AssociateVehiclesWithUser(vins.ToArray(),
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash);
            }
            catch (Exception e)
            {
                Log.Error(e, "Unable to associate vehicles to user.");
            }
            finally
            {
                service.Close();
            }
        }

        public void Dispose()
        {
            worker.Dispose();
        }
    }
}
