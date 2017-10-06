using ART.Infra.CrossCutting.WebApi;
using System.Linq;

namespace ART.Infra.CrossCutting.MQ.WebApi
{
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

        protected NoAuthenticatedContract<TContract> CreateContract<TContract>(TContract contract)
        {
            return new NoAuthenticatedContract<TContract>
            {
                SouceMQSession = _souceMQSession,
                Contract = contract,
            };
        }
    }
}
