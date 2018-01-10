namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums.SI;

    public class SensorInDeviceGetResponseIoTContract
    {
        #region Properties

        public short[] DeviceAddress
        {
            get; set;
        }

        public string Family
        {
            get; set;
        }

        public SensorTriggerGetResponseIoTContract HighAlarm
        {
            get; set;
        }

        public decimal HighChartLimiterCelsius
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public SensorTriggerGetResponseIoTContract LowAlarm
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

        public Guid SensorId
        {
            get; set;
        }

        public UnitMeasurementEnum UnitMeasurementId
        {
            get; set;
        }

        #endregion Properties
    }
}