using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using VTSWeb.Common;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Presentation.Workspace.Partner.DesktopMessages
{
    public partial class MessagesInnerControl : UserControl
    {
        private readonly IList<string> messages = new List<string>();
        private int currentSelectedNumber;

        public MessagesInnerControl()
        {
            InitializeComponent();
            InitializeMessages();
            TranslationManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private void InitializeMessages()
        {
            VtsWebServiceClient client = new VtsWebServiceClient();
            client.GetDesktopMessagesCompleted += MessagesRetrievedCallback;
            client.GetDesktopMessagesAsync(LoggedUserContext.LoggedUser.Login,
                LoggedUserContext.LoggedUser.PasswordHash);
        }

        private void MessagesRetrievedCallback(object o,
            GetDesktopMessagesCompletedEventArgs e)
        {
            messages.Clear();
            if (e.Error != null)
            {
                ErrorWindow w = new ErrorWindow(e.Error, e.Error.Message);
                w.Show();
            }
            else
            {
                IList<string> result = e.Result;
                if (result.Count == 0)
                {
                    DisplayEmptyView();
                }
                else
                {
                    int number = 0;
                    foreach (string s in result)
                    {
                        messages.Add(s);
                        SystemMessageItemControl itemControl = 
                            new SystemMessageItemControl();
                        itemControl.Tag = number;
                        itemControl.Click += ItemClicked;
                        itemControl.SetCaption((number+1).ToString(CultureInfo.InvariantCulture));
                        stackPanelMessageItems.Children.Add(itemControl);
                    }
                    textBlockMessageText.Text = CodeBehindStringResolver.Resolve(messages[0]);
                }
            }
        }

        private void DisplayEmptyView()
        {
            SmartDispatcher.BeginInvoke(() =>
                {
                    stackPanelMessageItems.Children.Clear();
                    textBlockMessageText.Text = String.Empty;
                });
        }

        private void ItemClicked(object s, EventArgs ea)
        {
            SystemMessageItemControl c = s as SystemMessageItemControl;
            currentSelectedNumber = (int)c.Tag;
            SmartDispatcher.BeginInvoke(UpdateCurrentMessageText);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            UpdateCurrentMessageText();
        }

        private void UpdateCurrentMessageText()
        {
            if (messages.Count > 0)
            {
                textBlockMessageText.Text = CodeBehindStringResolver.
                    Resolve(messages[currentSelectedNumber]);
            }
        }
    }
}
