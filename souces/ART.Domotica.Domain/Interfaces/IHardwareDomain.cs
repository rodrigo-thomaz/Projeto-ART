namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IHardwareDomain
    {
        #region Methods

        Task<List<HardwareGetListModel>> GetList(AuthenticatedMessageContract message);

        #endregion Methods
    }
}