using System;
using System.Windows;
using System.Windows.Controls;

namespace Agent.Common.Presentation.Users
{
    /// <summary>
    /// Interaction logic for HyperlinkButtonControl.xaml
    /// </summary>
    public partial class HyperlinkButtonControl : UserControl
    {
        public event EventHandler HyperlinkClicked;

        public HyperlinkButtonControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HyperlinkTextProperty =
            DependencyProperty.Register("HyperlinkText",
            typeof(string),
            typeof(HyperlinkButtonControl),
            new UIPropertyMetadata(OnHyperlinkTextChanged));

        public string HyperlinkText
        {
            get
            {
                return (string)GetValue(HyperlinkTextProperty);
            }
            set
            {
                SetValue(HyperlinkTextProperty, value);
            }
        }

        private static void OnHyperlinkTextChanged(
            DependencyObject dependencyObject, 
            DependencyPropertyChangedEventArgs ea)
        {
            HyperlinkButtonControl control = dependencyObject as HyperlinkButtonControl;
            control.SetHyperlinkText((string)ea.NewValue);
        }

        public void SetHyperlinkText(string text)
        {
            DataContext = text;
        }

        private void OnHyperlinkClicked(object sender, RoutedEventArgs e)
        {
            if (HyperlinkClicked != null)
            {
                HyperlinkClicked.Invoke(sender, e);
            }
        }
    }
}
