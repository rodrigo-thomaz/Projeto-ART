namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums;

    public class UnitOfMeasurementGetAllForIoTResponseContract
    {
        #region Properties

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