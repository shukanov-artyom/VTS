using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Agent.Common.Presentation.Data;

namespace Agent.Common.Presentation.Controls.Cascade
{
    public class CascadeViewModel : ViewModelBase
    {
        private readonly IEnumerable<PsaParameterDataViewModel> data;

        public CascadeViewModel(IEnumerable<PsaParameterDataViewModel> data)
        {
            this.data = data;
        }

        public ObservableCollection<PsaParameterDataViewModel> Data
        {
            get
            {
                return new ObservableCollection<PsaParameterDataViewModel>(data);
            }
        }
    }
}
