using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using VTS.Shared;
using VTSWebService.DataAccess;
using VTSWebService.DomainObjects.Assemblers;
using VehicleCharacteristicsEntity = VTSWebService.DataAccess.VehicleCharacteristics;
using VehicleCharacteristics = VTS.Shared.DomainObjects.VehicleCharacteristics;

namespace VTSWebService.VendorInfo
{
    public class VehicleCharacteristicsManager
    {
        public VehicleCharacteristics GetVehicleCharacteristicsForVin(
            string vin, SupportedLanguage preferredLanguage)
        {
            CharacteristicsLanguageMapper mapper =
                    new CharacteristicsLanguageMapper(
                        VinChecker.GetManufacturer(vin));
            string langCode = mapper.GetSpecificCode(preferredLanguage);

            // 1. check our database
            VehicleCharacteristics charsFromDatabase =
                GetExactMatchFromDatabase(vin, langCode);
            if (charsFromDatabase != null)
            {
                return charsFromDatabase;
            }
            
            // 2. Do we have anything at all for this vin?
            bool haveAnything = DoWeHaveAnythingForThisVin(vin);
            if (haveAnything)
            {
                RetrieveAndPersistAllLangsWeDoNotHaveYetForThisVin(vin);
            }
            else
            {
                RetrieveAndPersistAllLanguagesForThisVin(vin);
            }

            // 3. Get best match
            return GetBestMatchFromOurDatabase(vin, langCode);
        }

        private VehicleCharacteristics GetExactMatchFromDatabase(
            string vin, string language)
        {
            using (VTSDatabase db = new VTSDatabase())
            {
                IList<VehicleCharacteristicsEntity> exactMatch =
                    db.VehicleCharacteristics.Where(vc => 
                        vc.Vin == vin && vc.Language == language).ToList();
                if (exactMatch.Count == 0)
                {
                    return null;
                }
                return VehicleCharacteristicsAssembler.
                    FromEntityToDomainObject(exactMatch.First());
            }
        }

        private void RetrieveAndPersistAllLangsWeDoNotHaveYetForThisVin(string vin)
        {
            IList<SupportedLanguage> langsWeLackForThisVin =
                GetLangsWeDoNotHaveForVin(vin);
            foreach (SupportedLanguage language in langsWeLackForThisVin)
            {
                IVendorCharacteristicsProvider provider =
                    VendorCharacteristicsProviderFactory.Create(
                    VinChecker.GetManufacturer(vin));
                CharacteristicsLanguageMapper mapper = 
                    new CharacteristicsLanguageMapper(VinChecker.GetManufacturer(vin));
                VehicleCharacteristics chars = provider.GetByVin(vin, 
                    mapper.GetSpecificCode(language));
                PersistVehicleCharacteristics(chars);
            }
        }

        private void RetrieveAndPersistAllLanguagesForThisVin(string vin)
        {
            List<string> requiredNativeLangCodes = new List<string>();
            foreach (SupportedLanguage value in
                Enum.GetValues(typeof(SupportedLanguage)))
            {
                CharacteristicsLanguageMapper mapper =
                    new CharacteristicsLanguageMapper(
                        VinChecker.GetManufacturer(vin));
                string nativeLangValue = mapper.GetSpecificCode(value);
                if (!requiredNativeLangCodes.Contains(nativeLangValue))
                {
                    requiredNativeLangCodes.Add(nativeLangValue);
                }
            }
            foreach (string code in requiredNativeLangCodes)
            {
                IVendorCharacteristicsProvider provider =
                    VendorCharacteristicsProviderFactory.Create(
                VinChecker.GetManufacturer(vin));
                VehicleCharacteristics chars = provider.GetByVin(vin, code);
                PersistVehicleCharacteristics(chars);
            }
        }

        private bool DoWeHaveAnythingForThisVin(string vin)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                return database.VehicleCharacteristics.Any(
                    vc => vc.Vin == vin);
            }
        }

        private VehicleCharacteristics GetBestMatchFromOurDatabase(
            string vin, string preferredLang)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                VehicleCharacteristicsEntity perfectMatch =
                    database.VehicleCharacteristics.FirstOrDefault(
                        vc => vc.Vin == vin && vc.Language == preferredLang);
                if (perfectMatch != null)
                {
                    return VehicleCharacteristicsAssembler.
                        FromEntityToDomainObject(perfectMatch);
                }
                VehicleCharacteristicsEntity anything =
                    database.VehicleCharacteristics.FirstOrDefault(
                        vc => vc.Vin == vin);
                if (anything == null)
                {
                    return null;
                }
                return VehicleCharacteristicsAssembler.
                        FromEntityToDomainObject(anything);
            }
        }

        private List<SupportedLanguage> GetLangsWeDoNotHaveForVin(string vin)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                List<SupportedLanguage> result = new List<SupportedLanguage>();
                foreach (SupportedLanguage value in 
                    Enum.GetValues(typeof(SupportedLanguage)))
                {
                    CharacteristicsLanguageMapper mapper = 
                        new CharacteristicsLanguageMapper(VinChecker.GetManufacturer(vin));
                    string langValue = mapper.GetSpecificCode(value);
                    if (!database.VehicleCharacteristics.Any(vc =>
                        vc.Vin == vin && vc.Language == langValue))
                    {
                        if (!result.Contains(value))
                        {
                            result.Add(value);
                        }
                    }
                }
                return result;
            }
        }

        private void PersistVehicleCharacteristics(VehicleCharacteristics chars)
        {
            VehicleCharacteristicsEntity entity =
                VehicleCharacteristicsAssembler.FromDomainObjectToEntity(chars);
            using (VTSDatabase database = new VTSDatabase())
            {
                database.VehicleCharacteristics.Add(entity);
                try
                {
                    database.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    string result = String.Empty;
                    foreach (DbEntityValidationResult eve in e.EntityValidationErrors)
                    {
                        string entityTypename = eve.Entry.Entity.GetType().Name;
                        string errorString = String.Empty;
                        foreach (DbValidationError error in eve.ValidationErrors)
                        {
                            errorString = error.PropertyName + " : " + error.ErrorMessage;
                        }
                        result = String.Format("{0} | {1}", result, entityTypename + " -> " + errorString);
                    }
                    throw new Exception(result);
                }
            }
        }
    }
}
