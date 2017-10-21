using System;

namespace VTS.Agent.Host
{
    public static class BrowserCaller
    {
        /// <summary>
        /// GoToLink()
        /// </summary>
        /// <param name="link"></param>
        public static string GoToLink(string link)
        {
            try
            {
                System.Diagnostics.Process.Start(link);
                return String.Empty;
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                {
                    return noBrowser.Message;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
