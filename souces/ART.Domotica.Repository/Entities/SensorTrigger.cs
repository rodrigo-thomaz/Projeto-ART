namespace ART.Domotica.Repository.Entities
{
    using System;

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

        #endregion Properties
    }
}