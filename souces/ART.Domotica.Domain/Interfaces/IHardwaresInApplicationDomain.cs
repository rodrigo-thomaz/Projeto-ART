namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IHardwaresInApplicationDomain
    {
        #region Methods

        Task DeleteHardware(AuthenticatedMessageContract<HardwaresInApplicationDeleteHardwareContract> message);

        Task<List<HardwaresInApplicationGetListModel>> GetList(AuthenticatedMessageContract message);

        Task InsertHardware(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message);

        Task<HardwaresInApplicationSearchPinModel> SearchPin(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message);

        #endregion Methods
    }
}