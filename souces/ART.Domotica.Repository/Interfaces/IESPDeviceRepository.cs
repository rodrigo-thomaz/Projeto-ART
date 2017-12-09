namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IESPDeviceRepository : IRepository<ARTDbContext, ESPDevice, Guid>
    {
        #region Methods

        Task<List<ESPDevice>> GetAll();

        Task<List<ESPDevice>> GetAllByApplicationId(Guid applicationId);

        Task<ESPDevice> GetByPin(string pin);

        Task<List<string>> GetExistingPins();

        Task<ESPDevice> GetHardwareInApplication(int chipId, int flashChipId, string macAddress);

        Task<List<ESPDevice>> GetListNotInApplication();

        Task<ESPDevice> GetFullByKey(Guid deviceId);

        #endregion Methods
    }
}