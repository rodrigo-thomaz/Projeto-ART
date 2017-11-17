namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetAlarmBuzzerOnRequestIoTContract
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

        public TempSensorAlarmPositionIoTContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}