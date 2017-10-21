using System;
using System.Collections.Generic;

namespace VTS.Shared.DomainObjects
{
    public class SystemNews : DomainObject
    {
        private readonly List<SystemNewsLocalized> localized = 
            new List<SystemNewsLocalized>();

        public List<SystemNewsLocalized> SystemNewsLocalized
        {
            get
            {
                return localized;
            }
        }

        public bool IsBlocked
        {
            get;
            set;
        }

        public DateTime DatePublished
        {
            get;
            set;
        }
    }
}
