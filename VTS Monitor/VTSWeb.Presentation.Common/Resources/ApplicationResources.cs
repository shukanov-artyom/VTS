namespace VTSWeb.Presentation.Common.Resources
{
    /// <summary>
    /// Wraps access to the strongly-typed resource classes so that you can bind control properties to resource strings in XAML.
    /// </summary>
    public sealed class ApplicationResources2
    {
        private static readonly ApplicationStrings applicationStrings =
            new ApplicationStrings();

        /// <summary>
        /// Gets the <see cref="ApplicationStrings"/>.
        /// </summary>
        public ApplicationStrings Strings
        {
            get
            {
                return applicationStrings;
            }
        }
    }
}
