using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;
using VTS.Shared;

namespace VTSWeb.Storage.Retrievers.Users.Clients
{
    public class ClientRetriever
    {
        public delegate void UsersCallback(IList<User> users);

        private readonly UsersCallback usersCallback;
        private readonly ErrorCallbackDelegate errorCallback;

        public ClientRetriever(UsersCallback usersCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.usersCallback = usersCallback;
            this.errorCallback = errorCallback;
        }

        public void GetForPartner(User user)
        {
            VtsWebServiceClient client = new VtsWebServiceClient();
            client.GetClientsForPartnerCompleted += delegate(object s, GetClientsForPartnerCompletedEventArgs e)
            {
                if (e.Error != null)
                {
                    errorCallback.Invoke(e.Error, e.Error.Message);
                }
                else
                {
                    IList<User> result = new List<User>();
                    foreach (UserDto userDto in e.Result)
                    {
                        User gotUser = UserAssembler.Assemble(userDto);
                        result.Add(gotUser);
                    }
                    usersCallback.Invoke(result);
                }
            };
            client.GetAllUsersCompleted += delegate(object s, GetAllUsersCompletedEventArgs e)
            {
                if (e.Error != null)
                {
                    errorCallback.Invoke(e.Error, e.Error.Message);
                }
                else
                {
                    IList<User> result = new List<User>();
                    foreach (UserDto userDto in e.Result)
                    {
                        result.Add(UserAssembler.Assemble(userDto));
                    }
                    usersCallback.Invoke(result);
                }
            };
            if (user.Role == UserRole.Administrator)
            {
                client.GetAllUsersAsync(LoggedUserContext.LoggedUser.Login, 
                    LoggedUserContext.LoggedUser.PasswordHash);
            }
            else if (user.Role == UserRole.Partner)
            {
                client.GetClientsForPartnerAsync(user.Login, user.PasswordHash);
                client.CloseAsync();
            }
            else
            {
                throw new Exception("Client has no access to this code.");
            }
        }
    }
}
