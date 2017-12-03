﻿namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums.SI;

    public class SensorSetUnitMeasurementRequestIoTContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
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