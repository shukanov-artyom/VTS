using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Portal.Assemblers;
using Portal.Service.Service;
using VTS.Shared.DomainObjects;

namespace Portal.WebUI.Controllers
{
    public class DataController : Controller
    {
        private readonly IVtsWebService service;

        public DataController(IVtsWebService service)
        {
            this.service = service;
        }

        public ActionResult Index(string vin)
        {
            IEnumerable<PsaDataset> model = service.GetDatasetsForVehicle(vin).
                Select<PsaDatasetDto, PsaDataset>(PsaDatasetAssembler.FromDtoToDomainObject);
            return View(model);
        }

        /// <summary>
        /// Provides a view for PsaParameterData 
        /// </summary>
        public PartialViewResult ParameterChart(long parameterDataId)
        {
            PsaParameterDataDto dto = service.GetParameterById(parameterDataId);
            if (dto == null)
            {
                return new PartialViewResult();
            }
            PsaParameterData model = PsaParameterDataAssembler.FromDtoToDomainObject(dto);
            return PartialView(model);
        }

        /// <summary>
        /// Provides a view for Parameters set
        /// </summary>
        public PartialViewResult ParametersSet(long parametersSetId)
        {
            PsaParametersSetDto dto;
            if (dto = service.GetParametersSetById(parametersSetId) == null)
            {
                return new PartialViewResult();
            }
            return PartialView(PsaParametersSetAssembler.FromDtoToDomainObject(dto));
        }
    }
}