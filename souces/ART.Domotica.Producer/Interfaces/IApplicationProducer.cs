namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Contract;

    public interface IApplicationProducer
    {
        #region Methods

        Task<ApplicationGetRPCResponseContract> GetRPC(AuthenticatedMessageContract message);

        #endregion Methods
    }
}