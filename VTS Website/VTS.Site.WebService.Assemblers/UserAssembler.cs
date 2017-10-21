﻿using System;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.VtsWebService;

namespace VTS.Site.WebService.Assemblers
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
    }
}
