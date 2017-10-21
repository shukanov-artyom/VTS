using System;

namespace VTS.Shared
{
    public enum EngineType
    {
        // Douvrin family 
        // http://en.wikipedia.org/wiki/Douvrin_engine
        Douvrin20 = 10,
        Douvrin22,
        Douvrin21Diesel,

        // X Family
        // http://en.wikipedia.org/wiki/PSA_X_engine
        XV8 = 100,

        XW3,
        XW7,
        XW3S,

        XZ5,
        XZ6, // ??
        XZ7R,

        XY,
        XY6B,
        XY6A, // ?
        XY7,
        XY8,
        XYR,

        // TU Family
        // http://en.wikipedia.org/wiki/PSA_TU_engine
        TU9MZ = 200, //TU9 M/Z
        TU9K, //TU9/K

        TU1F2K, //TU1 F2/K
        TU1JP, //TU1 JP
        TU1MZ, //TU1 M/Z
        TU1K, //TU1/K

        TU2A, //TU2 Â
        TU24AK, //TU24 Â/K

        TU3A, //TU3 A
        TU3AK, //TU3 A/K
        TU3F2K, //TU3 F2/K
        TU3FJ2K, //TU3 FJ2/K
        TU3FJ2Z, //TU3 FJ2/Z
        TU3JP, //TU3 JP
        TU3MZ, //TU3 M/Z
        TU3S, //TU3 S

        ET3J4, //ET3 J4

        TU5J2L3, //TU5 J2/L3
        TU5J4, //TU5 J4
        TU5JP, //TU5 JP - 90 PS (88 hp/66 kW)	FI catalyst
        TU5JPPlus, //TU5 JP - 96 PS
        TU5JP_, //TU5 JP - 98 PS (97 hp/72 kW)	FI catalyst
        TU5JP4, //TU5 JP4
        TU5JP4S, //TU5 JP4
        TU1A,

        // EC Family
        // http://en.wikipedia.org/wiki/List_of_PSA_engines#EC
        EC5 = 300,
        EC8,

        // Prince - GIVE MOST ATTENTION!!! MODERN ENGINES
        // http://en.wikipedia.org/wiki/Prince_engine
        EP3 = 400, // 95
        EP6, // 120 - peugeot
        EP6C, // 120 - citroen
        EP6DT, // 150
        EP6DT140, // EP6DT - 140
        EP6DTS, // 175
        EP6CDTX, // 200
        EP6CDT, // For RCZ

        // TUD
        // http://en.wikipedia.org/wiki/List_of_PSA_engines#TUD
        TUD3 = 500,
        TUD5,

        // XU Family
        // http://en.wikipedia.org/wiki/PSA_XU
        XU5J = 550,
        XU51C,
        XU52C,
        XU5C,
        XU5M3Z,

        XU7JB,
        XU7LFW,
        XU7JP,
        XU7JP4,

        XU8T200,
        XU8T300,
        XU8TEvo1,
        XU8TEvo2,

        XU92C105,
        XU92C110,
        XU94C,
        XU9J1Z,
        XU9J2,
        XU9JAK,
        XU9JAZ,
        XU9J4D6CL,
        XU9J4ZDFW,

        XU102C, // 115 PS (85 kW; 113 hp) 2-bbl carb Citroën XM 2.0 8v 
        XU10J2U, // Both 109 and 110
        XU10J2CLRFX, // 123 PS (90 kW; 121 hp)
        XU10J2, // 130 PS (96 kW; 128 hp) FI Citroën Xantia I 2.0i
        XU10J2TERGY, // 145 PS (107 kW; 143 hp) turbo catalyst Citroën XM 2.0 Turbo CT
        XU10J2TERGX, // 150 PS (110 kW; 148 hp) turbo catalyst Citroën XM 2.0 Turbo CT
        XU10J4DZ, // 150 PS (110 kW; 148 hp) 16-valve DOHC catalyst Citroën ZX Coupé 2.0 16v
        XU10J4RRFV, // 135 PS (99 kW; 133 hp) 16-valve DOHC catalyst Citroën Xantia
        XU10J4RL3RFV, // 132 PS (97 kW; 130 hp) 16-valve DOHC catalyst Peugeot 306 (97-03) 
        XU10J4RSL3FS, // 167 PS (123 kW; 165 hp) 16-valve DOHC catalyst Citroën ZX Dakar 2.0 16v
        XU10J4TE, // 200 PS (147 kW; 197 hp) 16-valve DOHC turbo catalyst Peugeot 405 T16 2.0 16v Turbo 4x4 
        XU10J4LZRFY, // 155 PS (114 kW; 153 hp) 16-valve DOHC catalyst Peugeot 405 2.0 Mi16
        XU10M, // 130 PS (96 kW; 128 hp) FI  

