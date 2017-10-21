using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    internal static class LexiaScreensToPsaParametersSetConverter
    {
        public static IList<PsaParametersSet> Convert(IList<LexiaScreen> screens)
        {
            IList<PsaParametersSet> result = new List<PsaParametersSet>();
            // we need unique names collection.
            HashSet<string> distinctScreenNames = new HashSet<string>(screens.Select(s => s.Name));
            // have all distinct screen names here. 
            // Let's generate parameters sets from them 
            foreach (string screenName in distinctScreenNames)
            {
                PsaParametersSetFactory factory = new PsaParametersSetFactory(
                        screens.Where(s => s.Name.Equals(screenName, StringComparison.OrdinalIgnoreCase)).ToList());
                result.Add(factory.Create());
            }
            return result;
        }
    }
}
