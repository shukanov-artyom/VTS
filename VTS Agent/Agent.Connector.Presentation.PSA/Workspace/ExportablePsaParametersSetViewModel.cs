using System;
using System.Collections.ObjectModel;
using System.Linq;
using Agent.Common.Presentation.Data;
using Agent.Common.Presentation.Lexia;
using Agent.Connector.PSA;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.Presentation.PSA.Workspace
{
    public class ExportablePsaParametersSetViewModel : ExportableViewModel, IPsaParametersSetViewModel
    {
        private readonly PsaParametersSet model;
        private readonly ObservableCollection<PsaParameterDataViewModel> parameters
            = new ObservableCollection<PsaParameterDataViewModel>();

        private bool isSelected;
        private readonly bool containsUnrecognizedData;

        public ExportablePsaParametersSetViewModel(PsaParametersSet model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;

            foreach (PsaParameterData param in model.Parameters)
            {
                PsaParameterDataViewModel vm =
                    new PsaParameterDataViewModel(param);
                //RegisterExportableChild(vm);
                parameters.Add(vm);
            }
            if (model.Parameters.Any(p => p.Type == PsaParameterType.Unsupported))
            {
                containsUnrecognizedData = true;
            }
        }

        public string Type
        {
            get
            {
                string result = String.Empty;
                if (!String.IsNullOrEmpty(model.EcuLabel) && !String.IsNullOrEmpty(model.EcuName))
                {
                    result = model.EcuLabel.Equals(model.EcuName) ? 
                        model.EcuName : 
                        String.Format("{0} ({1})", Model.EcuName, Model.EcuLabel);
                }
                else
                {
                    result = PsaParametersSetTypeMapper.GetName(model.Type);
                }
                return result;
            }
        }

        public string OriginalName
        {
            get
            {
                return Model.OriginalName;
            }
        }

        public ObservableCollection<PsaParameterDataViewModel> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public bool ContainsUnrecognizedData
        {
            get
            {
                return containsUnrecognizedData;
            }
        }

        public override bool CanBeSelectedForExport
        {
            get
            {
                return base.CanBeSelectedForExport && Parameters.Any();
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public int PointsCount
        {
            get
            {
                if (Parameters.Count > 0)
                {
                    return Parameters[0].Values.Count;
                }
                return 0;
            }
        }

        public PsaParametersSet Model
        {
            get
            {
                return model;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("Type");
            base.ChangeLanguage();
        }
    }
}
