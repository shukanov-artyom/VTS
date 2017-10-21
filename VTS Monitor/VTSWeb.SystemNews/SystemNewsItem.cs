using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects;

namespace VTSWeb.SystemNews
{
    public class SystemNewsItem : DomainObject
    {
        private IList<SystemNewsLocalizedItem> localizedItems = 
            new List<SystemNewsLocalizedItem>();

        public DateTime DatePublished
        {
            get;
            set;
        }

        public bool IsBlocked
        {
            get;
            set;
        }

        public IList<SystemNewsLocalizedItem> LocalizedItems 
        {
            get
            {
                return localizedItems;
            }
        }
    }
}