        // XUD Family
        // http://en.wikipedia.org/wiki/PSA_XUD_engine
        XUD7TK = 600, //- median
        XUD7TE,
        XUD7K,
        XUD7Z,

        XUD9,
        XUD9A,
        XUD9TEL,
        XUD9SD,
        XUD9TEY,
        XUD9Z,

        XUD11A,
        XUD11ATE,
        XUD11BTE,

        // EW/DW - MAX ATTENTION!!
        // http://en.wikipedia.org/wiki/PSA_EW/DW_engine
        EW7J4 = 700,
        EW7A,

        EW10D = 710,
        EW10J4RFN,
        EW10J4RFR,
        EW10J4S,
        EW10A,

        EW12J4 = 720,

        DW8 = 725,
        DW8B,

        DW10ATED = 740, // Is it the same with DW10ATED4?
        DW10ATED4,
        DW10TD,
        DW10BTED4,
        DW10C,
        DW10CTED4,
        DW10BTED,

        DW12UTED = 770,
        DW12TED4,
        DW12MTED4,
        DW12C,

        // DK Family
        // http://en.wikipedia.org/wiki/List_of_PSA_engines#DK
        DK5 = 800,
        DK5ATE,

        // DV Family - ATTENTION! HAS MODERN ENGINES
        // http://en.wikipedia.org/wiki/Ford_DLD_engine
        DV4TD = 900,
        DV4TED4,

        DV6ATED4,
        DV6B,
        DV6TED4,
        DV6C,
        DV6D,

        // additional - ZSD (PUMA) engines http://en.wikipedia.org/wiki/Ford_Duratorq_engine#ZSD_.28.22Puma.22.29
        ZSD420, // ZSD-420
        ZSD422, // ZSD-422
        ZSD424, // ZSD-424

        DV4C,
        DV4TED,

        // PRV Family - before 1998
        // http://en.wikipedia.org/wiki/PRV_engine
        ZM = 1000,
        ZN,
        ZP,

        // ES Family
        // http://en.wikipedia.org/wiki/PSA_ES_engine
        ES9J4L7X = 1100,
        ES9J4S,
        ES9IA,

        // XB Family
        // http://en.wikipedia.org/wiki/List_of_PSA_engines#XB
        XB2 = 1200,
        XB5,

        // XC Family
        // http://en.wikipedia.org/wiki/List_of_PSA_engines#XC
        XC5 = 1300, // — 1.6 L (1618 cc)
        XC6, //  1.6 L (1618 cc)
        XC7, // — 1.6 L (1618 cc)
        XCKF1, //  — 1.6 L (1618 cc)
        XCKF2, //— 1.6 L (1618 cc)

        // XD Family
        // http://en.wikipedia.org/wiki/List_of_PSA_engines#XD
        XD75 = 1400,
        XD85, // — 1.8 L (1,816 cc)
        XD88, //— 1.9 L (1,948 cc)
        XD90, // — 2.1 L (2,112 cc)
        XD2, //— 2.3 L (2,304 cc)
        XD2S, //— 2.3 L (2,304 cc), turbocharged
        XD3, //— 2.5 L (2,498 cc)
        XD3TTE, //— 2.5 L (2,498 cc), turbocharged

        // XM Family
        // http://en.wikipedia.org/wiki/List_of_PSA_engines#XM
        XM7 = 1500, //— 1.8 L (1796 cc)
        XMKF5, //— 1.8 L (1796 cc)
        XMKF6, //— 1.8 L (1796 cc)


        // XN Family
        // http://en.wikipedia.org/wiki/List_of_PSA_engines#XN
        XN1 = 1600, //— 2.0 L (1,971 cc)
        XN2, //— 2.0 L (1,971 cc)
        XN6, //— 2.0 L (1,971 cc)
        XN8, //— 2.0 L (1,971 cc), for the Peugeot P4 only[2]


