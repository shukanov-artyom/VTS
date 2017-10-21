using System;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Partner.DataUpload
{
    public partial class PartnerDataUploadControl : NavigatablePage
    {
        public PartnerDataUploadControl()
        {
            InitializeComponent();
            InitializeHeader("DataUpload");
            DataUploadViewModel vm = new DataUploadViewModel();
            DialogWindowStatus.DialogWindowClosed += OnDialogClosed;
            DataContext = vm;
            dataControl.DataContext = vm;
        }

        private void OnDialogClosed(object sender, EventArgs e)
        {
            // TODO : remove this horrible workaround
            SmartDispatcher.BeginInvoke(() => dataControl.RefreshEnableUi());
        }
    }
}
