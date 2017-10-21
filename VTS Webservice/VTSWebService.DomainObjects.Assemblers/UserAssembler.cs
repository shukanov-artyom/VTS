using System;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using UserEntity = VTSWebService.DataAccess.User;

namespace VTSWebService.DomainObjects.Assemblers
{
    public class UserAssembler
    {
        public User FromEntityToDomainObject(UserEntity source)
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

        public UserDto FromEntityToDto(DataAccess.User source)
        {
            UserDto target = new UserDto();
            target.Id = source.Id;
            target.Email = source.Email;
            target.Login = source.Login;
            target.Name = source.Name;
            target.PasswordHash = source.PasswordHash;
            target.Phone = source.Phone;
            target.RegisteredDate = source.RegisteredDate;
            target.Role = source.Role;
            target.Surname = source.Surname;
            return target;
        }

        public UserEntity FromDtoToEntity(UserDto source)
        {
            UserEntity target = new UserEntity();
            target.Id = source.Id;
            target.Email = source.Email;
            target.Login = source.Login;
            target.Name = source.Name;
            target.PasswordHash = source.PasswordHash;
            target.Phone = source.Phone;
            target.RegisteredDate = source.RegisteredDate;
            target.Role = source.Role;
            target.Surname = source.Surname;
            return target;
        }
    }
}
