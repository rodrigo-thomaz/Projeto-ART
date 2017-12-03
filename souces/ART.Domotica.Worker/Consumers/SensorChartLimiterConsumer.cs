using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Contract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Worker.IConsumers;
using ART.Domotica.IoTContract;
using Autofac;
using ART.Infra.CrossCutting.Logging;
using AutoMapper;
using ART.Domotica.Model;

namespace ART.Domotica.Worker.Consumers
{
    public class SensorChartLimiterConsumer : ConsumerBase, ISensorChartLimiterConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _setValueConsumer;
        
        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public SensorChartLimiterConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _setValueConsumer = new EventingBasicConsumer(_model);

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
                  queue: SensorChartLimiterConstants.SetValueQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _setValueConsumer.Received += SetValueReceived;

            _model.BasicConsume(SensorChartLimiterConstants.SetValueQueueName, false, _setValueConsumer);
        }

        public void SetValueReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetValueReceivedAsync(sender, e));
        }

        public async Task SetValueReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorChartLimiterSetValueRequestContract>>(e.Body);
            var sensorChartLimiterDomain = _componentContext.Resolve<ISensorChartLimiterDomain>();
            await sensorChartLimiterDomain.SetValue(message.Contract.SensorChartLimiterId, message.Contract.Position, message.Contract.Value);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(message.Contract.SensorChartLimiterId);

            //Enviando para View
            var viewModel = Mapper.Map<SensorChartLimiterSetValueRequestContract, SensorChartLimiterSetValueCompletedModel>(message.Contract);
            viewModel.DeviceId = device.DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorChartLimiterConstants.SetValueViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorChartLimiterSetValueRequestContract, SensorChartLimiterSetValueRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorChartLimiterSetValueRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);            
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorChartLimiterConstants.SetValueIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
