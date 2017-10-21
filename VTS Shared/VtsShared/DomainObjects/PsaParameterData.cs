using System;
using System.Collections.Generic;
using System.Globalization;

namespace VTS.Shared.DomainObjects
{
    public class PsaParameterData : DomainObject
    {
        private readonly IList<string> values = new List<string>();
        private readonly IList<int> timestamps = new List<int>();

        public PsaParameterData(string originalTypeId)
        {
            OriginalTypeId = originalTypeId;
        }

        public long PsaParametersSetId
        {
            get;
            set;
        }

        public bool HasTimestamps
        {
            get;
            set;
        }

        public PsaParameterType Type
        {
            get;
            set;
        }

        /// <summary>
        /// We recognize type using this identifier.
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
        /// Not assembled nor persisted, it's required at source (agent)
        /// </summary>
        public string OriginalName
        {
            get;
            set;
        }

        public Unit Units
        {
            get;
            set;
        }

        public IList<string> Values
        {
            get
            {
                return values;
            }
        }

        public IList<int> Timestamps
        {
            get
            {
                return timestamps;
            }
        }

        public IList<double> GetDoubles()
        {
            IList<double> result = new List<double>();
            foreach (string s in Values)
            {
                double d;
                double.TryParse(s, NumberStyles.Float,
                    CultureInfo.InvariantCulture, out d);
                result.Add(d);
            }
            return result;
        }

        public IEnumerable<double> ExtractByIndexes(IList<int> indexes)
        {
            foreach (int index in indexes)
            {
                double d;
                double.TryParse(Values[index], NumberStyles.Float,
                    CultureInfo.InvariantCulture, out d);
                yield return d;
            }
        }

        public override void CopyTo(DomainObject target)
        {
            base.CopyTo(target);
            PsaParameterData tgt = target as PsaParameterData;
            if (tgt == null)
            {
                throw new ArgumentException("Wrong type!");
            }
            tgt.HasTimestamps = HasTimestamps;
            tgt.PsaParametersSetId = PsaParametersSetId;
            foreach (int ts in Timestamps)
            {
                tgt.Timestamps.Add(ts);
            }
            tgt.OriginalTypeId = OriginalTypeId;
            tgt.OriginalName = OriginalName;
            tgt.Type = Type;
            tgt.Units = Units;
            tgt.AdditionalSourceInfo = AdditionalSourceInfo;
            foreach (string val in Values)
            {
                tgt.Values.Add(val);
            }
        }
    }
}
