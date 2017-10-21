using System;
using System.Collections.Generic;
using System.Globalization;

namespace VTSWeb.Common
{
    public static class CsvCoder
    {
        public static string Encode(IList<long> elements)
        {
            string result = elements[0].ToString(
                CultureInfo.InvariantCulture);
            for (int i = 1; i < elements.Count; i++)
            {
                result = string.Format("{0},{1}", result, elements[i]);
            }
            return result;
        }

        public static IList<long> Decode(string source)
        {
            IList<long> result = new List<long>();
            string[] array = source.Split(',');
            foreach (string s in array)
            {
                result.Add(long.Parse(s));
            }
            return result;
        }
    }
}
