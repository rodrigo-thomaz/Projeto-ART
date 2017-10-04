namespace ART.Data.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using ART.Data.Repository.Entities;

    public interface IDSFamilyTempSensorRepository : IRepository<DSFamilyTempSensor, Guid>
    {
        #region Methods

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        #endregion Methods
    }
}