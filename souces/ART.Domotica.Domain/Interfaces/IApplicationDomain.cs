namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationDomain
    {
        #region Methods

        Task<ApplicationGetModel> Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}