namespace ART.Domotica.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationDomain
    {
        #region Methods

        Task<ApplicationGetAllModel> Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}