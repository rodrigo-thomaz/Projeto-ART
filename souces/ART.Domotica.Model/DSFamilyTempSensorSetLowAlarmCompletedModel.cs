using System;

namespace ART.Domotica.Model
{
    public class DSFamilyTempSensorSetLowAlarmCompletedModel
    {
        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public decimal? LowAlarm { get; set; }
    }
}
