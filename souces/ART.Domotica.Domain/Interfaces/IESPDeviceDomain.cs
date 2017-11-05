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

        Task<ESPDeviceDeleteFromApplicationResponseContract> DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message);

        Task<ESPDeviceGetByPinModel> GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message);

        Task<ESPDeviceGetConfigurationsResponseContract> GetConfigurations(ESPDeviceGetConfigurationsRequestContract contract);

        Task<List<ESPDeviceGetListModel>> GetListInApplication(AuthenticatedMessageContract message);

        Task<ESPDeviceInsertInApplicationResponseContract> InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message);

        Task<List<ESPDeviceUpdatePinsContract>> UpdatePins();

        #endregion Methods
    }
}