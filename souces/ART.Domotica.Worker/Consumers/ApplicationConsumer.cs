using ART.Domotica.Constant;
using ART.Domotica.Domain.Interfaces;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationConsumer : ConsumerBase
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
            Console.WriteLine();
            Console.WriteLine("[ApplicationConsumer.GetReceived] {0}", Encoding.UTF8.GetString(e.Body));

            _model.BasicAck(e.DeliveryTag, false);

            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var models = await _applicationDomain.Get(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(models);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, "GetCompleted");

            Console.WriteLine("[ApplicationConsumer.GetCompleted] {0}", Encoding.UTF8.GetString(buffer));
            
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion
    }
}
