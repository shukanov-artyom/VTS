using System;
using System.Collections.Generic;
using System.IO;
using Agent.Logging;
using FirebirdSql.Data.FirebirdClient;
using VTS.Agent.BusinessObjects.Enums;

namespace Agent.Connector.PSA.TypeConversion
{
    public class MnemocodesPersistency
    {
        private const string ConnectionStringFormat =
            @"User=SYSDBA;" +
            "Password=masterkey;" +
            "Database={0};" +
            "DataSource=localhost;" +
            "Port=3050;" +
            "Dialect=3;" +
            "Charset=NONE;" +
            "Role=;" +
            "Connection lifetime=15;" +
            "Pooling=true;" +
            "MinPoolSize=0;" +
            "MaxPoolSize=50;" +
            "Packet Size=8192;" +
            "ServerType=0";

        private const string GetMnemocodeByNativeIdQuery =
            @"SELECT DISTINCT PARNAME FROM PARAM WHERE PARLIBMP = @nativeId";

        private const string GetMnemocodeByNativeIdQueryFormat =
            @"SELECT DISTINCT PARNAME FROM PARAM WHERE PARLIBMP = '{0}'";


        public MnemocodesPersistency()
        {
            if (!File.Exists(KnownDiagBoxPath.GPC_DATABASE))
            {
                Log.Warn(String.Format("Database {0} not found.", KnownDiagBoxPath.GPC_DATABASE));
            }
            if (!File.Exists(KnownDiagBoxPath.GPC_DATABASE_SECONDARY))
            {
                Log.Warn(String.Format("Database {0} not found.", KnownDiagBoxPath.GPC_DATABASE_SECONDARY));
            }
        }

        /// <summary>
        /// Tests connection to a selected database.
        /// </summary>
        /// <returns>True if available, False if not.</returns>
        public bool TestConnection()
        {
            if (!File.Exists(KnownDiagBoxPath.GPC_DATABASE) &&
                !File.Exists(KnownDiagBoxPath.GPC_DATABASE_SECONDARY) && 
                !File.Exists(KnownDiagBoxPath.GPC_PP2000_FDB))
            {
                return false;
            }
            try
            {
                if (File.Exists(KnownDiagBoxPath.GPC_DATABASE))
                {
                    string connectionStringLexia1 = 
                        String.Format(ConnectionStringFormat, KnownDiagBoxPath.GPC_DATABASE_SECONDARY);
                    using (FbConnection connection = new FbConnection(connectionStringLexia1))
                    {
                        connection.Open();
                    }
                }
                else if (File.Exists(KnownDiagBoxPath.GPC_DATABASE_SECONDARY))
                {
                    string connectionStringLexia2 = 
                        String.Format(ConnectionStringFormat, KnownDiagBoxPath.GPC_DATABASE);
                    using (FbConnection connection = new FbConnection(connectionStringLexia2))
                    {
                        connection.Open();
                    }
                }
                else if (File.Exists(KnownDiagBoxPath.GPC_PP2000_FDB))
                {
                    string connectionStringPP20001 = 
                        String.Format(ConnectionStringFormat, KnownDiagBoxPath.GPC_PP2000_FDB);
                    using (FbConnection connection = new FbConnection(connectionStringPP20001))
                    {
                        connection.Open();
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Connection test to Firebird server failed.");
                return false;
            }
            return true;
        }

        public IEnumerable<Mnemocode> GetAvailableMnemocodes(string nativeId)
        {
            if (nativeId.ToUpper().Contains("CITACT"))
            {
                foreach (Mnemocode code in GetFromDatabase(KnownDiagBoxPath.GPC_DATABASE_SECONDARY, nativeId))
                {
                    yield return code;
                }
                foreach (Mnemocode code in GetFromDatabase(KnownDiagBoxPath.GPC_DATABASE, nativeId))
                {
                    yield return code;
                }
            }
            else if (nativeId.ToUpper().Contains("THESAU"))
            {
                foreach (Mnemocode code in GetFromDatabase(KnownDiagBoxPath.GPC_PP2000_FDB, nativeId))
                {
                    yield return code;
                }
            }
            else
            {
                Log.Warn(String.Format("Unusual native id postfix: {0}", nativeId));
            }
        }

        public IEnumerable<string> GetParamStrings(Mnemocode code)
        {
            string queryFormat = @"SELECT PARLNAME FROM PARAM WHERE PARSNAME='{0}'";
            HashSet<string> dsds = new HashSet<string>()
            {
                KnownDiagBoxPath.DSD_FDB,
                KnownDiagBoxPath.DSD_FDB_2,
                KnownDiagBoxPath.DSD_FDB_3
            };
            foreach (string dsd in dsds)
            {
                if (!File.Exists(dsd))
                {
                    continue;
                }
                string connectionString = String.Format(ConnectionStringFormat, dsd);
                using (FbConnection connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    using (FbTransaction transaction = connection.BeginTransaction())
                    using (FbCommand command = new FbCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = transaction;
                        command.CommandText = String.Format(queryFormat, code);
                        using (FbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string result = reader.GetString(reader.GetOrdinal("PARLNAME"));
                                    if (!String.IsNullOrEmpty(result))
                                    {
                                        yield return result;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<Mnemocode> GetFromDatabase(string databasePathName, string nativeId)
        {
            string connectionString = String.Format(ConnectionStringFormat, databasePathName);
            using (FbConnection connection = new FbConnection(connectionString))
            {
                connection.Open();
                using (FbTransaction transaction = connection.BeginTransaction())
                using (FbCommand command = new FbCommand())
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandText = String.Format(GetMnemocodeByNativeIdQueryFormat, nativeId);
                    using (FbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string mnemocodeString =
                                    reader.GetString(reader.GetOrdinal("PARNAME"));
                                yield return new Mnemocode(mnemocodeString);
                            }
                        }
                    }
                }
            }
        }
    }
}
