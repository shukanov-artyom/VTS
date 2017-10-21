using System;
using System.Collections.Generic;

namespace Agent.Connector.PSA.TypeConversion
{
    internal static class KnownDiagBoxPath
    {
        /// <summary>
        /// Main GPS database, proved to be about 112 MB.
        /// </summary>
        public const string GPC_DATABASE = @"C:\APP\LEXIA\DATA\GPC.FDB";

        /// <summary>
        /// Another GPC database, proved to be about 208 MB.
        /// </summary>
        public const string GPC_DATABASE_SECONDARY = @"C:\AWROOT\DTRD\COMM\DATA\GPC.FDB";

        /// <summary>
        /// GPC database for PP2000 system.
        /// </summary>
        public const string GPC_PP2000_FDB = @"C:\APP\OUTILREP\EXE\COM\DATA\GPC_PP2000.FDB";

        /// <summary>
        /// DiagBox launcher executable path.
        /// May be absent if diagbox is not installed.
        /// </summary>
        public const string DIAGBOX_LAUNCHER_PATH = @"C:\AWROOT\bin\launcher\lctpolux.exe";

        /// <summary>
        /// Firebird process name.
        /// </summary>
        public const string FIREBIRD_SERVER_PROCESS_NAME = "fbserver";

        /// <summary>
        /// Firebird server installed with DigBox should be here.
        /// </summary>
        public const string FIREBIRD_INSTALLATION_PATH = @"c:\AWRoot\bin\lib\firebird\bin\fbserver.exe";

        /// <summary>
        /// Path to a DSD database.
        /// </summary>
        public const string DSD_FDB = @"c:\APP\LEXIA\EXE\DSD.FDB";
        public const string DSD_FDB_2 = @"c:\APP\OUTILREP\EXE\DSD.FDB";
        public const string DSD_FDB_3 = @"c:\AWRoot\dtrd\comm\data\DSD.FDB";

        /// <summary>
        /// Gets all known paths.
        /// </summary>
        public static HashSet<string> All
        {
            get
            {
                return new HashSet<string>
                {
                    GPC_DATABASE,
                    GPC_DATABASE_SECONDARY,
                    DIAGBOX_LAUNCHER_PATH,
                    GPC_PP2000_FDB,
                    FIREBIRD_SERVER_PROCESS_NAME,
                    FIREBIRD_INSTALLATION_PATH,
                    DSD_FDB,
                    DSD_FDB_2,
                    DSD_FDB_3
                };
            }
        }
    }
}
