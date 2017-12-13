namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;

    public class SensorUnitMeasurementScaleRepository : RepositoryBase<ARTDbContext, SensorUnitMeasurementScale>, ISensorUnitMeasurementScaleRepository
    {
        #region Constructors

        public SensorUnitMeasurementScaleRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<SensorUnitMeasurementScale> GetByKey(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId)
        {
            return await _context.SensorUnitMeasurementScale
                .FindAsync(sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId);
        }
    }
}