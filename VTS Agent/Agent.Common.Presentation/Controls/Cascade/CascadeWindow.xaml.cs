using System;
using System.Windows;

namespace Agent.Common.Presentation.Controls.Cascade
{
    /// <summary>
    /// Interaction logic for CascadeWindow.xaml
    /// </summary>
    public partial class CascadeWindow : Window
    {
        public CascadeWindow()
        {
            InitializeComponent();
        }

        public new object DataContext
        {
            get
            {
                return base.DataContext;
            }
            set
            {
                CascadeViewModel vm = value as CascadeViewModel;
                if (vm != null)
                {
                    cascadeControl.Display(vm);
                }
                base.DataContext = value;
            }
        }
    }
}
