namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums;

    public class UnitMeasurementGetAllForIoTResponseContract
    {
        #region Properties

        public UnitMeasurementEnum Id
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