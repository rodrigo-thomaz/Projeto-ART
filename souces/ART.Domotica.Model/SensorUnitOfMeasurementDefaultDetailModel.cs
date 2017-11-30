namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

    public class SensorUnitOfMeasurementDefaultDetailModel
    {
        #region Properties

        public SensorDatasheetEnum SensorUnitOfMeasurementDefaultId
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

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        public UnitOfMeasurementEnum UnitOfMeasurementId
        {
            get; set;
        }

        public UnitOfMeasurementTypeEnum UnitOfMeasurementTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}