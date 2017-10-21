using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Core.WPFCompatibility;

namespace VTSWeb.Presentation.Graph.Checkboxes
{
    public partial class ParameterCheckBoxControl : UserControl
    {
        public event EventHandler Checked;
        public event EventHandler Unchecked;
        private Color lineColor;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ParameterCheckBoxControl()
        {
            InitializeComponent();
            lineColored.Stroke = new SolidColorBrush(LineColor);
            checkBox.IsChecked = IsChecked;
            checkBox.Checked += OnCheckBoxChecked;
            checkBox.Unchecked += OnCheckBoxUnchecked;
        }

        private void OnCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            IsChecked = true;
        }

        private void OnCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            IsChecked = false;
        }

        /*public void OnTextPropertyChanged(
            SLDependencyPropertyChangedEventArgs e)
        {
            textBlockName.Text = Text;
        }

        public void OnIsCheckedPropertyChanged(
            SLDependencyPropertyChangedEventArgs e)
        {
            checkBox.IsChecked = IsChecked;
        }

        public void OnLineColorPropertyChanged(
            SLDependencyPropertyChangedEventArgs e)
        {
            lineColored.Stroke = new SolidColorBrush(LineColor);
        }*/

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            if (Checked != null)
            {
                Checked.Invoke(DataContext, e);
            }
        }

        private void CheckBoxUnChecked(object sender, RoutedEventArgs e)
        {
            if (Unchecked != null)
            {
                Unchecked.Invoke(DataContext, e);
            }
        }

        public string Text
        {
            get
            {
                return textBlockName.Text;
            }
            set
            {
                textBlockName.Text = value;
            }
        }

        public bool IsChecked
        {
            get
            {
                return checkBox.IsChecked.Value;
            }
            set
            {
                checkBox.IsChecked = value;
            }
        }

        public Color LineColor
        {
            get
            {
                return lineColor;
            }
            set
            {
                lineColor = value;
                lineColored.Stroke = new SolidColorBrush(value);
            }
        }
    }
}
