using ART.Domotica.WebApi.IProducers;
using System.Threading.Tasks;
using RabbitMQ.Client;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Infra.CrossCutting.MQ.Contract;

namespace ART.Domotica.WebApi.Producers
{
    public class ApplicationProducer : ProducerBase, IApplicationProducer
    {
        #region constructors

        public ApplicationProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(message);
            _model.BasicPublish("", ApplicationConstants.GetAllQueueName, null, payload);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: ApplicationConstants.GetAllQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _basicProperties.Persistent = true;
        }

        #endregion
    }
}