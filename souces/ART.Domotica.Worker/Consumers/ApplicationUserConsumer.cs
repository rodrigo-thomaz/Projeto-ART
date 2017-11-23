using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Worker.IConsumers;
using ART.Infra.CrossCutting.Logging;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Security.Constant;
using ART.Security.Contract;
using Autofac;
using AutoMapper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationUserConsumer : ConsumerBase, IApplicationUserConsumer
    {
        #region private fields
        
        private readonly EventingBasicConsumer _registerUserConsumer;

        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public ApplicationUserConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _registerUserConsumer = new EventingBasicConsumer(_model);

            _componentContext = componentContext;

            _logger = logger;

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
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<NoAuthenticatedMessageContract<RegisterUserContract>>(e.Body);
            var domain = _componentContext.Resolve<IApplicationUserDomain>();
            var applicationUserEntity = Mapper.Map<RegisterUserContract, ApplicationUser>(message.Contract);
            await domain.RegisterUser(applicationUserEntity);

            var exchange = "amq.topic";

            //Enviando para View            
            var rountingKey = GetApplicationRoutingKeyForView(message.SouceMQSession, ApplicationUserQueueName.RegisterUserViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, null);

            _logger.DebugLeave();
        }

        #endregion
    }
}
