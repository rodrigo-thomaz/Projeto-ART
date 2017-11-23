namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationMQDomain
    {
        #region Methods

        Task<ApplicationMQ> Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}