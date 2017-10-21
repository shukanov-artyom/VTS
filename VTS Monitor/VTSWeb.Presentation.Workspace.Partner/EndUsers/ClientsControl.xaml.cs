using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.DomainObjects;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.DomainObjects;
using VTSWeb.Storage.Retrievers.Users.Clients;

namespace VTSWeb.Presentation.Workspace.Partner.EndUsers
{
    public partial class ClientsControl : NavigatablePage
    {
        private readonly ObservableCollection<UserViewModel> users = 
            new ObservableCollection<UserViewModel>();

        public ClientsControl()
        {
            InitializeComponent();
            InitializeHeader("Clients");
            dataGridClients.ItemsSource = users;
            dataGridClients.Height = ApplicationSizeKeeper.Height - 200;
            RefreshData();
        }

        private void RefreshData()
        {
            ClientRetriever clientsRetriever = 
                new ClientRetriever(ClientsRetrievedCallback, Error);
            clientsRetriever.GetForPartner(LoggedUserContext.LoggedUser);
        }

        private void ClientsRetrievedCallback(IList<User> gotUsers)
        {
            users.Clear();
            foreach (User user in gotUsers)
            {
                users.Add(new UserViewModel(user));
            }
        }

        private void Error(Exception e, string msg)
        {
            MessageBox.Show("msg");
        }

        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
