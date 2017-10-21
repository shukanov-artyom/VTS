using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Connector.PSA.TypeConversion;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    internal class PsaParametersSetFactory
    {
        private readonly IList<LexiaScreen> screens;

        public PsaParametersSetFactory(IList<LexiaScreen> screens)
        {
            this.screens = screens;
        }

        public PsaParametersSet Create()
        {
            PsaParametersSet set = new PsaParametersSet();
            if (screens.Count == 0)
            {
                return set;
            }
            string name = screens.FirstOrDefault().Name;
            set.Type = PsaParametersSetTypeMapper.Get(name);
            set.OriginalName = name;
            // assume all screens contain the same params, oherwise it's bad
            foreach (LexiaRawParameterPoint pt in screens.FirstOrDefault().Points)
            {
                if (!set.Parameters.Any(p => p.OriginalName.Equals(pt.ParameterName, 
                    StringComparison.OrdinalIgnoreCase)))
                {
                    PsaParameterData newParamData = new PsaParameterData(pt.ParameterName); // passed originalType Id
                    newParamData.OriginalName = pt.ParameterName;
                    newParamData.AdditionalSourceInfo = GenerateAdditionalSourceInfo(newParamData.OriginalTypeId);
                    newParamData.Type = DataTypeResolver2.GetType(pt.ParameterName);
                    newParamData.HasTimestamps = false;
                    newParamData.Units = UnitsConverter.Convert(pt.Units);
                    set.Parameters.Add(newParamData);
                }
            }
            foreach (LexiaScreen screen in screens)
            {
                foreach (LexiaRawParameterPoint p in screen.Points)
                {
                    PsaParameterData targetParamData = set.Parameters.First(
                        param => param.OriginalName.Equals(p.ParameterName, StringComparison.OrdinalIgnoreCase));
                    targetParamData.Values.Add(p.Value);
                }
            }
            return set;
        }

        private string GenerateAdditionalSourceInfo(string originalTypeId)
        {
            return String.Empty;
        }
    }
}
