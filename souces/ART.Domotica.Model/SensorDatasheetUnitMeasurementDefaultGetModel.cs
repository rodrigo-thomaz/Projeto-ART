namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;
    using ART.Domotica.Enums.SI;

    public class SensorDatasheetUnitMeasurementDefaultGetModel
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

        public NumericalScalePrefixEnum NumericalScalePrefixId
        {
            get; set;
        }

        public NumericalScaleTypeEnum NumericalScaleTypeId
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetUnitMeasurementDefaultId
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
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