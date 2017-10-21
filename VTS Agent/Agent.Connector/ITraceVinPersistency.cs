using System;

namespace Agent.Connector
{
    public interface ITraceVinPersistency
    {
        void PersistNewVin(string vin);
    }
}
