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

        public SensorBase SensorBase
        {
            get; set;
        }

        public Guid SensorBaseId
        {
            get; set;
        }

        public bool TriggerOn
        {
            get; set;
        }

        //public decimal TriggerValue
        //{
        //    get; set;
        //}

        #endregion Properties
    }
}