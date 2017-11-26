namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IESPDeviceDomain
    {
        #region Methods

        Task<ESPDevice> DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message);

        Task<List<ESPDevice>> GetAll();

        Task<ApplicationMQ> GetApplicationMQ(Guid deviceId);

        Task<ESPDevice> GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message);

        Task<ESPDevice> GetConfigurations(ESPDeviceGetConfigurationsRPCRequestContract contract);

        Task<List<ESPDevice>> GetListInApplication(AuthenticatedMessageContract message);

        Task<ESPDevice> InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message);        

        Task<List<ESPDevice>> UpdatePins();

        #endregion Methods
    }
}