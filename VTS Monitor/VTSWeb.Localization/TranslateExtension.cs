using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace VTSWeb.Localization
{
    public class TranslateExtension : MarkupExtension
    {
        private string key;

        public TranslateExtension(string key)
        {
            this.key = key;
        }

        public TranslateExtension()
        {

        }

        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Binding binding = new Binding("Value")
            {
                Source = new TranslationData(key)
            };
            return binding.ProvideValue(serviceProvider);
        }
    }
}
