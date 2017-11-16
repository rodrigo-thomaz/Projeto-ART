namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetAlarmBuzzerOnCompletedModel
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public bool AlarmBuzzerOn
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