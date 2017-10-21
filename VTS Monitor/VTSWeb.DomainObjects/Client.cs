using System;
using VTS.Shared.DomainObjects;

namespace VTSWeb.DomainObjects
{
    public class Client : User
    {
        public Client()
        {
            Role = UserRole.Client;
        }

        public long PartnerId
        {
            get;
            set;
        }

        public Partner Partner
        {
            get;
            set;
        }
    }
}
