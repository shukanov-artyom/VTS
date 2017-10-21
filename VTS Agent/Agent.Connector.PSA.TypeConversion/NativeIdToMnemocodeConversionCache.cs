using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Agent.BusinessObjects.Enums;

namespace Agent.Connector.PSA.TypeConversion
{
    internal static class NativeIdToMnemocodeConversionCache
    {
        private static readonly IDictionary<string, HashSet<Mnemocode>> Mapping =
            new Dictionary<string, HashSet<Mnemocode>>();

        private static readonly HashSet<string> alreadyFetched = new HashSet<string>();

        static NativeIdToMnemocodeConversionCache()
        {
            InitializeMapping();
        }

        public static HashSet<Mnemocode> Get(string nativeId)
        {
            if (!nativeId.StartsWith("@"))
            {
                return null;
            }
            lock (Mapping)
            {
                if (!Mapping.ContainsKey(nativeId))
                {
                    IEnumerable<Mnemocode> fromDb = GetFromDatabase(nativeId);
                    Set(nativeId, fromDb);
                }
                return Mapping.ContainsKey(nativeId) ? Mapping[nativeId] : null;
            }
        }

        public static void Set(string nativeId, Mnemocode code)
        {
            lock (Mapping)
            {
                if (!Mapping.ContainsKey(nativeId))
                {
                    Mapping[nativeId] = new HashSet<Mnemocode>();
                }
                Mapping[nativeId].Add(code);
            }
        }

        public static void Set(string nativeId, params string[] codes)
        {
            lock (Mapping)
            {
                if (!Mapping.ContainsKey(nativeId))
                {
                    Mapping[nativeId] = new HashSet<Mnemocode>(codes.Select(s => new Mnemocode(s)));
                }
                foreach (string code in codes)
                {
                    Mapping[nativeId].Add(new Mnemocode(code));
                }
            }
        }

        private static void Set(string nativeId, IEnumerable<Mnemocode> codes)
        {
            foreach (Mnemocode code in codes)
            {
                Set(nativeId, code);
            }
        }

        public static void Set(string nativeId, string mnemocode)
        {
            if (String.IsNullOrEmpty(nativeId))
            {
                throw new ArgumentNullException("nativeId");
            }
            if (String.IsNullOrEmpty(mnemocode))
            {
                throw new ArgumentNullException("mnemocode");
            }
            Set(nativeId, new Mnemocode(mnemocode));
        }

        private static IEnumerable<Mnemocode> GetFromDatabase(string nativeId)
        {
            if (!alreadyFetched.Contains(nativeId))
            {
                alreadyFetched.Add(nativeId);
                if (DatabaseMnemocodeResolver.DatabaseReady)
                {
                    return DatabaseMnemocodeResolver.Get(nativeId);
                }
            }
            return new List<Mnemocode>();
        }

