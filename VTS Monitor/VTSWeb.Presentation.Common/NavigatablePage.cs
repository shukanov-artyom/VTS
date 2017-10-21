using System;
using System.Windows.Controls;
using VTSWeb.Localization;

namespace VTSWeb.Presentation.Common
{
    public class NavigatablePage : UserControl
    {
        private string resourceKey;

        protected void InitializeHeader(string resourceKey)
        {
            this.resourceKey = resourceKey;
        }

        public string Header
        {
            get
            {
                return CodeBehindStringResolver.Resolve(
                    String.Format("NavigatablePageHeader{0}", resourceKey));
            }
        }
    }
}
