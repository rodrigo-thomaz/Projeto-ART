namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Repository.Entities;

    public interface IApplicationDomain
    {
        #region Methods

        Task<Application> Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}