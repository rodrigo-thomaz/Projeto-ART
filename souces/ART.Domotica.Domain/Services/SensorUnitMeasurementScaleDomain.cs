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

    public class SensorUnitMeasurementScaleDomain : DomainBase, ISensorUnitMeasurementScaleDomain
    {
        #region Fields

        private readonly ISensorUnitMeasurementScaleRepository _sensorUnitMeasurementScaleRepository;

        #endregion Fields

        #region Constructors

        public SensorUnitMeasurementScaleDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorUnitMeasurementScaleRepository = new SensorUnitMeasurementScaleRepository(context);
        }

        #endregion Constructors

        public async Task<SensorUnitMeasurementScale> SetValue(Guid sensorUnitMeasurementScaleId, SensorUnitMeasurementScalePositionEnum position, decimal value)
        {
            var entity = await _sensorUnitMeasurementScaleRepository.GetByKey(sensorUnitMeasurementScaleId);

            if (entity == null)
            {
                throw new Exception("SensorUnitMeasurementScale not found");
            }

            if (position == SensorUnitMeasurementScalePositionEnum.Max)
                entity.ChartLimiterMax = value;
            else if (position == SensorUnitMeasurementScalePositionEnum.Min)
                entity.ChartLimiterMin = value;

            await _sensorUnitMeasurementScaleRepository.Update(entity);

            return entity;
        }
    }
}