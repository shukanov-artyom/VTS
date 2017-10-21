using System;
using System.Collections.Generic;
using System.Globalization;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Psa.Interfaces;

namespace VTSWeb.Presentation.Psa
{
    public class PsaParameterDataViewModel : ViewModelBase, 
        IPsaParameterDataViewModel
    {
        private PsaParameterData model;
        private PsaParameterTypeViewModel type;
        private UnitsViewModel units;

        private IList<double> values = new List<double>();

        public PsaParameterDataViewModel(PsaParameterData model)
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
            units = new UnitsViewModel(model.Units);
        }

        public UnitsViewModel Units
        {
            get
            {
                return units;
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
