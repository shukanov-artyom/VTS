using System;
using System.Collections.Generic;
using System.ComponentModel;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;

using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VehicleEvents.Persistency
{
    public class VehicleEventsPersistency
    {
        private readonly VtsWebServiceClient service = new VtsWebServiceClient();

        #region Retrieve

        public delegate void EventsRetrieved(List<VehicleEvent> events);

        private EventsRetrieved eventsRetrievedDelegate;

        public VehicleEventsPersistency(
            EventsRetrieved eventsRetrievedDelegate,
            ErrorCallbackDelegate errorCallback)
        {
            this.eventsRetrievedDelegate = eventsRetrievedDelegate;
            this.errorCallback = errorCallback;
        }

        public void GetAllForVehicle(Vehicle vehicle)
        {
            service.GetVehicleEventsCompleted += OnEventsLoaded;
            service.GetVehicleEventsAsync(vehicle.Vin);
        }

        private void OnEventsLoaded(object s, 
            GetVehicleEventsCompletedEventArgs op)
        {
            service.GetVehicleEventsCompleted -= OnEventsLoaded;
            if (op.Error != null)
            {
                errorCallback.Invoke(op.Error, op.Error.Message);
            }
            else
            {
                List<VehicleEvent> result = new List<VehicleEvent>();
                foreach (VehicleEventDto eventEntity in op.Result)
                {
                    result.Add(VehicleEventAssembler.FromDtoToDomainObject(eventEntity));
                }
                eventsRetrievedDelegate.Invoke(result);
            }
        }

        #endregion

        #region Persist

        private SuccessCallbackDelegate successCallback;
        private ErrorCallbackDelegate errorCallback;

        public VehicleEventsPersistency(
            SuccessCallbackDelegate successCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.successCallback = successCallback;
        }

        public void Persist(VehicleEvent ev)
        {
            VehicleEventDto dto = VehicleEventAssembler.FromDomainObjectToDto(ev);
            service.SubmitVehicleEventCompleted += OnChangesSubmitted;
            service.SubmitVehicleEventAsync(dto);
        }

        private void OnChangesSubmitted(object s, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
                return;
            }
            successCallback.Invoke();
        }

        #endregion

        #region Delete

        public void Delete(VehicleEvent ev)
        {
            VehicleEventDto dto = VehicleEventAssembler.FromDomainObjectToDto(ev);
            service.DeleteVehicleEventCompleted += OnRemoved;
            service.DeleteVehicleEventAsync(dto);
        }

        private void OnRemoved(object s, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
                return;
            }
            OnChangesSubmitted(this, new AsyncCompletedEventArgs(null, false, null));
        }
    }

    #endregion
}
