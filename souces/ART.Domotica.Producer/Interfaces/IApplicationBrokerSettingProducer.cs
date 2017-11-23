namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationBrokerSettingProducer
    {
        #region Methods

        Task Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}