using System;
using System.ServiceModel;
using System.Web.Mvc;
using VTS.Site.AnalysisCore.Recognition;
using VTS.Site.WebService.Assemblers;
using VTS.Site.WebService.VtsWebService;

namespace Html.Controllers
{
    public class VinCheckController : ControllerBase
    {
        public PartialViewResult VinLengthErrorText()
        {
            return PartialView("_VinLengthErrorText");
        }

        public PartialViewResult VinCheck(string vin)
        {
            VehicleInformation info = null;
            VtsWebServiceClient client = new VtsWebServiceClient();
            try
            {
                VehicleInformationDto dto = client.GetVehicleInformationByVin(vin);
                info = VehicleInformationAssembler.FromDtoToDomainObject(dto);
            }
                catch (FaultException)
                {
                    return PartialView("_UnsupportedVehiclePartialView", info);
                }
            catch (NotSupportedException)
            {
                return PartialView("_UnsupportedVehiclePartialView", info);
            }
            return PartialView("_VinCheckResultPartialView", info);
        }

        public PartialViewResult VinCheckUnsupportedManufacturer()
        {
            return PartialView("_VinCheckUnsupportedManufacturerPartialView");
        }
    }
}
