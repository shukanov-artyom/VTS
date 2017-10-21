using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Connector.PSA.Refactor.Citroen.Trace;
using Agent.Connector.PSA.TypeConversion;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Peugeot
{
    internal class PeugeotScreensToPsaParametersSetConverter
    {
        private readonly IList<PeugeotScreen> screens;

        public PeugeotScreensToPsaParametersSetConverter(
            IList<PeugeotScreen> screens)
        {
            this.screens = screens;
        }

        public IList<PsaParametersSet> Generate()
        {

            IList<PsaParametersSet> result = new List<PsaParametersSet>();
            foreach (PeugeotScreen screen in screens)
            {
                PsaParametersSet oldSet =
                    result.FirstOrDefault(ps => ps.OriginalName.
                        Equals(screen.Name, StringComparison.OrdinalIgnoreCase) && 
                        ps.Parameters.Count == screen.Points.Count);
                if (oldSet == null)
                {
                    PsaParametersSet newSet = new PsaParametersSet();
                    InitializeSetWithScreenData(newSet, screen);
                    result.Add(newSet);
                }
                else
                {
                     UpdateSetWithScreenData(oldSet, screen);
                }
            }
            return result;
        }

        private void InitializeSetWithScreenData(PsaParametersSet set, 
            PeugeotScreen screen)
        {
            set.Type = PsaParametersSetTypeMapper.Get(screen.Name);
            set.OriginalName = screen.Name;
            foreach (PeugeotRawParameterPoint p in screen.Points)
            {
                PsaParameterData parameter = new PsaParameterData(p.ParameterName); // passed original type ID
                parameter.Type = DataTypeResolver2.GetType(p.ParameterName);
                parameter.OriginalName = p.ParameterName;
                parameter.HasTimestamps = false;
                parameter.Values.Add(p.Value);
                parameter.Units = UnitsConverter.Convert(p.Units);
                parameter.AdditionalSourceInfo = GenerateAdditionalSourceInfo(parameter.OriginalTypeId);
                set.Parameters.Add(parameter);
            }
        }

        private void UpdateSetWithScreenData(PsaParametersSet set, 
            PeugeotScreen screen)
        {
            foreach (PeugeotRawParameterPoint p in screen.Points)
            {
                // At first it was by type but an issue raised with unsupported data
                /*PsaParameterData target = set.Parameters.FirstOrDefault(pr =>
                    pr.Type == PeugeotParameterTypeConverter.Convert(p.ParameterName));*/
                if (set.Parameters.Any(pt => String.IsNullOrEmpty(pt.OriginalName)))
                {
                    throw new NotSupportedException();
                }
                PsaParameterData target = set.Parameters.FirstOrDefault(
                    pr => pr.OriginalName.Equals(p.ParameterName, StringComparison.OrdinalIgnoreCase));
                target.Values.Add(p.Value);
            }
        }

        private string GenerateAdditionalSourceInfo(string originalId)
        {
            return String.Empty;
        }
    }
}
