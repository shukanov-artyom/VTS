using System;

namespace Common.Transport.Resources
{
    public sealed class SerializationResources
    {
        private static readonly SerializationStrings serializationStrings =
            new SerializationStrings();

        /// <summary>
        /// Gets the <see cref="SerializationStrings"/>.
        /// </summary>
        public SerializationStrings Strings
        {
            get
            {
                return serializationStrings;
            }
        }
    }
}
