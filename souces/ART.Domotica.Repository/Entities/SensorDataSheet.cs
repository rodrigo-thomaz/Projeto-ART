namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class SensorDatasheet : IEntity<SensorDatasheetEnum>
    {
        #region Properties

        public SensorDatasheetEnum Id
        {
            get; set;
        }

        public SensorType SensorType
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        #endregion Properties

        #region Other

        //public UnitOfMeasurement UnitOfMeasurement
        //{
        //    get; set;
        //}
        //public UnitOfMeasurementEnum UnitOfMeasurementId
        //{
        //    get; set;
        //}
        //public UnitOfMeasurementTypeEnum UnitOfMeasurementTypeId
        //{
        //    get; protected set;
        //}
        //public short Max
        //{
        //    get; set;
        //}
        //public short Min
        //{
        //    get; set;
        //}

        #endregion Other
    }
}