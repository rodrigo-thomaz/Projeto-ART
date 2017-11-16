namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetAlarmOnCompletedModel
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public bool AlarmOn
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