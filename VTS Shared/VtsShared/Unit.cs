using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTS.Shared
{
    public enum Unit
    {
        Unsupported,
        NoUnits,

        Percent,
        Bar,
        Millibar,

        Rpm,
        Degree,
        CelsiusDegree,

        Gramm,

        Millilitre,
        Litre = 10,
        LitrePer100Km,

        Ohm,
        Milliohm,
        Volt,
        Millivolt,
        Amper,
        Milliamper,

        Km,
        Mile,
        KmH = 20,
        MpH,

        Sec,
        Millisec,
        Minute,
        Hour,

        Mm3PerStroke = 26,
        MgPerStroke,
        M3PerHour,

        Lux,
        NewtonMetre = 30,
        CelsiusDegreePerSecond,

        /// <summary>
        /// Air volume flow
        /// </summary>
        KgPerHour,

        /// <summary>
        /// Cubic millimeters per second.
        /// </summary>
        Mm3PerSecond
    }
}
