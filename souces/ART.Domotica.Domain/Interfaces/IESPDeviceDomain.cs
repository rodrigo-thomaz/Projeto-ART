namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;

    public interface IESPDeviceDomain
    {
        #region Methods

        Task<ESPDevice> DeleteFromApplication(Guid applicationId, Guid deviceId, Guid deviceDatasheetId);

        Task<List<ESPDevice>> GetAll();

        Task<List<ESPDevice>> GetAllByApplicationId(Guid applicationId);

        Task<ESPDevice> GetByPin(string pin);

        Task<ESPDevice> GetConfigurations(int chipId, int flashChipId, string stationMacAddress, string softAPMacAddress);

        Task<ESPDevice> InsertInApplication(Guid applicationId, Guid createByApplicationUserId, string pin);

        Task<List<ESPDevice>> UpdatePins();

        #endregion Methods
    }
}