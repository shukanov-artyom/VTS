using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Agent.Localization
{
    public class ResxTranslationProvider : ITranslationProvider
    {
        private readonly ResourceManager resourceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResxTranslationProvider"/> class.
        /// </summary>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="assembly">The assembly.</param>
        public ResxTranslationProvider(string baseName, Assembly assembly)
        {
            resourceManager = new ResourceManager(baseName, assembly);
        }

        /// <summary>
        /// See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public object Translate(string key)
        {
            string result = resourceManager.GetString(key);
            return result;
        }

        /// <summary>
        /// See <see cref="ITranslationProvider.Languages" />
        /// </summary>
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                // TODO: Resolve the available languages
                yield return new CultureInfo("en");
                yield return new CultureInfo("be");
                yield return new CultureInfo("ru");
            }
        }
    }

}
