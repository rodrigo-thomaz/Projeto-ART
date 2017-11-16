namespace ART.Domotica.IoTContract
{
    using System;

    public class DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseIoTContract
    {
        #region Properties

        public short[] DeviceAddress
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public string Family
        {
            get; set;
        }

        public TempSensorAlarmResponseIoTContract LowAlarm { get; set; }

        public TempSensorAlarmResponseIoTContract HighAlarm { get; set; }        

        public byte ResolutionBits
        {
            get; set;
        }

        public byte TemperatureScaleId
        {
            get; set;
        }

        #endregion Properties
    }
}