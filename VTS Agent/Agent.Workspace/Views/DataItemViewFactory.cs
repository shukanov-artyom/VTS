using System;
using Agent.Common.Presentation.Data;
using Agent.Workspace.ViewModels;

namespace Agent.Workspace.Views
{
    public class DataItemViewFactory
    {
        private readonly object viewModel;

        public DataItemViewFactory(object viewModel)
        {
            this.viewModel = viewModel;
        }

        public IDataItemView Create()
        {
            if (viewModel is PsaParametersSetViewModel)
            {
                IDataItemView result = new ParametersSetSettingsView(
                    viewModel as PsaParametersSetViewModel);
                result.DataContext = viewModel;
                return result;
            }
            if (viewModel is PsaParameterDataViewModel)
            {
                IDataItemView result = new ParameterView(viewModel as PsaParameterDataViewModel);
                result.DataContext = viewModel;
                return result;
            }
            return null;
        }
    }
}
