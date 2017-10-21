using System;

namespace VTS.Shared.DomainObjects
{
       public enum PsaParameterType
        {
            /// <summary>
            /// Engine mode, RPM
            /// </summary>
            EngineRpm,

            /// <summary>
            /// Fuel system pressure (for diesel)
            /// </summary>
            FuelSystemPressure,

            /// <summary>
            /// Difference between current and nominal 
            /// </summary>
            FuelSystemPressureDelta,

            /// <summary>
            /// A set of injector corrections for diesel engines.
            /// </summary>
            Injector1Correction,
            Injector2Correction,
            Injector3Correction,
            Injector4Correction,

            /// <summary>
            /// Синхронизация кулачкового и коленчатого валов, bool
            /// </summary>
            CamshaftCrankshaftSync,

            /// <summary>
            /// @P35639-CITACT
            /// Циклическое соотношение открытия регулятора давления топлива
            /// </summary>
            DieselPressureRegulatorRatio,

            /// <summary>
            /// P7014-CITACT
            /// Управление объёмом впрыска
            /// </summary>
            InjectionVolumeSetting,

            /// <summary>
            /// @P10260-CITACT@\*1@\*4, 
            /// напряжение пар инжекторов
            /// </summary>
            VoltageInjector1and4,
            VoltageInjector2and3,

            /// <summary>
            /// Напряжение(?) инжекторов
            /// </summary>
            InjectorsVoltage,

            /// <summary>
            /// DEBITAIR
            /// MP_DEBIT_AIR_MESURE
            /// Замерянный объём воздуха
            /// </summary>
            MeasuredAirVolume,

            /// <summary>
            /// Циклическое соотношение открытия электроклапана 
            /// системы рециркуляции выхлопных газов
            /// RCO EGR
            /// </summary>
            ExhaustCycleRatio,

            /// <summary>
            /// @P35642-CITACT
            /// Сила тока регулятора давления топлива
            /// </summary>
            FuelRegulatorCurrent,

            /// <summary>
            /// @P2763-CITACT
            /// Опережение предварительного впрыска
            /// </summary>
            InjectionAdvance,

            /// <summary>
            /// @P2754-CITACT
            /// Основное опережение системы впрыска
            /// </summary>
            BasicInjectionAdvance,

            /// <summary>
            /// @P10261-CITACT@\+@P24379-CITACT
            /// Отключение 3-его поршня (В случае наличия)
            /// </summary>
            ThirdCylOff,

            /// <summary>
            /// @P10533-CITACT
            /// Давление турбокомпрессора
            /// </summary>
            TurbinePressure,

            /// <summary>
            /// Номинальное давление в турбокомпрессоре
            /// @P10279-CITACT
            /// </summary>
            NominalTurbinePressure,

            /// <summary>
            /// @P35643-CITACT
            /// Циклическое соотношение открытия электроклапана Давления турбины
            /// </summary>
            TurbineCycleRatio,

            /// <summary>
            /// @P26901-CITACT
            /// Атмосферное давление
            /// </summary>
            AtmosphericPressure,

            /// <summary>
            /// @P6066-CITACT
            /// Температура воды
            /// </summary>
            /// TODO : Deprecate it
            WaterTemperature,

            /// <summary>
            /// @P36092-CITACT
            /// </summary>
            AirTemperature,

            /// <summary>
            /// @P5199-CITACT
            /// </summary>
            FuelTemperature,

            /// <summary>
            /// @P35641-CITACT
            /// Циклическое соотношение 
            /// открытия заслонки системы рециркуляции выхлопных газов
            /// ??? ExhaustCycleRatio
            /// </summary>
            ExhaustRecirculationGateCycle,

            BatteryVoltage,
            Sensor1Voltage,
            Sensor2Voltage,

            /// <summary>
            /// Реле предварительного пост - обогрева
            /// </summary>
            PreHeatRelay,

            /// <summary>
            /// Запрос на отключение системы кондиционирования
            /// </summary>
            ConditioningCutOffRequest,

            /// <summary>
            /// Реле низкой скорости группы мотовентилятора
            /// </summary>
            RelayLowSpeedVent,

            /// <summary>
            /// Скорость группы мотовентилятора
            /// RequesteedFanGmvSpeed
            /// </summary>
            SpeedVent,

            /// <summary>
            /// @P1543-CITACT@\*
            /// </summary>
            InjectionTime,

            /// <summary>
            /// @P4896-CITACT
            /// </summary>
            InjectionOffWhenSlowdown,

            /// <summary>
            /// Напряжение нижнего кислородного зонда
            /// </summary>
            LowerOxygenVoltage,

            /// <summary>
            /// напряжение верхнего кислородного зонда
            /// </summary>
            UpperOxygenSensorVoltage,
            MixRegState,

            /// <summary>
            /// NOT SUPPORTED TYPE
            /// </summary>
            Unsupported = 39,

           /// <summary>
           /// FAP-related
           /// </summary>
            ParticleFilterStatus,
            RegenerationAssistanceRequest,
            CatConverterUpstreamTemperature,
            CatConverterDownstreamTemperature,
           /// <summary>
           /// FAP-related
           /// </summary>
            ParticleFilterInletOutletPressureDifference,
            EngineCoolantTemperature,

            /// <summary>
            /// DEBITVOLUMIQUEAIR
            /// </summary>
            AirFlowVolume, // TODO : possibly it's the same as MeasuredAirVolume

            InletAirHeaterThrottleOpenCycleRatio,
            PostInjectionFlow,
            PostInjectionPhasing,

           /// <summary>
           /// think it's @P35748-CITACT, but this CITACT is about Volume.
            /// Amount of additive remaining in the tank
           /// </summary>
            TotalFuelAdditiveQuantity,
            DistanceTravelledSinceLastRegeneration,
            AverageDistanceOfLast5Regenerations,
            ActivationOfElectricalConsumers,

            VehicleSpeed,
            AcceleratorPedalPosition,
            GearboxGear,
            AutomaticGearboxRecognition,
            MainBrakeSignal,
           /// <summary>
           /// Дополнительный контакт педали тормоза
           /// </summary>
            RedundantBrakeSignal,
            ClutchPedalSignal,
            AcceleratorPedalHardSpotSignal1,
            AcceleratorPedalHardSpotSignal2,

            ThrottleStatus,
            ThrottlePositionVoltage,
            ThrottleAngle,
            ManifoldPressure,
            CanisterSolenoidValveStatus,
            CanisterElectrovalveOCR,
            IgnitionAdvance,

            CylinderCoilChargeTime14,
            CylinderCoilChargeTime23,

            HigherOxygenSensorState,
            LowerOxygenSensorState,

            CatalyticConverterCheck,
            FanAssemblyLowSpeedControl,
            FanAssemblyHighSpeedControl,

            /// <summary>
            /// Относительная скорость вращения вентилятора охлаждения двигателя
            /// </summary>
            /// Deprecated, use MeasuredFanSpeed
            Deprecated4,

            /// <summary>
            /// Разрешение на включение кондиционера
            /// </summary>
            AirConditioningAuthorisation,
            AirConditioningRequestInput,

            EngineTorque,

            /// <summary>
            /// Управление реле питания
            /// </summary>
            SupplyRelayControl,
            ECULocking,
            CodedEngineImmobiliserProgrammingStatus,
            ProblemsDetectedWhenTransmittingTheUnlockCode,
            EspEcuSupplyVoltage,
            FrontLeftWheelSpeed,
            FrontRightWheelSpeed,
            RearLeftWheelSpeed,
            RearRightWheelSpeed,

            BrakeFluidLevel,
            BrakeLiningsWear,

            SpeedElectropumpAssemblyMotor,
            SteeringWheelSpeed,
            SteeringWheelRotationDirection,
            EngineOilTemperature,
            ElectropumpAssemblyMotorCurrent,
            ElectropumpAssemblyMotorTerminalsVoltage,
            EngineRunningInformation,

            BrightnessButton,
            OdometerResetButton,

            TripometerDisplayUnits,
            MaintenanceIndicator,
            OilLevelFunction,
            Gearbox,
            RearElectricWindowSwitchesSupply,
            PowerSupplyOfRainSensor,
            Deprecated5, // Deprecated, use EngineRunningInformation instead.
            RearElectricWindowSwitchesDeactivation,
            BodyworkVanPlusStatus,
            ComfortVanPlusStatus,
            EconomyModeStatus,
            FactoryModeStatus,
            PresenceOfPlusAccessories,
            PresenceOfPlusIgnition,
            AlternatorExcitationVoltage,
            FuelPumpSupplyCutOff,
            FuelPumpRelayOutputStatus,
            AirPumpControl,
            DieselWaterAirPumpRelayOutputStatus,

            SmoothedConsumption,
            TotalMileage,
            AverageFuelConsumption,
            AverageJourneyConsumption,
           /// <summary>
           /// Fan aka GMV 
           /// </summary>
            FanRelayOutput,
            AirConditioningCompressorRelayOutput,
            EvaporatorIcingUpSafetyStatus,
            EvaporatorSensorTemperature,
            FanMidSpeedControl,
            AirConditioningCompressorControl,

