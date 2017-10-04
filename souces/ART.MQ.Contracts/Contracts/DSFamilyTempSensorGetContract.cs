using System;

namespace ART.MQ.Common.Contracts
{
    [Serializable]
    public class DSFamilyTempSensorGetContract
    {
        public Guid DSFamilyTempSensorId { get; set; }
        public string Session { get; set; }
    }
}
