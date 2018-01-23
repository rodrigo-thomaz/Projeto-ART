namespace ART.Domotica.IoTContract
{
    using System;

    using ART.Domotica.Enums.SI;

    public class SensorUnitMeasurementScaleSetDatasheetUnitMeasurementScaleRequestIoTContract
    {
        #region Properties

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