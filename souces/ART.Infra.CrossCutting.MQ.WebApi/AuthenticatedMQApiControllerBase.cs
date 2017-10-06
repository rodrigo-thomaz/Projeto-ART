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
    }
}
