using System;

namespace VTS.Shared
{
    public enum InjectionType
    {
        Unknown,

        /// <summary>
        // Petrol common rail
        // http://en.wikipedia.org/wiki/Multi_Point_Injection#Direct_injection
        /// </summary>
        Direct,

        // Diesel common rail
        CommonRail,

        // Petrol injector
        Injector,

        // For diesels with pre-chamber
        Indirect,

        Carburettor
    }
}
