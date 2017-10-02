using System;

namespace ART.MQ.Common.Contracts
{
    [Serializable]
    public class DSFamilyTempSensorSetHighAlarmContract
    {
        public Guid DSFamilyTempSensorId { get; set; }
        public decimal HighAlarm { get; set; }
    }
}
