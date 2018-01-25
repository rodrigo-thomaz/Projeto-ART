namespace ART.Domotica.IoTContract
{
    using System;
    using System.Collections.Generic;

    using ART.Domotica.Enums;

    public class SensorGetResponseIoTContract
    {
        #region Properties

        public short[] DeviceAddress
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public Guid SensorId
        {
            get; set;
        }

        public SensorTempDSFamilyGetResponseIoTContract SensorTempDSFamily
        {
            get; set;
        }

        public List<SensorTriggerGetResponseIoTContract> SensorTriggers
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
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