        // TDV 
        // http://en.wikipedia.org/wiki/Ford_AJD-V6/PSA_DT17
        // Ford engines - for new C5 and C6
        TDV6 = 1700,

        // EB series
        // http://c-elysee-club.com.ua/viewtopic.php?f=7&t=51
        EB2M = 1750,
        EB0,

        // 6-cylinder diesel for C6 and C5
        DT20C = 1770,

        // ==================================
        // OPEL ENGINES
        // ==================================

        // Opel engines start from 2000 number

        N22 = 2000, //22
        N23, //23
        N25, //25
        N10S, //10S
        N12N, //12N
        N12NC, //12NC
        N12NV, //12NV
        N12NZ, //12NZ
        N12S, //12S
        N12ST, //12ST
        N13N, //13N
        N13NB, //13NB
        N13S, //13S
        N13SB, //13SB
        N14NV, //14NV
        N14SE, //14SE
        N15D, //15D
        N15TD, //15TD
        N16D, //16D
        N16DA, //16DA
        N16LZ2, //16LZ2
        N16N, //16N
        N16NZ2, //16NZ2
        N16NZR, //16NZR
        N16S, //16S
        N16SH, //16SH
        N16SV, //16SV
        N17D, //17D
        N17DR, //17DR
        N17TD, //17TD
        N18E, //18E
        N18SE, //18SE
        N20NE, //20NE
        N20SEH, //20SEH
        N25TD, //25TD
        N31TD, //31TD

        // Opel A engines

        A10XER = 2050, // http://www.autopro.spb.ru/showeng.afp?engine=100
        A10XEP, // http://www.autopro.spb.ru/showeng.afp?engine=100
        A12XEL, // http://www.autopro.spb.ru/showeng.afp?engine=77
        A12XEP, // http://www.autopro.spb.ru/showeng.afp?engine=84
        A12XER, // http://www.autopro.spb.ru/showeng.afp?engine=78
        A13DTE, // http://www.opel-infos.de/kurzinfo_motoren_astra_j_a13dte.html
        A13DTJ, // http://www.autopro.spb.ru/showeng.afp?engine=72
        A13FD,
        A14FC,
        A14FP,
        A14NEL, // http://www.autopro.spb.ru/showeng.afp?engine=103
        A14NET, // http://www.autopro.spb.ru/showeng.afp?engine=70
        A14XEL, // http://www.autopro.spb.ru/showeng.afp?engine=79
        A14XER, // http://www.autopro.spb.ru/showeng.afp?engine=69
        A14XFL,
        A16FDH,
        A16FDL,
        A16LET, // http://www.autopro.spb.ru/showeng.afp?engine=68
        A16LEL, // http://www.autopro.spb.ru/showeng.afp?engine=80
        A16LER, // http://www.autopro.spb.ru/showeng.afp?engine=51
        A16XER, // http://www.autopro.spb.ru/showeng.afp?engine=71
        A16XHT, // http://www.autopro.spb.ru/showeng.afp?engine=122
        A17DT, // http://www.autopro.spb.ru/showeng.afp?engine=102
        A17DTC,
        A17DTE,
        A17DTF,
        A17DTJ, // http://www.autopro.spb.ru/showeng.afp?engine=73
        A17DTL,
        A17DTN,
        A17DTR, // http://www.autopro.spb.ru/showeng.afp?engine=74
        A17DTS, // http://www.autopro.spb.ru/showeng.afp?engine=81
        A18XER, // http://www.autopro.spb.ru/showeng.afp?engine=85
        A20DTC, // http://www.autopro.spb.ru/showeng.afp?engine=89
        A20DTH, // http://www.autopro.spb.ru/showeng.afp?engine=75
        A20DTR, // http://www.autopro.spb.ru/showeng.afp?engine=104
        A20DTJ, // http://www.autopro.spb.ru/showeng.afp?engine=90
        A20FD,
        A20NFT,
        A20NHT, // http://www.autopro.spb.ru/showeng.afp?engine=86
        A22DM, // http://www.autopro.spb.ru/showeng.afp?engine=110
        A22DMH, // http://www.autopro.spb.ru/showeng.afp?engine=111
        A24XE, // http://www.autopro.spb.ru/showeng.afp?engine=106
        A24XF,
        A28NET, // http://www.autopro.spb.ru/showeng.afp?engine=87
        A28NER, // http://www.autopro.spb.ru/showeng.afp?engine=88
        A30XF,
        A30XH, // http://www.autopro.spb.ru/showeng.afp?engine=112

