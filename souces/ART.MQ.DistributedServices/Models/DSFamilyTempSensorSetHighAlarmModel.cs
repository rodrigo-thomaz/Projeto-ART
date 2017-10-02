using System;

namespace ART.MQ.DistributedServices.Models
{
    public class DSFamilyTempSensorSetHighAlarmModel
    {
        public Guid DSFamilyTempSensorId { get; set; }
        public decimal HighAlarm { get; set; }
    }
}