        private static void InitializeMapping()
        {
            Set(@"@P16234-CITACT", "BSIDECONDA");
            Set(@"@P8985-CITACT", "BSIANTISCAN");
            Set(@"@P1984-CITACT", "CMDESSUYAGEAV");
            Set(@"@P6695-CITACT", "CMDDEGIVRAGE");
            Set(@"@P23995-CITACT", "ALNIVHIULEMINI");
            Set(@"@P15882-CITACT", "DEFAUTABR");
            Set(@"@P17514-CITACT", "NIVDELEST4");
            Set(@"@P128811-THESAU@\*", "MP_TOUCH_RAZ_ODOMETRE");
            Set(@"@P12276-CITACT", "MP_PB_TRANSMISSION_CODE_DEVERROUILLAGE");
            Set(@"@P25782-CITACT", "ETATAPPRENTISSAGE4");
            Set(@"@P10579-CITACT", "ETATVOLETRECYC");
            Set(@"@P10578-CITACT", "ETATDISTRIBUTION");
            Set(@"@P10577-CITACT", "ETATVOLETMIX");
            Set(@"@P13852-CITACT", "TENSIONCALCPULSEUR");
            Set(@"@P5201-CITACT", "TEMPERATUREINTER");
            Set(@"@P4685-CITACT", "TENSIONBATTERIE");
            Set(@"@P6670-CITACT", "NIVCARBUMESURE");
            Set(@"@P3436-CITACT@\+@P3595-CITACT", "TEMPAIREXT");
            Set(@"@P15605-CITACT", "NIVLAVEGLACEMINI");
            Set(@"@P15488-CITACT", "ALERTEMINICARBURANT");
            Set(@"@P28286-CITACT", "COMMTRANSPOK");
            Set(@"@P2277-CITACT", "DEMANDECLIM");
            Set(@"@P29813-CITACT@\*", "RELAISGMV");
            Set(@"@P35572-CITACT@\*", "VITESSEGMV");
            Set(@"@P19771-CITACT", "CONSIGNEGMV");
            Set(@"@P6980-CITACT", "ETATCALCULATEUR");
            Set(@"@P6972-CITACT", "ETATANTIDEMCODE");
            Set(@"@P8461-CITACT", "RELAISALIMTO");
            Set(@"@P2269-CITACT", "AUTORISATIONCLIM");
            Set(@"@P6642-CITACT", "ETIQRECONNUE");
            Set(@"@P22758-CITACT", "RCOVANNEEGR");
            Set(@"@P22759-CITACT", "RCORECOVANNEEGR");
            Set(@"@P22760-CITACT", "RCOPAPEGR");
            Set(@"@P22761-CITACT", "RCORECOPAPEGR");
            Set(@"@P22762-CITACT", "RCOVANNETURBO");
            Set(@"@P26079-CITACT@\+@P24379-CITACT", "RCORECOEVRAA");
            Set(@"@P1368-CITACT", "CA_PRESSION_ATMOS");
            Set(@"@P1373-CITACT", "RAPPORTBOITE");
            Set(@"@P17635-CITACT", "CONTACTFREIN");
            Set(@"@P17636-CITACT", "CONTACTFREINREGUL");
            Set(@"@P12534-CITACT", "CONTACTEMBRAYAGE");
            Set(@"@P987-CITACT", "ERREURTELECODAGE");
            Set(@"@P25779-CITACT", "DERNIERAPPRENTISSAGEVANNEEGR");
            Set(@"@P25780-CITACT", "PREMAPPRENTISSAGEVANNEEGR");
            Set(@"@P25781-CITACT", "ETATAPPRENTISSAGE3");
            Set(@"@P25783-CITACT@\+@P24379-CITACT", "ETATAPPRENTISSAGE5");
            Set(@"@P25784-CITACT@\+@P24379-CITACT", "ETATAPPRENTISSAGE6");
            Set(@"@P14440-CITACT", "CHAUFADDIT1");
            Set(@"@P6993-CITACT", "PRESSIONCLIM");
            Set(@"@P15616-CITACT", "ALIMRESCARCONF");
            Set(@"@P17515-CITACT@\*1", "NIVDELEST7");
            Set(@"@P17516-CITACT@\*1@\*2", "NIVDELEST8");
            Set(@"@P15764-CITACT", "DDEBVASPORT");
            Set(@"@P13033-CITACT", "CEINTBOUCLEAVG");
            Set(@"@P8930-CITACT", "PEDALEMBRAPP");
            Set(@"@P10041-CITACT", "SECUENFANTD");
            Set(@"@P10042-CITACT", "SECUENFANTG");
            Set(@"@P14737-CITACT", "CMDANTIBAV");
            Set(@"@P12705-CITACT", "ANTIDEMVERROU");
            Set(@"@P4807-CITACT@\*1", "CLE1SYNCHRO");
            Set(@"@P4807-CITACT@\*2", "CLE2SYNCHRO");
            Set(@"@P4807-CITACT@\*3", "CLE3SYNCHRO");
            Set(@"@P4807-CITACT@\*4", "CLE4SYNCHRO");
            Set(@"@P4807-CITACT@\*5", "CLE5SYNCHRO");
            Set(@"@P15631-CITACT", "ECMVERROU");
            Set(@"@P4808-CITACT", "NOMBRECLEACTIVES");
            Set(@"@P12450-CITACT", "NOMBRECLESSYNCHRO");
            Set(@"@P15632-CITACT", "BSIAPPAIRECM");
            Set(@"@P20682-CITACT", "BSIVIERGE");
            Set(@"@P17515-CITACT@\*2", "NIVDELEST6");

            Set(@"@P119777-THESAU@\*100", "MP_CONSOMMATION_INSTANTANEE");
            Set(@"@P33453-THESAU@\*100", "MP_CONSOMMATION_INSTANTANEE");
            Set(@"@P126046-THESAU", "MP_CONSO_MOYENNE_TRAJET");

            Set(@"@P100911-THESAU@\*", "CMDESSUYAGEAV");
            Set(@"@P110857-THESAU", "MP_DETECT_EFFRACTION_VOLUM");
            Set(@"@P107447-THESAU@\*1", "POSITIONROUE1");
            Set(@"@P109186-THESAU@\*", "NB");
            Set(@"@P109185-THESAU@\*", "SIGNA");
            Set(@"@P109184-THESAU@\*", "DATE_2");
            Set(@"@P108869-THESAU@\*:@\+@P18078-THESAU@\*1", "CARRAD1");

            Set(@"@P112741-THESAU@\*", "CODE_VIN");
            Set(@"@P112741-THESAU@\*", "CONFIG_NUM_SERIE_VEHICULE_CODE_VIN");

            Set(@"@P20122-CITACT", "NIVDELE03");
            Set(@"@P20122-CITACT", "NIVDELEST3");
            Set(@"@P20122-CITACT", "NIVDELESTAGE03");

            Set(@"@P87035-THESAU@\*",
                "CA_RAPPORT_BOITE_VITESSES",
                "MP_RAPPORT_BOITE",
                "MP_RAPPORT_BOITE_VIT",
                "MP_RAPPORT_BOITE_VITESSES",
                "RAPPBOITEVITESSES",
                "RAPPORTBOITE",
                "RAPPORTBV",
                "RAPPORTDEBOITE",
                "RAPPORT_DE_BOITE",
                "VA_RAPPORTBOITE");

            Set(@"@P112124-THESAU",
                "CONTACTFREIN",
                "FREINPRINCIPAL_EDC15");

            Set(@"@P111326-THESAU", "CONTACTFREINREDON");
            Set(@"@P126046-THESAU", "MP_CONSO_MOYENNE_TRAJET");
            Set(@"@P107566-THESAU@\+@T(@\+@P24901-THESAU@\*@\+@T)",
                "MP_INSTR_OBD_LIDCA_8", 
                "MP_KILOM_TOTAL");
            Set(@"@P122095-THESAU", "MP_POSITION_CLE_DEM");

            Set(@"@P125643-THESAU", 
                "Instant_Fuel_Consumption", 
                "MP_CONSOMMATION_INSTANTANEE");
            Set(@"@P1984-CITACT", "CMDESSUYAGEAV");
            Set(@"@P126044-THESAU", "CA_TEMPERATURE_EAU", "COOLANT_TEMP_TW", "Cooling_engine_temperature", "MP_TEMPERATURE_D_EAU_MOTEUR_d", "MP_TEMPERATURE_EAU_MOTEUR", "MP_TEMP_EAU_MOT", "TEMPERATUREEAU", "TEMPERATUREEAUMOT", "TEMPERATUREMOT", "TEMPMOTEUR", "VA_TEMPERATURE_EAU_MOTEUR");
            Set(@"@P125638-THESAU", "Brake_Fluid_Switch", "MP_ALERTE_NIVEAU_LIQUIDE_FREIN_MINI");
            Set(@"@P125639-THESAU", "MP_NIVEAU_LAVE_GLACE_MINI");
            Set(@"@P123238-THESAU@\*ESP", "MP_CDE_VOYANT_ESP");
            Set(@"@P119094-THESAU", "MODECLAUTO", "MP_ECL_SIGN_LIDC5_42", "MP_MODE_ECL_AUTO");
            Set(@"@P123214-THESAU", "MP_DEMANDE_LANTERNES");
            Set(@"@P125299-THESAU@\*", "CDEFEUXPOSD", "MP_COMMANDE_LANTERNES_DROITES");
            Set(@"@P125300-THESAU@\*", "CDEFEUXPOSG", "MP_COMMANDE_LANTERNES_GAUCHES");
            Set(@"@P123217-THESAU", "MP_DEMANDE_CODES");
            Set(@"@P115201-THESAU", "CMDCODES", "ECL_SIGN_LIDC5_20", "Head_Lamp_LO_ON_Duty", "MP_CDE_CODE");
            Set(@"@P46154-THESAU", "ALERTEPRESSIONHUILE", "ALERTPRESHUILE", "MP_ALERTE_PRESSION_HUILE");
            Set(@"@P107546-THESAU", "ALERTEEAUNIV", "ALERTNIVEAUMOT", "MP_ALERTE_NIV_EAU_MOT");
            Set(@"@P107817-THESAU", "MP_INSTR_MESURES_LIDC9_28", "MP_NIVEAU_HUILE_MESURE", "MP_NIVEAU_HUILE_MOTEUR_MESURE", "MP_NIVEAU_HUILE_MOTEUR_MESURE_CB", "MP_NIV_HUILE_MESURE", "NIVHUILEMESURE", "NIVOHUILE");
            Set(@"@P125626-THESAU", "MP_NIVEAU_HUILE_APRES_FILTRAGE");
            Set(@"@P107554-THESAU", "MP_INSTR_MESURES_LIDC9_30", "MP_NIV_CARB_MESURE", "NIVCARBMESURE", "NIVCARBUMESURE");
            Set(@"@P107818-THESAU@\*", "MP_INSTR_MESURES_LIDC9_44", "MP_NIV_CARB_AFFICHE", "NIVCARBAFFICHE", "NIVCARBUAFFIC");
            Set(@"@P23995-CITACT", "ALNIVHIULEMINI");
            Set(@"@P107566-THESAU@\+@T(@\+@P24901-THESAU@\*@\+@T)", "MP_INSTR_OBD_LIDCA_8", "MP_KILOM_TOTAL");
            Set(@"@P117695-THESAU", "MP_LIDD2_1", "MP_POSITION_LEVIER_BVA_BVMP");
            Set(@"@P108486-THESAU", "IMPEDJAUGECARBU", "MP_IMPEDANCE_JAUGE_CARB", "MP_INSTR_MESURES_LIDC9_48");
            Set(@"@P125629-THESAU", "MP_EAU_GAZOLE", "VA_TENSCAPPRESEAUGAZ");
            Set(@"@P125630-THESAU", "MP_AFFICHAGE_EAU_DANS_GASOIL");
            Set(@"@P107556-THESAU", "KMAVANTMAINT", "MPKMAVMAINT", "MP_INSTR_MESURES_LIDC9_46", "MP_KILOM_AVANT_MAINT");
            Set(@"@P126045-THESAU", "MP_JOUR_RESTANT_AVANT_MAINTENANCE");
            Set(@"@P125631-THESAU", "MP_KM_DERNIERE_RAZ");
            Set(@"@P125632-THESAU", "MP_DUREE_DEPUIS_DERNIERE_RAZ");
            Set(@"@P129684-THESAU", "MP_RAZ_2_EFFECTUEE");
            Set(@"@P45057-THESAU", "AMBIENCE_TEMP_TA", "MPTEMPEXT", "MP_TEMP_AIR_EXT", "MP_TEMP_EXT", "TEMPERATUREEXT", "TEMPEXT", "TEMP_EXT", "Temperatura_Esterna", "VA_TEMPEXT");
            Set(@"@P43478-THESAU", "Battery_voltage", "CA_TENSION_BATTERIE", "MP_ALIM_LIDC0_21", "MP_TENSION_BAT", "MP_TENSION_BATTERIE", "MP_TENS_BAT", "MP_VAL_TENSION_BAT", "TENSBAT", "TENSIONBAT", "TENSIONBATT", "TENSIONBATTERIE", "Tension_Batterie", "VA_TENSION_BAT", "VA_TENSION_BATTERIE", "Voltage", "Voltage_sensing_of_IOD_Line");
            Set(@"@P126023-THESAU", "MP_TENSION_EXCITATION_ALTERNATEUR");
            Set(@"@P3131-THESAU@\+@T(@\+@P122575-THESAU@\+@T)", "MP_PRESENCE_PLUS_APC");
            Set(@"@P126720-THESAU", "MP_DMD_DEM_CMM");
            Set(@"@P125574-THESAU", "MP_ETAT_GMP");
            Set(@"@P120317-THESAU", "MP_ETAT_PRINCIPAL");
            Set(@"@P125575-THESAU", "MP_CONFIG_SEV");
            Set(@"@P125576-THESAU", "MP_ETAT_MODE_ECO");
            Set(@"@P126723-THESAU", "MP_MODE_PHASE_VIE");
            Set(@"@P125281-THESAU@\*", "MP_CDE_POMPE_AIR");
            Set(@"@P125580-THESAU", "MP_ETAT_ZONE_MEMOIRE_SUITE_TELECHARGEMENT");
            Set(@"@P127335-THESAU", "MP_NBR_RESET_BSI");
            Set(@"@P126726-THESAU", "MP_COMPTEUR_RESET_HARD");
            Set(@"@P91156-THESAU@\*", "ID_VERSION_LOGICIEL", "ID_VERSION_SOFT", "LIN_Software_Version_Sunroof", "VERSION");
            Set(@"@P112983-THESAU", "ID_APPLI_VERSION", "ID_APPLI_VERSION_FE", "ID_INDICE_EVOLUTION_SOFT", "ID_VERS_LOGICIEL");
            Set(@"@P125194-THESAU", "DiagnosticVersion", "ID_VERSION_DIAGNOSTIC");
            Set(@"@P109183-THESAU@\*", "EDITION", "EDITION_SOFT", "ID_EDITION_LOGICIEL", "ID_EDITION_SOFT", "ID_INDICE_EVOLUTION_EDITION", "ID_VERSION_LOGICIEL", "MP_SOFT_EDITION");
            Set(@"@P89271-THESAU@\*", "SORTIECOMPRESS");
            Set(@"@P90122-THESAU", "CONSOMOYTRAJ", "CONSOMOYTRAJET");
            Set(@"@P112985-THESAU@\*", "ID_NUM_COMP2", "ID_REF_PSA", "ID_REF_PSA_new_3", "NUM_COMPL2", "REFHARD", "REF_PSA");
            Set(@"@P78763-THESAU", "NBTELECODAGE", "NUM");
            Set(@"@P5089-THESAU", "B2CAFFFF01", "B328FFFF01", "B333FFFF01", "COMBINIVHUIL", "COMBI_NIVHUILE", "COMBI_NIVO_HUILE", "COMB_LIDB3", "NIVEAUHUILE", "NIVHUILE");
            Set(@"@P118835-THESAU", "MP_ETAT_APPRENTISSAGE_PAPILLON_MOTORISE");
            Set(@"@P118836-THESAU", "APPRENTISSAGEVANNEEGR", "MP_ETAT_APPRENTISSAGE_EV_EGR");
            Set(@"@P116354-THESAU", "MP_PRESSION_CIRCUIT_REFRIGERANT", "MP_PRESSION_CIRCUIT_REFRIGERATION", "MP_PRES_CIRCUIT_REFRIGERATION", "PRESSIONCIRREF", "Pression_du_circuit_refrigerant");
            Set(@"@P87027-THESAU@\*", "ETATSONDEAMONT", "ETAT_SONDE_1_AMONT", "MP_ETAT_SONDE_AMONT", "MP_ETAT_SONDE_AMONT_SUP", "MP_ETAT_SONDE_OXYGENE_AMONT");
            Set(@"@P115628-THESAU@\*@T4", "BOBINE4", "MP_TEMPS_CHARGE_BOBINE_4");
            Set(@"@P115628-THESAU@\*@T1", "BOBINE1", "MP_TEMPS_CHARGE_BOBINE_1");
            Set(@"@P115628-THESAU@\*@T2", "BOBINE2", "MP_TEMPS_CHARGE_BOBINE_2");
            Set(@"@P115628-THESAU@\*@T3", "BOBINE3", "MP_TEMPS_CHARGE_BOBINE_3");
            Set(@"@P112031-THESAU@\*", "MP_RCO_EV_CANISTER", "RCOCANISTER", "RCOVANNEEGR");
            Set(@"@P125637-THESAU", "MP_AFFICHAGE_DEFAUT_CHARGE_BATTERIE");
            Set(@"@P125610-THESAU", "MP_INVERSION_CODES_PHARES");
            Set(@"@P123218-THESAU", "MP_DEMANDE_APPEL_AUTO", "OPT_HORN_RQ");
            Set(@"@P125302-THESAU", "MP_CDE_ANTIBROUILLARD_AVANT");
            Set(@"@P135354-THESAU@\+@T(@\+@P122575-THESAU@\+@T)", "MP_CMD_APC_BSM");
            Set(@"@P125578-THESAU", "MP_CDE_COUPURE_POMPE_CARB");
            Set(@"@P126652-THESAU", "ID_REF_PSA");
            Set(@"@P112984-THESAU@\*", "ID_INDICE_EVOLUTION_EDITION");
            Set(@"@P105000-THESAU@\*", "CMPLFOURN", "EDITIONSOFT2", "FILE", "ID_NUMERO_FICHIER_ULP_CAL", "ID_NUM_COMP", "ID_NUM_COMPLEMENTAIRE", "ID_REF_LOG", "ID_REF_LOGICIEL", "ID_REF_SOFT", "ID_VERSION_LOGICIEL", "MP_REF_LOGICIEL", "NUM_COMPL", "REFERENCE_LOG", "REFERENCE_SOFT", "REF_LOG", "REF_LOG2", "REF_LOGICIEL", "REF_SOFT", "REF_SOFT_ULP", "SIGNTELECHARGE");
            Set(@"@P4155-THESAU", "COMGMV", "GMV");
            Set(@"@P102368-THESAU", "ETATDEMSUSPSPORT", "MP_D_SUSPENSION_SPORT");
            Set(@"@P87428-THESAU", "ETATMODEECO", "MODEUSINE", "MP_ALIM_LIDC0_12");
            Set(@"@P110840-THESAU", "DEMECLAPLAFON", "DEMECLPLAF", "DEMFINPLAF", "ETATDEMECLAIPLAFON", "MP_ECL_SIGN_LIDC5_28");
            Set(@"@P65320-THESAU", "B2B2FFFF01", "COMANTIBROUAR", "LIDB2_178");
            Set(@"@P110856-THESAU", "ETATDEMEJECTVOLUM", "MP_FERM_ALR_LIDC6_6", "NTRPROTVOLUM");
            Set(@"@P9770-THESAU@\*", "MP_TEMP_HUILE_MOT", "TEMPERATUREHUILE", "TEMPHUILEMOT");
            Set(@"@P110874-THESAU", "EAUDANSGAZOLE", "EAUGAZOLE");
            Set(@"@P107023-THESAU", "CA_TEMPERATURE_AIR_EXTERIEUR", "MP_INSTR_MESURES_LIDC9_35", "MP_TEMPERATURE_AIR_EXTERIEUR", "MP_TEMPERATURE_AIR_EXTERIEUR_C2", "MP_TEMPERATURE_AIR_EXTERIEUR_CB", "MP_TEMP_AIR_EXTERN", "TEMPAIREXT", "TEMPAIRMESURE", "TEMPERATUREAIREXT", "TEMPERATUREEXTER", "VA_TEMPERATURE_AIR_EXTERIEUR");
            Set(@"@P100986-THESAU", "ETATINFOAFESSVITAV", "INFARRFIXESSUYAV", "VISIB_LIDCD_27");
            Set(@"@P100964-THESAU", "ETATDEMESSPV", "MP_DEMANDE_ESSUYAGE_PARE_BRISE_PV");
            Set(@"@P100963-THESAU", "ETATDEMESSGV", "MP_DEMANDE_ESSUYAGE_PARE_BRISE_GV");
            Set(@"@P19649-THESAU", "DATEFAB", "DATEFABSOFT", "DATE_FAB", "ID_DATE_FAB", "ID_DATE_FABRIC", "ID_DATE_TELECH", "MP_DATE", "MP_DATE_FAB", "MP_DATE_FABRICATION", "MP_HISTO_ANNEE_FAB", "MP_JOUR_FAB_MP_MOIS_FAB_MP_ANNEE_FAB");
            Set(@"@P107127-THESAU@\*", "NUM_SERIE_VIN");
            Set(@"@P106095-THESAU@\*", "CODEDIT", "CODEDIT03", "EDITIONSOFT2", "ID_REF_FIAT", "ID_SOFT", "ID_VERSION_LOGICIEL", "VERSIONSOFT", "VERSION_LOG");
            Set(@"@P108278-THESAU@\*", "RADIOFREQP");
            Set(@"@P108279-THESAU@\*", "RADIOFREQM");
            Set(@"@P108280-THESAU@\*", "RADIOVOLUMEP");
            Set(@"@P108281-THESAU@\*", "RADIOVOLUMEM");
            Set(@"@P108691-THESAU@\*", "RADIOMODETEL");
            Set(@"@P108283-THESAU", "RADIOCHOIX");
            Set(@"@P2390-THESAU", "CAPOT", "CAPOTOUVERT");
            Set(@"@P119117-THESAU", "CMDCONDA", "FERM_CONDA_LIDC8_5");
            Set(@"@P119123-THESAU", "CMDDECONDA", "FERM_CONDA_LIDC8_6");
            Set(@"@P119118-THESAU@\*", "CMDSUPERCONDA", "SUPERCONDA");
            Set(@"@P122109-THESAU", "CPOPORTEARD", "MP_CPO_PORTE_ARD");
            Set(@"@P122111-THESAU", "CPOPORTEAVD", "MP_CPO_PORTE_AVD");
            Set(@"@P122112-THESAU", "CPOPORTEAVG", "MP_CPO_PORTE_AVG");
            Set(@"@P100965-THESAU", "DEMOUVRECOFFRE", "ETATDEMOUVCOFF", "MP_FERM_CONDA_LIDC8_17");
            Set(@"@P90091-THESAU", "ETATCONDA", "LIDC8_PAR38");
            Set(@"@P119768-THESAU", "ENTE2V2SERAVD");
            Set(@"@P119769-THESAU", "ENTE2V2SERAVG");
            Set(@"@P111694-THESAU", "ALERTEEAUNIV");
            Set(@"@P89099-THESAU", "ALERTETEMPEAU", "ALERTETEMPEAUMOTEUR");
            Set(@"@P115358-THESAU", "NIVLAVEGLACEMINI");
            Set(@"@P87426-THESAU", "MP_INSTR_MESURES_LIDC9_34", "RAPPELMESSNAV");
            Set(@"@P118183-THESAU", "TEMPHUILEMOTEUR");
            Set(@"@P115214-THESAU", "MP_INSTR_MESURES_LIDC9_43", "MP_NIV_CARB_LISSE", "NIVCARBULISSE");
            Set(@"@P86969-THESAU@\*", "MP_RELAIS_GMV", "MP_REL_GMV", "MP_VIT_GMV", "RELAISGMV", "RELAISGMV2");
            Set(@"@P110065-THESAU","CONSIGNEGMV");
            Set(@"@P105976-THESAU@\*@T2/3","BOBINE23","TEMPSDECHARGEBOBINE2SUR3","TEMPSDECHARGEBOBINE2sur3");
            Set(@"@P105976-THESAU@\*@T1/4","BOBINE14","TEMPSDECHARGEBOBINE1SUR4","TEMPSDECHARGEBOBINE1sur4");
            Set(@"@P4541-THESAU", "MP_TENSION_PAPILLON", "MP_TENSION_POSITION_PAPILLON", "TENSIONPAPILLON");
            Set(@"@P10059-THESAU", "COUPUREINJECTION", "COUPURE_DECELERATION", "MP_COUPURE_INJECTION");
            Set(@"@P74322-THESAU@\*", "REGULPAUSE");
            Set(@"@P108277-THESAU", "REGULVITM");
            Set(@"@P10799-THESAU", "REGULATEUR_VITESSE", "REGULONOFF");
            Set(@"@P107510-THESAU@\*", "BSIVIERGE");
            Set(@"@P119112-THESAU", "BSIANTISCAN", "MP_BSI_ANTI_SCANNING", "MP_FERM_AD_LIDC7_28");
            Set(@"@P104656-THESAU", "CMDESSUYAVGV");
            Set(@"@P115215-THESAU@\*", "CMDESSUYAGEAR");
            Set(@"@P106610-THESAU@\*", "CMDDEGIVRAGE");
            Set(@"@P13546-THESAU@\*", "MP_RAPPORT_BOITE", "RAPPORT", "RAPPORTBV");

            //Set(@"", "");
            //Set(@"", "");
            //Set(@"", "");

            //Set(@"", "");
            //Set(@"", "");
            //Set(@"", "");
            //Set(@"", "");
            //Set(@"", "");
            //Set(@"", "");
        }
    }
}
