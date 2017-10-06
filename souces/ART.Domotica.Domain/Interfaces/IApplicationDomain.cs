namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationDomain
    {
        #region Methods

        Task<List<ApplicationGetAllModel>> GetAll(AuthenticatedMessageContract message);

        #endregion Methods
    }
}