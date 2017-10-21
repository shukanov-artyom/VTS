using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Common
{
    public partial class ExplanatoryTextBlock : UserControl,
        INotifyPropertyChanged
    {
        public static readonly DependencyProperty MainTextProperty =
            DependencyProperty.Register("MainText", typeof(string),
            typeof(ExplanatoryTextBlock),
            new PropertyMetadata(MainTextChangedCallback));

        public static readonly DependencyProperty ExplanatoryTextProperty =
            DependencyProperty.Register("ExplanatoryText", typeof(string),
            typeof(ExplanatoryTextBlock), 
            new PropertyMetadata(ExplanatoryTextChangedCallback));

        public event PropertyChangedEventHandler PropertyChanged;

        public ExplanatoryTextBlock()
        {
            InitializeComponent();
        }

        public string MainText
        {
            get
            {
                //return textBlockMainText.Text;
                 return (string)GetValue(MainTextProperty);
            }
            set
            {
                SetValue(TextBlock.TextProperty, textBlockMainText);
                SetValue(MainTextProperty, value);
            }
        }

        public string ExplanatoryText
        {
            get
            {
                //return textBlockExplanatoryText.Text;
                return (string)GetValue(ExplanatoryTextProperty);
            }
            set
            {
                SetValue(TextBlock.TextProperty, textBlockExplanatoryText);
                SetValue(ExplanatoryTextProperty, value);
            }
        }

        private void OnGridMouseEnter(object sender, EventArgs e)
        {
            textBlockMainText.Visibility = Visibility.Collapsed;
            textBlockExplanatoryText.Visibility = Visibility.Visible;
        }

        private void OnGridMouseLeave(object sender, EventArgs e)
        {
            textBlockMainText.Visibility = Visibility.Visible;
            textBlockExplanatoryText.Visibility = Visibility.Collapsed;
        }

        private static void MainTextChangedCallback(DependencyObject obj,
            DependencyPropertyChangedEventArgs ea)
        {
            ExplanatoryTextBlock etb = obj as ExplanatoryTextBlock;
            if (etb != null)
            {
                etb.OnPropertyChanged("MainText");
                etb.OnMainTextPropertyChanged(ea);
            }
        }

        private static void ExplanatoryTextChangedCallback(
            DependencyObject obj,
            DependencyPropertyChangedEventArgs ea)
        {
            ExplanatoryTextBlock etb = obj as ExplanatoryTextBlock;
            if (etb != null)
            {
                etb.OnPropertyChanged("ExplanatoryText");
                etb.OnExplanatoryTextPropertyChanged(ea);
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

        public void OnMainTextPropertyChanged(
            DependencyPropertyChangedEventArgs e)
        {
            textBlockMainText.Text = MainText;
        }

        public void OnExplanatoryTextPropertyChanged(
            DependencyPropertyChangedEventArgs e)
        {
            textBlockExplanatoryText.Text = ExplanatoryText;
        }
    }
}
