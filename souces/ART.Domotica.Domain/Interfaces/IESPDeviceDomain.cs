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

        Task<DeviceMQ> GetDeviceMQ(Guid deviceId);

        Task<List<ESPDevice>> GetListInApplication(AuthenticatedMessageContract message);

        Task<ESPDevice> InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message);

        Task<ESPDevice> SetTimeZone(Guid deviceId, byte timeZoneId);

        Task<ESPDevice> SetUpdateIntervalInMilliSecond(Guid deviceId, int updateIntervalInMilliSecond);

        Task<List<ESPDevice>> UpdatePins();

        Task<ESPDevice> SetLabel(Guid deviceId, string label);

        #endregion Methods
    }
}