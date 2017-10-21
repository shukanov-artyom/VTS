using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Logging;
using VTS.Agent.BusinessObjects.Enums;

namespace Agent.Connector.PSA.TypeConversion
{
    internal static class DatabaseMnemocodeResolver
    {
        private static readonly bool DatabaseAccessible;
        private static MnemocodesPersistency pers = new MnemocodesPersistency();

        static DatabaseMnemocodeResolver()
        {
            DatabaseAccessible = pers.TestConnection();
            if (DatabaseAccessible)
            {
                Log.Info("Mnemocode persistency reports available.");
            }
            else
            {
                Log.Warn("Mnemocode persistency reports unavailable.");
            }
        }

        public static IEnumerable<Mnemocode> Get(string nativeId)
        {
            return pers.GetAvailableMnemocodes(nativeId).ToList();
        }

        public static bool DatabaseReady
        {
            get
            {
                return DatabaseAccessible;
            }
        }
    }
}
