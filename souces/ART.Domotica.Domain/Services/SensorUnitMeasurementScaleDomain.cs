namespace ART.Domotica.Domain.Services
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Enums;
    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Domotica.Repository.Interfaces.SI;
    using ART.Domotica.Repository.Repositories;
    using ART.Domotica.Repository.Repositories.SI;
    using ART.Infra.CrossCutting.Domain;

    using Autofac;
    using System;
    using System.Threading.Tasks;

    public class SensorUnitMeasurementScaleDomain : DomainBase, ISensorUnitMeasurementScaleDomain
    {
        #region Fields

        private readonly ISensorUnitMeasurementScaleRepository _sensorUnitMeasurementScaleRepository;
        private readonly INumericalScaleTypeCountryRepository _numericalScaleTypeCountryRepository;
        private readonly ISensorDatasheetUnitMeasurementScaleRepository _sensorDatasheetUnitMeasurementScaleRepository;

        #endregion Fields

        #region Constructors

        public SensorUnitMeasurementScaleDomain(IComponentContext componentContext)
        {
            var context = componentContext.Resolve<ARTDbContext>();

            _sensorUnitMeasurementScaleRepository = new SensorUnitMeasurementScaleRepository(context);
            _sensorDatasheetUnitMeasurementScaleRepository = new SensorDatasheetUnitMeasurementScaleRepository(context);
            _numericalScaleTypeCountryRepository = new NumericalScaleTypeCountryRepository(context);
        }

        #endregion Constructors

        public async Task<SensorUnitMeasurementScale> SetRange(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, decimal value)
        {
            var entity = await _sensorUnitMeasurementScaleRepository.GetByKey(sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("SensorUnitMeasurementScale not found");
            }

            if (position == SensorUnitMeasurementScalePositionEnum.Max)
                entity.RangeMax = value;
            else if (position == SensorUnitMeasurementScalePositionEnum.Min)
                entity.RangeMin = value;

            await _sensorUnitMeasurementScaleRepository.Update(entity);

            return entity;
        }

        public async Task<SensorUnitMeasurementScale> SetChartLimiter(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, SensorUnitMeasurementScalePositionEnum position, decimal value)
        {
            var entity = await _sensorUnitMeasurementScaleRepository.GetByKey(sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId);

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

        public async Task<SensorUnitMeasurementScale> SetUnitMeasurementNumericalScaleTypeCountry(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId, UnitMeasurementEnum unitMeasurementId, UnitMeasurementTypeEnum unitMeasurementTypeId, NumericalScalePrefixEnum numericalScalePrefixId, NumericalScaleTypeEnum numericalScaleTypeId, short countryId)
        {
            var sensorDatasheetUnitMeasurementScale = await _sensorDatasheetUnitMeasurementScaleRepository.GetByKey(sensorDatasheetId, sensorTypeId, unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId);

            if (sensorDatasheetUnitMeasurementScale == null)
            {
                throw new Exception("SensorDatasheetUnitMeasurementScale not found");
            }

            var numericalScaleTypeCountry = await _numericalScaleTypeCountryRepository.GetByKey(numericalScaleTypeId, countryId);

            if (numericalScaleTypeCountry == null)
            {
                throw new Exception("NumericalScaleTypeCountry not found");
            }

            var entity = await _sensorUnitMeasurementScaleRepository.GetByKey(sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId);

            if (entity == null)
            {
                throw new Exception("SensorUnitMeasurementScale not found");
            }

            entity.UnitMeasurementId = unitMeasurementId;
            entity.UnitMeasurementTypeId = unitMeasurementTypeId;
            entity.NumericalScalePrefixId = numericalScalePrefixId;
            entity.NumericalScaleTypeId = numericalScaleTypeId;
            entity.CountryId = countryId;

            await _sensorUnitMeasurementScaleRepository.Update(entity);

            return entity;
        }
    }
}