namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationMQProducer
    {
        #region Methods

        Task<ApplicationMQGetRPCResponseContract> GetRPC(AuthenticatedMessageContract message);

        #endregion Methods
    }
}