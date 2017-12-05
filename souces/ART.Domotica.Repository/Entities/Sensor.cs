namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;

    public class Sensor : HardwareBase
    {
        #region Properties

        public SensorDatasheet SensorDatasheet
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public ICollection<SensorsInDevice> SensorsInDevice
        {
            get; set;
        }

        public SensorTempDSFamily SensorTempDSFamily
        {
            get; set;
        }

        public ICollection<SensorTrigger> SensorTriggers
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        public SensorUnitMeasurementScale SensorUnitMeasurementScale
        {
            get; set;
        }

        #endregion Properties
    }
}