using ART.Infra.CrossCutting.WebApi;
using MassTransit;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ART.MQ.DistributedServices.Helpers
{
    public abstract class MQApiControllerBase : BaseApiController
    {
        #region private readonly fields

        protected readonly IBus _bus;

        #endregion

        #region constructors

        protected MQApiControllerBase(IBus bus)
        {
            _bus = bus;
        }

        #endregion

        #region public voids

        protected async Task<ISendEndpoint> GetSendEndpoint(string queueName)
        {
            var hostName = ConfigurationManager.AppSettings["RabbitMQHostName"];
            var sendEndpoint = await _bus.GetSendEndpoint(new Uri(string.Format("rabbitmq://{0}/{1}", hostName, queueName)));
            return sendEndpoint;
        }

        #endregion

    }
}