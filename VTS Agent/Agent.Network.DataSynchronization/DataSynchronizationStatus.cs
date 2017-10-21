using System;

namespace Agent.Network.DataSynchronization
{
    public enum DataSynchronizationStatus
    {
        Unknown,
        NetworkNotAccessible,
        InProgress,
        Failed,
        Finished,
        DataUnsupported,
        VehicleUnsupported
    }
}
