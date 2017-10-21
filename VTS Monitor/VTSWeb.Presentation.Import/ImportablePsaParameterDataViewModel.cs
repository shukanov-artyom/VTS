using System;
using System.Collections.Generic;
using System.Globalization;
using VTS.Shared.DomainObjects;
using VTSWeb.Presentation.Psa;
using VTSWeb.Presentation.Psa.Interfaces;

namespace VTSWeb.Presentation.Import
{
    public class ImportablePsaParameterDataViewModel : ImportableViewModel, 
        IPsaParameterDataViewModel
    {
        private PsaParameterData model;
        private PsaParameterTypeViewModel type;
        //private string name;

        private IList<double> values = new List<double>();

        public ImportablePsaParameterDataViewModel(PsaParameterData model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            type = new PsaParameterTypeViewModel(model.Type);

            foreach (string value in model.Values)
            {
                double d;
                double.TryParse(value, NumberStyles.Float,
                    CultureInfo.InvariantCulture, out d);
                values.Add(d);
            }
        }

        public override bool CanBeSelectedForExport
        {
            get
            {
                return true;
            }
        }

        public PsaParameterTypeViewModel Type
        {
            get
            {
                return type;
            }
        }

        public PsaParameterData Model
        {
            get
            {
                return model;
            }
        }

        public bool HasTimestamps
        {
            get
            {
                return model.HasTimestamps;
            }
        }

        public IList<double> Values
        {
            get
            {
                return values;
            }
        }
    }
}
