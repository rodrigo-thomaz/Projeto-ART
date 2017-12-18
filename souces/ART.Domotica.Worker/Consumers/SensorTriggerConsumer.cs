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
    public class SensorTriggerConsumer : ConsumerBase, ISensorTriggerConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _setTriggerOnConsumer;
        private readonly EventingBasicConsumer _setTriggerValueConsumer;
        private readonly EventingBasicConsumer _setBuzzerOnConsumer;
        
        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public SensorTriggerConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _setTriggerOnConsumer = new EventingBasicConsumer(_model);
            _setTriggerValueConsumer = new EventingBasicConsumer(_model);
            _setBuzzerOnConsumer = new EventingBasicConsumer(_model);

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
                  queue: SensorTriggerConstants.SetTriggerOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetTriggerValueQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetBuzzerOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _setTriggerOnConsumer.Received += SetTriggerOnReceived;
            _setTriggerValueConsumer.Received += SetTriggerValueReceived;
            _setBuzzerOnConsumer.Received += SetBuzzerOnReceived;

            _model.BasicConsume(SensorTriggerConstants.SetTriggerOnQueueName, false, _setTriggerOnConsumer);
            _model.BasicConsume(SensorTriggerConstants.SetTriggerValueQueueName, false, _setTriggerValueConsumer);
            _model.BasicConsume(SensorTriggerConstants.SetBuzzerOnQueueName, false, _setBuzzerOnConsumer);
        }       

        public void SetTriggerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetTriggerOnReceivedAsync(sender, e));
        }

        public async Task SetTriggerOnReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorTriggerSetTriggerOnRequestContract>>(e.Body);
            var sensorTriggerDomain = _componentContext.Resolve<ISensorTriggerDomain>();
            var data = await sensorTriggerDomain.SetTriggerOn(message.Contract.SensorTriggerId, message.Contract.SensorId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.TriggerOn);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(data.SensorId);

            //Enviando para View
            var viewModel = Mapper.Map<SensorTriggerSetTriggerOnRequestContract, SensorTriggerSetTriggerOnModel>(message.Contract);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorTriggerConstants.SetTriggerOnViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(device.DeviceSensorsId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorTriggerSetTriggerOnRequestContract, SensorTriggerSetTriggerOnRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorTriggerSetTriggerOnRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorTriggerConstants.SetTriggerOnIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetTriggerValueReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetTriggerValueReceivedAsync(sender, e));
        }

        public async Task SetTriggerValueReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorTriggerSetTriggerValueRequestContract>>(e.Body);
            var sensorTriggerDomain = _componentContext.Resolve<ISensorTriggerDomain>();
            var data = await sensorTriggerDomain.SetTriggerValue(message.Contract.SensorTriggerId, message.Contract.SensorId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.Position, message.Contract.TriggerValue);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<SensorTriggerSetTriggerValueRequestContract, SensorTriggerSetTriggerValueModel>(message.Contract);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorTriggerConstants.SetTriggerValueViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(device.DeviceSensorsId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorTriggerSetTriggerValueRequestContract, SensorTriggerSetTriggerValueRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorTriggerSetTriggerValueRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorTriggerConstants.SetTriggerValueIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetBuzzerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetBuzzerOnReceivedAsync(sender, e));
        }

        public async Task SetBuzzerOnReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorTriggerSetBuzzerOnRequestContract>>(e.Body);
            var sensorTriggerDomain = _componentContext.Resolve<ISensorTriggerDomain>();
            var data = await sensorTriggerDomain.SetBuzzerOn(message.Contract.SensorTriggerId, message.Contract.SensorId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.BuzzerOn);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<SensorTriggerSetBuzzerOnRequestContract, SensorTriggerSetBuzzerOnModel>(message.Contract);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorTriggerConstants.SetBuzzerOnViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(device.DeviceSensorsId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorTriggerSetBuzzerOnRequestContract, SensorTriggerSetBuzzerOnRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorTriggerSetBuzzerOnRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorTriggerConstants.SetBuzzerOnIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
