namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;
    using ART.Domotica.Enums;

    public class SensorTrigger : IEntity<Guid>
    {
        #region Properties

        public bool BuzzerOn
        {
            get; set;
        }

        public Guid Id
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

        public bool TriggerOn
        {
            get; set;
        }

        public string TriggerValue
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