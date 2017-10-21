using System;
using System.Collections.Generic;
using System.Linq;

namespace Site.Common
{
    public static class LocalizationConverter
    {
        private static IDictionary<int, string> mapping = 
            new Dictionary<int, string>();

        static LocalizationConverter()
        {
            mapping.Add(0, "en-GB");
            mapping.Add(1, "ru-RU");
            mapping.Add(2, "be-BY");
        }

        public static string Convert(int value)
        {
            return mapping[value];
        }

        public static int Convert(string value)
        {
            return mapping.First(m => m.Value == value).Key;
        }
    }
}
