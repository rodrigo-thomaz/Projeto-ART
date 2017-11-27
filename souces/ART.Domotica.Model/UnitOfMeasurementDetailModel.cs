namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

    public class UnitOfMeasurementDetailModel
    {
        #region Properties

        public string Description
        {
            get; set;
        }

        public UnitOfMeasurementEnum Id
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

        #endregion Properties
    }
}