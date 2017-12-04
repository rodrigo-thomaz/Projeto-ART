namespace ART.Domotica.Model
{
    using System;
    using System.Collections.Generic;

    using ART.Domotica.Enums;

    public class SensorGetModel
    {
        #region Properties

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

        public SensorTempDSFamilyGetModel SensorTempDSFamily
        {
            get; set;
        }

        public List<SensorTriggerGetModel> SensorTriggers
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        public SensorUnitMeasurementScaleGetModel SensorUnitMeasurementScale
        {
            get; set;
        }

        #endregion Properties
    }
}