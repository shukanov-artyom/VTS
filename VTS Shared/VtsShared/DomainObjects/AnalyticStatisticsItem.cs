using System;
using System.Collections.Generic;

namespace VTS.Shared.DomainObjects
{
    public class AnalyticStatisticsItem : DomainObject
    {
        private readonly IList<AnalyticStatisticsValue> values =
            new List<AnalyticStatisticsValue>();

        public AnalyticStatisticsItem(AnalyticRuleType type,
            EngineFamilyType engineFamilyType, EngineType engineType)
        {
            Type = type;
            TargetEngineFamilyType = engineFamilyType;
            TargetEngineType = engineType;
            DateGenerated = DateTime.Now;
            VersionGenerated = new Version("0.0.0.0");
        }

        public IList<AnalyticStatisticsValue> Values
        {
            get
            {
                return values;
            }
        }

        public AnalyticRuleType Type
        {
            get;
            set;
        }

        public EngineType TargetEngineType
        {
            get;
            set;
        }

        public EngineFamilyType TargetEngineFamilyType
        {
            get;
            set;
        }

        public Version VersionGenerated
        {
            get;
            set;
        }

        public DateTime DateGenerated
        {
            get;
            set;
        }

        public IEnumerable<double> GetDoubleValues()
        {
            foreach (AnalyticStatisticsValue value in Values)
            {
                yield return value.Value;
            }
        }

        public bool SameAs(AnalyticStatisticsItem another)
        {
            return Type == another.Type &&
                   TargetEngineFamilyType == another.TargetEngineFamilyType &&
                   TargetEngineType == another.TargetEngineType;
        }

        public void Assimilate(AnalyticStatisticsItem another)
        {
            if (!SameAs(another))
            {
                throw new Exception("Cannot assimilate another type");
            }
            foreach (AnalyticStatisticsValue value in another.Values)
            {
                Values.Add(value);
            }
        }
    }
}
