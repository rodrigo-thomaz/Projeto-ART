namespace ART.Data.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Data.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDSFamilyTempSensorRepository : IRepository<ARTDbContext, DSFamilyTempSensor, Guid>
    {
        #region Methods

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        #endregion Methods
    }
}