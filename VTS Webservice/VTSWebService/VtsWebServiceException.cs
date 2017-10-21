using System;
using System.Runtime.Serialization;

namespace VTSWebService
{
    [DataContract]
    public class VtsWebServiceException
    {
        public VtsWebServiceException(string message)
        {
            Message = message;
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string StackTrace { get; set; }
    }
}