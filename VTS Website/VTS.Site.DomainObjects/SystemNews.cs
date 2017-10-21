using System;
using System.Collections.Generic;
using System.Linq;
using Site.Common;
using VTS.Shared.DomainObjects;

namespace VTS.Site.DomainObjects
{
    public class SystemNews : DomainObject
    {
        private readonly List<SystemNewsLocalized> localized =
            new List<SystemNewsLocalized>();

        public SystemNews()
        {
            DatePublished = DateTime.Now;
        }

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

        public string GetTopic(int lang)
        {
            return SystemNewsLocalized.First(l => l.Language == 
                LocalizationConverter.Convert(lang)).Header;
        }

        public string GetText(int lang)
        {
            return SystemNewsLocalized.First(l => l.Language ==
                LocalizationConverter.Convert(lang)).NewsContentText;
        }
    }
}
