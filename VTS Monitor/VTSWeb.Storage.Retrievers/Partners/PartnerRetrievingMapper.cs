using System.Collections.Generic;
using VTSWeb.DomainObjects;
using VTSWeb.WebServiceWrappers;

namespace VTSWeb.Mappers.Partners
{
    public class PartnerRetrievingMapper
    {
        public delegate void PartnersCallback(IList<Partner> partners);
        public delegate void PartnerCallback(Partner partner);

        private PartnersCallback partnerCollectionCallback;
        private PartnerCallback onePartnerCallback;

        private PartnersRetriever retr;

        public PartnerRetrievingMapper()
        {
            retr = new PartnersRetriever();
        }

        public void GetPartner(long id, PartnerCallback onePartnerCallback)
        {
            this.onePartnerCallback = onePartnerCallback;
            retr.GetPartner(id, GetPartnerCallback);
        }

        public void GetPartners(PartnersCallback callback)
        {
            partnerCollectionCallback = callback;
            retr.GetPartners(GetPartnersCallback);
        }

        private void GetPartnersCallback(IList<Partner> partners)
        {
            if (partnerCollectionCallback != null)
            {
                partnerCollectionCallback.Invoke(partners);
            }
        }

        private void GetPartnerCallback(Partner partner)
        {
            if (onePartnerCallback != null)
            {
                onePartnerCallback.Invoke(partner);
            }
        }
    }
}
