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
using System.Collections.Generic;
using ART.Domotica.IoTContract;
using Autofac;
using ART.Infra.CrossCutting.Logging;
using AutoMapper;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Model;
using System.Linq;

namespace ART.Domotica.Worker.Consumers
{
    public class DSFamilyTempSensorConsumer : ConsumerBase, IDSFamilyTempSensorConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllByDeviceInApplicationIdConsumer;
        private readonly EventingBasicConsumer _getAllResolutionsConsumer;
        private readonly EventingBasicConsumer _setUnitOfMeasurementConsumer;
        private readonly EventingBasicConsumer _setResolutionConsumer;
        private readonly EventingBasicConsumer _setLabelConsumer;
        private readonly EventingBasicConsumer _setAlarmOnConsumer;
        private readonly EventingBasicConsumer _setAlarmCelsiusConsumer;
        private readonly EventingBasicConsumer _setAlarmBuzzerOnConsumer;
        
        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public DSFamilyTempSensorConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _getAllByDeviceInApplicationIdConsumer = new EventingBasicConsumer(_model);
            _getAllResolutionsConsumer = new EventingBasicConsumer(_model);
            _setUnitOfMeasurementConsumer = new EventingBasicConsumer(_model);
            _setResolutionConsumer = new EventingBasicConsumer(_model);
            _setLabelConsumer = new EventingBasicConsumer(_model);
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
                  queue: DSFamilyTempSensorConstants.GetAllResolutionsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetUnitOfMeasurementQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                 queue: DSFamilyTempSensorConstants.SetResolutionQueueName
               , durable: true
               , exclusive: false
               , autoDelete: false
               , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetLabelQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetAlarmOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetAlarmCelsiusQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetAlarmBuzzerOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                queue: DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName
              , durable: false
              , exclusive: false
              , autoDelete: false
              , arguments: null);

            _model.QueueBind(
                  queue: DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName
                , exchange: "amq.topic"
                , routingKey: GetApplicationRoutingKeyForAllIoT(DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName)
                , arguments: null);

            _getAllByDeviceInApplicationIdConsumer.Received += GetAllByDeviceInApplicationIdReceived;
            _getAllResolutionsConsumer.Received += GetAllResolutionsReceived;
            _setResolutionConsumer.Received += SetResolutionReceived;
            _setUnitOfMeasurementConsumer.Received += SetUnitOfMeasurementReceived;
            _setLabelConsumer.Received += SetLabelReceived;
            _setAlarmOnConsumer.Received += SetAlarmOnReceived;
            _setAlarmCelsiusConsumer.Received += SetAlarmCelsiusReceived;
            _setAlarmBuzzerOnConsumer.Received += SetAlarmBuzzerOnReceived;

            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName, false, _getAllByDeviceInApplicationIdConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllResolutionsQueueName, false, _getAllResolutionsConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetUnitOfMeasurementQueueName, false, _setUnitOfMeasurementConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetLabelQueueName, false, _setLabelConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetAlarmOnQueueName, false, _setAlarmOnConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetAlarmCelsiusQueueName, false, _setAlarmCelsiusConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetAlarmBuzzerOnQueueName, false, _setAlarmBuzzerOnConsumer);
        }
                
        private void GetAllByDeviceInApplicationIdReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllByDeviceInApplicationIdReceivedAsync(sender, e));
        }

        private async Task GetAllByDeviceInApplicationIdReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<IoTRequestContract>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();            
            var data = await domain.GetAllByDeviceInApplicationId(requestContract.DeviceInApplicationId);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByDeviceId(requestContract.DeviceId);
            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(requestContract.DeviceId);

            var exchange = "amq.topic";

            //Enviando para o Iot
            var iotContract = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseIoTContract>>(data);
            var deviceMessage = new MessageIoTContract<List<DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseIoTContract>>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);            
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdCompletedIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void GetAllResolutionsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllResolutionsReceivedAsync(sender, e));            
        }

        public async Task GetAllResolutionsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.GetAllResolutions();

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<DSFamilyTempSensorResolution>, List<DSFamilyTempSensorResolutionDetailModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, DSFamilyTempSensorConstants.GetAllResolutionsViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        public void SetResolutionReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetResolutionReceivedAsync(sender, e));         
        }

        public async Task SetResolutionReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.SetResolution(message.Contract.DSFamilyTempSensorId, message.Contract.DSFamilyTempSensorResolutionId);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var device = await domain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensor, DSFamilyTempSensorSetResolutionCompletedModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DSFamilyTempSensorConstants.SetResolutionViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetResolutionRequestContract, DSFamilyTempSensorSetResolutionRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetResolutionRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetResolutionIoTQueueName);
            
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        private void SetUnitOfMeasurementReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetUnitOfMeasurementReceivedAsync(sender, e));
        }

        private async Task SetUnitOfMeasurementReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetUnitOfMeasurementRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.SetUnitOfMeasurement(message.Contract.DSFamilyTempSensorId, message.Contract.UnitOfMeasurementId);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var device = await domain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensor, DSFamilyTempSensorSetUnitOfMeasurementCompletedModel>(data);            
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DSFamilyTempSensorConstants.SetUnitOfMeasurementViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetUnitOfMeasurementRequestContract, DSFamilyTempSensorSetUnitOfMeasurementRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetUnitOfMeasurementRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetUnitOfMeasurementIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetLabelReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetLabelReceivedAsync(sender, e));
        }

        public async Task SetLabelReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetLabelRequestContract>>(e.Body);
            var hardwareDomain = _componentContext.Resolve<IHardwareDomain>();
            var data = await hardwareDomain.SetLabel(message.Contract.DSFamilyTempSensorId, message.Contract.Label);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            var dsFamilyTempSensorDomain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var device = await dsFamilyTempSensorDomain.GetDeviceFromSensor(message.Contract.DSFamilyTempSensorId);

            //Enviando para View
            var viewModel = new DSFamilyTempSensorSetLabelCompletedModel { DeviceId = device.DeviceBaseId };
            Mapper.Map(data, viewModel);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DSFamilyTempSensorConstants.SetLabelViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        public void SetAlarmOnReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetAlarmOnReceivedAsync(sender, e));
        }

        public async Task SetAlarmOnReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmOnRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.SetAlarmOn(message.Contract.DSFamilyTempSensorId, message.Contract.Position, message.Contract.AlarmOn);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var device = await domain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensorSetAlarmOnRequestContract, DSFamilyTempSensorSetAlarmOnCompletedModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DSFamilyTempSensorConstants.SetAlarmOnViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetAlarmOnRequestContract, DSFamilyTempSensorSetAlarmOnRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetAlarmOnRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetAlarmOnIoTQueueName);
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
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmCelsiusRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.SetAlarmCelsius(message.Contract.DSFamilyTempSensorId, message.Contract.Position, message.Contract.AlarmCelsius);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var device = await domain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensorSetAlarmCelsiusRequestContract, DSFamilyTempSensorSetAlarmCelsiusCompletedModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DSFamilyTempSensorConstants.SetAlarmCelsiusViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetAlarmCelsiusRequestContract, DSFamilyTempSensorSetAlarmCelsiusRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetAlarmCelsiusRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetAlarmCelsiusIoTQueueName);
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
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.SetAlarmBuzzerOn(message.Contract.DSFamilyTempSensorId, message.Contract.Position, message.Contract.AlarmBuzzerOn);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var device = await domain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract, DSFamilyTempSensorSetAlarmBuzzerOnCompletedModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DSFamilyTempSensorConstants.SetAlarmBuzzerOnViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract, DSFamilyTempSensorSetAlarmBuzzerOnRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetAlarmBuzzerOnRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetAlarmBuzzerOnIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
