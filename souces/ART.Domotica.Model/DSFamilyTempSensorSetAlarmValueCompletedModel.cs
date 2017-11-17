namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetAlarmValueCompletedModel
    {
        #region Properties

        public decimal AlarmValue
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