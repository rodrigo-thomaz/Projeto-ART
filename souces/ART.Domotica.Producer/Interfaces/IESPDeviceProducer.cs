namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IESPDeviceProducer
    {
        #region Methods

        Task<ESPDeviceCheckForUpdatesRPCResponseContract> CheckForUpdatesRPC(ESPDeviceCheckForUpdatesRPCRequestContract message);

        Task DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message);

        Task GetAll(AuthenticatedMessageContract message);

        Task GetAllByApplicationId(AuthenticatedMessageContract message);

        Task GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message);

        Task<ESPDeviceGetConfigurationsRPCResponseContract> GetConfigurationsRPC(ESPDeviceGetConfigurationsRPCRequestContract contract);

        Task InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message);

        Task SetLabel(AuthenticatedMessageContract<DeviceSetLabelRequestContract> message);

        #endregion Methods
    }
}