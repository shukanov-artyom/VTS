using System;
using System.Windows.Media;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Graph.Checkboxes
{
    public class ParameterCheckBoxViewModel : ViewModelBase
    {
        private ViewModelBase vm;

        public ParameterCheckBoxViewModel(ViewModelBase vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }
            this.vm = vm;
        }

        public Color StrokeColor
        {
            get;
            set;
        }

        public Brush Stroke
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public ViewModelBase ParamData
        {
            get
            {
                return vm;
            }
        }
    }
}
