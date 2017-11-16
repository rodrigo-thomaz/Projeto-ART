namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetAlarmOnRequestContract
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

        public TempSensorAlarmPositionContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}