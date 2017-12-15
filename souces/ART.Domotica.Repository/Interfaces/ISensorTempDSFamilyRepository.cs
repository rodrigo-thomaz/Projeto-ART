namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorTempDSFamilyRepository : IRepository<ARTDbContext, SensorTempDSFamily>
    {
        #region Methods

        Task<SensorTempDSFamily> GetByKey(Guid sensorTempDSFamilyId, SensorDatasheetEnum sensorDatasheetId, SensorTypeEnum sensorTypeId);

        #endregion Methods
    }
}