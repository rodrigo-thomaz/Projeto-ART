namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;

    public class UnitMeasurementScale
    {
        #region Properties

        public float CientificNotation
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public NumericalScale NumericalScale
        {
            get; set;
        }

        public NumericalScaleEnum NumericalScaleId
        {
            get; set;
        }

        public UnitMeasurementPrefix UnitMeasurementPrefix
        {
            get; set;
        }

        public UnitMeasurementPrefixEnum UnitMeasurementPrefixId
        {
            get; set;
        }

        #endregion Properties
    }
}