           /// <summary>
            /// Deprecated, use FanUnitReferenceSpeed
           /// </summary>
            Deprecated6,
            EspDeactivationRequest,
            AutomaticGearboxSnowProgramRequest,
            ParkingBrake,
            BrakePedal,
            ReverseGearInfo,
            DriverSeatBeltFastened,
            PassengerSeatBeltFastened,
            AutomaticGearboxInParkPosition,

            FrontLhWheelTyreDeflationDetectionSensorStatus,
            FrontRhWheelTyreDeflationDetectionSensorStatus,
            RearRhWheelTyreDeflationDetectionSensorStatus,
            RearLhWheelTyreDeflationDetectionSensorStatus,
            ValidityOfTyreDeflationSensorStatusParameters,
            DeflationDetectionFrontLhTyrePressure,
            DeflationDetectionFrontRhTyrePressure,
            DeflationDetectionRearRhTyrePressure,
            DeflationDetectionRearLhTyrePressure,
            FrontLhTyrePressureStatus,
            FrontRhTyrePressureStatus,
            RearRhTyrePressureStatus,
            RearLhTyrePressureStatus,

            FrontSillLightingControl,
            RearFogLampsControl,
            RightHandIndicatorsControl,
            LeftHandIndicatorsControl,
            BootLightingCommand,
            InteriorLampControl,
            HornControl,
            DippedBeamControl, // ближний свет
            MainBeamControl, // дальний свет
            InteriorLampRequest,
            AutomaticLightingMode,
            HeadlampBeamCorrectorFault,
            InteriorLampControlRelayOutput,
            BootLampControlRelayOutput,
            RhAndLhIndicatorRelayOutput,
            SidelampsControl,

            VolumetricBreakinDetection,
            VolumetricProtectionDeactivation,

            BonnetLock,
            BootLock,
            LockLockingControl,
            LockUnlockingControl,
            DeadlockingControl,
            BootLockingControl,
            RearRightHandDoorSwitch,
            RearLeftHandDoorSwitch,
            BootOpeningRequest,
            LocksLockingRelayOutput,
            LocksUnlockingRelayOutput,
            LocksDeadlockingRelayOutput,
            BootOpeningRelayOutput,
            FrontRightHandLockUnlockingInformation,
            FrontLeftHandLockUnlockingInformation,
            ChildSafetyActivationControl,
            ChildSafetyDeactivationControl,

            CoolantLevelWarning,
            OilPressureWarning,
            WaterInDiesel,
            MeasuredOilLevel,
            GrossFuelLevel,
            ExteriorAirTemperature,

           /// <summary>
           /// Deprecated. Use EngineOilTemperature
           /// </summary>
            Deprecated1,

            FilteredFuelLevel,
            DisplayedFuelLevel,
            NumberOfServicesPerformed,
            FuelSenderImpedance,
            LowBrakeFluidWarning,
            SunlightSensorDuskValue,

            VehicleProtectionStatusLedControl,
            LowScreenWashLevelDisplay,

            RearRhWindowRaiseControl,
            RearLhWindowRaiseControl,
            RearRhWindowLowerControl,
            RearLhWindowLowerControl,

            RearScreenWiperControl,
            WindscreenWiperControlAtHighSpeed,
            SlowSpeedWindscreenWiperControl,
            WindscreenWiperControl,
            RearScreenWashControl,
            WindscreenWasherControl,
            HeadlampWasherControl,
            RearWiperParkedPositionInformation,
            WindscreenWiperParkedPositionInformation,

            HeatedRearScreenPushButton,
            HazardWarningLampPushButton,
            AirConditioningPushButton,
            ControlPanelBrightnessLevel,
            SuspensionControlButton,

            AirTurbineSpeed,
            MeteringPumpSpeed,
            SparkPlugSpeed,

            AdditionalHeatingStatus,
            HeatingStatus,
            FlamePresence,
            IncandescentElementStatus,
            FuelPumpStatus,
            AirTurbine,
            WaterPump,

            NoLoadShedding,
            PreheaterPlugLoadShedding,
            ForcedFanUnitMidSpeedLoadShedding,
            ForcedFanUnitLowSpeedLoadShedding,
            ForcedDeicingLoadShedding,
            DeicingLoadShedding,
            PartialHeatingLoadShedding,
            CompleteHeatingLoadShedding,
            AirConditioningBlowerLoadShedding,
            AirConditioningCompressorLoadShedding,

            ReservoirCap,
            PlusVanSupply,

            LeftBlownAirTemperature,
            RightBlownAirTemperature,
            BlowerCopyVoltage,
            VoltageValueOnBlower,

            MixingStatusLeft,
            MixingStatusRight, // see also MixingShutterStatus
           /// <summary>
           /// ETATDISTRIBUTION
           /// </summary>
            DistributionStatus,
            RecirculationStatus,

            FanAssemblyControl,
            ConditionerCoolingCircuitPressure,

            VehicleSpeedSignalPresence,
            SteeringWheelRotationAngleDataPresence,
            SteeringElectropumpTemperature,

            CylinderCoilChargeTime1,
            CylinderCoilChargeTime2,
            CylinderCoilChargeTime3,
            CylinderCoilChargeTime4,

            LowFapAdditiveLevel,
            TotalAdditiveAmountDepositedCounter1,
            TotalAdditiveAmountDepositedCounter2,
           
            /// <summary>
            /// @P107329-THESAU@\*
            /// </summary>
            QualityOfRadioSignal,

            /// <summary>
            /// @P90267-THESAU
            /// </summary>
            LevelOfRadioSignal, 

            /// <summary>
            /// @P128677-THESAU
            /// </summary>
            MixRegStateInput,

            /// <summary>
            /// @P128678-THESAU
            /// </summary>
            MixRegStateOutput,

            RadioSearchButtonPlus,
            RadioSearchButtonMinus,
            RadioVolumeButtonPlus, 
            RadioVolumeButtonMinus,

            Deprecated2, // deprecated, use AutomaticGearboxSnowProgramRequest
            RequestForBVAProgramSport,
            RequestForBVAProgramSportState, 

            AudioButton,

            VinCodeVerification,

            UsageGeographicalZone,

            FaderFunction,

            /// <summary>
            /// Диапазон частот AM
            /// </summary>
            FrequencyBandAM, 

            VolumeLevelDependencyFromVehicleSpeed,

            /// <summary>
            /// Состояние активации вспомогательного аудиовхода
            /// </summary>
            AuxiliaryAudioInputActivationState,

            VolumeLevelCorrectionAlgorythm,

            /// <summary>
            /// Кривая чувствительности LO/DX
            /// </summary>
            SensibilityCurveLoDx,

            /// <summary>
            /// Конфигурация таблицы освещения. 
            /// MP_TABLECLAIR_MPM1 // Configuration table d'йclairage
            /// </summary>
            LightingTableConfiguration,

            /// <summary>
            /// Состояние выключателя рамки нагревателя.
            /// </summary>
            HeatingFramePushState, 

            /// <summary>
            /// MP_ETAT_PUSH_WARNING // EntrйePushWarning
            /// </summary>
            AlarmButtonState,

            /// <summary>
            /// NIVLUMINOCOMBINE !!OR!! P_NIV_RHEOSTAT // NiveauDeLuminositйRhйostat
            /// </summary>
            BrightnessLevelRheostat,

            /// <summary>
            /// // MP_ETAT_CONDAMNATION_CENTRALISEE // EntrйePushCondamnationCentralisйe
            /// </summary>
            CentralLockButtonState,

            /// <summary>
            /// CDEBOUTONCDS // Etat du bouton йjection du disque
            /// </summary>
            CdEjectButtonState,

            /// <summary>
            /// TOUCHREGLLUMPLUS // Etat du contacteur luminositй Plus
            /// </summary>
            BrightnessPlusSwitchState, 

            /// <summary>
            /// REF_PSA_CALC_DECALE
            /// </summary>
            PsaCalcDecale, 

            /// <summary>
            /// MP_CONFIG_SIGNE_CAPTEURS_AV // Signe capteurs
            /// </summary>
            AVInputSensors,

            /// <summary>
            /// FOURNISSEUR // Code Fourniseur
            /// </summary>
            SupplierCode,

            /// <summary>
            /// ID_VERSION_LOGICIEL // Version logiciel
            /// </summary>
            SoftwareVersion,

            /// <summary>
            /// VERSION_DIAG // Indice d'йvolution du Diagnostic
            /// </summary>
            DiagnosticsVersionIndex,

            /// <summary>
            /// MP_PROG_TEMP_EXT // Programmation de l?affichage de la tempйrature extйrieure
            /// </summary>
            ProgrammingOfExteriorTemperature,

            /// <summary>
            ///  MP_PRESENCE_RADIO // ConfigurationDePrйsenceD'unAutoradioSurLeRйseauVAN
            /// </summary>
            ConfiguredAutoradioPresenceOnVanBus,

