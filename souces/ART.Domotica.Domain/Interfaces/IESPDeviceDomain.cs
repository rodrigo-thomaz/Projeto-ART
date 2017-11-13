namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IESPDeviceDomain
    {
        #region Methods

        Task<ESPDeviceBase> DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message);

        Task<ESPDeviceBase> GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message);

        Task<ESPDeviceBase> GetConfigurations(ESPDeviceGetConfigurationsRPCRequestContract contract);

        Task<List<ESPDeviceBase>> GetAll();

        Task<List<ESPDeviceBase>> GetListInApplication(AuthenticatedMessageContract message);

        Task<ESPDeviceBase> InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message);

        Task<List<ESPDeviceBase>> UpdatePins();

        #endregion Methods
    }
}