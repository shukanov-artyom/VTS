using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace VTSWeb.Presentation.Common
{
    [ContentProperty("InnerContent")]
    public partial class HeaderedBorderControl : UserControl
    {
        //private FrameworkElement content;

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string),
            typeof(HeaderedBorderControl), new PropertyMetadata(OnHeaderPropertyChanged));

        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register("InnerContent",
            typeof(FrameworkElement), typeof(HeaderedBorderControl),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ContentPaddingProperty =
            DependencyProperty.Register("ContentPadding", typeof(int), 
            typeof(HeaderedBorderControl), 
            new PropertyMetadata(OnContentPaddingChanged));

        public HeaderedBorderControl()
        {
            InitializeComponent();
        }

        public string Header
        {
            get
            {
                return (string)GetValue(HeaderProperty);
            }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        public FrameworkElement InnerContent
        {
            get
            {
                return (FrameworkElement)GetValue(InnerContentProperty);
            }
            set
            {
                SetValue(InnerContentProperty, value);
            }
        }

        public int ContentPadding
        {
            get
            {
                return (int)GetValue(ContentPaddingProperty);
            }
            set
            {
                SetValue(ContentPaddingProperty, value);
            }
        }

        public void SetContentIfContentControl(UserControl content)
        {
            // TODO: Get ride of this workaround
            if (InnerContent is ContentControl)
            {
                ((ContentControl) InnerContent).Content = content;
            }
        }

        public void SetWaitingMode(bool wait)
        {
            SetEnabled(!wait);
            progressBarCircular.Visibility = wait ?
                Visibility.Visible : Visibility.Collapsed;
        }

        private void SetEnabled(bool isEnabled)
        {
            if (InnerContent is Control)
            {
                ((Control) InnerContent).IsEnabled = isEnabled;
            }
        }

        public void SetContentPadding(int padding)
        {
            borderInner.Padding = new Thickness(padding);
        }

        public void SetHeader(string value)
        {
            textBlockHeader.Text = value;
        }

        private static void OnHeaderPropertyChanged(
            object sender, DependencyPropertyChangedEventArgs ea)
        {
            HeaderedBorderControl control = sender as HeaderedBorderControl;
            control.SetHeader(ea.NewValue as string);
        }

        private static void OnContentPaddingChanged(
            object sender, DependencyPropertyChangedEventArgs ea)
        {
            HeaderedBorderControl control = sender as HeaderedBorderControl;
            control.SetHeader(ea.NewValue as string);
        }
    }
}