            /// <summary>
            /// OPTGESTIONVOL !!! MP_INFOS_RADIO_AUTOMATIQUE // InformationAl'EMFDeLaConnexionаUnAutoradioComportantLaFonctionVolumeAutomatique
            /// </summary>
            InformationAboutAutoVolumeRadioFunction,

            /// <summary>
            /// PI_OPT_ORDI_BORD // OptionOrdinateurDeBord.
            /// </summary>
            BoardComputerOption,

            // MP_PRESENCE_CD_CHANGEUR // ConfigurationDePrйsenceD'unCDChangeurSurLeRйseauVAN.
            CDChangerPresenceOnVANBusSetting,

            /// <summary>
            /// MP_ACTIVE_FONCT_SURVITESSE // ActivationFonctionAlerteSurvitesse.
            /// </summary>
            OverspeedingAlertFunction, 

            /// <summary>
            /// MP_NOTATION_DATE // TypeDeNotationDelaDate:Anglo-saxonneMMJJAA,FranзaiseJJMMAA
            /// </summary>
            DateDisplayNotation,

            /// <summary>
            /// VOLUMEDIST // Unitй de volume et de distance
            /// </summary>
            VolumeAndDistanceUnits,

            /// <summary>
            /// TEMPERATURE // Unitйsd'AffichageDeTempйrature(°Cou°F)
            /// </summary>
            TemperatureUnit,

            /// <summary>
            /// Тип дисплея: Положительный для черного текста на светлом фоне, отрицательный для светлого текста на черном фоне
            /// TYPEAFFICHAGE !!!! MP_AFFICH_POSIT_NEGAT // Typed'Affichage:PositifPourTexteNoirSurFondClair,NйgatifPourTexteClairSurFondNoir
            /// </summary>
            DisplayColorSchemeType,

            /// <summary>
            /// MP_ETAT_ORDI_BORD !!! ETATBOUTON ODB // EtatLuDuBoutonOrdinateurDeBord.
            /// </summary>
            BoardComputerState,

            /// <summary>
            /// MASSADDFAP, Masse d'additif dans le Filtre а Particules // Масса аддитива в фильтре частиц
            /// Weight of additive in the particulate filter
            /// </summary>
            MassAdditiveFap,

            /// <summary>
            /// TEMPERATUREAIRCOLLECT // Tempйrature d?air collecteur
            /// </summary>
            AirCollectorTemperature,

            /// <summary>
            /// Температура воздуха у расходомеров // 
            /// DEPRECATED
            /// use AirTemperature
            /// </summary>
            Deprecated3,

            /// <summary>
            /// DEBITINJECTE // Объём впрыска
            /// </summary>
            MeasuredInjectionVolume,

            /// <summary>
            /// @P10538-CITACT // MP_DEFAUT_SYS_ADDITIVATION // dйfaut systиme d'additivation // Enum type
            /// </summary>
            DefaultAdditiveSystem,

            /// <summary>
            /// COMMANDINJECTEUR
            /// Наличие ошибки управления форсунками 
            /// дизельный двигатель
            /// </summary>
            InjectorsCommandError,

            /// <summary>
            /// DEBITCARB
            /// debit gazole delivre par la pompe haute pression
            /// Объём топлива, подаваемого ТНВД
            /// </summary>
            HighPressureFuelPumpFuelSupply,

            /// <summary>
            /// CONSIGNEDEBITAIR
            /// Необходимый (запрошенный) расход воздуха
            /// </summary>
            RequestedAirVolumeFlow,

            // RAPPORTBOITE
            // ~Расчётное передаточное отношение КПП
            ReportedGearboxGearRatio,

            //RCORECOVANNEEGR
            // Получение обратной информации о положении клапана РВГ
            EgrValvePositionFeedback,

            /// <summary>
            /// Разблокировка BSI.
            /// BSIDECONDA
            /// </summary>
            BsiUnlock,

            /// <summary>
            /// BSI en Anti-scanning
            /// BSIANTISCAN
            /// BSI В режиме анти-скан
            /// </summary>
            BsiAntiScan,

            /// <summary>
            /// Команда на размораживание (антиобледенение).
            /// CMDDEGIVRAGE
            /// </summary>
            DeicingCommand,

            /// <summary>
            /// DEFAUTABR
            /// Умолчание системы адаптивного тормоза
            /// </summary>
            AbrDefault,

            /// <summary>
            /// Premier apprentissage papillon EGR
            /// ETATAPPRENTISSAGE4
            /// </summary>
            FirstEgrScan,

            /// <summary>
            /// ETATVOLETMIX
            /// Состояние заслонки смешивания (воздуха в салон?)
            /// </summary>
            MixingShutterStatus,

            /// <summary>
            /// TEMPERATUREINTER
            /// Температура внутри
            /// </summary>
            InternalTemperature,

            /// <summary>
            /// NIVCARBUMESURE
            /// Измеренный уровень топлива
            /// </summary>
            MeasuredFuelLevel,

            /// <summary>
            /// Минимальный уровень бачка омывателя.
            /// NIVLAVEGLACEMINI
            /// </summary>
            WindscreenWasherLiquidMinimumLevel,

            /// <summary>
            /// Предупреждение о минимальном уровне топлива.
            /// ALERTEMINICARBURANT
            /// </summary>
            MinimumFuelLevelAlert,

            /// <summary>
            /// Корректность коммуникации с передатчиком ключа.
            /// Communication correcte avec le transpondeur de la cle
            /// COMMTRANSPOK
            /// </summary>
            CorrectCommunicationWithKeyTransponder,

            /// <summary>
            /// Mesure de vitesse des groupe motoventilateur (GMV)
            /// @P35572-CITACT@\*
            /// VITESSEGMV
            /// </summary>
            MeasuredFanSpeed,

            /// <summary>
            /// Etat calculateur
            /// ETATCALCULATEUR
            /// </summary>
            CalculatorStatus,

            /// <summary>
            /// Alimentation relais lиve-vitres et toit ouvrant
            /// RELAISALIMTO
            /// </summary>
            SunroofAndWindowRelay,

            /// RCORECOPAPEGR
            /// Получение обратной информации о положении дроссельной заслонки РВГ
            EgrThrottlePositionFeedback,

            /// <summary>
            /// RCORECOEVRAA
            /// Получение обратной информации о позиции электроклапана турбины.
            /// </summary>
            ElectrovalveTurboPositionFeedback,

            /// <summary>
            /// Ошибка переменной телекодирования
            /// ERREURTELECODAGE
            /// </summary>
            TelecodingVariableError,

            /// <summary>
            /// Последнее сканирование клапана рециркуляции ОГ.
            /// DERNIERAPPRENTISSAGEVANNEEGR
            /// It's about valve
            /// </summary>
            LastEgrScan,

            /// <summary>
            /// Первое сканирование заслонки РВГ
            /// </summary>
            FirstErgThrottleScan,

            /// <summary>
            /// Последнее сканирование заслонки РВГ.
            /// ETATAPPRENTISSAGE3
            /// </summary>
            LastErgThrottleScan,

            /// <summary>
            /// Последнее сканирование дроссельной заслонки нагревателя воздуха
            /// ETATAPPRENTISSAGE5
            /// </summary>
            LastAirHeaterThrottleScan,

            /// <summary>
            /// Первое сканирование дроссельной заслонки нагревателя воздуха
            /// ETATAPPRENTISSAGE6
            /// </summary>
            FirstAirHeaterThrottleScan,

            /// <summary>
            /// Команда дополнительного обогрева
            /// AdditionalHeatingCommand
            /// </summary>
            AdditionalHeatingCommand,

            /// <summary>
            /// Режим работы BSI
            /// NIVDELESTAGE
            /// </summary>
            BsiMode,

            /// <summary>
            /// SECUENFANTD
            /// </summary>
            ChildSafetyRight,

            /// <summary>
            /// SECUENFANTG
            /// </summary>
            ChildSafetyLeft,

            /// <summary>
            /// CMDANTIBAV
            /// </summary>
            FrontFogLampsControl,

            /// <summary>
            /// ANTIDEMVERROU
            /// </summary>
            TransponderAntiscanActive,

            /// <summary>
            /// CLE1SYNCHRO
            /// </summary>
            Key1Sync,
            Key2Sync,
            Key3Sync,
            Key4Sync,
            Key5Sync,

            /// <summary>
            /// NOMBRECLEACTIVES
            /// </summary>
            NumberOfActiveKeys,

            /// <summary>
            /// NOMBRECLESSYNCHRO
            /// </summary>
            NumberOfSynchronizedRadiokeys,

            BsiPair,

            BsiEmpty,

            /// <summary>
            /// ???
            /// @P17515-CITACT@\*2
            /// </summary>
            NIVDELEST6,

            /// <summary>
            /// ETATAPPRENTISSAGE4
            /// </summary>
            FirstEgrThrottleScan,

            /// <summary>
            /// MP_COMMANDE_COMPRESSEUR_REFRIGERATION
            /// </summary>
            AirConditioningCompressorCommand,

            /// <summary>
            /// MP_MODE_FONCTIONNEMENT_DE_LA_CLIMATISATION
            /// </summary>
            ClimateControlFunctioningMode,

