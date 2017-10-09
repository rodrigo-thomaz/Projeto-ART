namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Contract;

    public interface IHardwaresInApplicationDomain
    {
        #region Methods

        Task<List<HardwaresInApplicationGetListModel>> GetList(AuthenticatedMessageContract message);

        Task<HardwaresInApplicationSearchPinModel> SearchPin(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message);

        Task InsertHardware(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message);

        Task DeleteHardware(AuthenticatedMessageContract<HardwaresInApplicationDeleteHardwareContract> message);

        #endregion Methods
    }
}