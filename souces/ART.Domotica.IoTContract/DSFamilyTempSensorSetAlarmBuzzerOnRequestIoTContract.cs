namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetAlarmBuzzerOnRequestIoTContract
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

        public TempSensorAlarmPositionIoTContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}