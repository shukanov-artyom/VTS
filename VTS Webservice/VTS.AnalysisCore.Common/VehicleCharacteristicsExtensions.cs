using System;
using System.Linq;
using VTS.AnalysisCore.Common.Psa;
using VTS.Shared.DomainObjects;


namespace VTS.AnalysisCore.Common
{
    public static class VehicleCharacteristicsExtensions
    {
        public static string GetEngineFamilyString(
            this VehicleCharacteristics chars)
        {
            string generalInformationGroupName =
                    LocalizedCharacteristicsStrings.
                    ResolveGeneralInformationGroupName(chars.Language);
            VehicleCharacteristicsItemsGroup generalInformationGroup =
                chars.ItemsGroups.FirstOrDefault(
                g => g.Name.Contains(generalInformationGroupName));
            if (generalInformationGroup == null)
            {
                throw new NoInfoForVinException("Insufficient characteristics data");
            }
            string engineFamilyString =
                generalInformationGroup.GetValueByKey(
                LocalizedCharacteristicsStrings.
                ResolveEngineFamilyKey(chars.Language));
            return engineFamilyString;
        }

        public static string GetEngineModelString(
            this VehicleCharacteristics chars)
        {
            string generalInformationGroupName =
                    LocalizedCharacteristicsStrings.
                    ResolveGeneralInformationGroupName(chars.Language);
            VehicleCharacteristicsItemsGroup generalInformationGroup =
                chars.ItemsGroups.FirstOrDefault(
                g => g.Name.Contains(generalInformationGroupName));
            if (generalInformationGroup == null)
            {
                throw new NoInfoForVinException("Insufficient characteristics data");
            }
            string engineModelKeyString =
                LocalizedCharacteristicsStrings.
                ResolveEngineModelKey(chars.Language);
            string engineModelString =
                generalInformationGroup.GetValueByKey(engineModelKeyString);
            return engineModelString;
        }

        public static string GetVehicleModelString(
            this VehicleCharacteristics chars)
        {
            string key = LocalizedCharacteristicsStrings.
                ResolveVehicleModelKey(chars);
            /*string groupKey = LocalizedCharacteristicsStrings.
                ResolveVehicleModelGroupKey(chars.Language);*/
            string groupKey = chars.GeneralVehicleInfo;
            VehicleCharacteristicsItemsGroup characteristicsGroup = chars.
                ItemsGroups.FirstOrDefault(g =>
                    g.Name.ToUpper().Contains(groupKey.ToUpper()));
            return characteristicsGroup.GetValueByKey(key);
        }

        public static string GetValueByKey(
            this VehicleCharacteristicsItemsGroup group, string key)
        {
            VehicleCharacteristicsItem item = group.Items.FirstOrDefault(i =>
                    i.Name.ToUpper() == key.ToUpper());
            if (item == null)
            {
                return String.Empty;
            }
            return item.Value;
        }
    }
}
