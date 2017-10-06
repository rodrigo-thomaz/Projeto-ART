namespace ART.Infra.CrossCutting.MQ.WebApi
{
    using System.Linq;

    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.WebApi;

    public abstract class AuthenticatedMQApiControllerBase : AuthenticatedApiControllerBase
    {
        #region Properties

        protected string _souceMQSession
        {
            get
            {
                var values = Request.Headers.GetValues("souceMQSession");
                var value = values.SingleOrDefault();
                return value;
            }
        }

        #endregion Properties

        #region Methods

        protected AuthenticatedMessageContract CreateMessage()
        {
            return new AuthenticatedMessageContract
            {
                SouceMQSession = _souceMQSession,
                ApplicationUserId = _applicationUserId,
            };
        }

        protected AuthenticatedMessageContract<TContract> CreateMessage<TContract>(TContract contract)
        {
            return new AuthenticatedMessageContract<TContract>
            {
                SouceMQSession = _souceMQSession,
                ApplicationUserId = _applicationUserId,
                Contract = contract,
            };
        }

        #endregion Methods
    }
}