﻿namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums;

    public class SensorDatasheetGetResponseIoTContract
    {
        #region Properties

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public SensorDatasheetUnitMeasurementDefaultGetResponseIoTContract SensorDatasheetUnitMeasurementDefault
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}