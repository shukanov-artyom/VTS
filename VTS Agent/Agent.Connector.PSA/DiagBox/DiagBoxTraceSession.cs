using System;
using System.Collections.Generic;
using Agent.Connector.PSA.GraphTypeData;
using VTS.Shared;

namespace Agent.Connector.PSA.DiagBox
{
    public sealed class DiagBoxTraceSession
    {
        private readonly List<LexiaGraphSessionRawData> data = 
            new List<LexiaGraphSessionRawData>();
        private readonly List<string> additionalFilePaths = 
            new List<string>();

        public string TraceSessionMainFilePath { get; set; }

        public string Vin { get; set; }

        public DateTime Date { get; set; }

        public string TraceId { get; set; }

        public string VehicleName { get; set; }

        public string VehicleArchiName { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public int Version { get; set; }

        public int Mileage { get; set; }

        public List<string> AdditionalFilePaths
        {
            get
            {
                return additionalFilePaths;
            }
        }

        public List<LexiaGraphSessionRawData> Data
        {
            get
            {
                return data;
            }
        }
    }
}
