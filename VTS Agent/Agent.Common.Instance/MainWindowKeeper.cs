namespace Agent.Common.Instance
{
    public static class MainWindowKeeper
    {
        private static object mainWindow;

        public static object MainWindowInstance
        {
            get
            {
                return mainWindow;
            }
            set
            {
                mainWindow = value;
            }
        }
    }
}
