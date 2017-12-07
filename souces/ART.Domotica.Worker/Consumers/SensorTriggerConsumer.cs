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
using System.Linq;

namespace ART.Domotica.Worker.Consumers
{
    public class SensorTriggerConsumer : ConsumerBase, ISensorTriggerConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _setAlarmOnConsumer;
        private readonly EventingBasicConsumer _setAlarmCelsiusConsumer;
        private readonly EventingBasicConsumer _setAlarmBuzzerOnConsumer;
        
        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public SensorTriggerConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _setAlarmOnConsumer = new EventingBasicConsumer(_model);
            _setAlarmCelsiusConsumer = new EventingBasicConsumer(_model);
            _setAlarmBuzzerOnConsumer = new EventingBasicConsumer(_model);

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
                  queue: SensorTriggerConstants.SetAlarmOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetAlarmCelsiusQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetAlarmBuzzerOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _setAlarmOnConsumer.Received += SetAlarmOnReceived;
            _setAlarmCelsiusConsumer.Received += SetAlarmCelsiusReceived;
            _setAlarmBuzzerOnConsumer.Received += SetAlarmBuzzerOnReceived;

            _model.BasicConsume(SensorTriggerConstants.SetAlarmOnQueueName, false, _setAlarmOnConsumer);
            _model.BasicConsume(SensorTriggerConstants.SetAlarmCelsiusQueueName, false, _setAlarmCelsiusConsumer);
            _model.BasicConsume(SensorTriggerConstants.SetAlarmBuzzerOnQueueName, false, _setAlarmBuzzerOnConsumer);
        }       

        public void SetAlarmOnReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetAlarmOnReceivedAsync(sender, e));
        }

        public async Task SetAlarmOnReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorTriggerSetAlarmOnRequestContract>>(e.Body);
            var sensorTriggerDomain = _componentContext.Resolve<ISensorTriggerDomain>();
            var data = await sensorTriggerDomain.SetAlarmOn(message.Contract.SensorTempDSFamilyId, message.Contract.Position, message.Contract.AlarmOn);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<SensorTriggerSetAlarmOnRequestContract, SensorTriggerSetAlarmOnModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceSensorsId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorTriggerConstants.SetAlarmOnViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorTriggerSetAlarmOnRequestContract, SensorTriggerSetAlarmOnRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorTriggerSetAlarmOnRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorTriggerConstants.SetAlarmOnIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetAlarmCelsiusReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetAlarmCelsiusReceivedAsync(sender, e));
        }

        public async Task SetAlarmCelsiusReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorTriggerSetAlarmCelsiusRequestContract>>(e.Body);
            var sensorTriggerDomain = _componentContext.Resolve<ISensorTriggerDomain>();
            var data = await sensorTriggerDomain.SetAlarmCelsius(message.Contract.SensorTempDSFamilyId, message.Contract.Position, message.Contract.AlarmCelsius);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<SensorTriggerSetAlarmCelsiusRequestContract, SensorTriggerSetAlarmCelsiusModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceSensorsId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorTriggerConstants.SetAlarmCelsiusViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorTriggerSetAlarmCelsiusRequestContract, SensorTriggerSetAlarmCelsiusRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorTriggerSetAlarmCelsiusRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorTriggerConstants.SetAlarmCelsiusIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetAlarmBuzzerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetAlarmBuzzerOnReceivedAsync(sender, e));
        }

        public async Task SetAlarmBuzzerOnReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorTriggerSetAlarmBuzzerOnRequestContract>>(e.Body);
            var sensorTriggerDomain = _componentContext.Resolve<ISensorTriggerDomain>();
            var data = await sensorTriggerDomain.SetAlarmBuzzerOn(message.Contract.SensorTempDSFamilyId, message.Contract.Position, message.Contract.AlarmBuzzerOn);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<SensorTriggerSetAlarmBuzzerOnRequestContract, SensorTriggerSetAlarmBuzzerOnModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceSensorsId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorTriggerConstants.SetAlarmBuzzerOnViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorTriggerSetAlarmBuzzerOnRequestContract, SensorTriggerSetAlarmBuzzerOnRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorTriggerSetAlarmBuzzerOnRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorTriggerConstants.SetAlarmBuzzerOnIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
