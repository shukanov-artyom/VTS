using System;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects;

namespace VTSWeb.SystemNews
{
    public class SystemNewsLocalizedItem : DomainObject
    {
        public string Locale { get; set; }

        public string Header { get; set; }

        public string Text { get; set; }
    }
}
