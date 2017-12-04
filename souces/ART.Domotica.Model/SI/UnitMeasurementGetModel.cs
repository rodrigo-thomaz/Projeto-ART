namespace ART.Domotica.Model.SI
{
    using ART.Domotica.Enums.SI;

    public class UnitMeasurementGetModel
    {
        #region Properties

        public string Description
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Symbol
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