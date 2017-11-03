namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IESPDeviceDomain
    {
        #region Methods

        Task DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationContract> message);

        Task<ESPDeviceGetByPinModel> GetByPin(AuthenticatedMessageContract<ESPDevicePinContract> message);

        Task<List<ESPDeviceGetListModel>> GetListInApplication(AuthenticatedMessageContract message);

        Task InsertInApplication(AuthenticatedMessageContract<ESPDevicePinContract> message);

        Task<List<ESPDeviceUpdatePinsContract>> UpdatePins();

        Task<ESPDeviceGetConfigurationsResponseContract> GetConfigurations(ESPDeviceGetConfigurationsRequestContract contract);

        #endregion Methods
    }
}