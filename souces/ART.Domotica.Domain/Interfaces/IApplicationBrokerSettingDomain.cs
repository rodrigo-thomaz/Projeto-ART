namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationBrokerSettingDomain
    {
        #region Methods

        Task<ApplicationBrokerSetting> Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}