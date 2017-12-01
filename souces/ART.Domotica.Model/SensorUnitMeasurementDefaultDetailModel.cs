namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

    public class SensorUnitMeasurementDefaultDetailModel
    {
        #region Properties

        public decimal Max
        {
            get; set;
        }

        public decimal Min
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        public SensorDatasheetEnum SensorUnitMeasurementDefaultId
        {
            get; set;
        }

        public UnitMeasurementEnum UnitMeasurementId
        {
            get; set;
        }

        public UnitMeasurementTypeEnum UnitMeasurementTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}