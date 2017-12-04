namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Repositories;
    using ART.Infra.CrossCutting.Domain;

    using Autofac;
    using System;
    using System.Threading.Tasks;

    public class SensorChartLimiterDomain : DomainBase, ISensorChartLimiterDomain
    {
        #region Fields

        private readonly ISensorChartLimiterRepository _sensorChartLimiterRepository;

        #endregion Fields

        #region Constructors

        public SensorChartLimiterDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorChartLimiterRepository = new SensorChartLimiterRepository(context);
        }

        #endregion Constructors

        public async Task<SensorChartLimiter> SetValue(Guid sensorChartLimiterId, SensorChartLimiterPositionEnum position, decimal value)
        {
            var entity = await _sensorChartLimiterRepository.GetById(sensorChartLimiterId);

            if (entity == null)
            {
                throw new Exception("SensorChartLimiter not found");
            }

            if (position == SensorChartLimiterPositionEnum.Max)
                entity.ChartLimiterMax = value;
            else if (position == SensorChartLimiterPositionEnum.Min)
                entity.ChartLimiterMin = value;

            await _sensorChartLimiterRepository.Update(entity);

            return entity;
        }
    }
}