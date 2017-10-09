namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IHardwaresInApplicationDomain
    {
        #region Methods

        Task<List<HardwaresInApplicationGetListModel>> GetList(AuthenticatedMessageContract message);

        #endregion Methods
    }
}