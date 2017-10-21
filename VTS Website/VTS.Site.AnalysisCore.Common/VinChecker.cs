using VTS.Site.DomainObjects.Enums;

namespace VTS.Site.AnalysisCore.Common
{
    /// <summary>
    /// Retrieves vehicle data by vin.
    /// </summary>
    public class VinChecker
    {
        private string vin;

        public VinChecker(string vin)
        {
            this.vin = vin;
        }

        public static bool IsValid(string vinCode)
        {
            string uppercaseVin = vinCode.ToUpper();
            if (uppercaseVin.Length == 17)
            {
                return true;
            }
            return false;
        }

        public static Manufacturer? GetManufacturer(string vin)
        {
            string uppercaseVin = vin.ToUpper();
            if (IsValid(uppercaseVin))
            {
                if (uppercaseVin.Substring(0, 3) == "VF7")
                {
                    return Manufacturer.Citroen;
                }
                if (uppercaseVin.Substring(0, 3) == "VF3")
                {
                    return Manufacturer.Peugeot;
                }
                if (uppercaseVin.Substring(0, 3) == "W0L")
                {
                    return Manufacturer.Opel;
                }
            }
            return null;
        }
    }
}
