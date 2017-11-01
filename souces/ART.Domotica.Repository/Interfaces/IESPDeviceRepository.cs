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

        Task DeleteFromApplication(HardwaresInApplication entity);

        Task<HardwareBase> GetByPin(string pin);

        Task<HardwaresInApplication> GetInApplicationById(Guid hardwaresInApplicationId);

        Task<List<HardwaresInApplication>> GetListInApplication(Guid applicationUserId);

        Task InsertInApplication(HardwaresInApplication entity);

        Task<List<string>> GetExistingPins();        

        Task<List<ESPDeviceBase>> GetESPDevicesNotInApplication();

        Task<HardwaresInApplication> GetInApplicationForDevice(string chipId, string flashChipId, string macAddress);

        #endregion Methods
    }
}