using System;
using System.Collections.Generic;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace VTSWeb.Import.Import
{
    internal class PsaPreImportDatasetsDuplicatesFilter
    {
        public delegate void PsaDatasetsFiltered(List<PsaDataset> filteredDatasets);

        private readonly PsaDatasetsFiltered callbackSuccess;
        private readonly ErrorCallbackDelegate callbackError;

        public PsaPreImportDatasetsDuplicatesFilter(
            PsaDatasetsFiltered callbackSuccess,
            ErrorCallbackDelegate callbackError)
        {
            this.callbackError = callbackError;
            this.callbackSuccess = callbackSuccess;
        }

        public void FilterAsync(IEnumerable<PsaTrace> datasetsToFilter)
        {
            throw new NotImplementedException();
        }
    }
}
