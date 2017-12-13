namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorUnitMeasurementScaleRepository : IRepository<ARTDbContext, SensorUnitMeasurementScale>
    {
        #region Methods

        Task<SensorUnitMeasurementScale> GetByKey(Guid sensorUnitMeasurementScaleId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);

        #endregion Methods
    }
}