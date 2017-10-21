using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Logging;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.TypeConversion
{
    /// <summary>
    /// Incapsulates a functionality for PSA DiagBox data types resolving.
    /// </summary>
    public static class DataTypeResolver
    {
        private static readonly HashSet<string> UnknownNativeIds = 
            new HashSet<string>();

        private static readonly HashSet<string> NativeIdsCheckedInDb = 
            new HashSet<string>();

        public static PsaParameterType GetType(string nativeId)
        {
            HashSet<Mnemocode> set = NativeIdToMnemocodeConversionCache.Get(nativeId);
            Mnemocode code = null;
            if (set != null)
            {
                code = set.FirstOrDefault();
            }
            if (code == null)
            {
                IList<Mnemocode> mnemocodesFromDatabase = null;
                if (!DatabaseMnemocodeResolver.DatabaseReady && !NativeIdsCheckedInDb.Contains(nativeId))
                {
                    PsaParameterType result = DataTypeMapper.GetType(nativeId);
                    return result;
                }
                NativeIdsCheckedInDb.Add(nativeId);
                if (mnemocodesFromDatabase.Any())
                {
                    code = mnemocodesFromDatabase.First();
                    if (!String.IsNullOrWhiteSpace(code.Code))
                    {
                        NativeIdToMnemocodeConversionCache.Set(nativeId, code);
                    }
                    else
                    {
                        throw new ArgumentException("Wrong mnemocode from database.");
                    }
                }
                PsaParameterType oldMappedType = DataTypeMapper.GetType(nativeId);
                if (oldMappedType == PsaParameterType.Unsupported)
                {
                    ReportUnknownNativeId(nativeId);
                    return oldMappedType;
                }
                foreach (Mnemocode dbmc in mnemocodesFromDatabase)
                {
                    MnemocodeToPsaTypeConversionCache.Add(dbmc, oldMappedType);
                }
                if (code == null && oldMappedType != PsaParameterType.Unsupported)
                {
                    Mnemocode mapBack = MnemocodeToPsaTypeConversionCache.MapBack(oldMappedType);
                    {
                        if (mapBack == null)
                        {
                            if (!UnknownNativeIds.Contains(nativeId))
                            {
                                Log.Warn(String.Format("Could not find a mnemocode for native id {0} which is {1}", nativeId, oldMappedType));
                                UnknownNativeIds.Add(nativeId);
                            }
                        }
                        else
                        {
                            NativeIdToMnemocodeConversionCache.Set(nativeId, mapBack);
                        }
                    }
                    return oldMappedType;
                }
            }
            PsaParameterType type = MnemocodeToPsaTypeConversionCache.Get(code);
            if (type == PsaParameterType.Unsupported)
            {
                type = DataTypeMapper.GetType(nativeId);
                if (type != PsaParameterType.Unsupported)
                {
                    MnemocodeToPsaTypeConversionCache.Add(code, type);
                }
            }
            if (type == PsaParameterType.Unsupported)
            {
                ReportUnknownNativeId(nativeId);
            }
            return type;
        }

        public static PsaParameterType GetType(Mnemocode code)
        {
            return MnemocodeToPsaTypeConversionCache.Get(code);
        }

        private static void ReportUnknownNativeId(string id)
        {
            if (!UnknownNativeIds.Contains(id))
            {
                UnknownNativeIds.Add(id);
                Log.Warn(String.Format("Unknown parameter native ID : {0}", id));
            }
        }

        public static HashSet<string> GetUnknownNativeIds()
        {
            return new HashSet<string>(UnknownNativeIds);
        }
    }
}
