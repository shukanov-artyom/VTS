using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;

namespace VTSWeb.SystemNews
{
    public partial class SystemNewsControl : NavigatablePage
    {
        private ObservableCollection<SystemNewsItem> treeItems =
            new ObservableCollection<SystemNewsItem>();

        public SystemNewsControl()
        {
            InitializeComponent();
            InitializeHeader("SystemNews");
            InitializeNewsList();
            dataGridNews.ItemsSource = treeItems;
        }

        private void InitializeNewsList()
        {
            SystemNewsPersistency persistency = new SystemNewsPersistency(
                NewsRetrieved, OnError);
            treeItems.Clear();
            persistency.GetAllNews();
        }

        private void NewsRetrieved(IList<SystemNewsItem> news)
        {
            foreach (SystemNewsItem item in news)
            {
                treeItems.Add(item);
            }
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Show();
        }
    }
}
