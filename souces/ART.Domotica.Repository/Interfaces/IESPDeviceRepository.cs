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

        Task<ESPDevice> GetByPin(string pin);

        Task<ESPDevice> GetDeviceInApplication(int chipId, int flashChipId, string macAddress);

        Task<List<string>> GetExistingPins();

        Task<List<ESPDevice>> GetListInApplication(Guid applicationId);

        Task<List<ESPDevice>> GetListNotInApplication();

        Task<DeviceBrokerSetting> GetDeviceBrokerSetting(Guid deviceId);

        #endregion Methods
    }
}