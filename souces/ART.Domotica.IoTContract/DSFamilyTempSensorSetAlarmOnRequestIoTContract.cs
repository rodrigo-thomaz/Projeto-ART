namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetAlarmOnRequestIoTContract
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

        public TempSensorAlarmPositionIoTContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}