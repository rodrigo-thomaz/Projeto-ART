namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;

    public interface IESPDeviceDomain
    {
        #region Methods

        Task<ESPDevice> DeleteFromApplication(Guid deviceInApplicationId);

        Task<List<ESPDevice>> GetAll();

        Task<ESPDevice> GetByPin(string pin);

        Task<ESPDevice> GetConfigurations(int chipId, int flashChipId, string macAddress);

        Task<List<ESPDevice>> GetListInApplication(Guid applicationId);

        Task<ESPDevice> InsertInApplication(string pin, Guid createByApplicationUserId);

        Task<List<ESPDevice>> UpdatePins();

        #endregion Methods
    }
}