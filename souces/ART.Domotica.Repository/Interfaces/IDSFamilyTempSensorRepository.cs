namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDSFamilyTempSensorRepository : IRepository<ARTDbContext, DSFamilyTempSensor, Guid>
    {
        #region Methods

        Task<List<DSFamilyTempSensor>> GetListInApplication(Guid applicationUserId);

        Task<List<DSFamilyTempSensor>> GetAllByHardwareId(Guid hardwareId);

        Task<List<DSFamilyTempSensor>> GetAllThatAreNotInApplicationByDevice(Guid deviceBaseId);

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        Task<List<DSFamilyTempSensor>> GetList();

        #endregion Methods
    }
}