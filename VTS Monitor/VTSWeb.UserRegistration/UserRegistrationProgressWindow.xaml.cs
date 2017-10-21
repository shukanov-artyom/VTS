using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.UserRegistration
{
    public partial class UserRegistrationProgressWindow : ChildWindow
    {
        private UserRegistrator registrator;

        public UserRegistrationProgressWindow(
            UserRegistrator registrator)
        {
            InitializeComponent();
            if (registrator == null)
            {
                throw new ArgumentNullException("registrator");
            }
            this.registrator = registrator;
            this.registrator.OnSuccess += OnSuccess;
            OKButton.IsEnabled = false;
        }

        public bool LoginAfterRegistration
        {
            get
            {
                if (ctrl.checkBoxEnterTheSystem.IsChecked == null)
                {
                    return false;
                }
                return ctrl.checkBoxEnterTheSystem.IsChecked.Value;
            }
        }

        protected override void OnOpened()
        {
            registrator.StartRegistration();
            base.OnOpened();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void OnSuccess(object sender, EventArgs e)
        {
            ctrl.SetFinishedState();
            OKButton.IsEnabled = true;
        }
    }
}

