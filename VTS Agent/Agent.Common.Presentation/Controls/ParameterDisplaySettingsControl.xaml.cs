using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Agent.Common.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for ParameterCheckBoxControl.xaml
    /// </summary>
    public partial class ParameterDisplaySettingsControl : UserControl,
        INotifyPropertyChanged
    {
        public event EventHandler Checked;
        public event EventHandler Unchecked;
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty LineColorProperty =
            DependencyProperty.Register(
            "LineColor",
            typeof(Color),
            typeof(ParameterDisplaySettingsControl),
            new FrameworkPropertyMetadata(Colors.Black,
                FrameworkPropertyMetadataOptions.AffectsRender,
                LineColorChangedCallBack));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(ParameterDisplaySettingsControl),
            new FrameworkPropertyMetadata("Default text", 
                FrameworkPropertyMetadataOptions.AffectsRender, 
                TextChangedCallBack));

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
            "IsChecked", 
            typeof(bool), 
            typeof(ParameterDisplaySettingsControl),
            new FrameworkPropertyMetadata(true,
                FrameworkPropertyMetadataOptions.AffectsRender,
                IsCheckedChangedCallBack));

        /// <summary>
        /// Constructor.
        /// </summary>
        public ParameterDisplaySettingsControl()
        {
            InitializeComponent();
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

        private static void TextChangedCallBack(DependencyObject obj, 
            DependencyPropertyChangedEventArgs ea)
        {
            ParameterDisplaySettingsControl cbControl =
                obj as ParameterDisplaySettingsControl;
            if (cbControl != null)
            {
                cbControl.OnPropertyChanged("Text");
                cbControl.OnTextPropertyChanged(ea);
            }
        }

        private static void IsCheckedChangedCallBack(DependencyObject obj, 
            DependencyPropertyChangedEventArgs ea)
        {
            ParameterDisplaySettingsControl cbControl =
                obj as ParameterDisplaySettingsControl;
            if (cbControl != null)
            {
                cbControl.OnPropertyChanged("IsChecked");
                cbControl.OnIsCheckedPropertyChanged(ea);
            }
        }

        private static void LineColorChangedCallBack(DependencyObject obj, 
            DependencyPropertyChangedEventArgs ea)
        {
            ParameterDisplaySettingsControl cbControl =
                obj as ParameterDisplaySettingsControl;
            if (cbControl != null)
            {
                cbControl.OnPropertyChanged("LineColor");
                cbControl.OnLineColorPropertyChanged(ea);
            }
        }

        public void OnTextPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            textBlockName.Text = Text;
        }

        public void OnIsCheckedPropertyChanged(
            DependencyPropertyChangedEventArgs e)
        {
            checkBox.IsChecked = IsChecked;
        }

        public void OnLineColorPropertyChanged(
            DependencyPropertyChangedEventArgs e)
        {
            
        }

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
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                textBlockName.Text = value;
            }
        }

        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
                checkBox.IsChecked = value;
            }
        }

        public Color LineColor
        {
            get
            {
                return (Color)GetValue(LineColorProperty);
            }
            set
            {
                SetValue(LineColorProperty, value);
            }
        }

        protected void OnPropertyChanged(string propertyName) 
        { 
            if (PropertyChanged != null) 
            { 
                PropertyChanged(this, 
                    new PropertyChangedEventArgs(propertyName)); 
            } 
        }
    }
}
