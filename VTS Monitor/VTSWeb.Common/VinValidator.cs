using System;
using VTS.Shared;

namespace VTSWeb.Common
{
    public class VinValidator
    {
        public static bool Validate(string vin)
        {
            string uppercaseVin = vin.ToUpper();
            if (uppercaseVin.Length == 17)
            {
                return true;
            }
            return false;
        }

        public static Manufacturer? GetManufacturer(string vin)
        {
            string uppercaseVin = vin.ToUpper();
            if (Validate(uppercaseVin))
            {
                if (uppercaseVin.Substring(0, 3) == "VF7")
                {
                    return Manufacturer.Citroen;
                }
                if (uppercaseVin.Substring(0, 3) == "VF3")
                {
                    return Manufacturer.Peugeot;
                }
            }
            return null;
        }
    }
}
