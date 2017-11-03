namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IESPDeviceProducer
    {
        #region Methods

        Task DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationContract> message);

        Task GetByPin(AuthenticatedMessageContract<ESPDevicePinContract> message);

        Task<ESPDeviceGetConfigurationsResponseContract> GetConfigurations(ESPDeviceGetConfigurationsRequestContract contract);

        Task GetListInApplication(AuthenticatedMessageContract message);

        Task InsertInApplication(AuthenticatedMessageContract<ESPDevicePinContract> message);

        #endregion Methods
    }
}