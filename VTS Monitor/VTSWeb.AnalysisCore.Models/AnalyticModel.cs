using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.AnalysisCore.Models
{
    public class AnalyticModel : IAnalyticModel
    {
        private  IList<IAnalyticModel> models = new List<IAnalyticModel>();
        IList<IAnalyticRule> rules = new List<IAnalyticRule>();
        private IDictionary<DateTime, double> marksHistory = 
            new Dictionary<DateTime, double>();
        private IDictionary<DateTime, IList<double>> cache =
            new Dictionary<DateTime, IList<double>>();

        public IList<IAnalyticModel> Models
        {
            get
            {
                return models;
            }
        }

        public AnalyticItemSettingsReliability Reliability
        {
            get
            {
                return SummarizeReliability();
            }
        }

        public IList<IAnalyticRule> Rules
        {
            get
            {
                return rules;
            }
        }

        public virtual string AdditionalInfo
        {
            get
            {
                return String.Empty;
            }
        }

        public IDictionary<DateTime, double> MarksHistory
        {
            get
            {
                foreach (DateTime revision in CollectRevisions())
                {
                    List<double> revisionMarks =
                        ExtractMarksSetForRevision(revision).ToList();
                    if (revisionMarks.Any(rm => double.IsNaN(rm)))
                    {
                        if (revisionMarks.Any(rm => !double.IsNaN(rm)))
                        {
                            marksHistory[revision] = revisionMarks.Where(
                                rm => !double.IsNaN(rm)).Average();
                        }
                        else
                        {
                            continue;
                            // we can not use this revision;
                        }
                    }
                    else
                    {
                        marksHistory[revision] = revisionMarks.Average();
                    }
                }
                return marksHistory;
            }
        }

        public void Pick(PsaDataset dataset)
        {
            foreach (IAnalyticRule rule in rules)
            {
                rule.Pick(dataset);
            }
            foreach (IAnalyticModel model in models)
            {
                model.Pick(dataset);
            }
        }

        private List<DateTime> CollectRevisions()
        {
            List<DateTime> result = new List<DateTime>();
            foreach (IAnalyticRule rule in rules)
            {
                foreach (KeyValuePair<DateTime, double> 
                    ruleMark in rule.MarksHistory)
                {
                    result.Add(ruleMark.Key);
                }
            }
            foreach (IAnalyticModel model in models)
            {
                foreach (KeyValuePair<DateTime, double> 
                    submodelMark in model.MarksHistory)
                {
                    result.Add(submodelMark.Key);
                }
            }
            return result;
        }

        private IEnumerable<double> ExtractMarksSetForRevision(DateTime revision)
        {
            foreach (IAnalyticRule rule in rules)
            {
                yield return ExtractClosest(rule.MarksHistory, revision);
            }
            foreach (IAnalyticModel model in models)
            {
                yield return ExtractClosest(model.MarksHistory, revision);
            }
        }

        private double ExtractClosest(
            IDictionary<DateTime, double> dictionary, DateTime revision)
        {
            if (dictionary.Count == 0)
            {
                return double.NaN; // this model  not ready for this revision
            }
            DateTime initialDate = dictionary.Min(p => p.Key);
            TimeSpan minSpan = initialDate - revision;
            double result = dictionary[initialDate];
            foreach (KeyValuePair<DateTime, double> pair in dictionary)
            {
                if (pair.Key == revision)
                {
                    return dictionary[revision];
                }
                if (pair.Key < revision)
                {
                    TimeSpan newSpan = pair.Key - revision;
                    if (newSpan < minSpan)
                    {
                        minSpan = newSpan;
                        result = dictionary[pair.Key];
                    }
                }
            }
            return result;
        }

        private AnalyticItemSettingsReliability SummarizeReliability()
        {
            IList<int> rels = new List<int>();
            foreach (IAnalyticRule rule in Rules)
            {
                rels.Add((int)rule.Reliability);
            }
            foreach (IAnalyticModel model in Models)
            {
                rels.Add((int)model.Reliability);
            }
            int median = (int)rels.Average();
            return (AnalyticItemSettingsReliability) median;
        }
    }
}
