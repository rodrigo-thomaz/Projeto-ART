namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseContract
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

        public bool HasAlarm
        {
            get; set;
        }

        public decimal? HighAlarm
        {
            get; set;
        }

        public decimal? LowAlarm
        {
            get; set;
        }

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