namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetHasAlarmCompletedModel
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public bool HasAlarm
        {
            get; set;
        }

        #endregion Properties
    }
}