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
    
    public partial class AnalyticStatisticsItem
    {
        public AnalyticStatisticsItem()
        {
            this.AnalyticStatisticsValue = new HashSet<AnalyticStatisticsValue>();
        }
    
        public long Id { get; set; }
        public string VersionGenerated { get; set; }
        public System.DateTime DateGenerated { get; set; }
        public int Type { get; set; }
        public int TargetEngineFamilyType { get; set; }
        public int TargetEngineType { get; set; }
    
        public virtual ICollection<AnalyticStatisticsValue> AnalyticStatisticsValue { get; set; }
    }
}