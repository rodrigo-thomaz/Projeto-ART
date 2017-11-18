namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetAlarmCelsiusCompletedModel
    {
        #region Properties

        public decimal AlarmCelsius
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public TempSensorAlarmPositionModel Position
        {
            get; set;
        }

        #endregion Properties
    }
}