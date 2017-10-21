using System;
using System.Globalization;
using System.Linq;
using VTS.AnalysisCore.Common.Psa;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Recognition.Psa
{
    public class PsaVehicleProductionYearRecognizer : IVehicleProductionYearRecognizer
    {
        public int Recognize(VehicleCharacteristics characteristics)
        {
            string itemName = LocalizedCharacteristicsStrings.ResolveProductionDateKey(characteristics.Language);
            VehicleCharacteristicsItemsGroup group = 
                characteristics.ItemsGroups.FirstOrDefault(ig => 
                    ig.Name.Equals(characteristics.GeneralVehicleInfo, 
                    StringComparison.InvariantCultureIgnoreCase));
            VehicleCharacteristicsItem item = group.Items.FirstOrDefault(i => 
                i.Name.Equals(itemName, StringComparison.InvariantCultureIgnoreCase));
            string date = DateTime.Today.ToShortDateString();
            if (item != null)
            {
                date = item.Value;
            }
            string[] split = date.Split('/');
            return Int32.Parse(split[2]);
        }
    }
}