        B14NEL, // http://www.autopro.spb.ru/showeng.afp?engine=123

        C12NZ = 2100, // http://www.autopro.spb.ru/showeng.afp?engine=121
        C13N,
        C14NZ, // http://www.autopro.spb.ru/showeng.afp?engine=98
        C14SE, // http://www.autopro.spb.ru/showeng.afp?engine=97
        C14SEL, // http://www.autopro.spb.ru/showeng.afp?engine=95
        C16LZ,
        C16NZ, // http://www.autopro.spb.ru/showeng.afp?engine=114
        C16SE, // http://www.autopro.spb.ru/showeng.afp?engine=115
        C16SEL,
        C16XE, // http://www.autopro.spb.ru/showeng.afp?engine=96
        C18NE,
        C18NZ, // http://www.autopro.spb.ru/showeng.afp?engine=117
        C18SEL,
        C18XE, // http://www.autopro.spb.ru/showeng.afp?engine=119
        C18XEL,
        C20LET,
        C20NE, // http://www.autopro.spb.ru/showeng.afp?engine=118
        C20XE, // http://www.autopro.spb.ru/showeng.afp?engine=120
        C25XE,

        D13A = 2130,

        E12GV = 2140,
        E16NZ,
        E16SE,
        E18NV,

        F8Q600 = 2150, //F8Q-600
        F8Q606, //F8Q-606

        K10B = 2160,
        K12B,

        M24 = 2170,
        M25,
        M26,
        M29,
        M79,
        MC8,
        MD0,
        MG1,
        MG3,
        MG4,
        MG7,
        ML5,
        MV4,

        S18NV = 2200,
        S8U758, //S8U-758
        S8U780, //S8U-780
        S8U782, //S8U-782

        // Opel X engines

        X10XE = 2230, // http://www.autopro.spb.ru/showeng.afp?engine=91
        X12SZ, // http://www.autopro.spb.ru/showeng.afp?engine=99
        X12XE, // http://www.autopro.spb.ru/showeng.afp?engine=3
        X14NZ, // http://www.autopro.spb.ru/showeng.afp?engine=113
        X14SZ,
        X14XE, // http://www.autopro.spb.ru/showeng.afp?engine=4
        X15D,
        X16NE,
        X16SZ, // http://www.autopro.spb.ru/showeng.afp?engine=116
        X16SZR, // http://www.autopro.spb.ru/showeng.afp?engine=5
        X16XEL, // http://www.autopro.spb.ru/showeng.afp?engine=6
        X17DTL, // http://www.autopro.spb.ru/showeng.afp?engine=7
        X18XE, // http://www.autopro.spb.ru/showeng.afp?engine=101
        X18XE1, // http://www.autopro.spb.ru/showeng.afp?engine=8
        X20DTL, // http://www.autopro.spb.ru/showeng.afp?engine=9
        X20XER, // http://www.autopro.spb.ru/showeng.afp?engine=10
        X20XEV, // http://www.autopro.spb.ru/showeng.afp?engine=82
        X25XE, // http://www.autopro.spb.ru/showeng.afp?engine=93

        Y13DTH = 2300, // http://www.autopro.spb.ru/showeng.afp?engine=28
        Y16XE, // http://www.autopro.spb.ru/showeng.afp?engine=92
        Y17DT, // http://www.autopro.spb.ru/showeng.afp?engine=11
        Y17DTL, // http://www.autopro.spb.ru/showeng.afp?engine=45
        Y20DTH,
        Y20DTJ,
        Y20DTL, // http://www.autopro.spb.ru/showeng.afp?engine=13
        Y22DTR, // http://www.autopro.spb.ru/showeng.afp?engine=59
        Y26SE, // http://www.autopro.spb.ru/showeng.afp?engine=94
        Y30DT, // http://www.autopro.spb.ru/showeng.afp?engine=60

        // Opel Z engines 

