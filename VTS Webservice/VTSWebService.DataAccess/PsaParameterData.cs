//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VTSWebService.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class PsaParameterData
    {
        public long Id { get; set; }
        public long PsaParametersSetEntityId { get; set; }
        public bool HasTimestamps { get; set; }
        public int Type { get; set; }
        public int Units { get; set; }
        public string Values { get; set; }
        public string Timestamps { get; set; }
        public string OriginalTypeId { get; set; }
        public string AdditionalSourceInfo { get; set; }
    
        public virtual PsaParametersSet PsaParametersSet { get; set; }
    }
}
