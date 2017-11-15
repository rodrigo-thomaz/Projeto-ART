using System;

namespace ART.Domotica.Model
{
    public class DSFamilyTempSensorSetHighAlarmCompletedModel
    {
        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public decimal? HighAlarm { get; set; }
    }
}
