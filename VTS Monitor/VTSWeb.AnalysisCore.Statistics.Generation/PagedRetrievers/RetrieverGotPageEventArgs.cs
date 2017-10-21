using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers
{
    public class RetrieverGotPageEventArgs : EventArgs
    {
        public RetrieverGotPageEventArgs(IList<PsaDataset> page)
        {
            Page = page;
        }

        public IList<PsaDataset> Page
        {
            get;
            set;
        }
    }

    public delegate void RetrieverGotPageEventHandler(
        object sender, RetrieverGotPageEventArgs e);
}
