namespace ART.Domotica.Model.SI
{
    using ART.Domotica.Enums.SI;

    public class UnitMeasurementScaleGetModel
    {
        #region Properties

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

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}