using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Logging;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.TypeConversion
{
    public class DataTypeResolver2
    {
        private static readonly HashSet<string> UnknownNativeIds =
            new HashSet<string>();

        public static PsaParameterType GetType(Mnemocode code)
        {
            return MnemocodeToPsaTypeConversionCache.Get(code);
        }

        public static PsaParameterType GetType(string nativeId)
        {
            PsaParameterType result = PsaParameterType.Unsupported;
            HashSet<Mnemocode> got = GetFromNative2Mnemocode(nativeId);
            if (got != null && got.Any())
            {
                result = GetFromMnemocodeToPsa(got);
                if (result == PsaParameterType.Unsupported)
                {
                    result = InferOldFashioned(nativeId);
                }
            }
            else
            {
                result = InferOldFashioned(nativeId);
            }
            if (result == PsaParameterType.Unsupported)
            {
                ReportUnknownNativeId(nativeId);
            }
            return result;
        }

        public static IEnumerable<string> GetUnknownNativeIds()
        {
            return new List<string>(UnknownNativeIds);
        }

        private static HashSet<Mnemocode> GetFromNative2Mnemocode(string nativeId)
        {
            return NativeIdToMnemocodeConversionCache.Get(nativeId);
        }

        private static PsaParameterType GetFromMnemocodeToPsa(HashSet<Mnemocode> codes)
        {
            PsaParameterType result = PsaParameterType.Unsupported;
            foreach (Mnemocode mnemocode in codes)
            {
                PsaParameterType type = MnemocodeToPsaTypeConversionCache.Get(mnemocode);
                if (type != PsaParameterType.Unsupported)
                {
                    result = type;
                    break;
                }
            }
            return result;
        }

        /*private static PsaParameterType GetFromMnemocodeToPsa(Mnemocode code)
        {
            return MnemocodeToPsaTypeConversionCache.Get(code);
        }*/

        private static PsaParameterType GetFromDataTypeMapper(string nativeId)
        {
            return DataTypeMapper.GetType(nativeId);
        }

        private static void MapBack(string nativeId, PsaParameterType type)
        {
            Mnemocode mapBack = MnemocodeToPsaTypeConversionCache.MapBack(type);
            {
                if (mapBack == null)
                {
                    if (!UnknownNativeIds.Contains(nativeId))
                    {
                        Log.Warn(String.Format("Could not find a mnemocode for native id {0} which is {1}", nativeId, type));
                        UnknownNativeIds.Add(nativeId);
                    }
                }
                else
                {
                    NativeIdToMnemocodeConversionCache.Set(nativeId, mapBack);
                }
            }
        }

        private static PsaParameterType InferOldFashioned(string nativeId)
        {
            PsaParameterType result = GetFromDataTypeMapper(nativeId);
            if (result != PsaParameterType.Unsupported)
            {
                MapBack(nativeId, result);
            }
            return result;
        }

        private static void ReportUnknownNativeId(string id)
        {
            if (!UnknownNativeIds.Contains(id))
            {
                UnknownNativeIds.Add(id);
                Log.Warn(String.Format("Unknown parameter native ID : {0}", id));
            }
        }
    }
}
