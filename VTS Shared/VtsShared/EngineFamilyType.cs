using System;

namespace VTS.Shared
{
    public enum EngineFamilyType
    {
        Douvrin,
        X,
        TU,
        EC,
        Prince,
        TUD,
        XU,
        XUD,
        EWDW,
        DK,
        DV,
        PRV,
        ES,
        XB,
        XC,
        XD,
        XM,
        XN,

        // http://c-elysee-club.com.ua/viewtopic.php?f=7&t=51
        EB, // new economy 3-cyl engines for c-elysee and others

        DT, // 6-cyl for C5 and C6

        // Opel families beginning from 100
        N = 100, // Opel engine family witn number only in engine ID
        A,
        B,
        C,
        D,
        E,
        F,
        K,
        M,
        S,
        X_Opel,
        Y,
        Z,

        // Toyota familiies used for small cars like C1
        KR = 200,

        // Mitsubishi Motors Corporation diesel Engines.
        // http://en.wikipedia.org/wiki/Mitsubishi_RVR
        MMC = 260,
    }
}
