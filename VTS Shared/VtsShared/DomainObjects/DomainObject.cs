using System;

namespace VTS.Shared.DomainObjects
{
    public class DomainObject
    {
        public long Id
        {
            get;
            set;
        }

        /// <summary>
        /// Copies properties
        /// instead of cloneable.
        /// </summary>
        public virtual void CopyTo(DomainObject target)
        {
            target.Id = Id;
        }
    }
}