            /// <summary>
            /// MP_SECURITE_BASSE_PRESSION_CIRCUIT_REFRIGERATION
            /// </summary>
            AirConditioningCircuitLowPressureProtection,

            /// <summary>
            /// MP_SECURITE_HAUTE_PRESSION_CIRCUIT_REFRIGERATION
            /// </summary>
            AirConditioningCircuitHighPressureProtection,

            /// <summary>
            /// MP_LOI_PASSAGE_VITESSES
            /// </summary>
            GearboxRule,

            /// <summary>
            /// MP_PRESSION_CONSIGNE_1
            /// </summary>
            OilPressure,

            /// <summary>
            /// MP_PRESSION_CONSIGNE 
            /// Oil Pressure setting?
            /// </summary>
            PressureSetting,

            /// <summary>
            /// 
            /// </summary>
            ElectromagneticModulationActivationTimes,

            /// <summary>
            /// Коллектор теплообменника.
            /// </summary>
            EPDECollector,

            /// <summary>
            /// POSITIONPAPILLON _6LP
            /// </summary>
            ThrottlePosition, 

            /// <summary>
            /// управление вентилятором климатической установки (CAN)
            /// MP_COMMANDE_PULSEUR_CLIMATISATION_PAR_CAN
            /// Скорее всего, как для климатических установок, так и для просто кондиционера
            /// </summary>
            ClimateAirConditioningFanCommand,

            /// <summary>
            /// MP_ETAT_CAPTEUR_TEMPERATURE_EVAPORATEUR 
            /// состояние датчика температуры в испарителе
            /// </summary>
            EvaporatorTemperatureSensorState,

            /// <summary>
            /// MP_ETAT_EMBRAYAGE_COMPRESSEUR_REFRIGERATION 
            /// состояние включения компрессора кондиционера
            /// </summary>
            AirConditionerCompressorOnOffState,

            /// <summary>
            /// Команда на генерацию электрической энергии
            /// MP_DEMANDE_PRODUCTION_ENERGIE_ELECTRIQUE
            /// </summary>
            ElectricEnergyGenerationCommand,

            /// <summary>
            /// Команда регулирования производства электрической энергии
            /// MP_DEMANDE_TYPE_REGULATION_PRODUCTION_ENERGIE_ELECTRIQUE
            /// </summary>
            ElectricEnergyGenerationRegulationCommand,

            /// <summary>
            /// Уровень электрической нагрузки
            /// MP_NIVEAU_LESTAGE_ELECTRIQUE
            /// </summary>
            ElectricLoadLevel,

            /// <summary>
            /// MP_TENSION_BATTERIE_SECURISEE
            /// Защищённое напряжение АКБ
            /// </summary>
            BatteryVoltageSecure,

            /// <summary>
            /// Углы опережения зажигания
            /// MP_AVANCE_CYLINDRE_01
            /// </summary>
            IgnitionTimingAdvanceAngleCylinder1,
            IgnitionTimingAdvanceAngleCylinder2,
            IgnitionTimingAdvanceAngleCylinder3,
            IgnitionTimingAdvanceAngleCylinder4,

            MaximumIgnitionAdvance,
            OptimalIgnitionAdvance,

            NoiseLevelMeasuredByDetonationSensor,


            RequestedManagedThermostatValue,

            /// <summary>
            /// Датчик уровня освещённости, числовое значение.
            /// Вероятно, милливольты.
            /// </summary>
            LightSensorOutput,

            /// <summary>
            /// Ток АКБ
            /// </summary>
            BatteryCurrent,

            /// <summary>
            /// MP_ETAT_DE_CHARGE_BATTERIE_DE_DEMARRAGE
            /// </summary>
            BatteryChargePercentage,
            BatteryChargePercentagePrecise,

            /// <summary>
            /// Температура АКБ
            /// </summary>
            BatteryTemperature,

            AutomaticGearboxEnterTimeRequested,
            AutomaticGearboxEnterTimeMeasured,
            AutomaticGearboxExitTimeRequested,
            AutomaticGearboxExitTimeMeasured,

            GearboxHandlePosition,

            OilPressureSensorVoltage,
            OilTemperatureSensorVoltage,

           MultifunctionalSwitchPNContact,
           MultifunctionalSwitchS4Contact,
           MultifunctionalSwitchS3Contact,
           MultifunctionalSwitchS2Contact,
           MultifunctionalSwitchS1OrImpulsePlusContact,
           MultifunctionalSwitch1OrImpulseMinusContact,

           // BVA-related
           /// <summary>
           /// Необходимость слива
           /// </summary>
            DrainRequired,

           /// <summary>
           /// Общее питание последовательных (секвентальных) электромагнитных клапанов
           /// </summary>
            SequentialElectromagneticValvesGeneralSupply,

           /// <summary>
           /// Выход климатической установки или функции Key-lock
           /// </summary>
            ClimateControlOrKeyLockFunctionOutput,

           /// <summary>
           /// Регулировка крутящего момента
           /// </summary>
            TorqueRegulation,

           /// <summary>
           /// Разблокировка рычага (shift-lock)
           /// </summary>
            ShiftLockHandleUnblocking,

           /// <summary>
           /// Блокировка гидространсформатора
           /// </summary>
            HydraulicTorqueConverterBlocking,

           /// <summary>
           /// Время активизации EVLU
           /// </summary>
            EVLUActivationTime,

            /// <summary>
            /// Секансный электромагнитный клапан 1
            /// </summary>
            SequentialSolenoidValve1,

            /// <summary>
            /// Секансный электромагнитный клапан 2
            /// </summary>
            SequentialSolenoidValve2,

            /// <summary>
            /// Секансный электромагнитный клапан 3
            /// </summary>
            SequentialSolenoidValve3,

            /// <summary>
            /// Секансный электромагнитный клапан 4
            /// </summary>
            SequentialSolenoidValve4,

            /// <summary>
            /// Секансный электромагнитный клапан 5
            /// </summary>
            SequentialSolenoidValve5,

            /// <summary>
            /// Секансный электромагнитный клапан 6
            /// </summary>
            SequentialSolenoidValve6,

           /// <summary>
           /// Состояние АКПП
           /// </summary>
            BVAState,

           /// <summary>
           /// Состояние программы АКПП
           /// </summary>
            BVAProgramState,

           /// <summary>
           /// Информация о движении задним ходом
           /// </summary>
            ReverseInformation,

           /// <summary>
           /// Положение ключа в замке зажигания
           /// </summary>
            KeyPositionInIgnitionLock,

           /// <summary>
           /// Положение рычага переключения передач, зарегистрированное SMD
           /// </summary>
            GearboxHandlePositionRegisteredBySMD,

            /// <summary>
            /// аварийный режим работы электросистемы
            /// </summary>
            ElectricSystemEmergencyMode,

           /// <summary>
           /// Плюс после контакта (+APC)
           /// </summary>
            PlusAfterContactAPC,

           /// <summary>
           /// Информация о плюсе после контакта, переданная на сервисный модуль двигателя
           /// </summary>
            InformationPlusAfterContactPassedToEngineServiceModule,

           /// <summary>
           /// Ключ в положении включения стартера
           /// </summary>
            KeyInStarterStartPosition,

           /// <summary>
           /// Команда на запуск двигателя
           /// </summary>
            EngineStartCommand,

           /// <summary>
           /// Управление выходом питания при работающем двигателе (разрешение включения электрообогревателя сидений)
           /// </summary>
           PowerOutputAtWorkingEngineControl_SeatHeaterOn,

           /// <summary>
           /// Состояние силового агрегата 
           /// Электрического?
           /// MP_ETAT_GMP
           /// </summary>
           PowerUnitState,

           /// <summary>
           /// Главное состояние системы электрооборудования
           /// MP_ETAT_PRINCIPAL
           /// </summary>
           MainElectricSystemState,

           /// <summary>
           /// When alternator is off
           /// Состояние электрической цепи при неработающем генераторе
           /// MP_ETAT_RESEAU_ELECTRIQUE_AVEC_GENERATEUR_NON_OPERATIONNEL
           /// </summary>
           ElectricChainStateWithGeneratorOffline,

           /// <summary>
           /// Состояние электрической цепи при работающем генераторе
           /// MP_ETAT_RESEAU_ELECTRIQUE_AVEC_GENERATEUR_OPERATIONNEL
           /// </summary>
           ElectricChainStateWithGeneratorOnline,

           /// <summary>
           /// Конфигурация системы электрооборудования автомобиля.
           /// </summary>
           VehicleElectricSystemConfiguration,

           /// <summary>
           /// Режим фазы жизни
           /// </summary>
           LifePhaseMode,

           /// <summary>
           /// Состояние памяти BSI после телекодировки
           /// </summary>
           BsiMemoryStateAfterTelecoding,

           /// <summary>
           /// Количество обнулений прошивки BSI
           /// </summary>
           BsiSoftwareResetsCount,

           /// <summary>
           /// Число перезагрузок программных средств BSI
           /// </summary>
           BsiSoftwareRebootsCount,

           /// <summary>
           /// Закончилось время неработоспособного состояния генератора.
           /// </summary>
           AlternatorNonOperationalTimeHasEnded,

