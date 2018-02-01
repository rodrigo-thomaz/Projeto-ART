namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class SensorInDevice : IEntity
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public DeviceSensors DeviceSensors
        {
            get; set;
        }

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public short Ordination
        {
            get; set;
        }

        public Sensor Sensor
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