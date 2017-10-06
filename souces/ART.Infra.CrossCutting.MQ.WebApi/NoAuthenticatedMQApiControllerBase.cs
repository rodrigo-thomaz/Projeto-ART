namespace ART.Infra.CrossCutting.MQ.WebApi
{
    using System.Linq;

    using ART.Infra.CrossCutting.WebApi;

    public abstract class NoAuthenticatedMQApiControllerBase : NoAuthenticatedApiControllerBase
    {
        #region Properties

        protected string _souceMQSession
        {
            get
            {
                var value = Request.Headers
                    .SingleOrDefault(x => x.Key == "souceMQSession")
                    .Value.SingleOrDefault();
                return value;
            }
        }

        #endregion Properties

        #region Methods

        protected NoAuthenticatedMessageContract CreateMessage()
        {
            return new NoAuthenticatedMessageContract
            {
                SouceMQSession = _souceMQSession,
            };
        }

        protected NoAuthenticatedMessageContract<TContract> CreateMessage<TContract>(TContract contract)
        {
            return new NoAuthenticatedMessageContract<TContract>
            {
                SouceMQSession = _souceMQSession,
                Contract = contract,
            };
        }

        #endregion Methods
    }
}