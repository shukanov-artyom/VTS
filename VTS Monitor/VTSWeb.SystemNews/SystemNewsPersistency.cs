using System;
using System.Collections.Generic;
using VTS.Shared;
using VTSWeb.Common;

namespace VTSWeb.SystemNews
{
    public class SystemNewsPersistency
    {
        public delegate void NewsRetrieved(IList<SystemNewsItem> list);

        private NewsRetrieved newsRetrievedDelegate;
        private ErrorCallbackDelegate errorCallback;

        public SystemNewsPersistency(
            NewsRetrieved newsRetrievedDelegate, 
            ErrorCallbackDelegate errorCallback)
        {
            this.newsRetrievedDelegate = newsRetrievedDelegate;
            this.errorCallback = errorCallback;
        }

        public void GetAllNews()
        {
            throw new NotImplementedException();
        }

        public void GetTopNews()
        {
            throw new NotImplementedException();
        }
    }
}
