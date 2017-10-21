using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public class UserAssembler
    {
        public static User FromDtoToDomainObject(UserDto source)
        {
            User target = new User();
            target.Id = source.Id;
            target.Email = source.Email;
            target.Login = source.Login;
            target.Name = source.Name;
            target.PasswordHash = source.PasswordHash;
            target.Phone = source.Phone;
            target.RegisteredDate = source.RegisteredDate;
            target.Role = (UserRole)source.Role;
            target.Surname = source.Surname;
            return target;
        }

        public static UserDto FromDomainObjectToDto(User source)
        {
            UserDto target = new UserDto();
            target.Id = source.Id;
            target.Email = source.Email;
            target.Login = source.Login;
            target.Name = source.Name;
            target.PasswordHash = source.PasswordHash;
            target.Phone = source.Phone;
            target.RegisteredDate = source.RegisteredDate;
            target.Role = (int)source.Role;
            target.Surname = source.Surname;
            return target;
        }
    }
}
