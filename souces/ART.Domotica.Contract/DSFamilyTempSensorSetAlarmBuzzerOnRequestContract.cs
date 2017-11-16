namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetAlarmBuzzerOnRequestContract
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

        public TempSensorAlarmPositionContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}