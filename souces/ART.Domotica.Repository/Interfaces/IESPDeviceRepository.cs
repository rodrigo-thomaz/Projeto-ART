namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IESPDeviceRepository : IRepository<ARTDbContext, ESPDeviceBase, Guid>
    {
        #region Methods

        Task DeleteFromApplication(HardwareInApplication entity);

        Task<HardwareBase> GetByPin(string pin);

        Task<ESPDeviceBase> GetDeviceInApplication(int chipId, int flashChipId, string macAddress);

        Task<List<ESPDeviceBase>> GetESPDevicesNotInApplication();

        Task<List<string>> GetExistingPins();

        Task<HardwareInApplication> GetInApplicationById(Guid hardwareInApplicationId);

        Task<List<HardwareInApplication>> GetListInApplication(Guid applicationUserId);

        Task InsertInApplication(HardwareInApplication entity);

        #endregion Methods
    }
}