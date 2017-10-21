using System;
using System.Globalization;
using System.Threading;
using Site.Common;
using VTS.Site.DomainObjects;

namespace Html.Models
{
    public class SiteSession
    {
        private static int culture;

        public User User { get; set; }

        public static int CurrentUICulture
        {
            get
            {
                UpdateCurrentThreadCulture(culture);
                return culture;
            }
            set
            {
                UpdateCurrentThreadCulture(value);
                culture = value;
            }
        }

        private static void UpdateCurrentThreadCulture(int value)
        {
            Thread.CurrentThread.CurrentUICulture = 
                new CultureInfo(LocalizationConverter.Convert(value));
            Thread.CurrentThread.CurrentCulture = 
                new CultureInfo(LocalizationConverter.Convert(value));
        }

        public static CultureInfo GetCurrentCulture()
        {
            return new CultureInfo(LocalizationConverter.Convert(culture));
        }
    }
}