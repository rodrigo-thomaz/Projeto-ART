namespace ART.Domotica.Repository.Interfaces
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;

    public interface ISensorTempDSFamilyRepository : IRepository<ARTDbContext, SensorTempDSFamily>
    {
        Task<SensorTempDSFamily> GetByKey(Guid sensorTempDSFamilyId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);
    }
}