namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;
    using System;

    public interface IESPDeviceDomain
    {
        #region Methods

        Task<ESPDevice> DeleteFromApplication(Guid deviceInApplicationId);

        Task<List<ESPDevice>> GetAll();

        Task<ESPDevice> GetByPin(string pin);

        Task<ESPDevice> GetConfigurations(ESPDeviceGetConfigurationsRPCRequestContract contract);

        Task<List<ESPDevice>> GetListInApplication(AuthenticatedMessageContract message);

        Task<ESPDevice> InsertInApplication(string pin, Guid createByApplicationUserId);

        Task<List<ESPDevice>> UpdatePins();

        #endregion Methods
    }
}