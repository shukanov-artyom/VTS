using System;

namespace Agent.Common
{
    /// <summary>
    /// Versatile exception for various in-process exceptional cases.
    /// </summary>
    public class VtsAgentException : Exception
    {
        public VtsAgentException(string msg)
            : base(msg)
        {
        }
    }
}
