namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetAlarmOnCompletedModel
    {
        #region Properties

        public bool AlarmOn
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