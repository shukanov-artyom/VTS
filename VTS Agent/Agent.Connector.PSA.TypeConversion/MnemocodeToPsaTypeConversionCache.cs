using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Logging;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.TypeConversion
{
    internal static class MnemocodeToPsaTypeConversionCache
    {
        private static readonly IDictionary<Mnemocode, PsaParameterType> MappingCache = 
            new Dictionary<Mnemocode, PsaParameterType>();

        static MnemocodeToPsaTypeConversionCache()
        {
            InitializeMapping();
        }

        public static PsaParameterType Get(Mnemocode code)
        {
            lock (MappingCache)
            {
                if (!MappingCache.ContainsKey(code))
                {
                    return PsaParameterType.Unsupported;
                }
                return MappingCache[code];
            }
        }

        public static void Add(Mnemocode code, PsaParameterType type)
        {
            lock (MappingCache)
            {
                if (MappingCache.ContainsKey(code) &&
                    MappingCache[code] != type)
                {
                    Log.Warn(String.Format("Wrong mapping attempt: {0} to {1} vs {2}", code.Code, type, MappingCache[code]));
                    return;
                }
                MappingCache[code] = type;
            }
        }

        public static void Add(string code, PsaParameterType type)
        {
            Add(new Mnemocode(code), type);
        }

        public static void Add(PsaParameterType type, params string[] codes)
        {
            foreach (var code in codes)
            {
                Add(new Mnemocode(code), type);
            }
        }

        public static Mnemocode MapBack(PsaParameterType type)
        {
            Mnemocode result;
            lock (MappingCache)
            {
                result = MappingCache.FirstOrDefault(it => it.Value == type).Key;
            }
            return result;
        }

        private static void InitializeMapping()
        {
            Add("COURANTMOTEUR", PsaParameterType.ElectropumpAssemblyMotorCurrent);
            Add("SIGNALMOTTOURNANT", PsaParameterType.EngineRunningInformation);
            Add("SROTATIONVOLANT", PsaParameterType.SteeringWheelRotationDirection);
            Add("NIVEAUHUILEMES1", PsaParameterType.MeasuredOilLevel);
            Add("TEMPERATUREHUILE1", PsaParameterType.EngineOilTemperature);
            Add("CDEPOMPEAIR", PsaParameterType.AirPumpControl);
            Add("COUPALIMPOMPECARB", PsaParameterType.FuelPumpSupplyCutOff);
            Add("PRESIGVITVEH", PsaParameterType.VehicleSpeedSignalPresence);
            Add("PRESSIGANGVOL", PsaParameterType.SteeringWheelRotationAngleDataPresence);
            Add("TEMPMOSFET", PsaParameterType.SteeringElectropumpTemperature);
            Add("PRESSIONCARBURANT", PsaParameterType.FuelSystemPressure);
            Add("PRESSIONTURBOBRU", PsaParameterType.TurbinePressure);
            Add("RCOVANNETURBO", PsaParameterType.TurbineCycleRatio);
            Add("REGIMEMOTEUR", PsaParameterType.EngineRpm);
            Add("RCOPAPEGR", PsaParameterType.ExhaustRecirculationGateCycle);
            Add("CONSPRESSIONADM", PsaParameterType.NominalTurbinePressure);
            Add("DEBITAIR", PsaParameterType.MeasuredAirVolume);
            Add("DEBITCONSIGNE", PsaParameterType.InjectionVolumeSetting);
            Add("ECARTPRAIL", PsaParameterType.FuelSystemPressureDelta);
            Add("COURANTREGULPCARB", PsaParameterType.FuelRegulatorCurrent);
            Add("TENSIONBATTERIE", PsaParameterType.BatteryVoltage);
            Add("RCOVANNEEGR", PsaParameterType.ExhaustCycleRatio);
            Add("CORRECTIONDEBITINJ1", PsaParameterType.Injector1Correction);
            Add("CORRECTIONDEBITINJ2", PsaParameterType.Injector2Correction);
            Add("CORRECTIONDEBITINJ3", PsaParameterType.Injector3Correction);
            Add("CORRECTIONDEBITINJ4", PsaParameterType.Injector4Correction);
            Add("RCOREGULPRESSCARB", PsaParameterType.DieselPressureRegulatorRatio);
            Add("CA_REGIME_MOTEUR", PsaParameterType.EngineRpm);
            Add("VV", PsaParameterType.VehicleSpeed);
            Add("PRESSIONATMO", PsaParameterType.AtmosphericPressure);
            Add("CONSIGNEDEBITAIR", PsaParameterType.RequestedAirVolumeFlow);
            Add("POSPEDACCELERATEUR", PsaParameterType.AcceleratorPedalPosition);
            Add("RAPPORTBOITE", PsaParameterType.ReportedGearboxGearRatio);
            Add("RCOVANEEGR", PsaParameterType.ExhaustCycleRatio);
            Add("RCORECOVANNEEGR", PsaParameterType.EgrValvePositionFeedback);
            Add("BSIDECONDA", PsaParameterType.BsiUnlock);
            Add("BSIANTISCAN", PsaParameterType.BsiAntiScan);
            Add("CMDDEGIVRAGE", PsaParameterType.DeicingCommand);
            Add("DEFAUTABR", PsaParameterType.AbrDefault);
            Add("MP_TOUCH_RAZ_ODOMETRE", PsaParameterType.OdometerResetButton);
            Add("MP_PB_TRANSMISSION_CODE_DEVERROUILLAGE", PsaParameterType.ProblemsDetectedWhenTransmittingTheUnlockCode);
            Add("ETATAPPRENTISSAGE4", PsaParameterType.FirstEgrThrottleScan);
            Add("ETATVOLETRECYC", PsaParameterType.RecirculationStatus);
            Add("ETATDISTRIBUTION", PsaParameterType.DistributionStatus);
            Add("ETATVOLETMIX", PsaParameterType.MixingShutterStatus);
            Add("TENSIONCALCPULSEUR", PsaParameterType.VoltageValueOnBlower);
            Add("TENSIONRECOPULSEUR", PsaParameterType.BlowerCopyVoltage);
            Add("TEMPERATUREINTER", PsaParameterType.InternalTemperature);
            Add("TENSIONBATTERIE", PsaParameterType.BatteryVoltage);
            Add("NIVCARBUMESURE", PsaParameterType.MeasuredFuelLevel);
            Add("NIVCARBUAFFIC", PsaParameterType.DisplayedFuelLevel);
            Add("TEMPAIREXT", PsaParameterType.ExteriorAirTemperature);
            Add("NIVLAVEGLACEMINI", PsaParameterType.WindscreenWasherLiquidMinimumLevel);
            Add("ALERTEMINICARBURANT", PsaParameterType.MinimumFuelLevelAlert);
            Add("COMMTRANSPOK", PsaParameterType.CorrectCommunicationWithKeyTransponder);
            Add("DEMANDECLIM", PsaParameterType.AirConditioningRequestInput);
            Add("RELAISGMV", PsaParameterType.FanRelayOutput);
            Add("VITESSEGMV", PsaParameterType.MeasuredFanSpeed);
            Add("CONSIGNEGMV", PsaParameterType.MeasuredFanSpeed);
            Add("ETATCALCULATEUR", PsaParameterType.CalculatorStatus);
            Add("ETATANTIDEMCODE", PsaParameterType.CodedEngineImmobiliserProgrammingStatus);
            Add("RELAISALIMTO", PsaParameterType.SunroofAndWindowRelay);
            Add("AUTORISATIONCLIM", PsaParameterType.AirConditioningAuthorisation);
            Add("AUTORISCLIM", PsaParameterType.AirConditioningAuthorisation);
            Add("ETATAUTORISCLIM", PsaParameterType.AirConditioningAuthorisation);
            Add("MP_AUTORISATION_CLIMATISATION", PsaParameterType.AirConditioningAuthorisation);
            Add("VA_AUTORISATION_CLIM", PsaParameterType.AirConditioningAuthorisation);
            Add("RCORECOPAPEGR", PsaParameterType.EgrThrottlePositionFeedback);
            Add("RCORECOEVRAA", PsaParameterType.ElectrovalveTurboPositionFeedback);
            Add("CA_PRESSION_ATMOS", PsaParameterType.AtmosphericPressure);
            Add("CONTACTFREINREGUL", PsaParameterType.RedundantBrakeSignal);
            Add("CONTACTEMBRAYAGE", PsaParameterType.ClutchPedalSignal);
            Add("ERREURTELECODAGE", PsaParameterType.TelecodingVariableError);
            Add("DERNIERAPPRENTISSAGEVANNEEGR", PsaParameterType.LastEgrScan);
            Add("PREMAPPRENTISSAGEVANNEEGR", PsaParameterType.FirstEgrScan);
            Add("ETATAPPRENTISSAGE3", PsaParameterType.LastErgThrottleScan);
            Add("ETATAPPRENTISSAGE5", PsaParameterType.LastAirHeaterThrottleScan);
            Add("ETATAPPRENTISSAGE6", PsaParameterType.FirstAirHeaterThrottleScan);
            Add("CHAUFADDIT1", PsaParameterType.AdditionalHeatingCommand);
            Add("PRESSIONCLIM", PsaParameterType.ConditionerCoolingCircuitPressure);
            Add("ALIMRESCARCONF", PsaParameterType.FuelPumpSupplyCutOff);
            Add("NIVDELESTAGE", PsaParameterType.BsiMode);
            Add("DDEBVASPORT", PsaParameterType.RequestForBVAProgramSport);
            Add("CEINTBOUCLEAVG", PsaParameterType.DriverSeatBeltFastened);
            Add("SECUENFANTD", PsaParameterType.ChildSafetyRight);
            Add("SECUENFANTG", PsaParameterType.ChildSafetyLeft);
            Add("CMDANTIBAV", PsaParameterType.FrontFogLampsControl);
            Add("ANTIDEMVERROU", PsaParameterType.TransponderAntiscanActive);
            Add("CLE1SYNCHRO", PsaParameterType.Key1Sync);
            Add("CLE2SYNCHRO", PsaParameterType.Key2Sync);
            Add("CLE3SYNCHRO", PsaParameterType.Key3Sync);
            Add("CLE4SYNCHRO", PsaParameterType.Key4Sync);
            Add("CLE5SYNCHRO", PsaParameterType.Key5Sync);
            Add("ECMVERROU", PsaParameterType.BsiUnlock);
            Add("NOMBRECLEACTIVES", PsaParameterType.NumberOfActiveKeys);
            Add("NOMBRECLESSYNCHRO", PsaParameterType.NumberOfSynchronizedRadiokeys);
            Add("BSIAPPAIRECM", PsaParameterType.BsiPair);
            Add("BSIVIERGE", PsaParameterType.BsiEmpty);
            Add("NIVDELEST6", PsaParameterType.NIVDELEST6);
            //Add("", PsaParameterType.);
            
            //Add("", PsaParameterType.);

            Add("MP_RAPPORT_ENGAGE", PsaParameterType.GearboxGear);
            Add("MP_COMMANDE_COMPRESSEUR_REFRIGERATION", PsaParameterType.AirConditioningCompressorCommand);
            Add("MP_COMMANDE_GMV_PAR_CAN", PsaParameterType.FanAssemblyControl);
            Add("MP_INFORMATION_AUTORISATION_COMPRESSEUR", PsaParameterType.AirConditioningAuthorisation);
            Add("MP_MODE_FONCTIONNEMENT_DE_LA_CLIMATISATION", PsaParameterType.ClimateControlFunctioningMode);
            Add("MP_PRESSION_FLUIDE_FRIGORIGENE", PsaParameterType.ConditionerCoolingCircuitPressure);
            Add("MP_SECURITE_BASSE_PRESSION_CIRCUIT_REFRIGERATION", PsaParameterType.AirConditioningCircuitLowPressureProtection);
            Add("MP_SECURITE_HAUTE_PRESSION_CIRCUIT_REFRIGERATION", PsaParameterType.AirConditioningCircuitHighPressureProtection);
            Add("MP_COUPLE_MOTEUR_THEORIQUE", PsaParameterType.EngineTorque);
            Add("MP_LOI_PASSAGE_VITESSES", PsaParameterType.GearboxRule);
            Add("MP_PRESSION_CONSIGNE_1", PsaParameterType.OilPressure);
            Add("MP_PRESSION_CONSIGNE", PsaParameterType.PressureSetting);
            Add("MP_TEMPERATURE_HUILE", PsaParameterType.EngineOilTemperature);
            Add("MP_TEMPS_ACTIVATION_EV_EVM", PsaParameterType.ElectromagneticModulationActivationTimes);
            Add("MP_EV_DEBIT_ECHANGEUR_BVA_EPDE_new", PsaParameterType.EPDECollector);
            Add("MP_REGIME_MOTEUR", PsaParameterType.EngineRpm);
            Add("MP_VITESSE_VEHICULE", PsaParameterType.VehicleSpeed);
            Add("MP_COMMANDE_PULSEUR_CLIMATISATION_PAR_CAN", PsaParameterType.ClimateAirConditioningFanCommand);
            Add("MP_DEMANDE_COMPRESSEUR_CLIMATISATION", PsaParameterType.AirConditioningCompressorCommand);
            Add("MP_DEMANDE_REDUCTION_PUISSANCE_COMPRESSEUR_REFRIGERATION", PsaParameterType.AirConditioningCompressorLoadShedding);
            Add("MP_ETAT_CAPTEUR_TEMPERATURE_EVAPORATEUR", PsaParameterType.EvaporatorTemperatureSensorState);
            Add("MP_ETAT_EMBRAYAGE_COMPRESSEUR_REFRIGERATION", PsaParameterType.AirConditionerCompressorOnOffState);
            Add("MP_COMMANDE_GROUPE_MOTOVENTILATEUR", PsaParameterType.FanAssemblyControl);
            Add("MP_COMMANDE_PULSEUR_CLIMATISATION", PsaParameterType.ClimateAirConditioningFanCommand);
            Add("MP_DEMANDE_PRODUCTION_ENERGIE_ELECTRIQUE", PsaParameterType.ElectricEnergyGenerationCommand);
            Add("MP_DEMANDE_TYPE_REGULATION_PRODUCTION_ENERGIE_ELECTRIQUE", PsaParameterType.ElectricEnergyGenerationRegulationCommand);
            Add("MP_NIVEAU_LESTAGE_ELECTRIQUE", PsaParameterType.ElectricLoadLevel);
            Add("MP_TEMPERATURE_HUILE_MOTEUR", PsaParameterType.EngineOilTemperature);
            Add("MP_TENSION_BATTERIE_SECURISEE", PsaParameterType.BatteryVoltageSecure);
            Add("MP_TENSION_EXCITATION_ALTERNATEUR", PsaParameterType.AlternatorExcitationVoltage);
            Add("MP_TEMPERATURE_D_EAU_MOTEUR_a", PsaParameterType.EngineCoolantTemperature);
            Add("MP_AVANCE_CYLINDRE_01", PsaParameterType.IgnitionTimingAdvanceAngleCylinder1);
            Add("MP_AVANCE_CYLINDRE_02", PsaParameterType.IgnitionTimingAdvanceAngleCylinder2);
            Add("MP_AVANCE_CYLINDRE_03", PsaParameterType.IgnitionTimingAdvanceAngleCylinder3);
            Add("MP_AVANCE_CYLINDRE_ETOILE", PsaParameterType.IgnitionTimingAdvanceAngleCylinder4);
            Add("MP_AVANCE_DE_CONSIGNE_DE_BASE", PsaParameterType.BasicInjectionAdvance);
            Add("MP_AVANCE_MAXI1", PsaParameterType.MaximumIgnitionAdvance);
            Add("MP_AVANCE_OPTIMALE", PsaParameterType.OptimalIgnitionAdvance);
            Add("MP_BRUIT_CAPTEUR_CLIQUETIS", PsaParameterType.NoiseLevelMeasuredByDetonationSensor);
            Add("MP_CONSIGNE_CMD_THERMOSTAT_PILOTE", PsaParameterType.RequestedManagedThermostatValue);
            Add("MP_CAPTEUR_ENSOLEILLEMENT", PsaParameterType.LightSensorOutput);
            Add("MP_SONDE_AIR_SOUFFLE_CONDUCTEUR", PsaParameterType.LeftBlownAirTemperature);
            Add("MP_SONDE_AIR_SOUFFLE_PASSAGER", PsaParameterType.RightBlownAirTemperature);
            Add("MP_TEMPERATURE_EVAPORATEUR_CLIMATISATION", PsaParameterType.EvaporatorSensorTemperature);
            Add("MP_VAL_TENSION_PULSEUR_PRINCIPAL", PsaParameterType.VoltageValueOnBlower);
            Add("MP_COURANT_BATTERIE", PsaParameterType.BatteryCurrent);
            Add("MP_ETAT_DE_CHARGE_BATTERIE_DE_DEMARRAGE", PsaParameterType.BatteryChargePercentage);
            Add("MP_PRECISION_VALEUR_ETAT_DE_CHARGE_BATTERIE_DE_DEMARRAGE", PsaParameterType.BatteryChargePercentagePrecise);
            Add("MP_TEMPERATURE_BATTERIE_DE_DEMARRAGE", PsaParameterType.BatteryTemperature);
            Add("MP_VITESSE_ENTREE_BVA_CONSIGNE", PsaParameterType.AutomaticGearboxEnterTimeRequested);
            Add("MP_VITESSE_ENTREE_BVA_MESUREE", PsaParameterType.AutomaticGearboxEnterTimeMeasured);
            Add("MP_VITESSE_SORTIE_BVA_MESUREE", PsaParameterType.AutomaticGearboxExitTimeMeasured);
            Add("MP_VITESSE_SORTIE_BVA_CONSIGNE", PsaParameterType.AutomaticGearboxExitTimeRequested);
            Add("MP_POSITION_PEDALE_ACCELERATEUR_RECALCULE", PsaParameterType.AcceleratorPedalPosition);
            Add("MP_POSITION_LEVIER_VITESSE", PsaParameterType.GearboxHandlePosition);
            
            Add("MP_PRESSION_HUILE", PsaParameterType.OilPressure);
            Add("MP_TENSION_CAPTEUR_PRESSION", PsaParameterType.OilPressureSensorVoltage);
            Add("MP_TEMPERATURE_HUILE_b", PsaParameterType.EngineOilTemperature);
            Add("MP_TEMPERATURE_HUILE_d", PsaParameterType.EngineOilTemperature);
            Add("MP_TENSION_CAPTEUR_TEMPERATURE", PsaParameterType.OilTemperatureSensorVoltage);

            Add("MP_CONTACT_FREIN_01", PsaParameterType.MainBrakeSignal);
            Add("MP_CONTACT_FREIN_02", PsaParameterType.RedundantBrakeSignal);
            
            Add("MP_SELECT_LOI_SPORT", PsaParameterType.RequestForBVAProgramSport);
            Add("MP_SELECTION_LOI_NEIGE", PsaParameterType.AutomaticGearboxSnowProgramRequest);
            Add("MP_TEMPERATURE_AIR_EXTERIEUR", PsaParameterType.ExteriorAirTemperature);

            Add("MP_CONTACTEUR_MULTIFONCTION_BVA_EN_POSITION_PN", PsaParameterType.MultifunctionalSwitchPNContact);
            Add("MP_CONTACTEUR_MULTIFONCTION_BVA_EN_POSITION_S4", PsaParameterType.MultifunctionalSwitchS4Contact);
            Add("MP_CONTACTEUR_MULTIFONCTION_BVA_EN_POSITION_S3", PsaParameterType.MultifunctionalSwitchS3Contact);
            Add("MP_CONTACTEUR_MULTIFONCTION_BVA_EN_POSITION_S2", PsaParameterType.MultifunctionalSwitchS2Contact);
            Add("MP_CONTACTEUR_MULTIFONCTION_BVA_EN_POSITION_S1_IMPULSIONNEL", PsaParameterType.MultifunctionalSwitchS1OrImpulsePlusContact);
            Add("MP_CONTACT_1ERE_OU_IMPULSIONNEL", PsaParameterType.MultifunctionalSwitch1OrImpulseMinusContact);

            Add("MP_VIDANGE_A_FAIRE", PsaParameterType.DrainRequired);
            Add("MP_ALIMENTATION_COMMUNE_ELECTROVANNES_SEQUENCE", PsaParameterType.SequentialElectromagneticValvesGeneralSupply);


            Add("MP_SORTIE_CLIM_KEYLOCK", PsaParameterType.ClimateControlOrKeyLockFunctionOutput);
            Add("MP_ESTOMPAGE_COUPLE", PsaParameterType.TorqueRegulation);
            Add("MP_DEBLOCAGE_LEVIER_DE_VITESSE_SHIFT_LOCK", PsaParameterType.ShiftLockHandleUnblocking);
            Add("MP_PONTAGE_CONVERTISSEUR_COUPLE", PsaParameterType.HydraulicTorqueConverterBlocking);
            Add("MP_TEMPS_ACTIVATION_EVLU_BVA", PsaParameterType.EVLUActivationTime);

            Add("MP_ELECTROVANNE_EVS_01", PsaParameterType.SequentialSolenoidValve1);
            Add("MP_ELECTROVANNE_EVS_02", PsaParameterType.SequentialSolenoidValve2);
            Add("MP_ELECTROVANNE_EVS_03", PsaParameterType.SequentialSolenoidValve3);
            Add("MP_ELECTROVANNE_EVS_04", PsaParameterType.SequentialSolenoidValve4);
            Add("MP_ELECTROVANNE_EVS_05", PsaParameterType.SequentialSolenoidValve5);
            Add("MP_ELECTROVANNE_EVS_06", PsaParameterType.SequentialSolenoidValve6);
            
            Add("MP_ETAT_BVA", PsaParameterType.BVAState);
            Add("MP_POSITION_PROGRAMME", PsaParameterType.BVAProgramState);
            Add("MP_POSITION_LEVIER_VITESSES", PsaParameterType.GearboxHandlePosition);

            Add("MP_ALERTE_PRESSION_HUILE_MOTEUR_a", PsaParameterType.OilPressureWarning);

            Add("MP_INFORMATION_MARCHE_ARRIERE", PsaParameterType.ReverseInformation);
            Add("MP_POSITION_CLE", PsaParameterType.KeyPositionInIgnitionLock);
            Add("MP_POSITION_LEVIER_BVA_BVMP_1", PsaParameterType.GearboxHandlePositionRegisteredBySMD);

            Add("MP_ALERTE_NIVEAU_EAU_MOTEUR", PsaParameterType.CoolantLevelWarning);

            Add("MP_ACTIVATION_MODE_URGENCE_SYST_ELEC", PsaParameterType.ElectricSystemEmergencyMode);
            Add("MP_PRESENCE_PLUS_APC", PsaParameterType.PlusAfterContactAPC);
            Add("MP_INFORMATION_PLUS_APRES_CONTACT", PsaParameterType.InformationPlusAfterContactPassedToEngineServiceModule);
            Add("MP_POSITION_CLE_EN_PLUS_DEM", PsaParameterType.KeyInStarterStartPosition);
            Add("MP_DEMANDE_DE_DEMARRAGE_MOTEUR", PsaParameterType.EngineStartCommand);
            Add("MP_COMMANDE_SORTIE_MOTEUR_TOURNANT", PsaParameterType.PowerOutputAtWorkingEngineControl_SeatHeaterOn);
            Add("MP_ETAT_GMP", PsaParameterType.PowerUnitState);
            Add("MP_ETAT_PRINCIPAL", PsaParameterType.MainElectricSystemState);
            Add("MP_ETAT_RESEAU_ELECTRIQUE_AVEC_GENERATEUR_NON_OPERATIONNEL", PsaParameterType.ElectricChainStateWithGeneratorOffline);
            Add("MP_ETAT_RESEAU_ELECTRIQUE_AVEC_GENERATEUR_OPERATIONNEL", PsaParameterType.ElectricChainStateWithGeneratorOnline);
            Add("MP_CONFIGURATION_SYSTEME_ELECTRIQUE_VEHICULE", PsaParameterType.VehicleElectricSystemConfiguration);
            Add("MP_ETAT_MODE_ECONOMIE", PsaParameterType.EconomyModeStatus);
            Add("MP_MODE_PHASE_VIE", PsaParameterType.LifePhaseMode);
            Add("MP_COMMANDE_COUPURE_POMPE_CARBURANT", PsaParameterType.FuelPumpSupplyCutOff);
            Add("MP_ETAT_ZONE_MEMOIRE_BSI_SUITE_TELECHARGEMENT", PsaParameterType.BsiMemoryStateAfterTelecoding);
            Add("MP_NOMBRE_RESET_LOGICIEL_BSI", PsaParameterType.BsiSoftwareResetsCount);
            Add("MP_COMPTEUR_RESET_HARD_BSI", PsaParameterType.BsiSoftwareRebootsCount);
            Add("MP_TEMPS_ECOULE_GENERATEUR_NON_OPERATIONNEL", PsaParameterType.AlternatorNonOperationalTimeHasEnded);
            Add("MP_TEMPS_ECOULE_AVEC_GENERATEUR_OPERATIONNEL", PsaParameterType.TimeSpanSinceAlternatorStart);
            Add("MP_POSITION_PHYSIQUE_DU_SHUNT_PARC", PsaParameterType.PhysicalStateOfParkingStateShunt);
            Add("MP_AUTO_DEMANDE_AIDE_REGENERATION_FAP", PsaParameterType.FapRegenerationHelpRequest);
            Add("MP_ETAT_CONSOMMATEURS_ELECTRIQUES_SENSIBLES", PsaParameterType.SensitiveElectricConsumersState);

            Add("MP_BSI_DELESTAGE_PAS_DE_DELESTAGE", PsaParameterType.NoLoadShedding);
            Add("MP_BSI_DELESTAGE_BOUGIES_PRECHAUFFAGE", PsaParameterType.PreheaterPlugLoadShedding);
            Add("MP_BSI_DELESTAGE_SYSTEME_DENOX", PsaParameterType.NOxSystemSwitchOffForPartialGeneratorUnload);
            Add("MP_BSI_DELESTAGE_GMV_PETITE_VITESSE", PsaParameterType.ForcedFanUnitLowSpeedLoadShedding);
            Add("MP_BSI_DELESTAGE_PARTIEL_CHAUFFAGE_ADDITIONNEL_CTP3", PsaParameterType.AdditionalHeatingSystemCTP3PartialBlock);
            Add("MP_BSI_DELESTAGE_LUNETTE_CHAUFFANTE_LORS_REGENERATION_FAP", PsaParameterType.RearWindowHeatingPowerSaveFunctionWhileRegeneratingFAP);
            Add("MP_BSI_DELESTAGE_TOTAL_LUNETTE_CHAUFFANTE", PsaParameterType.GlobalPowerSavingFunctionOfRearWindowHeater);
            Add("MP_BSI_DELESTAGE_PARTIEL_CHAUFFAGE_ADDITIONNEL", PsaParameterType.AdditionalHeaterPowerSavingFunctionCTP1And2);
            Add("MP_BSI_DELESTAGE_CHAUFFE_NUQUES", PsaParameterType.GlobalPowerSavingFunctionOfRearWindowHeater);
            Add("MP_BSI_DELESTAGE_TOTAL_CHAUFFAGE_ADDITIONNEL", PsaParameterType.AdditionalHeaterPowerSavingFunctionCTP1And2);
            Add("MP_BSI_DELESTAGE_PULSEUR_CLIMATISATION", PsaParameterType.AirConditioningBlowerLoadShedding);
            Add("MP_BSI_DELESTAGE_COMPRESSEUR_CLIMATISATION", PsaParameterType.AirConditioningCompressorLoadShedding);
            Add("MP_BSI_DELESTAGE_PARE_BRISE_CHAUFFANT", PsaParameterType.WindscreenHeaterLoadShedding);
            Add("MP_DELESTAGE_BOITIER_TRANSFORMATION_CARROSSERIE", PsaParameterType.BodyTransformationBlockLoadShedding);
            
            Add("MP_DIAG_CONTACT_PN_BVA_OU_AUTORISATION_DEM_BVMP", PsaParameterType.MultifunctionalSwitchPNContact);
            Add("MP_POSITION_LEVIER_BOITE_VITESSE_AUTO", PsaParameterType.GearboxHandlePosition);
            Add("MP_DEMANDE_MODE_BOITE_VITESSE_AUTO_NEIGE", PsaParameterType.AutomaticGearboxSnowProgramRequest);
            Add("MP_DEMANDE_MODE_BOITE_VITESSE_AUTO_SPORT", PsaParameterType.RequestForBVAProgramSport);
            Add("MP_DEMANDE_MISE_MARCHE_REGULATION_VITESSE", PsaParameterType.SpeedLimitationOrRegulation_SwitchOnCommand);
            Add("MP_DEMANDE_REGULATION_VITESSE_PLUS", PsaParameterType.SpeedLimitationOrRegulation_SpeedRegulationPlus);
            Add("MP_DEMANDE_REGULATION_VITESSE_MOINS", PsaParameterType.SpeedLimitationOrRegulation_SpeedRegulationMinus);
            Add("MP_REGULATION_LIMITATION_VITESSE_ACTIVE", PsaParameterType.SpeedLimitationOrRegulation_ActivationState);
            Add("MP_ETAT_SELECTION_LIMITATION_REGULATION_DE_VITESSE", PsaParameterType.SpeedLimitationOrRegulation_SelectionState);
            Add("MP_ETAT_PUSH_CANCEL_RESUME", PsaParameterType.SpeedLimitationOrRegulation_CancelRestoreButtonStatus);
            Add("MP_ETAT_REGULATION_OU_LIMITATION_VITESSE_NEW_1", PsaParameterType.SpeedLimitationOrRegulation_SpeedState);

            Add("MP_ALERTE_PRESSION_HUILE_MOTEUR", PsaParameterType.OilPressureWarning);
            Add("MP_NIVEAU_HUILE_MOTEUR_APRES_FILTRAGE", PsaParameterType.OilPressureAfterSignalFiltering);
            Add("MP_NIVEAU_CARBURANT_MESURE", PsaParameterType.MeasuredFuelLevel);
            Add("MP_NIVEAU_CARBURANT_LISSE", PsaParameterType.FilteredFuelLevel);
            Add("MP_NIVEAU_CARBURANT_AFFICHE", PsaParameterType.DisplayedFuelLevel);
            Add("MP_IMPEDANCE_JAUGE_CARBURANT", PsaParameterType.FuelSenderImpedance);
            Add("MP_DETECTION_PRESENCE_EAU_DANS_GAZOLE", PsaParameterType.WaterInGazolineDetected);
            Add("MP_AFFICHAGE_DEFAUT_CHARGE_BATTERIE", PsaParameterType.BatteryChargeCirquitErrorDisplaying);

            Add("MP_NIVEAU_LAVE_VITRE_MINI", PsaParameterType.WindscreenWasherLiquidMinimumLevel);
            Add("MP_ALERTE_NIVEAU_LIQUIDE_FREIN_MINI", PsaParameterType.LowBrakeFluidWarning);
            Add("MP_RAZ_APV_COMPTEUR_KILOMETRIQUE", PsaParameterType.MileageResetAtFirstPostDeliverService);
            Add("MP_TEMPERATURE_EXTERIEURE", PsaParameterType.ExteriorAirTemperature);
            Add("MP_CONSOMMATION_CARBURANT_INSTANTANEE", PsaParameterType.InstantFuelConsumption);
            Add("MP_CONSOMMATION_CARBURANT_MOYENNE_TRAJET", PsaParameterType.AverageJourneyConsumption);
            Add("MP_CONSOMMATION_CARBURANT_LISSEE", PsaParameterType.SmoothedConsumption);
            Add("MP_KILOMETRAGE_TOTAL", PsaParameterType.TotalMileage);
            Add("MP_COMMANDE_TEMOIN_OUBLI_CEINTURE", PsaParameterType.UnfastenedBeltSignalManagement);
            Add("MP_COMMANDE_VOYANT_ESP", PsaParameterType.ESPSwitchOffSignalManagement);
            
            Add("MP_COMPTEUR_NOMBRE_DE_MAINTENANCE_EFFECTUEES", PsaParameterType.NumberOfServicesPerformed);
            Add("MP_KILOMETRAGE_AVANT_MAINTENANCE", PsaParameterType.MileageBeforeService);
            Add("MP_SIGNE_KILOMETRAGE_AVT_MAINTENANCE", PsaParameterType.MileageBeforeServiceSign);
            Add("MP_NOMBRE_JOURS_RESTANT_AVANT_MAINTENANCE", PsaParameterType.DaysLeftTillService);
            Add("MP_SIGNE_NB_JOUR_AVT_MAINTENANCE", PsaParameterType.DaysLeftTillServiceSign);
            Add("MP_KILOMETRAGE_DERNIERE_RAZ", PsaParameterType.MileageCorrespondingToLastReset);
            Add("MP_DUREE_DEPUIS_DERNIERE_RAZ", PsaParameterType.PeriodAfterLastReset);

            Add("MP_MODE_ESSUYAGE_AUTOMATIQUE", PsaParameterType.ScreenWiperAutomaticMode);
            Add("MP_DEMANDE_ESSUYAGE_ENVOYEE_PAR_CDPL", PsaParameterType.ScreenWiperCommandSentByRainSensor);
            Add("MP_DEMANDE_ESSUYAGE_PARE_BRISE_PETITE_VITESSE", PsaParameterType.SlowSpeedWindscreenWiperControl);
            Add("MP_DEMANDE_ESSUYAGE_PARE_BRISE_GV", PsaParameterType.WindscreenWiperControlAtHighSpeed);
            Add("MP_COMMANDE_ESSUYAGE_PARE_BRISE", PsaParameterType.WindscreenWiperControl);
            Add("MP_INFORMATION_ARRET_FIXE_ESSUIE_VITRE_AVANT", PsaParameterType.WindscreenWiperParkedPositionInformation);
            Add("MP_DEMANDE_ESSUYAGE_LUNETTE", PsaParameterType.WindscreenWiperRequest);
            Add("MP_COMMANDE_ESSUYAGE_LUNETTE_ARRIERE", PsaParameterType.RearScreenWiperControl);
            Add("MP_INFORMATION_ARRET_FIXE_ESSUIE_VITRE_ARRIERE", PsaParameterType.RearWiperParkedPositionInformation);

            Add("MP_DEMANDE_LAVAGE_PARE_BRISE", PsaParameterType.WindscreenWasherRequest);
            Add("MP_DEMANDE_LAVAGE_LUNETTE", PsaParameterType.RearScreenWasherRequest);
            Add("MP_COMMANDE_LAVAGE_LUNETTE_ARRIERE", PsaParameterType.RearScreenWashControl);
            Add("MP_COMMANDE_LAVAGE_PROJECTEUR_OU_SORTIE_MOTEUR", PsaParameterType.HeadlightWasherControlOrExitWithWorkingEngine);
            Add("MP_DEMANDE_DEGIVRAGE", PsaParameterType.DeicingCommand);
            Add("MP_COMMANDE_DEGIVRAGE", PsaParameterType.DeicingControl);
            
            Add("MP_ETAT_KICKDOWN_BVA", PsaParameterType.BVAKickDownContactState);

            Add("MP_DEMANDE_CYLINDREE_MAXIMALE_COMPRESSEUR_REGRIGERATION", PsaParameterType.RequestForMaximumAirConditionerCompressorVolume);
            Add("MP_DEMANDE_ENCLENCHEMENT_CTP", PsaParameterType.RequestForCTPResistiveHeaterSwitchOn);
            Add("MP_COMMUTATEUR_ESP", PsaParameterType.ESPSwitch);
            Add("MP_COMMUTATEUR_AIDE_STATIONNEMENT", PsaParameterType.ParkingAidSystemSwitch);
            Add("MP_COMMUTATEUR_LUNETTE_CHAUFFANTE", PsaParameterType.HeatedRearScreenPushButton);
            Add("MP_COMMUTATEUR_CLIMATISATION_MANUELLE", PsaParameterType.ClimateManualSwitch);
            Add("MP_COMMUTATEUR_REGLAGE_LUM_PLUS", PsaParameterType.BrightnessPlusSwitchState);
            Add("MP_COMMUTATEUR_ODOMETRE", PsaParameterType.OdometerResetButton);

            Add("MP_ETAT_INHIBITION_SORTIE_FEU_STOP_GAUCHE", PsaParameterType.LeftStoplight);
            Add("MP_ETAT_INHIBITION_SORTIE_FEU_CLIGNOTANT_ARRIERE_GAUCHE", PsaParameterType.RearLeftTurnSignal);
            Add("MP_ETAT_INHIBITION_SORTIE_FEU_DE_RECUL_GAUCHE", PsaParameterType.LeftReverseLight);
            Add("MP_ETAT_INHIBITION_SORTIE_LANTERNE_ARRIERE_GAUCHE", PsaParameterType.RearLeftMarkerLamp);
            Add("MP_ETAT_INHIBITION_SORTIE_FEU_CLIGNOTANT_ARRIERE_DROIT", PsaParameterType.RearRightTurnIndicator);
            Add("MP_ETAT_INHIBITION_SORTIE_LANTERNE_ARRIERE_DROITE", PsaParameterType.RearRightMarkerLamp);
            Add("MP_ETAT_INHIBITION_SORTIE_FEU_STOP_SUPPLEMENTAIRE", PsaParameterType.AdditionalStopLight);
            Add("MP_ETAT_INHIBITION_SORTIE_FEU_STOP_DROIT", PsaParameterType.RightStoplight);
            Add("MP_ETAT_INHIBITION_SORTIE_OUVERTURE_LUNETTE", PsaParameterType.RearWindowOpening);
            Add("MP_ETAT_INHIBITION_SORTIE_ECLAIRAGE_CAVE_A_PIEDS", PsaParameterType.SillLighting);
            Add("MP_ETAT_INHIBITION_SORTIE_ECLAIREUR_SEUIL_AVANT", PsaParameterType.FrontSillLightingCover);
            Add("MP_ETAT_INHIBITION_SORTIE_SECURITE_ANTIVOL_ELECTRIQUE", PsaParameterType.ElectricIgnitionLockSafety);
            Add("MP_ETAT_INHIBITION_SORTIE_ECLAIREUR_SEUIL_ARRIERE", PsaParameterType.RearSillLightingCover);
            Add("MP_ETAT_INHIBITION_SORTIE_OUVERTURE_COFFRE", PsaParameterType.BootOpen);
            Add("MP_ETAT_INHIBITION_SORTIE_ECLAIREUR_COFFRE", PsaParameterType.BootLightingCover);
            Add("MP_ETAT_INHIBITION_SORTIE_PLAFONNIER", PsaParameterType.FrontLight);
            Add("MP_ETAT_INHIBITION_SORTIE_FEU_DE_BROUILLARD_ARRIERE_DROIT", PsaParameterType.RearRightFogLamp);
            Add("MP_ETAT_INHIBITION_SORTIE_PLAFONNIER_ARRIERE", PsaParameterType.RearLight);
            Add("MP_ETAT_INHIBITION_SORTIE_FEU_DE_RECUL_DROIT", PsaParameterType.RightReverseLight);
            Add("MP_ETAT_INHIBITION_SORTIE_ECLAIRAGE_INTERRUPTEURS", PsaParameterType.SwitchesLighting);
            Add("MP_ETAT_INHIBITION_SORTIE_FEU_DE_BROUILLARD_ARRIERE_GAUCHE", PsaParameterType.RearLeftFogLamp);

            Add("MP_DIAG_CONTACT_PN_BOITE_VITESSE_AUTO_OU_AUTORISATION_DEM_BVMP", PsaParameterType.PnContactDiagnosticsOrEngineStartPermissionStatusBVA);

            Add("MP_DETECTION_EFFRACTION_VOLUMETRIQUE", PsaParameterType.VolumetricBreakinDetection);
            Add("MP_DEMANDE_NEUTRALISATION_VOLUMETRIQUE_ALARME", PsaParameterType.VolumetricProtectionDeactivation);

            Add("REMPLISSAGE_CONSIGNE_CORRIGE", PsaParameterType.CorrectedRequestedFilling);
            Add("REMPLISSAGE_MESURE_CORRIGE", PsaParameterType.CorrectedMeasuredFilling);
            Add("REMPLISSAGE_CONSIGNE_BRUT", PsaParameterType.BruttoRequestedFilling);
            Add("REMPLISSAGE_MESURE_BRUT", PsaParameterType.BruttoMeasuredFilling);
            Add("MP_DEBIT_AIR", PsaParameterType.AirFlowVolume);
            Add("MP_PRESS_RELATIVE_CONSIGNE_COLLECTEUR", PsaParameterType.CollectorPressureRelativeToSpecified);
            Add("MP_PRESS_RELATIVE_MESURE_COLLECTEUR", PsaParameterType.CollectorPressureRelativeToMeasured);
            Add("MP_ANGLE_PAPILLON_CONSIGNE", PsaParameterType.ThrottleAngleSpecified);
            Add("MP_ANGLE_PAPILLON_MESURE", PsaParameterType.ThrottleAngleMeasured);
            Add("MP_TENSION_RECOPIE_POSITION_PAPILLON_01", PsaParameterType.ThrottleAngleSensorVoltageDuplicate1);
            Add("MP_TENSION_RECOPIE_POSITION_PAPILLON_02", PsaParameterType.ThrottleAngleSensorVoltageDuplicate2);
            Add("MP_CONSIGNE_POSITION_DEPHASEUR_AAC_ADMISSION", PsaParameterType.PrescriptionForInputValvesDephaserPosition);
            Add("MP_POS_DEPHASEUR_ACC_1", PsaParameterType.InputValvesDephaserPosition);
            Add("MP_RCO_ELECTROVANNE_DEPHASEUR_ACC_1", PsaParameterType.InputValvesPhaserElectrovalveRCO);
            Add("MP_ETAT_DEPHASEUR_AAC_ADMISSION", PsaParameterType.InputCamshaftPhaserMechanismState);
            Add("MP_ETAT_DEPHASEUR_AAC_ECHAPPEMENT", PsaParameterType.RequestedAirVolumeFlow);
            Add("MP_PRESSAIRADM", PsaParameterType.InletAirPressure);
            Add("MP_ETAT_REGULATEUR_PRESSION_AIR_ADMISSION", PsaParameterType.InletAirPressureRegulatorState);
            Add("MP_FACTEUR_CORRECTION_PRESSION_ADMISSION", PsaParameterType.AirPressureCorrectionFactorInlet);
            Add("MP_ETAT_REGULATEUR_REMPLISSAGE", PsaParameterType.FillingRegulatorState);
            Add("MP_FACTEUR_CORRECTION_REMPLISSAGE", PsaParameterType.FillingCorrectionFactor);
            Add("MP_CONSIGNE_POSITION_DEPHASEUR_AAC_ECHAPPEMENT", PsaParameterType.PrescriptionForExhaustValvesDephaserPosition);
            Add("MP_MESURE_DEPHA_AAC_ECHAP_CA", PsaParameterType.ExhaustValvesDephaserPosition);
            Add("MP_RCO_ELEC_DEPHA_AAC_ECHAP_CA", PsaParameterType.ExhaustValvesPhaserMechanismElectrovalveRCO);
            Add("MP_RCO_COMM_PAPILLON", PsaParameterType.ThrottleControlPWMSignal);
            Add("MP_SENS_COMMANDE_PAPILLON", PsaParameterType.ThrottleGearingDirection);
            Add("MP_TEMP_AIR_ADMISSION_SUP", PsaParameterType.AirTemperature);
            Add("MP_PRES_ATMO_MES", PsaParameterType.AtmosphericPressure);

            Add("MP_TEMPS_INJECTION_CYLINDRE_01", PsaParameterType.InjectionTimeCylinder1);
            Add("MP_TEMPS_INJECTION_CYLINDRE_02", PsaParameterType.InjectionTimeCylinder2);
            Add("MP_TEMPS_INJECTION_CYLINDRE_03", PsaParameterType.InjectionTimeCylinder3);
            Add("MP_TEMPS_INJECTION_CYLINDRE_04", PsaParameterType.InjectionTimeCylinder4);
            Add("MP_RICHESSE_AMONT", PsaParameterType.UpperMixCompositionBorder);
            Add("MP_ETAT_SONDE_REGUL_AMONT", PsaParameterType.UpperOxygenSensorRegulationState);
            Add("MP_ETAT_SONDE_REGUL_AMONT_new_1", PsaParameterType.UpperOxygenSensorRegulationState);
            Add("MP_TENSION_SONDE_A_OXYGENE_AMONT", PsaParameterType.UpperOxygenSensorVoltage);
            Add("MP_ETAT_SONDE_AVAL", PsaParameterType.LowerOxygenSensorState);
            Add("MP_ETAT_SONDE_REGUL_AVAL_new_1", PsaParameterType.LowerOxygenSensorRegulationState);
            Add("MP_TENSION_SONDE_A_OXYGENE_AVAL", PsaParameterType.LowerOxygenVoltage);
            Add("MP_RCO_CHAUFFAGE_AMONT1", PsaParameterType.UpperLambdaProbeHeatingRCO);
            Add("MP_RCO_CHAUFFAGE_AVAL1", PsaParameterType.LowerLambdaProbeHeatingRCO);
            Add("MP_FACTEUR_CORRECTION_RICHESSE_AMONT", PsaParameterType.MixtureCompositionCorrectionFactorOnEnter);
            Add("MP_FACTEUR_CORRECTION_RICHESSE_AVAL", PsaParameterType.MixtureCompositionCorrectionFactorOnExit);
            Add("MP_CHARGE_ESTIMEE_CANISTER", PsaParameterType.FuelVaporsAbsorberEstimatedLoad);
            Add("MP_CON_RICHESSE", PsaParameterType.MixRegSetup);
            Add("MP_RCO_EV_CANISTER", PsaParameterType.CanisterElectrovalveOCR);

            Add("MP_CONSIGNE_PRESSION_HUILE", PsaParameterType.RequestedOilPressure);
            Add("MP_SEUIL_DETECTION_CLIQUETIS", PsaParameterType.DetonationDetectionThreshold);

            Add("MP_RETRAIT_AVANCE_ALLUMAGE_CYLINDRE_01", PsaParameterType.DelayAdvanceCylinder1);
            Add("MP_RETRAIT_AVANCE_ALLUMAGE_CYLINDRE_02", PsaParameterType.DelayAdvanceCylinder2);
            Add("MP_RETRAIT_AVANCE_ALLUMAGE_CYLINDRE_03", PsaParameterType.DelayAdvanceCylinder3);
            Add("MP_RETRAIT_AVANCE_ALLUMAGE_CYLINDRE_04", PsaParameterType.DelayAdvanceCylinder4);
            Add("MP_TENSION_BATTERIE", PsaParameterType.BatteryVoltage);
            Add("MP_TEMPERATURE_EAU_MOTEUR", PsaParameterType.WaterTemperature);

            Add("MP_AVANCE_MINI1", PsaParameterType.MinInjectionAdvance);
            Add("MP_COUPLE_RESISTANT_MOTEUR_ESTIME", PsaParameterType.EstimatedResistanceTorque);
            Add("MP_COUPLE_VOLONTE_CONDUCTEUR", PsaParameterType.TorqueCorrespondingToDriversDesire);

            Add("MP_RCO_MOTEUR_LEVEE_SOUPAPE_VARIABLES", PsaParameterType.VariableValveLiftDriveRCO);
            Add("ETAT_CMD_VALVETRONIC_MOT", PsaParameterType.VariableValveLiftSystemsSupplyaRelayState);
            Add("MP_TENSION_ALIM_MOT_LEVEE_SOUPAPE", PsaParameterType.VariableValveLiftSystemsElectricEngineSupplyVoltage);
            Add("MP_LEVEE_SOUPAPE_MESUREE", PsaParameterType.MeasuredValveLiftingValue);
            Add("MP_CONSIGNE_DE_LEVEE_DE_SOUPAPE", PsaParameterType.RequestedValveLiftingValue);
            Add("MP_APPRENTISSAGE_BUTEE_BASSE_VVT", PsaParameterType.ValveLiftLearnAngleMin);
            Add("MP_APPRENTISSAGE_BUTEE_HAUTE_VVT", PsaParameterType.ValveLiftLearnAngleMax);
            Add("MP_ETAT_APPRENTISSAGE_BUTEES_SOUPAPES", PsaParameterType.StateOfLearnStopValve);
            Add("MP_BESOIN_APPRENTISSAGE_BUTEES_SOUPAPES", PsaParameterType.LearnStateStopValveRequirement);
            Add("ETAT_FONCTIONNEMENT_MOTEUR", PsaParameterType.EngineRunningInformation);
            Add("MP_SENS_COMMANDE_MOT_LEVEE_SOUPAPE", PsaParameterType.ValvesRaisingElectricEngineRotationDirection);
            Add("MP_COURANT_APPRENTISSAGE_VVT", PsaParameterType.CurrentConsumedByValvesRaisingElectricEngineWhileSetup);
            Add("MP_COURANT_COMMANDE_MOTEUR_VVT", PsaParameterType.CurrentConsumedByValvesRaisingElectricEngine);
            Add("MP_ERREUR_REMPLISSAGE", PsaParameterType.FillingError);
            Add("MP_TEMPERATURE_MOTEUR_VVT", PsaParameterType.TemperatureOfValvesRaisingElectricEngine);
            Add("MP_ETAT_APPRENTISSAGE_INITIAL_BUTEES_SOUPAPES", PsaParameterType.StateofInitialSetupOfUpperAndLowerValveLimiters);

            Add("MP_CONSIGNE_DEBIT_AIR", PsaParameterType.RequestedAirConsumption);

            Add("MP_TOUCHE_ESC", PsaParameterType.ButtonEscPressed);
            Add("MP_TOUCHE_MODE_1", PsaParameterType.ButtonModePressed);
            Add("MP_TOUCHE_MENU", PsaParameterType.ButtonMenuPressed);
            Add("MP_TOUCHE_OK_1", PsaParameterType.ButtonOkPressed);
            Add("MP_TOUCHE_VERS_GAUCHE", PsaParameterType.ButtonLeftPressed);
            Add("MP_TOUCHE_VERS_DROITE", PsaParameterType.ButtonRightPressed);
            Add("MP_TOUCHE_VERS_BAS", PsaParameterType.ButtonDownPressed);
            Add("MP_TOUCHE_VERS_HAUT", PsaParameterType.ButtonUpPressed);

            Add("MP_LANGUE_DU_CLIENT", PsaParameterType.ClientLanguage);
            Add("MP_FORMAT_DATE", PsaParameterType.DateDisplayNotation);
            Add("CFG_ADF_CLIM_UNITETEMP_000", PsaParameterType.TemperatureUnit);
            Add("MP_UNITE_VOLUME_ET_DISTANCE", PsaParameterType.VolumeAndDistanceUnits);

            Add("MP_OPTION_CHANGEUR_CD", PsaParameterType.CDChangerPresenceOnVANBusSetting);
            Add("MP_MENU_VEHICULE", PsaParameterType.SettingsMenuDisplayed);
            Add("AFFICHAGE_TEMPERATURE_EXTERIEURE", PsaParameterType.ProgrammingOfExteriorTemperature);
            Add("MP_OPTION_RADIO", PsaParameterType.ConfiguredAutoradioPresenceOnVanBus);
            Add("CFG_A98_BSI_ODB_000", PsaParameterType.BoardComputerOption);
            Add("CFG_A6N_BSI_DSG_000", PsaParameterType.LowTyresPressureDetectionOption);
            Add("CFG_PRESENCE_AAS", PsaParameterType.ParkingAidSystemOption);
            Add("MP_TYPE_COMBINE", PsaParameterType.ControlPanelType);
            Add("MP_KML", PsaParameterType.HandsFreeKitOption);
            Add("MP_AFFICHAGE_JOURNAL_DES_ALERTES", PsaParameterType.SecurityAlarmLogOutput);
            Add("MP_AFFICHAGE_JOURNAL_DES_ETATS", PsaParameterType.StatesJournalOutput);
            Add("MP_OPTION_NOMADE", PsaParameterType.NomadeMenuOption);
            Add("MP_AFFICHAGE_ALERTES_EMF", PsaParameterType.AlertsOutputOnMultifunctionalDisplay);
            Add("MP_MENU_CONFIGURATION_UNITE_TEMPERATURE", PsaParameterType.TemperatureUnitsSetupOption);
            Add("MP_COMMANDE_EMF_PAR_COMMANDE_RADIO_SOUS_VOLANT", PsaParameterType.RadioAndScreenControlUsingSteeringWheenControls);
            Add("MP_TECHNOCDT", PsaParameterType.ConnectionTypeToRemoteControl);

            Add("MP_CONSIGNE_PULSEUR_AFFICHEE", PsaParameterType.DisplayedFanSpeed); // Выводимая на дисплей заданная скорость вентилятора
            Add("MP_DEMANDE_UTILISATEUR_MISE_ROUTE_CLIMATISATION", PsaParameterType.UserCommandToSwicthClimateControlOn); // Команда пользователя на включение климатической установки
            Add("MP_COMMANDE_SORTIE_AUTORISATION_COMPRESSEUR", PsaParameterType.AirConditioningCompressorRelayOutput); // Управление на выходе разрешения включения компрессора
            Add("MP_COMMANDE_ALLUMAGE_CTP1_ET_CPT2", PsaParameterType.AdditionalCTP1AndCTP2ResistorsControl); // Управление дополнительными резисторами CTP1 и CTP2
            Add("MP_ETAT_SECURITE_EAU_MOTEUR", PsaParameterType.EngineCoolantSafetyState); // Состояние безопасности охлаждающей жидкости
            Add("MP_ETAT_SECURITE_GIVRAGE_EVAPORATEUR", PsaParameterType.EvaporatorIcingUpSafetyStatus); 
            Add("MP_TEMPERATURE_EXTERIEURE_CALCULEE", PsaParameterType.CalculatedExternalTemperature); // Рассчитанная наружная температура
            Add("MP_ENTREE_SONDE_EVAPORATEUR_CLIMATISATION", PsaParameterType.EvaporatorSensorTemperature); // Вход датчика температуры испарителя
            Add("MP_ETAT_CAPTEUR_PRESSION_CIRCUIT_REFRIGERATION", PsaParameterType.PressureSensorStateInConditionerCirquit); // Состояние датчика давления в контуре кондиционера
            Add("MP_SECURITE_REGIME_MOTEUR", PsaParameterType.EngineRpmSafety); // Безопасность частоты вращения двигателя
            Add("MP_ETAT_CAPTEUR_TEMPERATURE_EXTERIEURE", PsaParameterType.ExternalTemperatureSensorState); // Состояние датчика температуры наружного воздуха

            Add("MP_COMMANDE_LAVAGE_PROJECTEUR", PsaParameterType.HeadlightWasherControlOrExitWithWorkingEngine);

            Add("MP_APP_POS_MINII_PEDALE_1", PsaParameterType.RecognitionOfAcceleratorPedalMinState1); // Распознавание минимального положения педали акселератора 1
            Add("MP_APP_POS_MINII_PEDALE_2", PsaParameterType.RecognitionOfAcceleratorPedalMinState2);
            Add("MP_APP_POS_MAXI_PEDALE_1", PsaParameterType.RecognitionOfAcceleratorPedalMaxState1);
            Add("MP_APP_POS_MAXI_PEDALE_2", PsaParameterType.RecognitionOfAcceleratorPedalMaxState2);
            Add("MP_ZONE_FONC_APP", PsaParameterType.FunctioningZonesLearningPerformed); // Проведено обучение зон функционирования
            Add("MP_ADAPTATION_CIBLE_VILEBREQUIN_EN_DECELERATION", PsaParameterType.AdaptationOfCrankshaftPositionSensorTargetWhileDecelerating); // Адаптация "мишени" датчика положения коленвала при замедлении
            Add("MP_ADAPTATIFS_COUPLE_DE_FROTTEMENT_FCT_TEMPERATURE_EAU", PsaParameterType.AdaptationRateOfFrictionTorqueDependingOnCoolantTemperature); // Величины адаптации момента трения в зависимости от температуры охлаждающей жидкости
            Add("MP_ETAT_APPRENTISSAGE_BUTEES_PAPILLON", PsaParameterType.StateOfLearningOfThrottleStops); // Состояние "обучения" упорам дроссельной заслонки
            Add("MP_BESOIN_APPRENTISSAGE_BUTEES_PAPILLON", PsaParameterType.ThrottleStopsLearningRequired); // Необходимость "обучения" упорам дроссельной заслонки
            Add("MP_APPRENTISSAGE_POSITION_LIMP_HOME_01", PsaParameterType.PositionRecognitionToDriveInEmergencyMode1); // Распознавание положения для движения в аварийном режиме 1
            Add("MP_APPRENTISSAGE_POSITION_LIMP_HOME_02", PsaParameterType.PositionRecognitionToDriveInEmergencyMode2); // Распознавание положения для движения в аварийном режиме 2
            Add("MP_APP_BUTE_MINII_PAPILLON_NEW_1", PsaParameterType.RecognitionOfMinPositionLimiterForThrottle1); // Распознавание ограничителя минимального положения заслонки 1
            Add("MP_APP_BUTE_MINII_PAPILLON_NEW_2", PsaParameterType.RecognitionOfMinPositionLimiterForThrottle2); // Распознавание ограничителя минимального положения заслонки 2
            Add("MP_VALEURS_ADAPTATIFS_COUPLE_MOTEUR_INDIQUE", PsaParameterType.ValuesOfIndicatedTorqueAdaptations); // Величины адаптации указанного крутящего момента двигателя
            Add("MP_VALEURS_ADAPTATIFS_RICHESSE_AU_RALENTI", PsaParameterType.ValueOfMixtureAdaptiveFunctionsAtIdlingRPM); // Значение адаптивных функций состава смеси на холостом ходу
            Add("MP_VALEURS_ADAPTATIFS_RICHESSE_EN_TRANSITOIRE", PsaParameterType.ValueOfMixtureAdaptiveFunctionsAtTransitionalMode); // Значение адаптивных функций состава смеси на переходном режиме
            Add("MP_VALEURS_ADAPTATIFS_RICHESSE_AVAL", PsaParameterType.MixtureCompositionAdaptiveValuesOfOfInletSensor); // Адаптивные величины состава смеси датчика на выходе
            Add("MP_VALEURS_ADAPTATIFS_CLIQUETIS_CYLINDRE_04", PsaParameterType.AdaptiveValuesForDetonationPreventionInCylinder4); // Адаптивные величины предотвращения детонации цилиндра 4
            Add("MP_VALEURS_ADAPTATIFS_CLIQUETIS_CYLINDRE_03", PsaParameterType.AdaptiveValuesForDetonationPreventionInCylinder3); // Адаптивные величины предотвращения детонации цилиндра 3
            Add("MP_VALEURS_ADAPTATIFS_CLIQUETIS_CYLINDRE_02", PsaParameterType.AdaptiveValuesForDetonationPreventionInCylinder2); // Адаптивные величины предотвращения детонации цилиндра 2
            Add("MP_VALEURS_ADAPTATIFS_CLIQUETIS_CYLINDRE_01", PsaParameterType.AdaptiveValuesForDetonationPreventionInCylinder1); // Адаптивные величины предотвращения детонации цилиндра 1
            Add("MP_ETAT_APP_DEPH_AAC_ADM", PsaParameterType.LearningStateForInletCamshaftDephaser); // Состояние "обучения" механизма изменения фаз впускного распредвала
            Add("MP_VALEUR_APPRENTISSAGE_BUTEE_BASSE_DEPHASEUR_AAC_ADMISSION", PsaParameterType.LearningValueForLowerStopForInletDephaser); // Значение при "обучении" положению нижнего упора механизма рег-я фаз на впуске
            Add("MP_ETAT_APP_DEPH_AAC_ECH", PsaParameterType.LearmingStateOfExhaustDephaser); // Состояние "обучения" механизма изменения фаз выпускного распредвала
            Add("MP_VALEUR_APPRENTISSAGE_BUTEE_BASSE_DEPHASEUR_AAC_ECHAPPEMENT", PsaParameterType.LearningValueForUpperStopForExhaustDephaser); // Значение при "обучении" положению верхнего упора механизма рег-я фаз на выпуске
            Add("MP_STATUS_PREMIER_APP_DEPH_AAC_ADM", PsaParameterType.StateOfTheFirstInletDephaserSetup); // Статус первой настройки фазорегулятора впускного распредвала
            Add("MP_STATUS_PREMIER_APP_DEPH_AAC_ECH", PsaParameterType.StateOfTheFirstExhaustDephaserSetup); // Статус первой настройки фазорегулятора выпускного распредвала

            Add("MP_COUPLE_MOTEUR_EFFECTIF_AVANCE", PsaParameterType.TorqueValueObtainedByIgnitionAdvance_Requested); // Крутящий момент, полученный путём регулировки опережения зажигания (запрос)
            Add("MP_COUPLE_MOTEUR_EFFECTIF_AIR", PsaParameterType.TorqueValueObtainedByAirVolumeFlowRegulation_Requested); // Крутящий момент, полученный путём регулировки объёма воздуха (запрос)
            Add("MP_COUPLE_MOTEUR_AVANCE", PsaParameterType.TorqueValueObtainedByIgnitionAdvance_Real); // Крутящий момент, полученный путём регулировки опережения зажинания (реальный)
            Add("MP_COUPLE_MOTEUR_AIR", PsaParameterType.TorqueValueObtainedByAirVolumeFlowRegulation_Real); // Крутящий момент, полученный путём регулировки объёма воздуха (реальный)

            Add("MP_ZONE_GEOGRAPHIQUE_UTILISATION", PsaParameterType.UsageGeographicalZone);
            Add("MP_FONCTION_FADER", PsaParameterType.FaderFunction);
            Add("MP_BANDE_FREQUENCE_AM", PsaParameterType.FrequencyBandAM);
            Add("MP_TYPE_DE_BATTERIE", PsaParameterType.BatteryType);

            Add("MP_PEDALE_ACCELERATEUR_1", PsaParameterType.AcceleratorPedalPositionSensorVoltage1); // Напряжение датчика положения педали акселератора 1
            Add("MP_PEDALE_ACCELERATEUR_2", PsaParameterType.AcceleratorPedalPositionSensorVoltage2); // Напряжение датчика положения педали акселератора 2
            Add("MP_POSITION_ACCELERATEUR_1", PsaParameterType.AcceleratorPedalHardSpotSignal1);
            Add("MP_POSITION_ACCELERATEUR_2", PsaParameterType.AcceleratorPedalHardSpotSignal2);
            Add("MP_VITESSE_CONSIGNE_REGULATION_DE_VITESSE", PsaParameterType.SpeedRegulatorSpeedSettings); // Заданная скорость регулятора скорости автомобиля
            Add("MP_ETAT_RVV_2", PsaParameterType.SpeedRegulationState); // Состояние регулирования скорости автомобиля
            Add("MP_VIT_CONSIGNE_LVV", PsaParameterType.SpeedLimitationSettings); // Заданная скорость ограничителя скорости автомобиля
            Add("MP_ETAT_LVV_2", PsaParameterType.SpeedLimitationState); // Состояние ограничения скорости автомобиля
            Add("MP_INFORMATION_CAPTEUR_PEDALE_FREIN_01", PsaParameterType.MainBrakeSignal); // Информация датчика педали тормоза 1
            Add("MP_INFORMATION_CAPTEUR_PEDALE_FREIN_02", PsaParameterType.RedundantBrakeSignal);
            Add("MP_INFORMATION_CAPTEUR_EMBRAYAGE", PsaParameterType.ClutchPedalSignal);
            Add("MP_INFORMATION_POINT_DUR_PEDALE_ACCELERATEUR", PsaParameterType.InformationAboutAcceleratorPedalAbutment); // Информация датчика упора педали акселератора
            Add("MP_INFO_FREIN_STATIONNEMENT_COMMANDE_ELECTRIQUE", PsaParameterType.InformationFromElectricParkingBrake); // Информация стояночного тормоза с электроуправлением
            Add("MP_ETAT_VERROUILLAGE_DEVERROUILLAGE_CALCULTATEUR", PsaParameterType.CalculatorStatus); // Состояние калькулятора
            Add("MP_ETAT_MOTEUR", PsaParameterType.EngineState); // Состояние двигателя
            Add("MP_FONCTIONNEMENT_MOTEUR", PsaParameterType.EngineRunningInformation); // Функционирование двигателя
            Add("MP_ETAT_REVEIL_CALCULATEUR", PsaParameterType.CalculatorActivationState); // Состояние активизации калькулятора
            Add("MP_COURSE_POS_EMBRAYAGE", PsaParameterType.ClutchPedalPosition); // Положение педали сцепления
            Add("MPCARB", PsaParameterType.MeasuredFuelLevel); // Уровень топлива
            Add("MP_TEMPS_ARRET_MOTEUR", PsaParameterType.EngineStopTime); // Время остановки двигателя
            Add("MP_TEMPERATURE_EAU_DERNIER_ARRET_MOTEUR", PsaParameterType.CoolantLiquidTemperatureAtLastEngineStop); // Температура ОЖ при последней остановке двигателя
            Add("MP_REGIME_MOTEUR_CONSIGNE", PsaParameterType.RequestedEngineRpm); // Заданная частота вращения двигателя

            Add("MP_TEMPERATURE_EAU_d", PsaParameterType.EngineCoolantTemperature);
            Add("MP_PRESSION_HUILE_ME", PsaParameterType.OilPressure); //
            Add("MP_CONSIGNE_SOULAGEMENT_PUISSANCE_COMPRESSEUR", PsaParameterType.AirConditioningCompressorLoadShedding); // Заданная величина снижения мощности компрессора
            Add("MP_PRESSION_CIRCUIT_REFRIGERANT", PsaParameterType.ConditionerCoolingCircuitPressure); // Давление в контуре кондиционера воздуха

            Add("MP_CONSIGNE_TEMPERATURE_EAU", PsaParameterType.CoolantTemperatureSettings); // Заданное значение температуры охлаждающей жидкости
            Add("MP_ACT_PPE_EAU_1", PsaParameterType.CoolantPumpActuatorControl); // Управление исполнительным элементом насоса системы охлаждения
            Add("MP_ETAT_REL_GMV_C5", PsaParameterType.FanAssemblyHighSpeedRelayControl); // Состояние реле большой скорости группы электровентилятора
            Add("MP_ETAT_GMV_PTIT_C5", PsaParameterType.FanAssemblyLowSpeedRelayControl); // Состояние реле малой скорости группы электровентилятора
            Add("MP_ETATGMV", PsaParameterType.FanAssemblyStateInputState); // Состояние входа "Состояние группы электровентилятора"
            Add("MP_ESTIMCLIM", PsaParameterType.EstimatedPowerConsumptionByAirConditionerCompressor); // Оценка потребляемой мощности компрессора кондиционера
            Add("MP_CONSIGNE_DE_CHARGE_PROGRESSIVE_ALTERNATEUR", PsaParameterType.RequiredValueForGradualGeneratorLoad); // Заданное значение постепенной нагрузки генератора
            Add("MP_CONSIGNE_TENSION_ALTERNATEUR", PsaParameterType.ManagedAlternatorVoltageSetting); // Заданное напряжение управляемого генератора
            Add("MP_TEMPERATURE_ALTERNATEUR_PILOTE", PsaParameterType.AlternatorTemperature); // Температура управляемого генератора
            Add("MP_COURANT_ALTER", PsaParameterType.GeneratorCurrent); // Ток генератора
            Add("MP_VALEUR_CHARGE_ALTERNATEUR", PsaParameterType.AlternatorChargeValue); // Величина заряда генератора
            Add("MP_TENSION_ALIMENTATION_CAPTEURS_01", PsaParameterType.SensorsSupplyVoltage); // Напряжение питания датчиков
            Add("MP_NOMBRE_RESET_CALCULATEUR", PsaParameterType.CalculatorResetsCount); // Число обнулений компьютера 
            Add("MP_CONSIGNE_COMMANDE_EV_POMPE_A_HUILE", PsaParameterType.RequestToControlElectromagneticValveOfOilPump); // Требование управления электромагнитным клапаном масляного насоса
            Add("MP_ETAT_RELAIS_PUISSA", PsaParameterType.PowerRelayState); // Состояние реле мощности 
            Add("MP_ETAT_COMMANDE_RELAIS_PILOTAGE_DEMARREUR", PsaParameterType.StateOfControlOfStarterControlRelay); // Состояние управления реле управления стартером
            Add("MP_TEMPERATURE", PsaParameterType.CalculatorTemperature); // Температура компьютера
            Add("MP_MODELE_TEMPERATURE_EAU", PsaParameterType.ModeledCoolantTemperature); // Смоделированная температура охлаждающей жидкости (достоверность)
            Add("MP_MODE_POMPE_HUILE", PsaParameterType.OilPumpFunctioningMode); // Режим работы масляного насоса

            Add("MP_ETAT_VERROUILLAGE_DEVERROUILLAGE_CALCULTATEUR1", PsaParameterType.CalculatorStatus);
            Add("MP_ETAT_PROGRAMMATION_ANTI_DEMARRAGE", PsaParameterType.CodedEngineImmobiliserProgrammingStatus);

            Add("MP_TENSION_ALIMENTATION", PsaParameterType.SupplyVoltage); // Напряжение питания
            Add("MP_VITESSE_ROUE_ARRIERE_GAUCHE", PsaParameterType.RearLeftWheelSpeed);
            Add("MP_VITESSE_ROUE_ARRIERE_DROITE", PsaParameterType.RearRightWheelSpeed);
            Add("MP_VITESSE_ROUE_AVANT_GAUCHE", PsaParameterType.FrontLeftWheelSpeed);
            Add("MP_VITESSE_ROUE_AVANT_DROIT", PsaParameterType.FrontRightWheelSpeed);

            Add("MP_TENSION_ALIMENTATION_CALCULATEUR_C0", PsaParameterType.CalculatorSupplyVoltage); // Напряжение питания компьютера
            Add("MP_CONTACTEUR_FEUX_STOP", PsaParameterType.StopSignalContact); // Контакт стоп-сигнала
            Add("MP_ETAT_RELAIS_ALIMENTATION_EV", PsaParameterType.ElectricValvesSupplyRelayState); // Состояние реле питания электроклапанов
            Add("MP_ETAT_RELAIS_POMPE_RECIRCULATION_1", PsaParameterType.RecirculationPumpRelaystate); // состояние реле насоса рециркуляции
            Add("MP_NIVEAU_LIQUIDE_DE_FREIN", PsaParameterType.BrakeFluidLevel); // Уровень тормозной жидкости
            Add("MP_USURE_GARNITURES_FREIN_1", PsaParameterType.BrakeLiningsWear); // Износ тормозных колодок

            Add("MP_ETAT_COMMANDE_ECLAIRAGE_COMMUTATEUR_3_POSITIONS", PsaParameterType.LightingControl_SwitchInPosition3); // Управление освещением - переключатель в 3 положении
            Add("MP_ETAT_COMMANDE_ECLAIRAGE_COMMUTATEUR_4_POSITIONS", PsaParameterType.LightingControl_SwitchInPosition4); // Управление освещением - переключатель в 4 положении
            Add("MP_COMMANDE_INVERSEUR_FEUX_DE_CROISEMENT_FEUX_DE_ROUTE", PsaParameterType.DippedAndHighBeamSwitch); // Переключатель ближнего / дальнего света
            Add("MP_COMMANDE_IMPULSIONNELLE_FEUX_DE_ROUTE", PsaParameterType.HighBeamImpulseSwitch); // Импульсный переключатель фар дальнего света
            Add("MP_ACTIVATION_FEUX_DE_BROUILLARD", PsaParameterType.FogLightsState_Minus); // Состояние противотуманных фар / фонарей (плюс)
            Add("MP_COMMANDE_ANTIBROUILLARD_MOINS", PsaParameterType.FogLightsState_Plus); // Состояние противотуманных фар / фонарей (минус)
            Add("MP_PUSH_CDE_GH", PsaParameterType.LightSwitchButtonOn); // Включение кнопки переключателя света
            Add("MP_COMMANDE_CLIGNOTANTS", PsaParameterType.TurnIndicatorsControl); // Управление указателями поворота

            Add("MP_ETAT_SELECTION_LVV_RVV", PsaParameterType.SpeedRegulationORLimitationSelection); // Выбор регулирование/ограничение скорости
            Add("MP_AUGMENTATION_CONSIGNE_DE_VITESSE", PsaParameterType.IncreasingSelectedSpeed); // Увеличение устанавливаемой скорости
            Add("MP_REGULATION_DE_VITESSE_MOINS", PsaParameterType.DecreasingSelectedSpeed); // Уменьшение устанавливаемой скорости
            Add("MP_REGUL_ON_OFF", PsaParameterType.WakingUpCruiseControl); // Вывод круиз-контроля из режима сна

            Add("MP_COMMANDE_RADIO_FREQUENCE_PLUS", PsaParameterType.RadioSearchButtonPlus); // Автомагнитола: поиск станций выше по диапазону
            Add("MP_COMMANDE_RADIO_FREQUENCE_MOINS", PsaParameterType.RadioSearchButtonMinus); // Автомагнитола: поиск станций ниже по диапазону
            Add("MP_COMMANDE_RADIO_VOLUME_PLUS", PsaParameterType.RadioVolumeButtonPlus); // Радоприёмник: + громкость
            Add("MP_COMMANDE_RADIO_VOLUME_MOINS", PsaParameterType.RadioVolumeButtonMinus); // Радиоприёмник: - громкость
            Add("MP_COMMANDE_RADIO_COMPTEUR_MOLETTE_SCAN", PsaParameterType.RadioScanRegulator); // Радиоприёмник: счётчик круглого регулятора "Scan"
            Add("MP_ETAT_COMMUTATEUR_SOURCE_TELEPHONE", PsaParameterType.SourceToTelephoneSwitch); // Переключатель "Источник/Телефон"

            Add("MP_AVERTISSEUR_SONORE", PsaParameterType.SoundAlarm); // Звуковая сигнализация
            Add("COMMANDE_ORDINATEUR_DE_BORD", PsaParameterType.BoardComputerControl); // Управление бортовым компьютером
            Add("MP_COMMANDE_ESSUYAGE_IMPULSIONNELLE_AVANT", PsaParameterType.SingleTimeSwitchOfWindscreenWiper); // Управление разовым включением переднего стеклоочистителя
            Add("COMMANDE_DU_BOUTON_POUSSOIR_COMMUTATEUR_ESSUYAGE", PsaParameterType.ScreenWiperSwitchControl); // Управление кнопкой переключателя стеклоочистителя

            Add("MP_BSI_MAINTIEN_RESEAUX_PAR_PRESENCE_DU_CONTACT", PsaParameterType.MaintainingDueToThePresenceOfTheIgnition); // Maintaining due to the presence of the ignition
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_ALARME", PsaParameterType.MaintainingRequestForAlarm); // Maintaining request for alarm
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_LOCALISATION_VEHICULE", PsaParameterType.MaintainingDueToALocatingOfAVehicleBeingInProgress); // Maintaining due to locating of the vehicle being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_ALERTE_OUBLI_CLE", PsaParameterType.MaintainginForKeyInIgnitionWarning); // Maintaining for key in ignition warning
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_WARNING", PsaParameterType.MaintainingDueToThePresenceOfTheWarning); // Maintaining due to the presence of the warning
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_APPEL_OPTIQUE", PsaParameterType.MaintainingDueToTheHeadlampFlashBeingActivated); // Maintaining due to the headlamp flash being activated // 
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_FEUX_DE_POSITION_ALLUMES", PsaParameterType.MaintainingDueToTheSideLightsBeingOn); // Maintaining due to the side lights being on
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_ECLAIREUR_COFFRE_ALLUME", PsaParameterType.MaintainingDueToTheBootLightBeingOn); // Maintaining due to the boot light being on
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_SESSION_DIAGNOSTIC_OU_TELECHARGMT", PsaParameterType.MaintainingDueToADiagnosticSessionOrDownloadingBeingInProgress); // Maintaining due to a diagnostic session or downloading being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_PORTE_LATERALE_COULISSANTE", PsaParameterType.MaintainingDueToTheSlidingDoorMovingOrBeingStopped); // Maintaining due to the sliding side door moving or being stopped
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_COLONNE_DIRECTION_EN_MOUVEMENT", PsaParameterType.MaintainingDueToTheMovementOfSteeringColumn); // Maintainig due to the movement of the steering column
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_RETROVISEURS", PsaParameterType.MaintainingDueToMovingOrFoldingOfTheExteriorMirrors); // Maintaining due to movement of the exterior mirrors or folding the exterior mirrors being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_SIEGE_EN_MOUVEMENT", PsaParameterType.MaintainingForSeatMovement); // Maintaining for seat movement 
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_LEVE_VITRE_EN_MOUVEMENT", PsaParameterType.MaintainingForElectricWindowMovement); // Maintaining for electric window movement
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_TOIT_OUVRANT_EN_MOUVEMENT", PsaParameterType.MaintainingForSunroofMovement); // Maintaining for sunroof movement
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_MIRROIR_VTH", PsaParameterType.MaintainingForHighHeadVisionMirrorMovement); // Maintaining for High Head Vision mirror movement
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_RAPPEL_MEMOIRE_POSTE_DE_CONDUITE", PsaParameterType.MaintainingDueToRecallingOfTheDrivingPositionMemoryBeingInProgress); // Maintaining due to recalling of the driving position memory being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_MEMORISATION_POSTE_CONDUITE_EN_COUR", PsaParameterType.MaintainingDueToMemorisingOfTheDrivingPositionBeingInProgress); // Maintaining due to memorising of the driving position being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_FONCTION_ACCUEIL_EN_COURS", PsaParameterType.MaintainingDueToWelcomeFunctionBeingInProgress); // Maintaining due to the welcome function being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_PRE_OU_POST_CONDITIONNEMENT_THERMIQ", PsaParameterType.MaintainingDueToThermalPrePostConditioningBeingInProgress); // Maintaining due to thermal pre-post conditioning being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_AVERTISSEUR_SONORE_ACTIF", PsaParameterType.MaintainingDueToTheHornBeingActive); // Maintaining due to the horn being active
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_ACTIVITE_RADIO_OU_TELEMATIQUE", PsaParameterType.MaintainingForAudioEquipmentOrTelematicsActivity); // Maintaining for audio equipment or telematics activity
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_CORRECTION_HAUTEUR_EN_COURS", PsaParameterType.MaintainingDueToHeightAdjustmentBeingInProgress); // Maintaining due to height adjustment being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_AFFICHAGE_ETAT_FSE", PsaParameterType.MaintainingDueToDisplayingOfTheStatusOfTheElectricEmergencyBrakeBeingInProgress); // Maintaining due to displaying of the status of the electric emergency brake being in progress
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_COFFRE_OU_VOLET_MOTORISE_EN_MVT", PsaParameterType.MaintainingDueToMovingMotorisedBootOrTailgate); // Maintaining due to moving motorised boot or tailgate
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_ECLAIRAGE_AMBIANCE_OU_ACCUEIL", PsaParameterType.MaintainingDueToAmbientOrWelcomeLightingBeingOn); // Maintaining due to ambient or welcome lighting being on
            Add("MP_BSI_MAINTIEN_RESEAUX_POUR_CPC", PsaParameterType.MaintenanceDueToTheCentralisedPowerSwitchPositivePlusCpc); // Maintenance due to the centralised power switch positive (+CPC)
            Add("MP_BSI_MAINTIEN_RESEAUX_PAR_MDS", PsaParameterType.MaintenanceByTheServiceModuleMds); // Maintenance by the Service Module (MDS)

            Add("MP_ETAT_FREIN_PARKING_ENCLENCHE", PsaParameterType.ParkingBrake); // Handbrake engaged
            Add("MP_PEDALE_DE_FREIN_ENFONCEE", PsaParameterType.BrakePedal); // Brake pedal pressed
            Add("MP_CEINTURE_AVANT_GAUCHE_BLOUCLEE", PsaParameterType.DriverSeatBeltFastened); // Front left seat belt fastened
            Add("MP_CEINTURE_AVANT_DROITE_BOUCLEE", PsaParameterType.PassengerSeatBeltFastened); // Front right seat belt fastened
            Add("MP_CEINTURE_SEC_ARRIERE_GAUCHE_BOUCLEE", PsaParameterType.RearLeftSeatBeltFastened); // Rear left seat belt fastened
            Add("MP_CEINTURE_SEC_ARRIERE_DROITE_BOUCLEE", PsaParameterType.RearRightSeatBeltFastened); // Rear right seat belt fastened
            Add("MP_CEINTURE_ARRIERE_CENTRALE_BOUCLEE", PsaParameterType.RearCentreSeatBeltFastened); // Rear centre seat belt fastened
            Add("MP_CEINTURE_SECURITE_GAUCHE_RANG_3_BOUCLEE", PsaParameterType.Row3LeftHandSeatBeltBuckled); // Row 3 left-hand seat belt buckled
            Add("MP_CEINTURE_SECURITE_DROITE_RANG_3_BOUCLEE", PsaParameterType.Row3RightHandSeatBeltBuckled); // Row 3 right-hand seat belt buckled
            Add("MP_DEMANDE_ACTIVATION_SECURITE_ENFANT_NEW_1", PsaParameterType.ChildSafetyActivationControl); // Child lock activation request
            Add("MP_SECURITE_ENFANT_DROIT_ACTIVEE", PsaParameterType.ChildSafetyRight); // Right-hand side child lock
            Add("MP_SECURITE_ENFANT_GAUCHE_ACTIVEE", PsaParameterType.ChildSafetyLeft); // Left-hand side child lock

            Add("MP_DEMANDE_D_ECLAIRAGE_PLAFONNIER", PsaParameterType.SillLighting); // Request to turn the courtesy light on // Команда на включение плафона
            Add("MP_COMMANDE_ECLAIRAGE_PLAFONNIER_AVANT", PsaParameterType.FrontSillLightingCover); // Front courtesy light lighting control // Управление работой переднего плафона
            Add("MP_COMMANDE_ECLAIRAGE_PLAFONNIER_ARRIERE", PsaParameterType.RearSillLightingCover); // Rear courtesy light lighting control // управление рабоой заднего плафона
            Add("MP_COMMANDE_ECLAIRAGE_AFFICHEURS_ET_PUSH_INTERIEURS", PsaParameterType.InteriorLampControl); // control of lighting of the interior controls and display
            Add("MP_COMMANDE_ECLAIRAGE_COFFRE_SANS_BSC", PsaParameterType.BootLightingCommand); // Boot light control
            Add("MP_COMMANDE_ECLAIRAGE_SEUILS_PORTES_AVANT_a", PsaParameterType.FrontSillLightingControl); // Front doors sills lighting control
            Add("MP_COMMANDE_ECLAIRAGE_SEUILS_PORTES_ARRIERE_a", PsaParameterType.RearSillLightingControl); // Rear doors sills lighting control

            Add("MP_CDE_FEUX_POSITION_GAUCHE_VEH_ET_REMORQUE", PsaParameterType.LeftHandVehicleAndTrailerSidelampsControl); // Управление включением габаритных огней автомобиля с левой стороны и прицепа // Vehicle and trailer LH side lamps illumination control
            Add("MP_DEMANDE_ALLUMAGE_FEUX_DE_CROISEMENT", PsaParameterType.DippedBeamSwitchOnCommand); // Команда включения фар ближнего света // Request to switch on the dipped headlamps
            Add("MP_DEMANDE_APPEL_PHARE", PsaParameterType.HeadlampFlashRequest); // Команда на сигнализацию светом фар // Headlamp flash request
            Add("MP_DEMANDE_FEUX_DE_CROISEMENT_FEUX_DE_ROUTE", PsaParameterType.DippedBeamHighBeamHeadlampsInversion); // Переключение фар ближнего/дальнего света // Dipped beam / main beam headlamps inversion
            Add("MP_COMMANDE_FEUX_DIURNES_AVANT_DEDIES_LAVE_PROJ", PsaParameterType.FrontDaylightsSwitchOnOrHeadlampWasher); // Включение передних фар дневного света или омывателей фар // Control of the dedicated front daytime-running lamps or of headlamp wash
            Add("MP_COMMANDE_ECLAIRAGE_FEUX_PLAQUE_POLICE", PsaParameterType.NumberPlateLightingControl); // Управление включением освещения регистрационного знака // Number plate lights lighting command
            Add("MP_COMMANDE_FEUX_DE_ROUTE", PsaParameterType.MainBeamControl); // Управление фарами дальнего света // Check the main beam lights 
            Add("MP_COMMANDE_FEUX_DE_BROUILLARD_AVANT", PsaParameterType.FrontFogLampsControl); // Управление передними противотуманными фарами // Front fog lamps control
            Add("MP_COMMANDE_FEUX_DE_BROUILLARD_ARRIERE", PsaParameterType.RearFogLampsControl); // Управление задждними противотуманными фарами // Rear fog lamps control
            Add("MP_COMMANDE_FEUX_DE_CROISEMENT", PsaParameterType.DippedBeamControl); // Управление фарами ближнего света // Check on dipped lights 
            Add("MP_MODE_ECLAIRAGE_AUTOMATIQUE", PsaParameterType.AutomaticLightingMode); // Автоматический режим включения приборов освещения // Automatic lighting mode
            Add("MP_DEMANDE_ALLUMAGE_FEUX_DE_POSITION", PsaParameterType.SidelampsControl); // Управление включением габаритных огней // Request to switch on the side lights
            Add("MP_CDE_FEUX_POSITION_DROIT_VEH_ET_REMORQUE", PsaParameterType.RightHandVehicleAndTrailerSidelampsControl); // Управление включением габаритных огней автомобиля с правой стороны и прицепа // Vehicle and trailer RH side lamps illumination control

            Add("MP_DEMANDE_FEUX_DE_DETRESSE", PsaParameterType.HazardWarningLampSwitchOnRequest); // Hazard warning laps illumination request // Запрос на включение аварийной сигнализации
            Add("CLIGNOTANTG", PsaParameterType.LeftHandTurnIndicatorIndicationRequest); // LH indicator illumination request // Запрос на включение указателя поворота слева
            Add("MP_COMMANDE_DE_CLIGNOTANTS_DROIT", PsaParameterType.VehicleAndTrailerRightHandIndicatorIllumicationControl); // Vehicle and trailer RH indicator illumination control // Управление активизацией правых указателей поворота автомобиля и прицепа
            Add("MP_COMMANDE_DE_CLIGNOTANTS_GAUCHE", PsaParameterType.VehicleAndTrailerLeftHandIndicatorIllumicationControl); // Vehicle and trailer LH indicator illumination control // Управление активизацией левых указателей поворота автомобиля и прицепа 
            Add("CLIGNOTANTD", PsaParameterType.RightHandTurnIndicatorIndicationRequest); // RH indicator illumination request // Запрос на включение указателей поворота справа
            Add("MP_COMMANDE_FEUX_DE_RECUL", PsaParameterType.ReverseLampsIllumunationControl); // Reversing lamps illumination control // Управление включением фонарей заднего хода
            Add("MP_COMMANDE_FEUX_STOP", PsaParameterType.StopLampsIlluminationControl); // Stop lamps illumination control // Управление включением фонарей стоп
            Add("MP_DEMANDE_AVERTISSEUR_SONORE", PsaParameterType.RequestForHornSound); // Request for horns // Команда на включение звукового сигнала
            Add("MP_COMMANDE_AVERTISSEUR_SONORE", PsaParameterType.HornControl); // Horn control // Управление звуковым сигналом

            Add("MP_TELECOMMANDE_HF_01_SYNCHRONISEE", PsaParameterType.Key1Sync); // ПДУ 1 синхронизирован // Remote control 1 synchronized
            Add("MP_TELECOMMANDE_HF_02_SYNCHRONISEE", PsaParameterType.Key2Sync); // ПДУ 2 синхронизирован // Remote control 2 synchronized 
            Add("MP_TELECOMMANDE_HF_03_SYNCHRONISEE", PsaParameterType.Key3Sync); // ПДУ 3 синхронизирован // Remote control 3 synchronized
            Add("MP_TELECOMMANDE_HF_04_SYNCHRONISEE", PsaParameterType.Key4Sync); // ПДУ 4 синхронизирован // Remote control 4 synchronized
            Add("MP_TELECOMMANDE_HF_05_SYNCHRONISEE", PsaParameterType.Key5Sync); // ПДУ 5 синхронизирован // Remote control 5 synchronized
            Add("MP_CALCULATEUR_MOTEUR_VERROUILLE", PsaParameterType.ECULocking); // Калькулятор двигателя заблокирован // ECU locked
            Add("MP_BSI_ANTI_SCANNING", PsaParameterType.TransponderAntiscanActive); // BSI в режиме антисканнинга // BSI in anti-scanning mode
            Add("MP_NOMBRE_TRANSPONDEUR_APPRIS", PsaParameterType.NumberOfSynchronizedRadiokeys); // Число ключей транспондера, для которых проведено обучение // Transponder key number learned
            Add("MP_TRANSPONDEUR_RECONNU", PsaParameterType.TransponderLabelRecognized); // Наклейка транспондера не указана // Transponder label recognized
            Add("MP_NOMBRE_TELECOMMANDE_HF_APPRISES", PsaParameterType.NumberOfHichFrequencyRemoteControlsLearned); // Число высокочастотных ПДУ, занесённых в память // Number of high frequency remote control commands learned
            Add("MP_TELECOMMANDE_HF_DESYNCHRONISEE", PsaParameterType.HighFrequencyRemoteControlDesynchronized); // Высокочастотный ПДУ синхронизирован // Remote desynchronized high frequency control
            Add("MP_BSI_APP_CALCULATEUR_CONTROLE_MOTEUR", PsaParameterType.BSIMatchedToECU); // СМ синхронизирован с калькулятором контроля двигателя // BSI matched to the engine management ECU
            Add("MP_BSI_VIERGE_DE_CODE_ADC", PsaParameterType.BsiHasNoEngineImmobilizerCode); // В BSI не записан код иммобилайзера (ADC) // BSI with no engine immobiliser code (coded engine immobilizer)
            Add("MP_PRESENCE_CLE", PsaParameterType.PresenceOfTheKey); // Присустствие ключа // Presence of the key
            Add("MP_PILE_TELECOMMANDE_HF_USEE", PsaParameterType.HighFrequencyRemoteControlBatteryFlat); // Элемент питания высокочастотного ПДУ разряжен // High frequency remote control battery flat
            Add("MP_NOMBRE_ESSAI_AUTHENTIFICATION_TRANSPONDEUR", PsaParameterType.NumberOfTransponderAuthenticationAttempts); // Порядковый номер попытки аутентификации транспондера // Number of attempts to authenticate the transponder
            Add("MP_NOMBRE_ESSAI_IDENTIFICATION_TRANSPONDEUR", PsaParameterType.NumberOfTransponderIdentifictionAttempts); // Порядковый номер попытки идентификации транспондера // Number of attempts to identify the transponder
            Add("MP_ETAT_ENVOI_DONNEE_ADC_CMM_1", PsaParameterType.EmissionOfTheEncryptedDatatoEngineECU); // Передача зашифрованных данных на калькулятор двигателя // Emission of the encrypted data to the engine ECU
            Add("MP_BSI_PRET_POUR_APPRENTISSAGES", PsaParameterType.BSIReadyForProgramming); // СМ готов для выполнения процедуры распознавания // BSI ready for programming

            Add("MP_PRESENCE_AAS", PsaParameterType.PresenceOfParkingAssistance); // Presence of parking assistance
            Add("MP_ENTREE_AUX_1", PsaParameterType.AuxiliaryInput1); // Auxiliary input 1
            Add("MP_ENTREE_AUX_2", PsaParameterType.AuxiliaryInput1); // Auxiliary input 2

            Add("MP_VITESSE_MOTEUR_GEP", PsaParameterType.SpeedElectropumpAssemblyMotor);
            Add("MP_VITESSE_VOLANT_DE_DIRECTION", PsaParameterType.SteeringWheelSpeed);
            Add("MP_TEMPERATURE_GEP", PsaParameterType.SteeringElectropumpTemperature);
            Add("MP_PRESENCE_SIGNAL_ANGLE_VOLANT_DE_DIRECTION", PsaParameterType.SteeringWheelRotationAngleDataPresence); // Presence of the steering wheel angle signal
            Add("INTENSITE_MESUREE", PsaParameterType.MeasuredCurrent); // Measured current
            Add("MP_ETAT_MOTEUR_THERMIQUE_1_2", PsaParameterType.ThermalEngineStatus); // Thermal engine status
            Add("CFG____ASSISTANCE_1", PsaParameterType.SteeringStatus); // Steering status
            Add("TYPE_ANGLE_VOLANT_DE_DIRECTION", PsaParameterType.SteeringWheelAngleSensorType); // Type of steering wheel angle sensor

            Add("MP_COMMANDE_FEUX_DE_BROUILLARD_AVANT_13", PsaParameterType.FrontFogLampsControl); // Front fog lamps illumination control
            Add("MP_CDE_CLIGNO_DRT_15_1", PsaParameterType.VehicleAndTrailerRightHandIndicatorIllumicationControl); // Vehicle and trailer RH indicator illumination control
            Add("MP_CDE_CLIGNO_GH_16_1", PsaParameterType.VehicleAndTrailerLeftHandIndicatorIllumicationControl); // Vehicle and trailer LH indicator illumination control
            Add("MP_COMMANDE_FEUX_DE_CROISEMENT_20_1", PsaParameterType.DippedBeamControl); // Dipped beam illumination control
            Add("MP_COMMANDE_ECLAIRAGE_FEUX_DE_ROUTE_1", PsaParameterType.MainBeamControl); // Main beam illumination control
            Add("MP_COMMANDE_LANTERNES_DROITES_55_1", PsaParameterType.RightHandVehicleAndTrailerSidelampsControl); // Vehicle and trailer RH side lamps illumination control
            Add("MP_COMMANDE_LANTERNES_GAUCHES_56_1", PsaParameterType.LeftHandVehicleAndTrailerSidelampsControl); // Vehicle and trailer LH side lamps illumination control

            Add("APC", PsaParameterType.PresenceOfPlusIgnition);

            Add("MP_PERSONNALISATION", PsaParameterType.PersonalizationMenu); // Personalization menu // Меню персонализации
            Add("MP_AFFICHAGE_DPC", PsaParameterType.DisplayOfTheDrivingPositionClearanceOption); // Display of the "Driving position clearance" option // Вывод информации опции "Разблокировка места водителя"
            Add("MP_DOPTOTAL", PsaParameterType.DisplayOfTheTotalNearbyObstacleDetectionOption); // Display of the "Total nearby obstacle detection" option // Вывод информации опции "Определение расстояния до препятствия"
            Add("MP_ELAVESAR", PsaParameterType.DisplayOfTheEngagementOfTheRearWipingWhenReversingOption); // Display of the "Engagement of the rear wiping whn reversing" option // Вывод информации опции "Включение заднего дворника при движении задним ходом"
            Add("MP_OPTION_AUTO", PsaParameterType.DisplayOfTheAutomaticHeadlampIlluminationOption); // Display of the "Automatic headlamp illumunation" option // Вывод информации опции "Автоматическое включение света"
            Add("MP_EEXFMH", PsaParameterType.DisplayOfTheFollowMeHomeLightingOption); // Display of the "Follow me home lighting" option // Вывод информации опции "Освещение сопровождения"
            Add("MP_EEXADAPT", PsaParameterType.DisplayOfTheDirectionlaHeadlampOption); // Display of the "Directional headlamp" option // Вывод информации опции "Поворотные фары"
            Add("MP_OPTION_ECLAIRAGE_AMBIANCE", PsaParameterType.DisplayOfTheAmbientLightingOption); // Display of the "Ambient lighting" option // Вывод информации опции "Внутренее освещение автомобиля"
            Add("MP_COND_COFFRE", PsaParameterType.DisplayOfThePermanentLockingOfTheBootOption); // Display of the "Permanent locking of the boot" option // Вывод информации опции "Постоянное запирание багажника"
            Add("MP_GAV_PORTE_COND", PsaParameterType.DisplayOfTheLockingOfTheDriversDoorOnlyOption); // Display of the "Locking of the driver's door only" option // Вывод информации опции "Запирание только двери водителя"
            Add("MP_GAV_ELOIGN", PsaParameterType.DisplayOfTheLockingOfTheDoorsWhenYouMoveAwayOption); // Display of the "Locking of the doors when you move away" option // Вывод информации опции "Дистанционное запирание дверей"
            Add("MP_MODE_DE_VERROUILLAGE_VEHICULE", PsaParameterType.DisplayOfTheLockingDeadlockingModeOption); // Display of the "Locking/deadlocking mode" option // Вывод информации опции "Режим запирания / суперблокировки"
            Add("MP_FSE", PsaParameterType.DisplayOfTheDeactivationAutomaticElectricSecondaryBrakeOption); // Display of the "Deactivation automatic electric secondary brake" option // Вывод информации опции "Отключение электрического автоматического вторичного тормоза-замедлителя"
            Add("MP_FEUX_DIURNES", PsaParameterType.DisplayOfTheDaylightLightsOption); // Display of the "Daylight lights" option // Вывод информации опции "Свет, включаемый в дневное время"

            Add("MP_POSITION_MOTEUR_PAS_A_PAS_RECYCLAGE_AIR", PsaParameterType.AirRecyclingMotorPosition); // Air recycling motor position // Положение привода заслонки рециркуляции воздуха
            Add("MP_POSITION_MOTEUR_PAS_A_PAS_MIXAGE_COTE_CONDUCTEUR", PsaParameterType.MixingStatusLeft); // Driver's side air mixing motor position // Положение привода смесителя воздуха состороны водителя
            Add("MP_POSITION_MOTEUR_PAS_A_PAS_MIXAGE_COTE_PASSAGER", PsaParameterType.MixingStatusRight); // Passenger side air mixing motor position // Положение привода смесителя воздуха со стороны пассажира
            Add("MP_VOLET_DISTR_AERA", PsaParameterType.MixingShutterStatus); // Posiotion of the distribution flap at ventilation // Положение заслонки распределения вентиляции

            Add("MP_DEMANDE_AIR_CONDITIONNE_TOUCHE_AC", PsaParameterType.AirConditioningRequest); // Команда на включение кондиционера воздуха (АС) // Air conditioning request (AC)
            Add("MP_TOUCHE_DEGIVRAGE_DESEMBUAGE_AVANT", PsaParameterType.DeicingCommand); // Команда на включение системы удаленяи инея со стёкол / обеспечения обзора // Deicing / visibility request
            Add("MP_DEMANDE_REGULATION_AUTOMATIQUE", PsaParameterType.AutomaticRegulationCommand); // Команда на включение автоматического регулирования (AUTO) // Automatic regulation request (AUTO)
            Add("MP_DEMANDE_DE_RECYCLAGE_1", PsaParameterType.RecirculationRequest); // Команда на включение рециркуляции // Recirculation request
            Add("MP_DEMANDE_DEGIVRAGE_LUNETTE_ARRIERE_RETRO_7", PsaParameterType.RearDeicingCommand); // Запрос на активизацию обогрева заднего стекла и зеркал заднего вида // Rear screeen and door mirror de-icing request
            Add("MP_DEMANDE_DISTR_AIR_DERIV", PsaParameterType.AirDistributionRequest_Deicing); // Команда распределения воздуха - удаления инея со стёкол // Air distribution request - De-icing
            Add("MP_DEMANDE_DISTR_AIR_AERA", PsaParameterType.AirDistributionRequest_Ventilation); // Команда распределения воздуха - вентиляции // Air distribution request - Ventilation
            Add("MP_DEMANDE_DISTR_AIR_PIEDS", PsaParameterType.AirDistributionRequest_Footwells); // Команда распределения воздуха - подачи воздуха к ногам // Air distribution request - Footwells
            Add("MP_DEMANDE_REG_MONOZONE", PsaParameterType.MonozoneRegulationRequest); // Команда на однозонное регулирование // Monozone regulation request

            Add("MP_NBR_JOURS_UTILISATION_TUNER", PsaParameterType.NumberOfDaysOfUseOfTheTuner); // Number of days of use of the tuner // Число дней использования тюнера
            Add("MP_NBR_JOURS_UTILISATION_CD", PsaParameterType.NumberOfDaysOfUseCDPlayer); // Number of days of use of the CD Player // Число дней использования проигрывателя CD

            Add("MP_QUALITE_RECEPTION_ANTENNE_1", PsaParameterType.Aerial1RadioQuality); // Quality of radio reception from the 1 aerial // Качество радиоприёма с помощью антенны 1
            Add("MP_NIVEAU_SIGNAL_RADIO_FM1_AM", PsaParameterType.LevelOfAmFmIncomingSignal); // Level of incoming radio signal from FM1 or AM // Уровень входного сигнала радио с помощью антенны 2
            Add("MP_QUALITE_RECEPTION_ANTENNE_2", PsaParameterType.Aerial2RadioQuality); // Quality of radio reception from the 2 aerial // Качество радиоприёма с помощью антенны 2
            Add("MP_NIVEAU_SIGNAL_RADIO_FM2", PsaParameterType.FM2IncomingSignalLevel); // Level of the incoming radio signal from FM2 // Уровень входного раиосигнала от FM2

            Add("MP_DETECT_EFFRACTION_VOLUM", PsaParameterType.VolumetricBreakinDetection);

            Add("MP_CONSO_MOYENNE_TRAJET", PsaParameterType.AverageJourneyConsumption);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);

            Add("MP_NIVEAU_HUILE_MOTEUR_MESURE", PsaParameterType.MeasuredOilLevel);
            Add("MP_COMMANDE_LAVAGE_PARE_BRISE", PsaParameterType.WindscreenWiperControl); // Управление режимом работы передних "дворников"
            Add("MP_VITESSE_VEHICULE_a", PsaParameterType.VehicleSpeed);
            //Add("", PsaParameterType.);

            Add("ETAT_REGULATION_RICHESSE_6LP", PsaParameterType.MixRegState);
            Add("AUTORISATIONCLIM_6LP", PsaParameterType.AirConditioningAuthorisation);
            Add("BOBINE1_6LP", PsaParameterType.CylinderCoilChargeTime1);
            Add("BOBINE2_6LP", PsaParameterType.CylinderCoilChargeTime2);
            Add("BOBINE3_6LP", PsaParameterType.CylinderCoilChargeTime3);
            Add("BOBINE4_6LP", PsaParameterType.CylinderCoilChargeTime4);
            Add("AVANCEALL_6LP", PsaParameterType.IgnitionAdvance);
            Add("BOBINE14_6LP", PsaParameterType.CylinderCoilChargeTime14);
            Add("BOBINE23_6LP", PsaParameterType.CylinderCoilChargeTime23);
            Add("TENSIONSONDEAMONT_6LP", PsaParameterType.UpperOxygenSensorVoltage);
            Add("TENSIONSONDEAVAL_6LP", PsaParameterType.LowerOxygenVoltage);
            Add("TPSINJECTION_6LP", PsaParameterType.InjectionTime);
            Add("TENSIONBATT_6LP", PsaParameterType.BatteryVoltage);
            Add("REGIMEMOTEUR_6LP", PsaParameterType.EngineRpm);
            Add("TEMPERATUREEAU_6LP", PsaParameterType.EngineCoolantTemperature);
            Add("RCOCANISTER_6LP", PsaParameterType.CanisterElectrovalveOCR);
            Add("PRESSIONTUBULURE_6LP", PsaParameterType.ManifoldPressure);
            Add("POSITIONPAPILLON_6LP", PsaParameterType.ThrottlePosition);
            Add("ANGLEPAP_6LP", PsaParameterType.ThrottleAngle);
            Add("ETATCANISTER_6LP", PsaParameterType.CanisterSolenoidValveStatus);
            Add("NB", PsaParameterType.CalculatorResetsCount); // Not sure

            Add("SIGNA", PsaParameterType.TelecodingToolSignature); // Telecoding tool signature
            Add("DATE_2", PsaParameterType.TelecodingDate); // Telecoding date

            Add("ETIQRECONNUE", PsaParameterType.TransponderLabelRecognized);
            Add("ETIQTRANSPOK", PsaParameterType.TransponderLabelRecognized);
            Add("ETIRECONNUE", PsaParameterType.TransponderLabelRecognized);

            Add("CODE_VIN", PsaParameterType.Vin);
            Add("CONFIG_NUM_SERIE_VEHICULE_CODE_VIN", PsaParameterType.Vin);

            Add("NIVDELEST7", PsaParameterType.NIVDELEST7);
            Add("NIVDELEST8", PsaParameterType.NIVDELEST8);

            Add("PEDALEMBRAPP", PsaParameterType.ClutchPedalPosition);

            Add("CONTACTFREIN", PsaParameterType.MainBrakeSignal);
            Add("FREINPRINCIPAL_EDC15", PsaParameterType.MainBrakeSignal);

            Add("CONTACTFREINREDON", PsaParameterType.RedundantBrakeSignal);
            Add("MP_KILOM_TOTAL", PsaParameterType.AverageJourneyConsumption);
            Add(PsaParameterType.GearboxHandlePosition, 
                "MP_POSITION_CLE_DEM", 
                "MP_POSITION_CLE_VU_PAR_BSM", 
                "Starter_Switch");
            Add(PsaParameterType.InstantFuelConsumption, 
                "MP_CONSOMMATION_INSTANTANEE",
                "Instant_Fuel_Consumption");

            Add("CMDESSUYAGEAV", PsaParameterType.WindscreenWiperControl);
            Add(PsaParameterType.CoolantLevelWarning, 
                "ALERTEEAUNIV", 
                "ALERTNIVEAUMOT", 
                "MP_ALERTE_NIV_EAU_MOT");
            Add(PsaParameterType.EngineCoolantTemperature, "CA_TEMPERATURE_EAU");
            Add(PsaParameterType.LowBrakeFluidWarning, "Brake_Fluid_Switch", "MP_ALERTE_NIVEAU_LIQUIDE_FREIN_MINI");
            Add(PsaParameterType.LowScreenWashLevelDisplay, "MP_NIVEAU_LAVE_GLACE_MINI");
            Add(PsaParameterType.EspDeactivationRequest, "MP_CDE_VOYANT_ESP");
            Add(PsaParameterType.AutomaticLightingMode, "MODECLAUTO", "MP_ECL_SIGN_LIDC5_42", "MP_MODE_ECL_AUTO");
            Add(PsaParameterType.SidelampsControl, "MP_DEMANDE_LANTERNES");
            Add(PsaParameterType.RightHandVehicleAndTrailerSidelampsControl, "CDEFEUXPOSD", "MP_COMMANDE_LANTERNES_DROITES");
            Add(PsaParameterType.LeftHandVehicleAndTrailerSidelampsControl, "CDEFEUXPOSG", "MP_COMMANDE_LANTERNES_GAUCHES");
            Add(PsaParameterType.FrontLight, "MP_DEMANDE_CODES");
            Add(PsaParameterType.FrontLight, "CMDCODES", "ECL_SIGN_LIDC5_20", "Head_Lamp_LO_ON_Duty", "MP_CDE_CODE");
            Add(PsaParameterType.OilPressureWarning, "ALERTEPRESSIONHUILE", "ALERTPRESHUILE", "MP_ALERTE_PRESSION_HUILE");
            Add(PsaParameterType.CoolantLevelWarning, "ALERTEEAUNIV", "ALERTNIVEAUMOT", "MP_ALERTE_NIV_EAU_MOT");
            Add(PsaParameterType.MeasuredOilLevel, "MP_INSTR_MESURES_LIDC9_28", "MP_NIVEAU_HUILE_MESURE", "MP_NIVEAU_HUILE_MOTEUR_MESURE", "MP_NIVEAU_HUILE_MOTEUR_MESURE_CB", "MP_NIV_HUILE_MESURE", "NIVHUILEMESURE", "NIVOHUILE");
            Add(PsaParameterType.FilteredOilLevel, "MP_NIVEAU_HUILE_APRES_FILTRAGE");
            Add(PsaParameterType.MeasuredFuelLevel, "MP_INSTR_MESURES_LIDC9_30", "MP_NIV_CARB_MESURE", "NIVCARBMESURE", "NIVCARBUMESURE");
            Add(PsaParameterType.DisplayedFuelLevel, "MP_INSTR_MESURES_LIDC9_44", "MP_NIV_CARB_AFFICHE", "NIVCARBAFFICHE", "NIVCARBUAFFIC");
            Add(PsaParameterType.OilLevelWarning, "ALNIVHIULEMINI");
            Add(PsaParameterType.AverageFuelConsumption, "MP_INSTR_OBD_LIDCA_8", "MP_KILOM_TOTAL");
            Add(PsaParameterType.GearboxHandlePositionRegisteredBySMD, "MP_LIDD2_1", "MP_POSITION_LEVIER_BVA_BVMP");
            Add(PsaParameterType.FuelSenderImpedance, "IMPEDJAUGECARBU", "MP_IMPEDANCE_JAUGE_CARB", "MP_INSTR_MESURES_LIDC9_48");
            Add(PsaParameterType.WaterInDiesel, "MP_EAU_GAZOLE", "VA_TENSCAPPRESEAUGAZ");
            Add(PsaParameterType.WaterInGazolineDetected, "MP_AFFICHAGE_EAU_DANS_GASOIL");
            Add(PsaParameterType.MileageBeforeService, "KMAVANTMAINT", "MPKMAVMAINT", "MP_INSTR_MESURES_LIDC9_46", "MP_KILOM_AVANT_MAINT");
            Add(PsaParameterType.DaysLeftTillService, "MP_JOUR_RESTANT_AVANT_MAINTENANCE");
            Add(PsaParameterType.MileageCorrespondingToLastReset, "MP_KM_DERNIERE_RAZ");
            Add(PsaParameterType.PeriodAfterLastReset, "MP_DUREE_DEPUIS_DERNIERE_RAZ");
            Add(PsaParameterType.MileageResetAtFirstPostDeliverService, "MP_RAZ_2_EFFECTUEE");
            Add(PsaParameterType.ExteriorAirTemperature, "AMBIENCE_TEMP_TA", "MPTEMPEXT", "MP_TEMP_AIR_EXT", "MP_TEMP_EXT", "TEMPERATUREEXT", "TEMPEXT", "TEMP_EXT", "Temperatura_Esterna", "VA_TEMPEXT");
            Add(PsaParameterType.BatteryVoltage, "Battery_voltage", "CA_TENSION_BATTERIE", "MP_ALIM_LIDC0_21", "MP_TENSION_BAT", "MP_TENSION_BATTERIE", "MP_TENS_BAT", "MP_VAL_TENSION_BAT", "TENSBAT", "TENSIONBAT", "TENSIONBATT", "TENSIONBATTERIE", "Tension_Batterie", "VA_TENSION_BAT", "VA_TENSION_BATTERIE", "Voltage", "Voltage_sensing_of_IOD_Line");
            Add(PsaParameterType.AlternatorExcitationVoltage, "MP_TENSION_EXCITATION_ALTERNATEUR");
            Add(PsaParameterType.PlusAfterContactAPC, "MP_PRESENCE_PLUS_APC");
            Add(PsaParameterType.EngineStartCommand, "MP_DMD_DEM_CMM");
            Add(PsaParameterType.EngineState, "MP_ETAT_GMP");
            Add(PsaParameterType.MainElectricSystemState, "MP_ETAT_PRINCIPAL");
            Add(PsaParameterType.BsiMode, "MP_CONFIG_SEV");
            Add(PsaParameterType.EconomyModeStatus, "MP_ETAT_MODE_ECO");
            Add(PsaParameterType.LifePhaseMode, "MP_MODE_PHASE_VIE");
            Add(PsaParameterType.AirPumpControl, "MP_CDE_POMPE_AIR");
            Add(PsaParameterType.BsiMemoryStateAfterTelecoding, "MP_ETAT_ZONE_MEMOIRE_SUITE_TELECHARGEMENT");
            Add(PsaParameterType.BsiSoftwareResetsCount, "MP_NBR_RESET_BSI");
            Add(PsaParameterType.CalculatorResetsCount, "MP_COMPTEUR_RESET_HARD");
            Add(PsaParameterType.SoftwareVersion, "ID_VERSION_LOGICIEL", "ID_VERSION_SOFT", "LIN_Software_Version_Sunroof", "VERSION");
            Add(PsaParameterType.DiagnosticsVersionIndex, "ID_APPLI_VERSION", "ID_APPLI_VERSION_FE", "ID_INDICE_EVOLUTION_SOFT", "ID_VERS_LOGICIEL");
            Add(PsaParameterType.DiagnosticsVersionIndex, "DiagnosticVersion", "ID_VERSION_DIAGNOSTIC");
            Add(PsaParameterType.SoftwareVersion, "EDITION", "EDITION_SOFT", "ID_EDITION_LOGICIEL", "ID_EDITION_SOFT", "ID_INDICE_EVOLUTION_EDITION", "ID_VERSION_LOGICIEL", "MP_SOFT_EDITION");
            Add(PsaParameterType.AirConditioningAuthorisation, "SORTIECOMPRESS");
            Add(PsaParameterType.AverageJourneyConsumption, "CONSOMOYTRAJ", "CONSOMOYTRAJET");
            Add(PsaParameterType.PsaReference, "ID_NUM_COMP2", "ID_REF_PSA", "ID_REF_PSA_new_3", "NUM_COMPL2", "REFHARD", "REF_PSA");
            Add(PsaParameterType.TelecodingsCount, "NBTELECODAGE", "NUM");
            Add(PsaParameterType.MeasuredOilLevel, "B2CAFFFF01", "B328FFFF01", "B333FFFF01", "COMBINIVHUIL", "COMBI_NIVHUILE", "COMBI_NIVO_HUILE", "COMB_LIDB3", "NIVEAUHUILE", "NIVHUILE");
            Add(PsaParameterType.StateOfLearningOfThrottleStops, "MP_ETAT_APPRENTISSAGE_PAPILLON_MOTORISE");
            Add(PsaParameterType.StateOfLearnStopValve, "APPRENTISSAGEVANNEEGR", "MP_ETAT_APPRENTISSAGE_EV_EGR");
            Add(PsaParameterType.ConditionerCoolingCircuitPressure, "MP_PRESSION_CIRCUIT_REFRIGERANT", "MP_PRESSION_CIRCUIT_REFRIGERATION", "MP_PRES_CIRCUIT_REFRIGERATION", "PRESSIONCIRREF", "Pression_du_circuit_refrigerant");
            Add(PsaParameterType.HigherOxygenSensorState, "ETATSONDEAMONT", "ETAT_SONDE_1_AMONT", "MP_ETAT_SONDE_AMONT", "MP_ETAT_SONDE_AMONT_SUP", "MP_ETAT_SONDE_OXYGENE_AMONT");
            Add(PsaParameterType.CylinderCoilChargeTime4, "BOBINE4", "MP_TEMPS_CHARGE_BOBINE_4");
            Add(PsaParameterType.CylinderCoilChargeTime3, "BOBINE3", "MP_TEMPS_CHARGE_BOBINE_3");
            Add(PsaParameterType.CylinderCoilChargeTime2, "BOBINE2", "MP_TEMPS_CHARGE_BOBINE_2");
            Add(PsaParameterType.CylinderCoilChargeTime1, "BOBINE1", "MP_TEMPS_CHARGE_BOBINE_1");
            Add(PsaParameterType.CanisterElectrovalveOCR, "MP_RCO_EV_CANISTER", "RCOCANISTER", "RCOVANNEEGR");
            Add(PsaParameterType.LowBatteryVoltageAlert, "MP_AFFICHAGE_DEFAUT_CHARGE_BATTERIE");
            Add(PsaParameterType.DippedBeamHighBeamHeadlampsInversion, "MP_INVERSION_CODES_PHARES");
            Add(PsaParameterType.HighBeamImpulseSwitch, "MP_DEMANDE_APPEL_AUTO", "OPT_HORN_RQ");
            Add(PsaParameterType.FrontFogLampsControl, "MP_CDE_ANTIBROUILLARD_AVANT");
            Add(PsaParameterType.PlusAfterContactAPC, "MP_CMD_APC_BSM");
            Add(PsaParameterType.FuelPumpSupplyCutOff, "MP_CDE_COUPURE_POMPE_CARB");
            Add(PsaParameterType.PsaReference, "ID_REF_PSA");
            Add(PsaParameterType.DiagnosticsVersionIndex, "ID_INDICE_EVOLUTION_EDITION");
            Add(PsaParameterType.SoftwareVersion, "CMPLFOURN", "EDITIONSOFT2", "FILE", "ID_NUMERO_FICHIER_ULP_CAL", "ID_NUM_COMP", "ID_NUM_COMPLEMENTAIRE", "ID_REF_LOG", "ID_REF_LOGICIEL", "ID_REF_SOFT", "ID_VERSION_LOGICIEL", "MP_REF_LOGICIEL", "NUM_COMPL", "REFERENCE_LOG", "REFERENCE_SOFT", "REF_LOG", "REF_LOG2", "REF_LOGICIEL", "REF_SOFT", "REF_SOFT_ULP", "SIGNTELECHARGE");
            Add(PsaParameterType.FanAssemblyControl, "COMGMV", "GMV");
            Add(PsaParameterType.SportSuspensionRequest, "ETATDEMSUSPSPORT", "MP_D_SUSPENSION_SPORT");
            Add(PsaParameterType.EngineStartCommand, "ETATMODEECO", "MODEUSINE", "MP_ALIM_LIDC0_12");
            Add(PsaParameterType.CeilingLightingRequest, "DEMECLAPLAFON", "DEMECLPLAF", "DEMFINPLAF", "ETATDEMECLAIPLAFON", "MP_ECL_SIGN_LIDC5_28");
            Add(PsaParameterType.RearFogLampsControl, "B2B2FFFF01", "COMANTIBROUAR", "LIDB2_178");
            Add(PsaParameterType.VolumetricBreakinDetection, "ETATDEMEJECTVOLUM", "MP_FERM_ALR_LIDC6_6", "NTRPROTVOLUM");
            Add(PsaParameterType.EngineOilTemperature, "MP_TEMP_HUILE_MOT", "TEMPERATUREHUILE", "TEMPHUILEMOT");
            Add(PsaParameterType.WaterInGazolineDetected, "EAUDANSGAZOLE", "EAUGAZOLE");
            Add(PsaParameterType.ExteriorAirTemperature, "CA_TEMPERATURE_AIR_EXTERIEUR", "MP_INSTR_MESURES_LIDC9_35", "MP_TEMPERATURE_AIR_EXTERIEUR", "MP_TEMPERATURE_AIR_EXTERIEUR_C2", "MP_TEMPERATURE_AIR_EXTERIEUR_CB", "MP_TEMP_AIR_EXTERN", "TEMPAIREXT", "TEMPAIRMESURE", "TEMPERATUREAIREXT", "TEMPERATUREEXTER", "VA_TEMPERATURE_AIR_EXTERIEUR");
            Add(PsaParameterType.RearWiperParkedPositionInformation, "ETATINFOAFESSVITAV", "INFARRFIXESSUYAV", "VISIB_LIDCD_27");
            Add(PsaParameterType.SlowSpeedWindscreenWiperControl, "ETATDEMESSPV", "MP_DEMANDE_ESSUYAGE_PARE_BRISE_PV");
            Add(PsaParameterType.WindscreenWiperControlAtHighSpeed, "ETATDEMESSGV", "MP_DEMANDE_ESSUYAGE_PARE_BRISE_GV");
            Add(PsaParameterType.ProductionDate, "DATEFAB", "DATEFABSOFT", "DATE_FAB", "ID_DATE_FAB", "ID_DATE_FABRIC", "ID_DATE_TELECH", "MP_DATE", "MP_DATE_FAB", "MP_DATE_FABRICATION", "MP_HISTO_ANNEE_FAB", "MP_JOUR_FAB_MP_MOIS_FAB_MP_ANNEE_FAB");
            Add(PsaParameterType.Vin, "NUM_SERIE_VIN");
            Add(PsaParameterType.SoftwareVersion, "CODEDIT", "CODEDIT03", "EDITIONSOFT2", "ID_REF_FIAT", "ID_SOFT", "ID_VERSION_LOGICIEL", "VERSIONSOFT", "VERSION_LOG");
            Add(PsaParameterType.RadioSearchButtonPlus, "RADIOFREQP");
            Add(PsaParameterType.RadioSearchButtonMinus, "RADIOFREQM");
            Add(PsaParameterType.RadioVolumeButtonPlus, "RADIOVOLUMEP");
            Add(PsaParameterType.RadioVolumeButtonMinus, "RADIOVOLUMEM");
            Add(PsaParameterType.SourceToTelephoneSwitch, "RADIOMODETEL");
            Add(PsaParameterType.PredefinedRadioStationsSelection, "RADIOCHOIX");
            Add(PsaParameterType.BonnetOpen, "CAPOT", "CAPOTOUVERT");
            Add(PsaParameterType.LockingLocksCommand, "CMDCONDA", "FERM_CONDA_LIDC8_5");
            Add(PsaParameterType.UnlockingLocksCommand, "CMDDECONDA", "FERM_CONDA_LIDC8_6");
            Add(PsaParameterType.Deadlockingcommand, "CMDSUPERCONDA", "SUPERCONDA");
            Add(PsaParameterType.BootOpeningRequest, "CPOPORTEARD", "MP_CPO_PORTE_ARD");
            Add(PsaParameterType.RearRightHandDoorSwitch, "CPOPORTEAVD", "MP_CPO_PORTE_AVD");
            Add(PsaParameterType.RearLeftHandDoorSwitch, "CPOPORTEAVG", "MP_CPO_PORTE_AVG");
            Add(PsaParameterType.BootOpeningRequest, "DEMOUVRECOFFRE", "ETATDEMOUVCOFF", "MP_FERM_CONDA_LIDC8_17");
            Add(PsaParameterType.CentralLockButtonState, "ETATCONDA", "LIDC8_PAR38");
            Add(PsaParameterType.FrontRightHandLockUnlockingInformation, "ENTE2V2SERAVD");
            Add(PsaParameterType.FrontLeftHandLockUnlockingInformation, "ENTE2V2SERAVG");
            Add(PsaParameterType.CoolantLevelWarning, "ALERTEEAUNIV");
            Add(PsaParameterType.CoolantLevelWarning, "ALERTETEMPEAU", "ALERTETEMPEAUMOTEUR");
            Add(PsaParameterType.LowScreenWashLevelDisplay, "NIVLAVEGLACEMINI");
            Add(PsaParameterType.LowScreenWashLevelDisplay, "MP_INSTR_MESURES_LIDC9_34", "RAPPELMESSNAV");
            Add(PsaParameterType.EngineCoolantTemperature, "TEMPHUILEMOTEUR");
            Add(PsaParameterType.GrossFuelLevel, "MP_INSTR_MESURES_LIDC9_43", "MP_NIV_CARB_LISSE", "NIVCARBULISSE");
            Add(PsaParameterType.MeasuredFanSpeed, "MP_RELAIS_GMV", "MP_REL_GMV", "MP_VIT_GMV", "RELAISGMV", "RELAISGMV2");
            Add(PsaParameterType.SpeedVent, "CONSIGNEGMV");
            Add(PsaParameterType.CylinderCoilChargeTime23, "BOBINE23", "TEMPSDECHARGEBOBINE2SUR3", "TEMPSDECHARGEBOBINE2sur3");
            Add(PsaParameterType.CylinderCoilChargeTime14, "BOBINE14", "TEMPSDECHARGEBOBINE1SUR4", "TEMPSDECHARGEBOBINE1sur4");
            Add(PsaParameterType.ThrottlePositionVoltage, "MP_TENSION_PAPILLON", "MP_TENSION_POSITION_PAPILLON", "TENSIONPAPILLON");
            Add(PsaParameterType.InjectionOffWhenSlowdown, "COUPUREINJECTION", "COUPURE_DECELERATION", "MP_COUPURE_INJECTION");
            Add(PsaParameterType.SpeedLimitationOrRegulation_PauseState, "REGULPAUSE");
            Add(PsaParameterType.SpeedLimitationOrRegulation_SpeedRegulationMinus, "REGULVITM");
            Add(PsaParameterType.SpeedLimitationOrRegulation_SpeedRegulationPlus, "REGULVITP");
            Add(PsaParameterType.SpeedLimitationOrRegulation_ActivationState, "REGULATEUR_VITESSE", "REGULONOFF");
            Add(PsaParameterType.BsiEmpty, "BSIVIERGE");
            Add(PsaParameterType.BsiAntiScan, "BSIANTISCAN", "MP_BSI_ANTI_SCANNING", "MP_FERM_AD_LIDC7_28");
            Add(PsaParameterType.SlowSpeedWindscreenWiperControl, "CMDESSUYAVGV");
            Add(PsaParameterType.RearScreenWiperControl, "CMDESSUYAGEAR");
            Add(PsaParameterType.DeicingControl, "CMDDEGIVRAGE");
            Add(PsaParameterType.ReportedGearboxGearRatio, "MP_RAPPORT_BOITE", "RAPPORT", "RAPPORTBV");

            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
            //Add("", PsaParameterType.);
        }
    }
}
