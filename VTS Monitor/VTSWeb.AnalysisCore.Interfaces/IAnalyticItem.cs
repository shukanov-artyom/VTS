using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace VTSWeb.AnalysisCore.Interfaces
{
    public interface IAnalyticItem
    {
        /// <summary>
        /// Used to distinguish different types of the same model.
        /// I.E. Injector correction number can be 1,2,3,4
        /// </summary>
        string AdditionalInfo
        {
            get;
        }

        AnalyticItemSettingsReliability Reliability
        {
            get;
        }

        /// <summary>
        /// Marks history for this item
        /// </summary>
        IDictionary<DateTime, double> MarksHistory
        {
            get;
        }

        /// <summary>
        /// Collects data from the provided dataset,
        /// Builds hictory for all Picks 
        /// The latses Pick will be used as the most current data
        /// </summary>
        void Pick(PsaDataset dataset);
    }
}
