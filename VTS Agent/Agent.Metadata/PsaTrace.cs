using System;
using System.Collections.Generic;
using Common.Interfaces.Enums;
using VTS.Agent.BusinessObjects;

namespace VTS.Agent.Connector.Ghetto
{
    public class PsaTrace : DomainObject
    {
        private IList<PsaParametersSet> parametersSets;

        public PsaTrace()
        {
            Date = DateTime.Now;
            parametersSets = new List<PsaParametersSet>();
        }

        public long PsaDatasetId
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

        public IList<PsaParametersSet> ParametersSets
        {
            get
            {
                return parametersSets;
            }
        }

        public override void CopyTo(DomainObject target)
        {
            base.CopyTo(target);
            PsaTrace trace = target as PsaTrace;

            trace.Address = Address;
            trace.Application = Application;
            trace.VehicleModelName = VehicleModelName;
            trace.Mileage = Mileage;
            trace.Manufacturer = Manufacturer;
            trace.Date = Date;
            trace.Format = Format;
            trace.Phone = Phone;
            trace.PhoneChannel = PhoneChannel;
            trace.SavesetId = SavesetId;
            trace.ToolName = ToolName;
            trace.ToolSerialNumber = ToolSerialNumber;
            trace.Vin = Vin;
            trace.PsaDatasetId = PsaDatasetId;
        }

        /*public PsaTrace Clone()
        {
            PsaTrace result = new PsaTrace();
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
        }*/
    }
}
