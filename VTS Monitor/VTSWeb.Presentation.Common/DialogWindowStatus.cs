using System;

namespace VTSWeb.Presentation.Common
{
    public static class DialogWindowStatus
    {
        public static event EventHandler DialogWindowClosed;

        private static bool isDialogOpen;

        public static bool IsDialogOpen
        {
            get
            {
                return isDialogOpen;
            }
            set 
            {
                if (value == false)
                {
                    if (DialogWindowClosed != null)
                    {
                        DialogWindowClosed.Invoke(new object(), EventArgs.Empty);
                    }
                }
                isDialogOpen = value;
            }
        }

        public static void OnDialogClosed(object sender, EventArgs e)
        {
            IsDialogOpen = false;
        }
    }
}
