using ART.Domotica.Model;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ART.Domotica.Domain.Interfaces
{
    public interface IHardwaresInApplicationDomain
    {
        #region Methods

        Task<List<HardwaresInApplicationGetListModel>> GetList(AuthenticatedMessageContract message);

        #endregion Methods
    }
}