namespace ART.Infra.CrossCutting.MQ.WebApi
{
    using System.Linq;

    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.WebApi;

    public abstract class NoAuthenticatedMQApiControllerBase : NoAuthenticatedApiControllerBase
    {
        #region Properties

        protected string _webUITopic
        {
            get
            {
                var value = Request.Headers
                    .SingleOrDefault(x => x.Key == "webUITopic")
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
                WebUITopic = _webUITopic,
            };
        }

        protected NoAuthenticatedMessageContract<TContract> CreateMessage<TContract>(TContract contract)
        {
            return new NoAuthenticatedMessageContract<TContract>
            {
                WebUITopic = _webUITopic,
                Contract = contract,
            };
        }

        #endregion Methods
    }
}