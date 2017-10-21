using System;
using System.Reflection;
using System.Resources;

namespace VTSWebService.Emailing
{
    internal class EmailingStringsProvider
    {
        private static readonly ResourceManager Rm;

        static EmailingStringsProvider()
        {
            Rm = new ResourceManager("VTSWebService.Emailing.EmailingStrings", 
                Assembly.GetAssembly(typeof(Emailer)));
        }

        public static string GetString(string key)
        {
            return Rm.GetString(key);
        }
    }
}
