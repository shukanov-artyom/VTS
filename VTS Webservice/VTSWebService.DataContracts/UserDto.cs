using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class UserDto : DomainObjectDto
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string Surname
        {
            get;
            set;
        }

        [DataMember]
        public string Phone
        {
            get;
            set;
        }

        [DataMember]
        public string Email
        {
            get;
            set;
        }

        [DataMember]
        public string Login
        {
            get;
            set;
        }

        [DataMember]
        public string PasswordHash
        {
            get;
            set;
        }

        [DataMember]
        public DateTime RegisteredDate
        {
            get;
            set;
        }

        [DataMember]
        public int Role
        {
            get;
            set;
        }
    }
}