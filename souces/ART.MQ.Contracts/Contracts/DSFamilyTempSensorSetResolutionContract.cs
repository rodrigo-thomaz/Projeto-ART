using System;

namespace ART.MQ.Common.Contracts
{
    public interface DSFamilyTempSensorSetResolutionContract
    {
        Guid TrackingNumber { get; }
        DateTime Timestamp { get; }
        string DeviceAddress { get; }
        int Value { get; }
    }
}
