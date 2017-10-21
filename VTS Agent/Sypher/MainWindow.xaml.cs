using System;
using System.Windows;
using Agent.Common.Data;


namespace Sypher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Translate(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(sourceBox.Text))
            {
                targetBox.Text = Cipher.Encrypt(sourceBox.Text, pwdBox.Text);
            }
        }
    }
}
