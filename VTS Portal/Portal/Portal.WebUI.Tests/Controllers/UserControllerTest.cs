using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Portal.Service.Service;
using Portal.WebUI.Controllers;
using VTS.Shared;

namespace Portal.WebUI.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Login()
        {
            var mock = new Mock<IVtsWebService>();
            mock.Setup(c => c.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).
                Returns((string login, string passwdHash) => new UserDto()
                {
                    Login = login, PasswordHash = passwdHash
                });
            UserController controller = new UserController(mock.Object);
            var t = controller.Signin("dummy", Sha256Hash.Calculate("dummy"), "Ru");
            Assert.IsInstanceOfType(t, typeof(RedirectToRouteResult));
        }
    }
}
