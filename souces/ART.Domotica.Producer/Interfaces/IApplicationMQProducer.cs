namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationMQProducer
    {
        #region Methods

        Task Get(AuthenticatedMessageContract message);

        #endregion Methods
    }
}