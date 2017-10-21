using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.DomainObjects.Psa.Persistency;

namespace VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers
{
    public abstract class DatasetsPagedRetrieverBase : IDatasetsPagedRetriever
    {
        private PsaDatasetPersistency persistency;
        private long lastId = 0;
        private long processedCount = 0;
        private long totalCount;
        private int pageSize = 5;

        public event RetrieverGotPageEventHandler GotNextPage;

        public event PagedRetrieverErrorEventHandler Error;

        protected long ProcessedCount
        {
            get
            {
                return processedCount;
            }
            set
            {
                processedCount = value;
            }
        }

        protected long LastId
        {
            get
            {
                return lastId;
            }
            set
            {
                lastId = value;
            }
        }

        protected PsaDatasetPersistency Persistency
        {
            get
            {
                return persistency;
            }
            set
            {
                persistency = value;
            }
        }

        protected int PageSize
        {
            get
            {
                return pageSize;
            }
        }

        protected void GotCount(long count)
        {
            totalCount = count;
        }

        protected void PageRetrievedCallback(IList<PsaDataset> page)
        {
            if (page.Count != 0)
            {
                ProcessedCount += page.Count;
                LastId = page.Max(ds => ds.Id);
            }
            ReportPageRetrieved(page);
        }

        protected void ErrorCallback(Exception e, string msg)
        {
            if (Error == null)
            {
                throw new Exception("Error handler should be at place here.");
            }
            Error.Invoke(this, new PagedRetrieverErrorEventArgs(e, msg));
        }

        private void ReportPageRetrieved(IList<PsaDataset> page)
        {
            if (GotNextPage == null)
            {
                throw new Exception("Result return handler should be at place here.");
            }
            GotNextPage.Invoke(this, new RetrieverGotPageEventArgs(page));
        }

        public abstract void GetNextPage();

        public int PercentComplete
        {
            get
            {
                if (totalCount == 0)
                {
                    return 100;
                }
                return (int)((processedCount / totalCount) * 100);
            }
        }
    }
}
