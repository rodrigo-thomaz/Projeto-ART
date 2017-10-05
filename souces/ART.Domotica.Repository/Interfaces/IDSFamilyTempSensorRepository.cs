namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;
    using System.Collections.Generic;

    public interface IDSFamilyTempSensorRepository : IRepository<ARTDbContext, DSFamilyTempSensor, Guid>
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetAll(Guid applicationId);
        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        #endregion Methods
    }
}