using System;

namespace ART.MQ.DistributedServices.Models
{
    public class DSFamilyTempSensorSetLowAlarmModel
    {
        public Guid DSFamilyTempSensorId { get; set; }
        public decimal LowAlarm { get; set; }
    }
}
