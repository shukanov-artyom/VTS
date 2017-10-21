using System;

namespace VTSWeb.Localization
{
    public class CodeBehindStringResolver
    {
        public static string Resolve(string key)
        {
            return TranslationManager.Instance.Translate(key) as string;
        }
    }
}
