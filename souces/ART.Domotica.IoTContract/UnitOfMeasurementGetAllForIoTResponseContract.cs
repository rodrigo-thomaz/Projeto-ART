using ART.Domotica.Enums;

namespace ART.Domotica.IoTContract
{
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