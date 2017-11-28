using ART.Infra.CrossCutting.Repository;
using System;

namespace ART.Domotica.Repository.Entities
{
    public class SensorChartLimiter : IEntity<Guid>
    {
        public SensorBase SensorBase
        {
            get; set;
        }

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
    }
}
