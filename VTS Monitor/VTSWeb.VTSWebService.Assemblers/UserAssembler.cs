using System;
using VTS.Shared.DomainObjects;

using VTSWeb.DomainObjects;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public static class UserAssembler
    {
        public static User Assemble(UserDto source)
        {
            User target = new User();
            target.Email = source.Email;
            target.Id = source.Id;
            target.Login = source.Login;
            target.PasswordHash = source.PasswordHash;
            target.Name = source.Name;
            target.Phone = source.Phone;
            target.RegisteredDate = source.RegisteredDate;
            target.Role = (UserRole)source.Role;
            target.Surname = source.Surname;
            return target;
        }

        public static UserDto Disassemble(User source)
        {
            UserDto target = new UserDto();
            target.Email = source.Email;
            target.Id = source.Id;
            target.Login = source.Login;
            target.PasswordHash = source.PasswordHash;
            target.Name = source.Name;
            target.Phone = source.Phone;
            target.RegisteredDate = source.RegisteredDate;
            target.Role = (int)source.Role;
            target.Surname = source.Surname;
            return target;
        }
    }
}
