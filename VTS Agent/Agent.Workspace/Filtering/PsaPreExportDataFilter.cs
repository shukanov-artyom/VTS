using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Common.Presentation.Data;
using Agent.Connector.Presentation.PSA.Workspace;
using Agent.Workspace.ViewModels;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.Filtering
{
    public class PsaPreExportDataFilter
    {
        private readonly IEnumerable<ExportablePsaTraceViewModel> sourceTraces;
        private readonly IEnumerable<ExportableVehicleViewModel> sourceVehicles;

        public PsaPreExportDataFilter(IEnumerable<ExportablePsaTraceViewModel> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            sourceTraces = source;
        }

        public PsaPreExportDataFilter(IEnumerable<ExportableVehicleViewModel> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            sourceVehicles = source;
        }

        public IEnumerable<PsaTrace> Filter()
        {
            if (sourceTraces != null)
            {
                foreach (PsaTrace t in EnumerateSelectedTraces(sourceTraces))
                {
                    yield return t;
                }
            }
            if (sourceVehicles != null)
            {
                foreach (ExportableVehicleViewModel veh in sourceVehicles)
                {
                    foreach (PsaTrace t in EnumerateSelectedTraces(veh.Traces))
                    {
                        yield return t;
                    }
                }
            }
        }

        private IEnumerable<PsaTrace> EnumerateSelectedTraces(
            IEnumerable<ExportablePsaTraceViewModel> unfiltered)
        {
            foreach (ExportablePsaTraceViewModel trace in unfiltered.Where(
                    t => t.IsSelectedForExport))
            {
                var res = new PsaTrace();
                trace.Model.CopyTo(res);
                foreach (ExportablePsaParametersSetViewModel vm in
                    trace.ParametersSets.Where(
                    ps => ps.IsSelectedForExport))
                {
                    var set = new PsaParametersSet();
                    vm.Model.CopyTo(set);
                    foreach (PsaParameterDataViewModel pd in vm.Parameters)
                    {
                        var data = new PsaParameterData(pd.Model.OriginalTypeId);
                        pd.Model.CopyTo(data);
                        set.Parameters.Add(data);
                    }
                    res.ParametersSets.Add(set);
                }
                yield return res;
            }
        }
    }
}
