using System;

namespace VTSWeb.VendorData.Factories
{
    public class VehicleCharacteristicsRetrievingFactory
    {
        /*private VehicleCharacteristicsRetrievedCallback successCallback;
        private ErrorCallbackDelegate errorCallback;

        public VehicleCharacteristicsRetrievingFactory(
            VehicleCharacteristicsRetrievedCallback successCallback, 
            ErrorCallbackDelegate errorCallback)
        {
            this.successCallback = successCallback;
            this.errorCallback = errorCallback;
        }

        public delegate void VehicleCharacteristicsRetrievedCallback
            (VehicleCharacteristics vehicleCharacteristics);

        public void RetrieveAndCreate(string vin, string lang)
        {
            Manufacturer manufacturer = VinValidator.GetManufacturer(vin).Value;
            lang = VehicleCharacteristicsLanguageMapper.GetForRequest(lang);
            if (manufacturer == Manufacturer.Citroen)
            {
                VendorVehicleInfoRetrievingContext context = 
                    new VendorVehicleInfoRetrievingContext();
                context.GetCitroenRawVendorData(vin, lang, OnRetrieved, null);
            }
            else if (manufacturer == Manufacturer.Peugeot)
            {
                VendorVehicleInfoRetrievingContext context =
                    new VendorVehicleInfoRetrievingContext();
                context.GetPeugeotRawVendorData(vin, lang, OnRetrieved, null);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void OnRetrieved(InvokeOperation op)
        {
            if (op.HasError)
            {
                if (errorCallback != null)
                {
                    errorCallback.Invoke(op.Error, op.Error.Message);
                }
            }
            else
            {
                if (successCallback != null)
                {
                    successCallback.Invoke(
                        VehicleCharacteristicsDtoAssembler.Assemble(
                        op.Value as VehicleCharacteristicsDto));
                }
            }
        }*/
    }
}
