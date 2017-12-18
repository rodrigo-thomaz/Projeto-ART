namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

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

        public bool TriggerOn
        {
            get; set;
        }

        public decimal Max
        {
            get; set;
        }

        public decimal Min
        {
            get; set;
        }

        #endregion Properties
    }
}