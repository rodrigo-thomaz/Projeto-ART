namespace ART.Domotica.Model
{
    using System;
    using ART.Domotica.Enums;
    using System.Collections.Generic;

    public class SensorGetModel
    {
        #region Properties

        public Guid SensorId
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

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        //public SensorTempDSFamily SensorTempDSFamily
        //{
        //    get; set;
        //}

        public List<SensorTriggerGetModel> SensorTriggers
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