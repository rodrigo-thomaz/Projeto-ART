namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums.SI;

    public class SensorGetModel
    {
        #region Properties

        public Guid Id
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public SensorUnitMeasurementScaleGetModel SensorUnitMeasurementScale
        {
            get; set;
        }

        public UnitMeasurementEnum UnitMeasurementId
        {
            get; set;
        }

        public UnitMeasurementTypeEnum UnitMeasurementTypeId
        {
            get; protected set;
        }

        #endregion Properties
    }
}