           /// <summary>
           /// Время, прошедшее с начала работы генератора
           /// </summary>
           TimeSpanSinceAlternatorStart,

           /// <summary>
           /// Физическое расположение шунта положения "Парковка"
           /// </summary>
           PhysicalStateOfParkingStateShunt,

           /// <summary>
           /// Команда помощи в регенерации FAP
           /// </summary>
           FapRegenerationHelpRequest,

           /// <summary>
           /// Состояние чувствительных потребителей электроэнергии
           /// </summary>
           SensitiveElectricConsumersState,

           // Load Shedding variables 

           /// <summary>
           /// Отключение системы NOx для частичной разгрузки генератора
           /// </summary>
           NOxSystemSwitchOffForPartialGeneratorUnload,

           /// <summary>
           /// Частичная блокировка системы дополнительного обогрева (CTP3)
           /// </summary>
           AdditionalHeatingSystemCTP3PartialBlock,

           /// <summary>
           /// Функция экономии электроэнергии обогревателем заднего стекла во время регенерации FAP
           /// </summary>
           RearWindowHeatingPowerSaveFunctionWhileRegeneratingFAP,

           /// <summary>
           /// Функция глобальной экономии электроэнергии обогревателем заднего стекла
           /// </summary>
           GlobalPowerSavingFunctionOfRearWindowHeater,

           /// <summary>
           /// Глобальная экономия электроэнергии дополнительным отопителем (CTP 1 и 2)
           /// </summary>
           AdditionalHeaterPowerSavingFunctionCTP1And2,

           /// <summary>
           /// Отключение обогрева лобового стекла
           /// </summary>
           WindscreenHeaterLoadShedding,

           /// <summary>
           /// Отключение блока трансформации кузова
           /// </summary>
           BodyTransformationBlockLoadShedding,

            // Automatic gearbox options
           /// <summary>
           /// Регулирование/ограничение скорости: команда на включение
           /// </summary>
           SpeedLimitationOrRegulation_SwitchOnCommand,
           /// <summary>
           /// Регулирование/ограничение скорости: управление скоростью +
           /// </summary>
           SpeedLimitationOrRegulation_SpeedRegulationPlus,
           /// <summary>
           /// Регулирование/ограничение скорости: управление скоростью -
           /// </summary>
           SpeedLimitationOrRegulation_SpeedRegulationMinus,
           /// <summary>
           /// Регулирование/ограничение скорости: состояние активации
           /// </summary>
           SpeedLimitationOrRegulation_ActivationState,
           /// <summary>
           /// Регулирование/ограничение скорости: состояние выбора
           /// </summary>
           SpeedLimitationOrRegulation_SelectionState,
           /// <summary>
           /// Регулирование/ограничение скорости: состояние кнопки «Аннулировать/восстановить»
           /// </summary>
           SpeedLimitationOrRegulation_CancelRestoreButtonStatus,
           /// <summary>
           /// Регулирование/ограничение скорости: состояние скорости
           /// </summary>
           SpeedLimitationOrRegulation_SpeedState,

           /// <summary>
           /// Уровень масла после фильтрации сигнала
           /// </summary>
           OilPressureAfterSignalFiltering,

           /// <summary>
           /// Вывод информации об ошибке цепи заряда АКБ
           /// </summary>
           BatteryChargeCirquitErrorDisplaying,

           /// <summary>
           /// Вода в бензине
           /// </summary>
           WaterInGazolineDetected,

           /// <summary>
           /// Сброс пробега при послепродажном обслуживании
           /// </summary>
           MileageResetAtFirstPostDeliverService,

           /// <summary>
           /// Мгновенный расход топлива
           /// </summary>
           InstantFuelConsumption,

           /// <summary>
           /// Управление сигналом непристёгнутого ремня безопасности
           /// </summary>
           UnfastenedBeltSignalManagement,

           /// <summary>
           /// Управление сигналом запрета системы CDS (ESP)
           /// </summary>
           ESPSwitchOffSignalManagement, 

           /// <summary>
           /// Пробег перед техинческим обслуживанием
           /// </summary>
           MileageBeforeService, 

           /// <summary>
           /// Знак пробега до технического обслуживания
           /// </summary>
           MileageBeforeServiceSign,

           /// <summary>
           /// Число дней до технического обслуживания
           /// </summary>
           DaysLeftTillService, 

           /// <summary>
           /// Знак числа дней до технического обслуживания
           /// </summary>
           DaysLeftTillServiceSign,

           /// <summary>
           /// Пробег, соответствующий последнему сбросу
           /// </summary>
           MileageCorrespondingToLastReset,

           /// <summary>
           /// Период времени после последнего сброса
           /// </summary>
           PeriodAfterLastReset,

           /// <summary>
           /// Режим автоматической работы стеклоочистителя
           /// </summary>
           ScreenWiperAutomaticMode, 

           /// <summary>
           /// Команда на включение стеклоочистителя, поданная датчиком дождя
           /// </summary>
           ScreenWiperCommandSentByRainSensor, 

           /// <summary>
           /// Total mess, be careful
           /// </summary>
           WindscreenWiperRequest,

           WindscreenWasherRequest,
           RearScreenWasherRequest,

           HeadlightWasherControlOrExitWithWorkingEngine,

           // see also DeicingCommand
           DeicingControl,

           /// <summary>
           /// MP_ETAT_KICKDOWN_BVA
           /// </summary>
           BVAKickDownContactState,

           RequestForMaximumAirConditionerCompressorVolume,

           RequestForCTPResistiveHeaterSwitchOn,

           ESPSwitch,

           ParkingAidSystemSwitch,

           ClimateManualSwitch,

           LeftStoplight,
           RearLeftTurnSignal,
           LeftReverseLight,
           RearLeftMarkerLamp, // габарит
           RearRightTurnIndicator,
           RearRightMarkerLamp,
           AdditionalStopLight,
           //
           RightStoplight,
           RearWindowOpening,
           SillLighting,
           FrontSillLightingCover, // плафон
           ElectricIgnitionLockSafety,
           RearSillLightingCover, //плафон
           BootOpen,
           BootLightingCover,
           FrontLight,
           RearRightFogLamp,
           RearLight,
           RightReverseLight,
           SwitchesLighting,
           RearLeftFogLamp,

           /// <summary>
           /// Диагностика концевого выключателя положений P/N (АКП) или состояния разрешения запуска двигателя
           /// </summary>
           PnContactDiagnosticsOrEngineStartPermissionStatusBVA,

           /// <summary>
           /// Откорректированное заданное наполнение
           /// ??? наполнение чего? бака?
           /// </summary>
           CorrectedRequestedFilling,
           /// <summary>
           /// Откорректированное измеренное наполнение
           /// </summary>
           CorrectedMeasuredFilling,
           BruttoRequestedFilling,
           BruttoMeasuredFilling,

           /// <summary>
           /// Давление в коллекторе относительно заданного
           /// </summary>
           CollectorPressureRelativeToSpecified,
           /// <summary>
           /// - // - измеренного
           /// </summary>
           CollectorPressureRelativeToMeasured,

           ThrottleAngleSpecified,
           ThrottleAngleMeasured,

           /// <summary>
           /// Also called copy voltage
           /// See also ThrottlePositionVoltage
           /// </summary>
           /// Дублирующее напряжение датчика положения заслонки 1 и 2
           ThrottleAngleSensorVoltageDuplicate1,
           ThrottleAngleSensorVoltageDuplicate2,

           /// <summary>
           /// Инструкция положения механизма изменения фазы распредвала впускных клапанов
           /// </summary>
           PrescriptionForInputValvesDephaserPosition,
           InputValvesDephaserPosition,

           /// <summary>
           /// ЦСО электроклапана механизма фазорегуляции впускных клапанов
           /// </summary>
           InputValvesPhaserElectrovalveRCO,

           InputCamshaftPhaserMechanismState,
           ExhaustCamshaftPhaserMechanismState,

           /// <summary>
           /// Направление привода дроссельной заслонки
           /// </summary>
           ThrottleGearingDirection,

           /// <summary>
           /// ШИМ-сигнал управления дроссельной заслонкой
           /// </summary>
           ThrottleControlPWMSignal, 

           /// <summary>
           /// ЦСО электроклапана механизма изменения фаз распредвала выпускных клапанов
           /// </summary>
           ExhaustValvesPhaserMechanismElectrovalveRCO,

           PrescriptionForExhaustValvesDephaserPosition,
           ExhaustValvesDephaserPosition,

           FillingCorrectionFactor,
           FillingRegulatorState,

           /// <summary>
           /// Фактор коррекции давления воздуха на впуске
           /// </summary>
           AirPressureCorrectionFactorInlet,
           /// <summary>
           /// Состояние регулятора давления воздуха на впуске
           /// </summary>
           InletAirPressureRegulatorState,
           /// <summary>
           /// Моделированное давление воздуха на впуске
           /// </summary>
           InletAirPressure,

           InjectionTimeCylinder1,
           InjectionTimeCylinder2,
           InjectionTimeCylinder3,
           InjectionTimeCylinder4,

