using System;

namespace ART.Domotica.Model
{
    public class DSFamilyTempSensorSetScaleCompletedModel
    {
        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public byte TemperatureScaleId { get; set; }
    }
}
