using System;

namespace VTS.Shared
{
    public class VinChecker
    {
        public static bool IsValid(string vinCode)
        {
            string uppercaseVin = vinCode.ToUpper();
            if (uppercaseVin.Length == 17)
            {
                return true;
            }
            return false;
        }

        public static Manufacturer GetManufacturer(string vin)
        {
            string uppercaseVin = vin.ToUpper();
            if (IsValid(uppercaseVin))
            {
                if (uppercaseVin.Substring(0, 3) == "VF7")
                {
                    return Manufacturer.Citroen;
                }
                if (uppercaseVin.Substring(0, 3) == "VF3" ||
                    uppercaseVin.Substring(0, 3) == "Z8T")
                {
                    return Manufacturer.Peugeot;
                }
                if (uppercaseVin.Substring(0, 3) == "W0L")
                {
                    return Manufacturer.Opel;
                }
            }
            throw new NotSupportedException("Manufacturer not supported!");
        }
    }
}
