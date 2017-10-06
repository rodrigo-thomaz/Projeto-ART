using ART.Infra.CrossCutting.WebApi;
using System.Linq;

namespace ART.Infra.CrossCutting.MQ.WebApi
{
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

        protected AuthenticatedContract CreateContract()
        {
            return new AuthenticatedContract
            {
                ApplicationUserId = _applicationUserId,
                SouceMQSession = _souceMQSession,
            };
        }

        protected AuthenticatedContract<TContract> CreateContract<TContract>(TContract contract)
        {
            return new AuthenticatedContract<TContract>
            {
                ApplicationUserId = _applicationUserId,
                SouceMQSession = _souceMQSession,
                Contract = contract,
            };
        }        
    }
}
