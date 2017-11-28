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

        public SensorBase SensorBase
        {
            get; set;
        }

        #endregion Properties
    }
}