using ART.Domotica.Constant;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Model;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Worker.IConsumers;
using ART.Infra.CrossCutting.Logging;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using Autofac;
using AutoMapper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationBrokerSettingConsumer : ConsumerBase, IApplicationBrokerSettingConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getConsumer;

        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public ApplicationBrokerSettingConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _getConsumer = new EventingBasicConsumer(_model);

            _componentContext = componentContext;

            _logger = logger;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            var queueName = ApplicationBrokerSettingConstants.GetQueueName;

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
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IApplicationBrokerSettingDomain>();
            var data = await domain.Get(message);

            var exchange = "amq.topic";

            //Enviando para View
            var viewModel = Mapper.Map<ApplicationBrokerSetting, ApplicationBrokerSettingDetailModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);            
            var rountingKey = GetApplicationRoutingKeyForView(message.SouceMQSession, ApplicationBrokerSettingConstants.GetViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }        

        #endregion
    }
}
