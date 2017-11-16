namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetAlarmValueRequestIoTContract
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

        public TempSensorAlarmPositionIoTContract Position
        {
            get; set;
        }

        #endregion Properties
    }
}