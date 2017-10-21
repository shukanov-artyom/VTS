using System;
using System.Collections.Generic;
using System.Linq;

namespace VTS.Shared.DomainObjects
{
    public class PsaTrace : DomainObject
    {
        private readonly IList<PsaParametersSet> parametersSets =
            new List<PsaParametersSet>();

        public PsaTrace()
        {
            Date = DateTime.Now;
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

        public bool HasUnrecognizedData()
        {
            if (ParametersSets.Any(ps => ps.Type == PsaParametersSetType.Unsupported))
            {
                return true;
            }
            foreach (PsaParametersSet set in ParametersSets)
            {
                if (set.Parameters.Any(p => p.Type == PsaParameterType.Unsupported))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            PsaTrace trace = obj as PsaTrace;
            if (trace == null)
            {
                throw new ArgumentException("Should be the same object type to compare.");
            }
            return Equals(trace);
        }

        public bool Equals(PsaTrace another)
        {
            if (Vin.Equals(another.Vin, StringComparison.OrdinalIgnoreCase) &&
                Date == another.Date)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Most likely will not be used as a hashtable key.
            throw new NotImplementedException();
        }
    }
}
