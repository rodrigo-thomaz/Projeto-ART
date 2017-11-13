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

        Task<ESPDeviceBase> GetByPin(string pin);

        Task<ESPDeviceBase> GetDeviceInApplication(int chipId, int flashChipId, string macAddress);

        Task<List<string>> GetExistingPins();

        Task<List<ESPDeviceBase>> GetListInApplication(Guid applicationUserId);

        Task<List<ESPDeviceBase>> GetListNotInApplication();

        #endregion Methods
    }
}