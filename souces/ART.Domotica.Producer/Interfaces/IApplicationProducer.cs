namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IApplicationProducer
    {
        #region Methods

        Task<ApplicationGetRPCResponseContract> GetRPC(AuthenticatedMessageContract message);

        #endregion Methods
    }
}