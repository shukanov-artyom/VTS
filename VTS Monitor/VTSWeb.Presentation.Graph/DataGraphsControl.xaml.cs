using System;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Graph
{
    public partial class DataGraphsControl : UserControl
    {
        public DataGraphsControl()
        {
            InitializeComponent();
        }

        public ContentControl ContentControlUpper
        {
            get
            {
                return contentControlUpper;
            }
        }

        public ContentControl ContentControlLower
        {
            get
            {
                return contentControlLower;
            }
        }
    }
}
