using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Worker.IConsumers;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Security.Constant;
using ART.Security.Contract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationUserConsumer : ConsumerBase, IApplicationUserConsumer
    {
        #region private fields
        
        private readonly EventingBasicConsumer _registerUserConsumer;

        private readonly IApplicationUserDomain _applicationUserDomain;

        #endregion

        #region constructors

        public ApplicationUserConsumer(IConnection connection, IApplicationUserDomain applicationUserDomain) : base(connection)
        {
            _registerUserConsumer = new EventingBasicConsumer(_model);

            _applicationUserDomain = applicationUserDomain;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            var queueName = ApplicationUserQueueName.RegisterUserQueueName;

            _model.QueueDeclare(
                 queue: queueName
               , durable: true
               , exclusive: false
               , autoDelete: false
               , arguments: null);

            _registerUserConsumer.Received += RegisterUserReceived;

            _model.BasicConsume(queueName, false, _registerUserConsumer);
        }

        public void RegisterUserReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(RegisterUserAsync(sender, e));
        }

        public async Task RegisterUserAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[{0}] {1}", ApplicationUserQueueName.RegisterUserQueueName, Encoding.UTF8.GetString(e.Body));

            _model.BasicAck(e.DeliveryTag, false);

            var message = SerializationHelpers.DeserializeJsonBufferToType<NoAuthenticatedMessageContract<RegisterUserContract>>(e.Body);
            await _applicationUserDomain.RegisterUser(message.Contract);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ApplicationUserQueueName.RegisterUserCompletedQueueName);

            Console.WriteLine("[{0}] Ok", ApplicationUserQueueName.RegisterUserCompletedQueueName);

            _model.BasicPublish(exchange, rountingKey, null, null);
        }

        #endregion
    }
}
