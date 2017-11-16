namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetAlarmValueCompletedModel
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public decimal AlarmValue
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