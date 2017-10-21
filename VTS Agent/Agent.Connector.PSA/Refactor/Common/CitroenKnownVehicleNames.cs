using System.Collections.Generic;

namespace Agent.Connector.PSA.Refactor.Common
{
    internal static class KnownVehicleNames
    {
        private static IDictionary<string, string> dictionary =
            new Dictionary<string, string>();

        static KnownVehicleNames()
        {
            dictionary.Add(@"@P27373-CITTD", "Evasion");
            dictionary.Add(@"@P16403-CITTD", "C5");
            dictionary.Add(@"@P16408-CITTD", "C8");
            dictionary.Add(@"@P20015-CITTD", "C4");
            dictionary.Add(@"@P36916-CITTD", "C5 (X7)");
            dictionary.Add(@"@P02026-CITTD", "Xsara Picasso");
            dictionary.Add(@"@P20014-CITTD", "C5 Restyle");
        }

        public static bool Knows(string codename)
        {
            return dictionary.ContainsKey(codename);
        }

        public static string Get(string codename)
        {
            if (Knows(codename))
            {
                return dictionary[codename];
            }
            return "<?>";
        }
    }
}
