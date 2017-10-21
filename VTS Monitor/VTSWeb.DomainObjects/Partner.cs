using System;
using VTS.Shared.DomainObjects;


namespace VTSWeb.DomainObjects
{
    public class Partner : User
    {
        public Partner()
        {
            Role = UserRole.Partner;
        }

        public string OrgName
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public override void CopyTo(DomainObject target)
        {
            Partner p = target as Partner;
            if (p != null)
            {
                p.OrgName = OrgName;
                p.Address = Address;
            }
            base.CopyTo(target);
        }
    }
}