           UpperOxygenSensorRegulationState,
           LowerOxygenSensorRegulationState,

           /// <summary>
           /// Верхняя граница состава смеси
           /// </summary>
           UpperMixCompositionBorder,

           /// <summary>
           /// ЦСО обогрева верхнего зонда
           /// </summary>
           UpperLambdaProbeHeatingRCO,
           LowerLambdaProbeHeatingRCO,

           /// <summary>
           /// Фактор коррекции состава смеси на выходе
           /// </summary>
           MixtureCompositionCorrectionFactorOnEnter,
           MixtureCompositionCorrectionFactorOnExit,

           /// <summary>
           /// Оценочная нагрузка абсорбера топливных паров
           /// </summary>
           FuelVaporsAbsorberEstimatedLoad,

           /// <summary>
           /// Заданное значение обогащения смеси
           /// </summary>
           MixRegSetup,

           /// <summary>
           /// Требуемое давление масла
           /// </summary>
           RequestedOilPressure,

           /// <summary>
           /// Порог обнаружения детонации
           /// </summary>
           DetonationDetectionThreshold,

           /// <summary>
           /// Задержка опережения цилиндра 1
           /// </summary>
           DelayAdvanceCylinder1,
           DelayAdvanceCylinder2,
           DelayAdvanceCylinder3,
           DelayAdvanceCylinder4,

           MinInjectionAdvance,

           EstimatedResistanceTorque,
           TorqueCorrespondingToDriversDesire,

           /// <summary>
           /// ЦСО привода подъёма регулируемых клапанов
           /// </summary>
           VariableValveLiftDriveRCO,

           /// <summary>
           /// Состояние реле питания привода системы изменяемого подъёма клапанов
           /// </summary>
           VariableValveLiftSystemsSupplyaRelayState,

           /// <summary>
           /// Напряжение питания электродвигателя системы регулируемого подъёма клапанов
           /// </summary>
           VariableValveLiftSystemsElectricEngineSupplyVoltage,

           /// <summary>
           /// Измеренная величина подъёма клапана
           /// </summary>
           MeasuredValveLiftingValue,

           /// <summary>
           /// Заданная величина подъёма клапана
           /// </summary>
           RequestedValveLiftingValue, 

           /// <summary>
           /// Угловое положение, для кот. проведено "обучение", соотв-е мин подъёму клапанов
           /// </summary>
           ValveLiftLearnAngleMin,
           ValveLiftLearnAngleMax,

           /// <summary>
           /// Состояние "обучения" упоров клапанов
           /// </summary>
           StateOfLearnStopValve,

           /// <summary>
           /// Необходимость "обучения" упоров клапанов
           /// </summary>
           LearnStateStopValveRequirement,

           /// <summary>
           /// Направление вращения электродвигателя подъёма клапанов
           /// </summary>
           ValvesRaisingElectricEngineRotationDirection, 

           /// <summary>
           /// Ток, потребляемый электродвигателем системы регулирования подъёма клапанов при настройке
           /// </summary>
           CurrentConsumedByValvesRaisingElectricEngineWhileSetup,

           /// <summary>
           /// Ток, потребляемый электродвигателем системы регулирования подъёма клапанов 
           /// </summary>
           CurrentConsumedByValvesRaisingElectricEngine,

           /// <summary>
           /// Ошибка заполнения
           /// </summary>
           FillingError,

           /// <summary>
           /// Температура электродвигателя системы регулирования подъёма клапанов
           /// </summary>
           TemperatureOfValvesRaisingElectricEngine, 

           /// <summary>
           /// Состояние первоначальнйо настройки верхнего и нижнего ограничителей клапанов
           /// </summary>
           StateofInitialSetupOfUpperAndLowerValveLimiters,

           /// <summary>
           /// CONSIGNE_DEBIT_AIR
           /// </summary>
           RequestedAirConsumption,

           // Buttons
           ButtonEscPressed,
           ButtonModePressed,
           ButtonMenuPressed,
           ButtonOkPressed,
           ButtonLeftPressed,
           ButtonRightPressed,
           ButtonDownPressed,
           ButtonUpPressed,

           ClientLanguage,

           SettingsMenuDisplayed,
           
           /// <summary>
           /// Опция автомагнитолы
           /// </summary>
           AutoRadioOption,
           LowTyresPressureDetectionOption,

           /// <summary>
           /// Система помощи при парковке
           /// </summary>
           ParkingAidSystemOption,

           /// <summary>
           /// Тип приборной панели
           /// </summary>
           ControlPanelType,

           /// <summary>
           /// Опция набор HandsFree
           /// </summary>
           HandsFreeKitOption,

           /// <summary>
           /// Вывод протокола срабатываний охранной исгнализации
           /// </summary>
           SecurityAlarmLogOutput,

           /// <summary>
           /// Вывод журнала состояний
           /// </summary>
           StatesJournalOutput,

           /// <summary>
           /// Опция меню "Nomade"
           /// </summary>
           NomadeMenuOption,

           /// <summary>
           /// Отображение предупреждений на многофункциональном дисплее
           /// </summary>
           AlertsOutputOnMultifunctionalDisplay,

           /// <summary>
           /// Опция конфигурирования единиц измерения температуры
           /// </summary>
           TemperatureUnitsSetupOption,

           /// <summary>
           /// Управление радио и экраном органами управления на рулевом колесе
           /// </summary>
           RadioAndScreenControlUsingSteeringWheenControls,

           /// <summary>
           /// Тип связи с ПДУ
           /// </summary>
           ConnectionTypeToRemoteControl,

           /// <summary>
           /// Выводимая на дисплей заданная скорость вентилятора
           /// </summary>
           DisplayedFanSpeed,

           /// <summary>
           /// Команда пользователя на включение климатической установки
           /// </summary>
           UserCommandToSwicthClimateControlOn,

           /// <summary>
           /// Управление дополнительными резисторами CTP1 и CTP2
           /// </summary>
           AdditionalCTP1AndCTP2ResistorsControl,

           /// <summary>
           /// Состояние безопасности охлаждающей жидкости
           /// </summary>
           EngineCoolantSafetyState,

           /// <summary>
           /// Расчётная наружная температура
           /// </summary>
           CalculatedExternalTemperature,

           /// <summary>
           /// Состояние датчика давления в контуре кондиционера
           /// </summary>
           PressureSensorStateInConditionerCirquit,

           /// <summary>
           /// Безопасность частоты вращения двигателя
           /// </summary>
           EngineRpmSafety,

           /// <summary>
           /// Состояние датчика наружной температуры
           /// </summary>
           ExternalTemperatureSensorState,

           /// <summary>
           /// Тип батареи
           /// </summary>
           BatteryType,

           /// <summary>
           /// Распознавание минимального положения педали акселератора 1
           /// </summary>
           RecognitionOfAcceleratorPedalMinState1,
           RecognitionOfAcceleratorPedalMinState2,
           RecognitionOfAcceleratorPedalMaxState1,
           RecognitionOfAcceleratorPedalMaxState2,

           /// <summary>
           /// Проведено обучение зон функционирования
           /// </summary>
           FunctioningZonesLearningPerformed,

           /// <summary>
           /// Адаптация "мишени" датчика положения коленвала при замедлении
           /// </summary>
           AdaptationOfCrankshaftPositionSensorTargetWhileDecelerating,

           // Величины адаптации момента трения в зависимости от температуры охлаждающей жидкости
           AdaptationRateOfFrictionTorqueDependingOnCoolantTemperature,

           // Состояние "обучения" упорам дроссельной заслонки
           StateOfLearningOfThrottleStops,

           /// <summary>
           ///  Необходимость "обучения" упорам дроссельной заслонки
           /// </summary>
           ThrottleStopsLearningRequired,

            /// <summary>
            /// Распознавание положения для движения в аварийном режиме 1
            /// </summary>
            PositionRecognitionToDriveInEmergencyMode1,
            PositionRecognitionToDriveInEmergencyMode2,

            // Распознавание ограничителя минимального положения заслонки 1
            RecognitionOfMinPositionLimiterForThrottle1,
            RecognitionOfMinPositionLimiterForThrottle2,

            // Величины адаптации указанного крутящего момента двигателя
            ValuesOfIndicatedTorqueAdaptations,

            // Значение адаптивных функций состава смеси на холостом ходу
            ValueOfMixtureAdaptiveFunctionsAtIdlingRPM,

