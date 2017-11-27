namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums;
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

        public TempSensorAlarmResponseIoTContract HighAlarm
        {
            get; set;
        }

        public decimal HighChartLimiterCelsius
        {
            get; set;
        }

        public TempSensorAlarmResponseIoTContract LowAlarm
        {
            get; set;
        }

        public decimal LowChartLimiterCelsius
        {
            get; set;
        }

        public byte ResolutionBits
        {
            get; set;
        }

        public UnitOfMeasurementEnum UnitOfMeasurementId
        {
            get; set;
        }

        #endregion Properties
    }
}