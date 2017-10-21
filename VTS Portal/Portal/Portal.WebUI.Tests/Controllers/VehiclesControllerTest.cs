using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Portal.Service.Service;
using Portal.WebUI.Controllers;
using VTS.Shared.DomainObjects;

namespace Portal.WebUI.Tests.Controllers
{
    [TestClass]
    public class VehiclesControllerTest
    {
        [TestMethod]
        public void TestVehiclesList()
        {
            Mock<IVtsWebService> service = new Mock<IVtsWebService>();
            service.Setup(s => s.GetVehiclesForUser(It.IsAny<string>(), It.IsAny<string>())).Returns(new VehicleDto[]
            {
                new VehicleDto()
                {
                    Id = 30,
                    Manufacturer = 1,
                    Mileage = 23000,
                    Model = "307",
                    ProductionYear = 2007,
                    RegisteredDate = DateTime.Now,
                    Vin = "1234567891234567;"
                },
                new VehicleDto()
                {
                    Id = 30,
                    Manufacturer = 2,
                    Mileage = 24000,
                    Model = "307",
                    ProductionYear = 2007,
                    RegisteredDate = DateTime.Now,
                    Vin = "1234567891454567;"
                },
            });
            VehiclesController controller = new VehiclesController(service.Object);
            ViewResult res = controller.Index(new User()
            {
                Login = "dodo", PasswordHash = "1234dfsfsd"
            });
            Assert.IsNotNull(res);
            List<Vehicle> model = res.Model as List<Vehicle>;
            Assert.IsNotNull(model);
            Assert.AreEqual(model.Count, 2);
        }
    }
}
