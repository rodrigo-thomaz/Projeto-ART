namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;
    using ART.Domotica.Enums;

    public class SensorInDevice : IEntity
    {
        #region Properties

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

        public Guid SensorId
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
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