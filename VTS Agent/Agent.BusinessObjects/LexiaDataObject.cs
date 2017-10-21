using VTS.Shared.DomainObjects;

namespace VTS.Agent.BusinessObjects
{
    public abstract class LexiaDataObject
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
        public virtual void CopyTo(LexiaDataObject target)
        {
            target.Id = Id;
        }
    }
}
