namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums.SI;

    public class SensorDatasheetUnitMeasurementDefaultGetResponseIoTContract
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