namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetAlarmOnRequestContract
    {
        #region Properties

        public bool AlarmOn
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