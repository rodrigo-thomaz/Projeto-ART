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

        private readonly EventingBasicConsumer _getAllConsumer;

        private readonly IApplicationDomain _applicationDomain;

        #endregion

        #region constructors

        public ApplicationConsumer(IConnection connection, IApplicationDomain applicationDomain) : base(connection)
        {
            _getAllConsumer = new EventingBasicConsumer(_model);

            _applicationDomain = applicationDomain;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            var queueName = ApplicationConstants.GetAllQueueName;

            _model.QueueDeclare(
                 queue: queueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _getAllConsumer.Received += GetAllReceived;

            _model.BasicConsume(queueName, false, _getAllConsumer);
        }

        private void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        private async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[ApplicationConsumer.GetAllReceived] {0}", Encoding.UTF8.GetString(e.Body));

            _model.BasicAck(e.DeliveryTag, false);

            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var models = await _applicationDomain.GetAll(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(models);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, "GetAllCompleted");

            Console.WriteLine("[ApplicationConsumer.GetAllCompleted] {0}", Encoding.UTF8.GetString(buffer));
            
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion
    }
}
