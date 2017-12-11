using ART.Domotica.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Worker.IConsumers;
using Autofac;
using AutoMapper;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Model;
using ART.Infra.CrossCutting.Logging;
using ART.Domotica.Contract;

namespace ART.Domotica.Worker.Consumers
{
    public class HardwareConsumer : ConsumerBase, IHardwareConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _setLabelConsumer;

        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public HardwareConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _setLabelConsumer = new EventingBasicConsumer(_model);

            _componentContext = componentContext;

            _logger = logger;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.ExchangeDeclare(
                  exchange: "amq.topic"
                , type: ExchangeType.Topic
                , durable: true
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: HardwareConstants.SetLabelQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _setLabelConsumer.Received += SetLabelReceived;
                        
            _model.BasicConsume(HardwareConstants.SetLabelQueueName, false, _setLabelConsumer);
        }        
        
        public void SetLabelReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetLabelReceivedAsync(sender, e));
        }

        public async Task SetLabelReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<HardwareSetLabelRequestContract>>(e.Body);
            var hardwareDomain = _componentContext.Resolve<IHardwareDomain>();
            var data = await hardwareDomain.SetLabel(message.Contract.HardwareId, message.Contract.Label);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<HardwareBase, HardwareSetLabelModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, HardwareConstants.SetLabelViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
