using ART.Domotica.Constant;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Model;
using ART.Domotica.Worker.IConsumers;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationConsumer : ConsumerBase, IApplicationConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getConsumer;

        private readonly IApplicationDomain _applicationDomain;

        #endregion

        #region constructors

        public ApplicationConsumer(IConnection connection, IApplicationDomain applicationDomain) : base(connection)
        {
            _getConsumer = new EventingBasicConsumer(_model);

            _applicationDomain = applicationDomain;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            var queueName = ApplicationConstants.GetQueueName;

            _model.QueueDeclare(
                 queue: queueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _getConsumer.Received += GetReceived;

            _model.BasicConsume(queueName, false, _getConsumer);
        }

        private void GetReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetReceivedAsync(sender, e));
        }

        private async Task GetReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var data = await _applicationDomain.Get(message);
            SendGetCompleted(message, data);
        }

        public void SendGetCompleted(AuthenticatedMessageContract message, ApplicationGetModel data)
        {
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ApplicationConstants.GetCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion
    }
}
