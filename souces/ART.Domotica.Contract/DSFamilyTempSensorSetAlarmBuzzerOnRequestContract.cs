namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetAlarmBuzzerOnRequestContract
    {
        #region Properties

        public bool AlarmBuzzerOn
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
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