using System;
using System.Globalization;

namespace VTSWeb.Presentation.Common
{
    public class LanguageViewModel : ViewModelBase
    {
        private string locale;
        private CultureInfo loc;

        public LanguageViewModel(CultureInfo loc)
        {
            locale = loc.NativeName;
            this.loc = loc;
        }

        public string Locale
        {
            get
            {
                return locale;
            }
        }

        public CultureInfo Model
        {
            get
            {
                return loc;
            }
        }
    }
}
