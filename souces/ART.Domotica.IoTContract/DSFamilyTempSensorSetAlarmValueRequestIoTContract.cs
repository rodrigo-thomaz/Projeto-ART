namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorSetAlarmValueRequestIoTContract
    {
        #region Properties

        public decimal AlarmValue
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