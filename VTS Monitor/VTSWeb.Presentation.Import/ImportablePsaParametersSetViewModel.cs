using System;
using System.Collections.ObjectModel;
using VTS.Shared.DomainObjects;
using VTSWeb.Presentation.Psa;
using VTSWeb.Presentation.Psa.Interfaces;

namespace VTSWeb.Presentation.Import
{
    public class ImportablePsaParametersSetViewModel : ImportableViewModel, 
        IPsaParametersSetViewModel
    {
        private PsaParametersSet model;
        private PsaParametersSetTypeViewModel type;
        private ObservableCollection<ImportablePsaParameterDataViewModel> parameters =
            new ObservableCollection<ImportablePsaParameterDataViewModel>();

        public ImportablePsaParametersSetViewModel(PsaParametersSet model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            type = new PsaParametersSetTypeViewModel(model.Type);
            foreach (PsaParameterData param in model.Parameters)
            {
                ImportablePsaParameterDataViewModel vm =
                    new ImportablePsaParameterDataViewModel(param);
                RegisterExportableChild(vm);
                Parameters.Add(vm);
            }
        }

        public PsaParametersSetTypeViewModel Type
        {
            get
            {
                return type;
            }
        }

        public ObservableCollection<
            IPsaParameterDataViewModel> Params
        {
            get
            {
                return new ObservableCollection<
                    IPsaParameterDataViewModel>(parameters);
            }
        }

        public ObservableCollection<
            ImportablePsaParameterDataViewModel> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public override bool CanBeSelectedForExport
        {
            get
            {
                return true;
            }
        }

        public PsaParametersSet Model
        {
            get
            {
                return model;
            }
        }
    }
}
