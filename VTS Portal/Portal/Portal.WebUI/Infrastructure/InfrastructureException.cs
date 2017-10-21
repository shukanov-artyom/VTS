using System;

namespace Portal.WebUI.Infrastructure
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string msg)
            : base(msg)
        {
        }
    }
}