            ValueOfMixtureAdaptiveFunctionsAtTransitionalMode, // Значение адаптивных функций состава смеси на переходном режиме
            MixtureCompositionAdaptiveValuesOfOfInletSensor, // Адаптивные величины состава смеси датчика на выходе
            AdaptiveValuesForDetonationPreventionInCylinder4, // Адаптивные величины предотвращения детонации цилиндра 4
            AdaptiveValuesForDetonationPreventionInCylinder3, // Адаптивные величины предотвращения детонации цилиндра 3
            AdaptiveValuesForDetonationPreventionInCylinder2, // Адаптивные величины предотвращения детонации цилиндра 2
            AdaptiveValuesForDetonationPreventionInCylinder1, // Адаптивные величины предотвращения детонации цилиндра 1
            LearningStateForInletCamshaftDephaser, // Состояние "обучения" механизма изменения фаз впускного распредвала
            LearningValueForLowerStopForInletDephaser, // Значение при "обучении" положению нижнего упора механизма рег-я фаз на впуске
            LearmingStateOfExhaustDephaser, // Состояние "обучения" механизма изменения фаз выпускного распредвала
            LearningValueForUpperStopForExhaustDephaser, // Значение при "обучении" положению верхнего упора механизма рег-я фаз на выпуске
            StateOfTheFirstInletDephaserSetup, // Статус первой настройки фазорегулятора впускного распредвала
            StateOfTheFirstExhaustDephaserSetup, // Статус первой настройки фазорегулятора выпускного распредвала

            TorqueValueObtainedByIgnitionAdvance_Requested, // Крутящий момент, полученный путём регулировки опережения зажигания (запрос)
            TorqueValueObtainedByAirVolumeFlowRegulation_Requested, // Крутящий момент, полученный путём регулировки объёма воздуха (запрос)
            TorqueValueObtainedByIgnitionAdvance_Real, // Крутящий момент, полученный путём регулировки опережения зажинания (реальный)
            TorqueValueObtainedByAirVolumeFlowRegulation_Real, // Крутящий момент, полученный путём регулировки объёма воздуха (реальный)

            AcceleratorPedalPositionSensorVoltage1, // Напряжение датчика положения педали акселератора 1
            AcceleratorPedalPositionSensorVoltage2, // Напряжение датчика положения педали акселератора 2
            SpeedRegulatorSpeedSettings, // Заданная скорость регулятора скорости автомобиля
            SpeedRegulationState, // Состояние регулирования скорости автомобиля
            SpeedLimitationSettings, // Заданная скорость ограничителя скорости автомобиля
            SpeedLimitationState, // Состояние ограничения скорости автомобиля
            InformationAboutAcceleratorPedalAbutment, // Информация датчика упора педали акселератора
            InformationFromElectricParkingBrake, // Информация стояночного тормоза с электроуправлением
            EngineState, // Состояние двигателя
            CalculatorActivationState, // Состояние активизации калькулятора
            ClutchPedalPosition, // Положение педали сцепления
            EngineStopTime, // Время остановки двигателя
            CoolantLiquidTemperatureAtLastEngineStop, // Температура ОЖ при последней остановке двигателя
            RequestedEngineRpm, // Заданная частота вращения двигателя

            CoolantTemperatureSettings, // Заданное значение температуры охлаждающей жидкости
            CoolantPumpActuatorControl, // Управление исполнительным элементом насоса системы охлаждения
            FanAssemblyHighSpeedRelayControl, // Состояние реле большой скорости группы электровентилятора
            FanAssemblyLowSpeedRelayControl, // Состояние реле малой скорости группы электровентилятора
            FanAssemblyStateInputState, // Состояние входа "Состояние группы электровентилятора"
            EstimatedPowerConsumptionByAirConditionerCompressor, // Оценка потребляемой мощности компрессора кондиционера
            RequiredValueForGradualGeneratorLoad, // Заданное значение постепенной нагрузки генератора
            ManagedAlternatorVoltageSetting, // Заданное напряжение управляемого генератора
            AlternatorTemperature, // Температура управляемого генератора
            GeneratorCurrent, // Ток генератора
            AlternatorChargeValue, // Величина заряда генератора
            SensorsSupplyVoltage, // Напряжение питания датчиков
            CalculatorResetsCount, // Число обнулений компьютера 
            RequestToControlElectromagneticValveOfOilPump, // Требование управления электромагнитным клапаном масляного насоса
            PowerRelayState, // Состояние реле мощности 
            StateOfControlOfStarterControlRelay, // Состояние управления реле управления стартером
            CalculatorTemperature, // Температура компьютера
            ModeledCoolantTemperature, // Смоделированная температура охлаждающей жидкости
            OilPumpFunctioningMode, // Режим работы масляного насоса
            SupplyVoltage, // Напряжение питания

            CalculatorSupplyVoltage, //Напряжение питания компьютера
            StopSignalContact, //Контакт стоп-сигнала
            ElectricValvesSupplyRelayState, //Состояние реле питания электроклапанов
            RecirculationPumpRelaystate, // состояние реле насоса рециркуляции

            LightingControl_SwitchInPosition3, // Управление освещением - переключатель в 3 положении
            LightingControl_SwitchInPosition4, // Управление освещением - переключатель в 4 положении
            DippedAndHighBeamSwitch, // Переключатель ближнего / дальнего света
            HighBeamImpulseSwitch, // Импульсный переключатель фар дальнего света
            FogLightsState_Minus, // Состояние противотуманных фар / фонарей (плюс)
            FogLightsState_Plus, // Состояние противотуманных фар / фонарей (минус)
            LightSwitchButtonOn, // Включение кнопки переключателя света
            TurnIndicatorsControl, // Управление указателями поворота

            SpeedRegulationORLimitationSelection, // Выбор регулирование/ограничение скорости
            IncreasingSelectedSpeed, // Увеличение устанавливаемой скорости
            DecreasingSelectedSpeed, // Уменьшение устанавливаемой скорости
            WakingUpCruiseControl, // Вывод круиз-контроля из режима сна

            RadioScanRegulator, // Радиоприёмник: счётчик круглого регулятора "Scan"
            SourceToTelephoneSwitch, // Переключатель "Источник/Телефон"

            SoundAlarm, // Звуковая сигнализация
            BoardComputerControl, // Управление бортовым компьютером
            SingleTimeSwitchOfWindscreenWiper, // Управление разовым включением переднего стеклоочистителя
            ScreenWiperSwitchControl, // Управление кнопкой переключателя стеклоочистителя

            MaintainingDueToThePresenceOfTheIgnition, // Maintaining due to the presence of the ignition
            MaintainingRequestForAlarm, // Maintaining request for alarm
            MaintainingDueToALocatingOfAVehicleBeingInProgress, // Maintaining due to locating of the vehicle being in progress
            MaintainginForKeyInIgnitionWarning, // Maintaining for key in ignition warning
            MaintainingDueToThePresenceOfTheWarning, // Maintaining due to the presence of the warning
            MaintainingDueToTheHeadlampFlashBeingActivated, // Maintaining due to the headlamp flash being activated // 
            MaintainingDueToTheSideLightsBeingOn, // Maintaining due to the side lights being on
            MaintainingDueToTheBootLightBeingOn, // Maintaining due to the boot light being on
            MaintainingDueToADiagnosticSessionOrDownloadingBeingInProgress, // Maintaining due to a diagnostic session or downloading being in progress
            MaintainingDueToTheSlidingDoorMovingOrBeingStopped, // Maintaining due to the sliding side door moving or being stopped
            MaintainingDueToTheMovementOfSteeringColumn, // Maintainig due to the movement of the steering column
            MaintainingDueToMovingOrFoldingOfTheExteriorMirrors, // Maintaining due to movement of the exterior mirrors or folding the exterior mirrors being in progress
            MaintainingForSeatMovement, // Maintaining for seat movement 
            MaintainingForElectricWindowMovement, // Maintaining for electric window movement
            MaintainingForSunroofMovement, // Maintaining for sunroof movement
            MaintainingForHighHeadVisionMirrorMovement, // Maintaining for High Head Vision mirror movement
            MaintainingDueToRecallingOfTheDrivingPositionMemoryBeingInProgress, // Maintaining due to recalling of the driving position memory being in progress
            MaintainingDueToMemorisingOfTheDrivingPositionBeingInProgress, // Maintaining due to memorising of the driving position being in progress
            MaintainingDueToWelcomeFunctionBeingInProgress, // Maintaining due to the welcome function being in progress
            MaintainingDueToThermalPrePostConditioningBeingInProgress, // Maintaining due to thermal pre-post conditioning being in progress
            MaintainingDueToTheHornBeingActive, // Maintaining due to the horn being active
            MaintainingForAudioEquipmentOrTelematicsActivity, // Maintaining for audio equipment or telematics activity
            MaintainingDueToHeightAdjustmentBeingInProgress, // Maintaining due to height adjustment being in progress
            MaintainingDueToDisplayingOfTheStatusOfTheElectricEmergencyBrakeBeingInProgress, // Maintaining due to displaying of the status of the electric emergency brake being in progress
            MaintainingDueToMovingMotorisedBootOrTailgate, // Maintaining due to moving motorised boot or tailgate
            MaintainingDueToAmbientOrWelcomeLightingBeingOn, // Maintaining due to ambient or welcome lighting being on
            MaintenanceDueToTheCentralisedPowerSwitchPositivePlusCpc, // Maintenance due to the centralised power switch positive (+CPC)
            MaintenanceByTheServiceModuleMds, // Maintenance by the Service Module (MDS)

            RearLeftSeatBeltFastened, // Rear left seat belt fastened
            RearRightSeatBeltFastened, // Rear right seat belt fastened
            RearCentreSeatBeltFastened, // Rear centre seat belt fastened
            Row3LeftHandSeatBeltBuckled, // Row 3 left-hand seat belt buckled
            Row3RightHandSeatBeltBuckled, // Row 3 right-hand seat belt buckled

