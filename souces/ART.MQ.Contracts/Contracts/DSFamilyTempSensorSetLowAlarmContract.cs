using System;

namespace ART.MQ.Common.Contracts
{
    [Serializable]
    public class DSFamilyTempSensorSetLowAlarmContract
    {
        public Guid DSFamilyTempSensorId { get; set; }
        public decimal LowAlarm { get; set; }
    }
}
