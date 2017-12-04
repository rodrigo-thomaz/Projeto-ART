namespace ART.Domotica.Model
{
    using System;

    public class SensorUnitMeasurementScaleGetModel
    {
        #region Properties

        public Guid SensorUnitMeasurementScaleId
        {
            get; set;
        }

        public decimal Max
        {
            get; set;
        }

        public decimal Min
        {
            get; set;
        }

        #endregion Properties
    }
}