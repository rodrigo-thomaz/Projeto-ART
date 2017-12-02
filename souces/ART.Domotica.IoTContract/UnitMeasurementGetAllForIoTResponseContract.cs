namespace ART.Domotica.IoTContract
{
    using ART.Domotica.Enums.SI;

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