        Z10XE = 2350, // http://www.autopro.spb.ru/showeng.afp?engine=46
        Z10XEP, // http://www.autopro.spb.ru/showeng.afp?engine=47
        Z12XE, // http://www.autopro.spb.ru/showeng.afp?engine=15
        Z12XEP, // http://www.autopro.spb.ru/showeng.afp?engine=48
        Z13DT, // http://www.autopro.spb.ru/showeng.afp?engine=49
        Z13DTH, // http://www.autopro.spb.ru/showeng.afp?engine=52
        Z13DTI, // http://www.autopro.spb.ru/showeng.afp?engine=53
        Z13DTJ, // http://www.autopro.spb.ru/showeng.afp?engine=54
        Z13DTR, // http://www.autopro.spb.ru/showeng.afp?engine=55
        Z14XE, // http://www.autopro.spb.ru/showeng.afp?engine=16
        Z14XEL, // http://www.autopro.spb.ru/showeng.afp?engine=29
        Z14XEP, // http://www.autopro.spb.ru/showeng.afp?engine=17
        Z16LET, // http://www.autopro.spb.ru/showeng.afp?engine=30
        Z16LEL, // http://www.autopro.spb.ru/showeng.afp?engine=56
        Z16LER, // http://www.autopro.spb.ru/showeng.afp?engine=57
        Z16SE, // http://www.autopro.spb.ru/showeng.afp?engine=18
        Z16XE, // http://www.autopro.spb.ru/showeng.afp?engine=19
        Z16XE1, // http://www.autopro.spb.ru/showeng.afp?engine=31
        Z16XEP, // http://www.autopro.spb.ru/showeng.afp?engine=20
        Z16XER, // http://www.autopro.spb.ru/showeng.afp?engine=32
        Z16YNG, // http://www.autopro.spb.ru/showeng.afp?engine=21
        Z17DTH, // http://www.autopro.spb.ru/showeng.afp?engine=33
        Z17DTJ, // http://www.autopro.spb.ru/showeng.afp?engine=34
        Z17DTL, // http://www.autopro.spb.ru/showeng.afp?engine=22
        Z17DTR, // http://www.autopro.spb.ru/showeng.afp?engine=35
        Z18XE, // http://www.autopro.spb.ru/showeng.afp?engine=23
        Z18XEL, // http://www.autopro.spb.ru/showeng.afp?engine=61
        Z18XER, // http://www.autopro.spb.ru/showeng.afp?engine=36
        Z19DT, // http://www.autopro.spb.ru/showeng.afp?engine=37
        Z19DTH, // http://www.autopro.spb.ru/showeng.afp?engine=38
        Z19DTJ, // http://www.autopro.spb.ru/showeng.afp?engine=39
        Z19DTL, // http://www.autopro.spb.ru/showeng.afp?engine=40
        Z20DM, // http://www.autopro.spb.ru/showeng.afp?engine=108
        Z20DMH, // http://www.autopro.spb.ru/showeng.afp?engine=109
        Z20DTJ,
        Z20LEH, // http://www.autopro.spb.ru/showeng.afp?engine=41
        Z20LEL, // http://www.autopro.spb.ru/showeng.afp?engine=42
        Z20LER, // http://www.autopro.spb.ru/showeng.afp?engine=43
        Z20LET, // http://www.autopro.spb.ru/showeng.afp?engine=24
        Z20NET, // http://www.autopro.spb.ru/showeng.afp?engine=62
        Z22SE, // http://www.autopro.spb.ru/showeng.afp?engine=25
        Z22YH, // http://www.autopro.spb.ru/showeng.afp?engine=44
        Z24XE,
        Z24XED, // http://www.autopro.spb.ru/showeng.afp?engine=105
        Z28NEH, // http://www.autopro.spb.ru/showeng.afp?engine=63
        Z28NEL, // http://www.autopro.spb.ru/showeng.afp?engine=64
        Z28NET, // http://www.autopro.spb.ru/showeng.afp?engine=65
        Z30DT, // http://www.autopro.spb.ru/showeng.afp?engine=66
        Z32SE, // http://www.autopro.spb.ru/showeng.afp?engine=67
        Z32SED, // http://www.autopro.spb.ru/showeng.afp?engine=107

        // Toyota engines used at small cars, KR family
        KR_384F = 3000, // 1KR-FE,

        // MMC engine made with Mitsubishi
        MMC24 // 2.4 L mitsubishi diesel engines
    }
}
