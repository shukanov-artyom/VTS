using System;
using System.Collections.Generic;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace VTS.Agent.BusinessObjects.Trace
{
    public class LexiaTrace : LexiaDataObject
    {
        private IList<LexiaParametersSet> parameterSets =
            new List<LexiaParametersSet>();

        public string SourceFilePath
        {
            get;
            set;
        }

        public long LexiaTracesDataId
        {
            get;
            set;
        }

        public LexiaTracesData LexiaTracesData
        {
            get;
            set;
        }

        public long ParentVehicleId
        {
            get;
            set;
        }

        public Vehicle ParentVehicle
        {
            get;
            set;
        }

        public string VehicleModelName
        {
            get;
            set;
        }

        public int Mileage
        {
            get;
            set;
        }

        public Manufacturer Manufacturer
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public string Application
        {
            get;
            set;
        }

        public string Format
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string PhoneChannel
        {
            get;
            set;
        }

        public string Vin
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string ToolSerialNumber
        {
            get;
            set;
        }

        public string ToolName
        {
            get;
            set;
        }

        public string SavesetId
        {
            get;
            set;
        }

        public IList<LexiaParametersSet> ParameterSets
        {
            get
            {
                return parameterSets;
            }
        }

        public override void CopyTo(LexiaDataObject target)
        {
            base.CopyTo(target);
            LexiaTrace trace = target as LexiaTrace;
            trace.Address = Address;
            trace.Application = Application;
            trace.VehicleModelName = VehicleModelName;
            trace.Mileage = Mileage;
            trace.Manufacturer = Manufacturer;
            trace.Date = Date;
            trace.Format = Format;
            trace.LexiaTracesDataId = LexiaTracesDataId;
            trace.ParentVehicleId = ParentVehicleId;
            trace.Phone = Phone;
            trace.PhoneChannel = PhoneChannel;
            trace.SavesetId = SavesetId;
            trace.ToolName = ToolName;
            trace.ToolSerialNumber = ToolSerialNumber;
            trace.Vin = Vin;
        }

        public LexiaTrace Clone()
        {
            LexiaTrace result = new LexiaTrace();
            CopyTo(result);
            foreach (LexiaParametersSet set in ParameterSets)
            {
                LexiaParametersSet resultSet = new LexiaParametersSet();
                set.CopyTo(resultSet);
                foreach (LexiaParameterData parameter in set.Parameters)
                {
                    LexiaParameterData resultParameter = new LexiaParameterData();
                    parameter.CopyTo(resultParameter);
                    foreach (string s in parameter.Values)
                    {
                        resultParameter.Values.Add(s);
                    }
                    resultSet.Parameters.Add(resultParameter);
                }
                result.ParameterSets.Add(resultSet);
            }
            return result;
        }
    }
}
