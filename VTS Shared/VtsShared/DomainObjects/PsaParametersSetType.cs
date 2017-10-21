using System;

namespace VTS.Shared.DomainObjects
{
    public enum PsaParametersSetType
    {
        Unsupported,

        /// <summary>
        /// If graph data has been collected
        /// </summary>
        Mixed,

        /// <summary>
        /// Основные параметры 1
        /// </summary>
        BasicParameters1,

        /// <summary>
        /// Основные параметры 2
        /// </summary>
        BasicParameters2,

        /// <summary>
        /// Информация об электросистеме
        /// </summary>
        ElectricSystemInfo,

        /// <summary>
        /// Информация о вождении
        /// </summary>
        DrivingInfo = 5,

        /// <summary>
        /// FAP filter
        /// </summary>
        ParticleFilterInfo = 6,

        /// <summary>
        /// Впрыск топлива и система очистки
        /// </summary>
        InjectionInformation = 7,

        /// <summary>
        /// Внутреннее состояние ЖК-экрана одометра
        /// </summary>
        OdometerScreenState = 8,

        /// <summary>
        /// Параметры панели управления
        /// </summary>
        ControlPanelParameters = 9,

        /// <summary>
        /// Электропитания основных блоков.
        /// </summary>
        BasicBlocksSupply = 10,

        /// <summary>
        /// Климатическая установка
        /// </summary>
        ClimateDevices = 11,

        /// <summary>
        /// Путевые данные
        /// </summary>
        TripData = 12,

        /// <summary>
        /// Впуск воздуха
        /// </summary>
        AirIntake = 13,

        /// <summary>
        /// Зажигание
        /// </summary>
        Ignition = 14,

        /// <summary>
        /// Система очистки
        /// </summary>
        Purification = 15,

        /// <summary>
        /// Количество топливной присадки
        /// </summary>
        FapAdditiveParameters = 16,

        /// <summary>
        /// Различные типы электропитания и иммобилайзер
        /// </summary>
        DifferentElectricSupplyAndImmobilizer = 17,

        /// <summary>
        /// Охлаждение двигателя и климатическая установка
        /// </summary>
        EngineCoolingAndClimateUnit = 18,

        /// <summary>
        /// Динамические виды информации
        /// </summary>
        DynamicInformation = 19,

        /// <summary>
        /// Данные электронасоса
        /// </summary>
        ElectricPumpData = 20,

        /// <summary>
        /// Обогрев и вентиляция
        /// </summary>
        HeatingAndVentilation = 21,

        /// <summary>
        /// Крышка топливного бака
        /// </summary>
        FuelTankCap = 22,

        /// <summary>
        /// Состояние датчиков и вентилятора печки
        /// </summary>
        HeaterSensorsAndFanState = 23,

        /// <summary>
        /// Давление в колёсах
        /// </summary>
        TyresPressure,

        /// <summary>
        /// освещение
        /// </summary>
        Lighting,

        /// <summary>
        /// Сигнализация
        /// </summary>
        Alarm,

        /// <summary>
        /// замки
        /// </summary>
        Locks,

        /// <summary>
        /// Показания измерительных приборов
        /// </summary>
        MeasuredData,

        /// <summary>
        /// Инструментальная индикация
        /// </summary>
        InstrumentalIndication,

        /// <summary>
        /// Констроль видимости
        /// </summary>
        VisibilityControl,

        /// <summary>
        /// Двери
        /// </summary>
        Doors,

        /// <summary>
        /// Состояние элементов панели управления
        /// </summary>
        ControlPanelElementsState,

        /// <summary>
        /// Переменные параметры
        /// </summary>
        Variables,

        /// <summary>
        /// Состояния агрегатов
        /// </summary>
        AggregatesState,

        /// <summary>
        /// Уровень сброса нагрузки (??)
        /// </summary>
        LoadSheddingLevel,

        /// <summary>
        /// Общий потреблённый уровень присадки
        /// </summary>
        TotalAdditiveAmountDepositedCounter1,
        TotalAdditiveAmountInjectedCounter2,

        VentilationDistribution,

        /// <summary>
        /// Инструментарий - измерения
        /// </summary>
        InstrumentationMeasurements,

        /// <summary>
        /// Конфигурация радио.
        /// @P95862-THESAU@\*
        /// </summary>
        RadioConfiguration,

        ParametersSet1,

        /// <summary>
        /// Дополнительные устройства и безопасность.
        /// @P115128-THESAU@\*
        /// </summary>
        AdditionalGearAndSafety,

        /// <summary>
        /// @P90030-THESAU@\\*
        /// </summary>
        ControlButtonsStates,

        /// <summary>
        /// @P298-THESAU@\*@*\
        /// </summary>
        SoftwareVersions,

        /// <summary>
        /// @P106646-THESAU
        /// </summary>
        AudioAndComputer,

        UnitsAndNotations,

        BoardComputer,

        RadioButtons,

        BasicParameters3,

        CommandsAndRelays,

        ParametersSet2,

        Immobilizer,

        Keys,

        ParametersSet3,

        MixingState
    }
}
