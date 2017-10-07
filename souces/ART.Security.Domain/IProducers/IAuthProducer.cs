namespace ART.Security.Domain.IProducers
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Security.Contract;

    public interface IAuthProducer
    {
        #region Methods

        Task RegisterUser(NoAuthenticatedMessageContract<RegisterUserContract> message);

        #endregion Methods
    }
}