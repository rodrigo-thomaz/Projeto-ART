namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorInDeviceSetOrdinationRequestContract
    {
        #region Properties

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public short Ordination
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public Guid SensorId
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}