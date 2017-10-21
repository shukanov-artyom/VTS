using System;
using VTS.Shared.DomainObjects;

namespace VTS.Site.DomainObjects
{
    public class SystemNewsLocalized : DomainObject
    {
        public long SystemNewsId { get; set; }

        public string Header { get; set; }

        public string NewsContentText { get; set; }

        public string Language { get; set; }
    }
}
