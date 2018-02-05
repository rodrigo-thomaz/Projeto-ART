namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IESPDeviceRepository : IRepository<ARTDbContext, ESPDevice>
    {
        #region Methods

        Task<List<ESPDevice>> GetAll();

        Task<List<ESPDevice>> GetAllByApplicationId(Guid applicationId);

        Task<ESPDevice> GetByKey(Guid deviceId, Guid deviceDatasheetId);

        Task<ESPDevice> GetByPin(string pin);

        Task<ESPDevice> GetDeviceInApplication(int chipId, int flashChipId, string stationMacAddress, string softAPMacAddress);

        Task<List<string>> GetExistingPins();

        Task<ESPDevice> GetFullByKey(Guid deviceId, Guid deviceDatasheetId);

        Task<List<ESPDevice>> GetListNotInApplication();

        #endregion Methods
    }
}