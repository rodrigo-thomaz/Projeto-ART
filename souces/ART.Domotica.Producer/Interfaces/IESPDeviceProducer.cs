namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IESPDeviceProducer
    {
        #region Methods

        Task DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message);

        Task GetAll(AuthenticatedMessageContract message);

        Task GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message);

        Task<ESPDeviceGetConfigurationsRPCResponseContract> GetConfigurationsRPC(ESPDeviceGetConfigurationsRPCRequestContract contract);

        Task GetListInApplication(AuthenticatedMessageContract message);

        Task InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message);

        Task SetTimeZone(AuthenticatedMessageContract<ESPDeviceSetTimeZoneRequestContract> message);

        Task SetUpdateIntervalInMilliSecond(AuthenticatedMessageContract<ESPDeviceSetUpdateIntervalInMilliSecondRequestContract> message);

        Task SetLabel(AuthenticatedMessageContract<ESPDeviceSetLabelRequestContract> message);

        #endregion Methods
    }
}