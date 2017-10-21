using System;

namespace VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers
{
    public class PagedRetrieverErrorEventArgs : EventArgs
    {
        public PagedRetrieverErrorEventArgs(Exception e, string msg)
        {
            Error = e;
            Msg = msg;
        }

        public Exception Error { get; set; }

        public string Msg { get; set; }
    }

    public delegate void PagedRetrieverErrorEventHandler(
        object sender, PagedRetrieverErrorEventArgs e);
}
