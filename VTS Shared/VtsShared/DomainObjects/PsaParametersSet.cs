using System;
using System.Collections.Generic;
using System.Linq;


namespace VTS.Shared.DomainObjects
{
    public class PsaParametersSet : DomainObject
    {
        private readonly IList<PsaParameterData> parameters =
            new List<PsaParameterData>();

        public long PsaTraceId
        {
            get;
            set;
        }

        public PsaParametersSetType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Is not assembled as currently is required only at source 
        /// </summary>
        public string OriginalName
        {
            get;
            set;
        }

        /// <summary>
        /// String identifying a type of PsaParametersSet.
        /// </summary>
        public string OriginalTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// Additional information such as mnemocodes,parameter french names etc.
        /// </summary>
        public string AdditionalSourceInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the ECU this parameters set was collected from (is available).
        /// </summary>
        public string EcuName
        {
            get;
            set;
        }

        /// <summary>
        /// The label of the ECU this data has been collected from.
        /// </summary>
        public string EcuLabel
        {
            get;
            set;
        }

        public IList<PsaParameterData> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public bool HasParameterOfType(PsaParameterType type)
        {
            return Parameters.Any(p => p.Type == type);
        }

        public PsaParameterData GetParameterOfType(PsaParameterType type)
        {
            PsaParameterData rpmData = null;
            if (Parameters.Any(p => p.Type == type))
            {
                rpmData = Parameters.
                    FirstOrDefault(p => p.Type == type);
            }
            return rpmData;
        }

        public PsaParameterData GetCertainInjectorCorrections(int injectorNumber)
        {
            switch (injectorNumber)
            {
                case 1:
                    return GetParameterOfType(
                        PsaParameterType.Injector1Correction);
                case 2:
                    return GetParameterOfType(
                        PsaParameterType.Injector2Correction);
                case 3:
                    return GetParameterOfType(
                        PsaParameterType.Injector3Correction);
                case 4:
                    return GetParameterOfType(
                        PsaParameterType.Injector4Correction);
                default:
                    throw new ArgumentException("Wrong injector number");
            }
        }

        public override void CopyTo(DomainObject target)
        {
            base.CopyTo(target);
            PsaParametersSet tgt = target as PsaParametersSet;
            if (tgt == null)
            {
                throw new ArgumentException("Wrong type");
            }
            tgt.Type = Type;
            tgt.PsaTraceId = PsaTraceId;
        }
    }
}
