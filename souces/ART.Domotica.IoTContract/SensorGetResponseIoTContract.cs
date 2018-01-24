namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums;
    using System;

    public class SensorGetResponseIoTContract
    {
        #region Properties

        public short[] DeviceAddress
        {
            get; set;
        }

        public SensorTriggerGetResponseIoTContract HighAlarm
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

        public Guid SensorId
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        public SensorTempDSFamilyGetResponseIoTContract SensorTempDSFamily
        {
            get; set;
        }

        public SensorUnitMeasurementScaleGetResponseIoTContract SensorUnitMeasurementScale
        {
            get; set;
        }

        #endregion Properties
    }
}