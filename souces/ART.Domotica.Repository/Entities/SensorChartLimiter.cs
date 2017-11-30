namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class SensorChartLimiter : IEntity<Guid>
    {
        #region Properties

        public Guid Id
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

        public Sensor Sensor
        {
            get; set;
        }

        #endregion Properties
    }
}