namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IESPDeviceDomain
    {
        #region Methods

        Task<ESPDevice> DeleteFromApplication(Guid applicationId, Guid deviceId);

        Task<List<ESPDevice>> GetAll();

        Task<List<ESPDevice>> GetAllByApplicationId(Guid applicationId);

        Task<ESPDevice> GetByPin(string pin);

        Task<ESPDevice> GetConfigurations(int chipId, int flashChipId, string macAddress);

        Task<ESPDevice> InsertInApplication(string pin, Guid createByApplicationUserId);

        Task<List<ESPDevice>> UpdatePins();

        #endregion Methods
    }
}