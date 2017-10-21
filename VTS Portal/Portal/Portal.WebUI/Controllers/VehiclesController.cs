using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Portal.Assemblers;
using Portal.Service.Service;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Portal.WebUI.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVtsWebService service;

        public VehiclesController(IVtsWebService service)
        {
            this.service = service;
        }

        public ViewResult Index(User currentUser)
        {
            List<Vehicle> model = service.GetVehiclesForUser(currentUser.Login, currentUser.PasswordHash).
                Select(v => VehicleAssembler.FromDtoToDomainObject(v))
                .ToList();
            return View(model);
        }

        public ViewResult Characteristics(string vin, SupportedLanguage language)
        {
            VehicleCharacteristicsDto dto = service.GetVehicleCharacteristics(vin, (int)language);
            var data = VehicleCharacteristicsAssembler.FromDtoToDomainObject(dto);
            return View(data);
        }
    }
}