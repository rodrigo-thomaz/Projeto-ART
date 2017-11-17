namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetAlarmBuzzerOnCompletedModel
    {
        #region Properties

        public bool AlarmBuzzerOn
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