            RearSillLightingControl, // Rear doors sills lighting control

            RightHandVehicleAndTrailerSidelampsControl, // Управление включением габаритных огней автомобиля с правой стороны и прицепа // Vehicle and trailer RH side lamps illumination control

            LeftHandVehicleAndTrailerSidelampsControl, // Управление включением габаритных огней автомобиля с левой стороны и прицепа // Vehicle and trailer LH side lamps illumination control
            DippedBeamSwitchOnCommand, // Команда включения фар ближнего света // Request to switch on the dipped headlamps
            HeadlampFlashRequest, // Команда на сигнализацию светом фар // Headlamp flash request
            DippedBeamHighBeamHeadlampsInversion, // Переключение фар ближнего/дальнего света // Dipped beam / main beam headlamps inversion
            FrontDaylightsSwitchOnOrHeadlampWasher, // Включение передних фар дневного света или омывателей фар // Control of the dedicated front daytime-running lamps or of headlamp wash
            NumberPlateLightingControl, // Управление включением освещения регистрационного знака // Number plate lights lighting command

            HazardWarningLampSwitchOnRequest, // Hazard warning laps illumination request // Запрос на включение аварийной сигнализации
            LeftHandTurnIndicatorIndicationRequest, // LH indicator illumination request // Запрос на включение указателя поворота слева
            VehicleAndTrailerRightHandIndicatorIllumicationControl, // Vehicle and trailer RH indicator illumination control // Управление активизацией правых указателей поворота автомобиля и прицепа
            VehicleAndTrailerLeftHandIndicatorIllumicationControl, // Vehicle and trailer LH indicator illumination control // Управление активизацией левых указателей поворота автомобиля и прицепа 
            RightHandTurnIndicatorIndicationRequest, // RH indicator illumination request // Запрос на включение указателей поворота справа
            ReverseLampsIllumunationControl, // Reversing lamps illumination control // Управление включением фонарей заднего хода
            StopLampsIlluminationControl, // Stop lamps illumination control // Управление включением фонарей стоп
            RequestForHornSound, // Request for horns // Команда на включение звукового сигнала

            TransponderLabelRecognized, //	Наклейка транспондера не указана	Transponder label recognized	
            NumberOfHichFrequencyRemoteControlsLearned, //	Число высокочастотных ПДУ, занесённых в память	Number of high frequency remote control commands learned	
            HighFrequencyRemoteControlDesynchronized, //	Высокочастотный ПДУ синхронизирован	Remote desynchronized high frequency control	
            BSIMatchedToECU, //	СМ синхронизирован с калькулятором контроля двигателя	BSI matched to the engine management ECU	
            BsiHasNoEngineImmobilizerCode, //	В BSI не записан код иммобилайзера (ADC)	BSI with no engine immobiliser code (coded engine immobilizer)	
            PresenceOfTheKey, //	Присустствие ключа	Presence of the key	
            HighFrequencyRemoteControlBatteryFlat, //	Элемент питания высокочастотного ПДУ разряжен	High frequency remote control battery flat	
            NumberOfTransponderAuthenticationAttempts, //	Порядковый номер попытки аутентификации транспондера	Number of attempts to authenticate the transponder
            NumberOfTransponderIdentifictionAttempts, //	Порядковый номер попытки идентификации транспондера	Number of attempts to identify the transponder
            EmissionOfTheEncryptedDatatoEngineECU, //	Передача зашифрованных данных на калькулятор двигателя	mission of the encrypted data to the engine ECU
            BSIReadyForProgramming, //	СМ готов для выполнения процедуры распознавания	BSI ready for programming

            PresenceOfParkingAssistance, // Presence of parking assistance
            AuxiliaryInput1, // Auxiliary input 1
            AuxiliaryInput2, // Auxiliary input 2

            MeasuredCurrent, // Measured current
            ThermalEngineStatus, // Thermal engine status
            SteeringStatus, // Steering status
            SteeringWheelAngleSensorType, // Type of steering wheel angle sensor

            PersonalizationMenu, //	Personalization menu	Меню персонализации	
            DisplayOfTheDrivingPositionClearanceOption, //	Display of the "Driving position clearance" option	Вывод информации опции "Разблокировка места водителя"	
            DisplayOfTheTotalNearbyObstacleDetectionOption, //	Display of the "Total nearby obstacle detection" option	Вывод информации опции "Определение расстояния до препятствия"	
            DisplayOfTheEngagementOfTheRearWipingWhenReversingOption, //	Display of the "Engagement of the rear wiping whn reversing" option	Вывод информации опции "Включение заднего дворника при движении задним ходом"	
            DisplayOfTheAutomaticHeadlampIlluminationOption, //	Display of the "Automatic headlamp illumunation" option	Вывод информации опции "Автоматическое включение света"	
            DisplayOfTheFollowMeHomeLightingOption, //	Display of the "Follow me home lighting" option	Вывод информации опции "Освещение сопровождения"	
            DisplayOfTheDirectionlaHeadlampOption, //	Display of the "Directional headlamp" option	Вывод информации опции "Поворотные фары"	
            DisplayOfTheAmbientLightingOption, //	Display of the "Ambient lighting" option	Вывод информации опции "Внутренее освещение автомобиля"	
            DisplayOfThePermanentLockingOfTheBootOption, //	Display of the "Permanent locking of the boot" option	Вывод информации опции "Постоянное запирание багажника"	
            DisplayOfTheLockingOfTheDriversDoorOnlyOption, //	Display of the "Locking of the driver's door only" option	Вывод информации опции "Запирание только двери водителя"	
            DisplayOfTheLockingOfTheDoorsWhenYouMoveAwayOption, //	Display of the "Locking of the doors when you move away" option	Вывод информации опции "Дистанционное запирание дверей"	
            DisplayOfTheLockingDeadlockingModeOption, //	Display of the "Locking/deadlocking mode" option	Вывод информации опции "Режим запирания / суперблокировки"	
            DisplayOfTheDeactivationAutomaticElectricSecondaryBrakeOption, //	Display of the "Deactivation automatic electric secondary brake" option	Вывод информации опции "Отключение электрического автоматического вторичного тормоза-замедлителя"	
            DisplayOfTheDaylightLightsOption, //	Display of the "Daylight lights" option	Вывод информации опции "Свет, // включаемый в дневное время"	
            AirRecyclingMotorPosition, //	Air recycling motor position	Положение привода заслонки рециркуляции воздуха	
            AirConditioningRequest, //	Команда на включение кондиционера воздуха (АС)	Air conditioning request (AC)	
            AutomaticRegulationCommand, //	Команда на включение автоматического регулирования (AUTO)	Automatic regulation request (AUTO)	
            RecirculationRequest, //	Команда на включение рециркуляции	Recirculation request	
            RearDeicingCommand, //	Запрос на активизацию обогрева заднего стекла и зеркал заднего вида	Rear screeen and door mirror de-icing request	
            AirDistributionRequest_Deicing, //	Команда распределения воздуха - удаления инея со стёкол	Air distribution request - De-icing	
            AirDistributionRequest_Ventilation, //	Команда распределения воздуха - вентиляции	Air distribution request - Ventilation	
            AirDistributionRequest_Footwells, //	Команда распределения воздуха - подачи воздуха к ногам	Air distribution request - Footwells	
            MonozoneRegulationRequest, //	Команда на однозонное регулирование	Monozone regulation request	
            NumberOfDaysOfUseOfTheTuner, //	Number of days of use of the tuner	Число дней использования тюнера	
            NumberOfDaysOfUseCDPlayer, //	Number of days of use of the CD Player	Число дней использования проигрывателя CD	
            Aerial1RadioQuality, //	Quality of radio reception from the 1 aerial	Качество радиоприёма с помощью антенны 1	
            LevelOfAmFmIncomingSignal, //	Level of incoming radio signal from FM1 or AM	Уровень входного сигнала радио с помощью антенны 2	
            Aerial2RadioQuality, //	Quality of radio reception from the 2 aerial	Качество радиоприёма с помощью антенны 2	
            FM2IncomingSignalLevel, //	Level of the incoming radio signal from FM2	Уровень входного раиосигнала от FM2	

            TelecodingToolSignature, // Telecoding tool signature
            TelecodingDate, // Telecoding date

            Vin, // Vehicle vin Code

            NIVDELEST3, // Какие-то сбросы нагрузки
            NIVDELEST4, 
            NIVDELEST7,
            NIVDELEST8,

            FilteredOilLevel,
            OilLevelWarning,
            PsaReference,
            TelecodingsCount, // оно и есть
            LowBatteryVoltageAlert,

            SportSuspensionRequest,
            PredefinedRadioStationsSelection,
            BonnetOpen,
            LockingLocksCommand,
            UnlockingLocksCommand,
            Deadlockingcommand,

            CeilingLightingRequest,
            ProductionDate,

            SpeedLimitationOrRegulation_PauseState,
        }
}
