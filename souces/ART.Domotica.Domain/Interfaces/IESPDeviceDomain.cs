namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Domain.DTOs;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IESPDeviceDomain
    {
        #region Methods

        Task<ESPDeviceBaseDTO> DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message);

        Task<ESPDeviceBaseDTO> GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message);

        Task<ESPDeviceBaseDTO> GetConfigurations(ESPDeviceGetConfigurationsRPCRequestContract contract);

        Task<List<ESPDeviceBaseDTO>> GetListInApplication(AuthenticatedMessageContract message);

        Task<ESPDeviceBaseDTO> InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message);

        Task<List<ESPDeviceBaseDTO>> UpdatePins();

        #endregion Methods
    }
}