using System;

namespace ART.MQ.Common.Contracts
{
    [Serializable]
    public class DSFamilyTempSensorSetResolutionContract
    {
        public Guid DSFamilyTempSensorId { get; set; }
        public byte DSFamilyTempSensorResolutionId { get; set; }
    }
}
