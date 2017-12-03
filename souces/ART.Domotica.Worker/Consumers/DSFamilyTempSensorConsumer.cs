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

namespace ART.Domotica.Worker.Consumers
{
    public class DSFamilyTempSensorConsumer : ConsumerBase, IDSFamilyTempSensorConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllByDeviceInApplicationIdConsumer;
        private readonly EventingBasicConsumer _getAllResolutionsConsumer;
        private readonly EventingBasicConsumer _setUnitMeasurementConsumer;
        private readonly EventingBasicConsumer _setResolutionConsumer;
        private readonly EventingBasicConsumer _setLabelConsumer;
        
        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public DSFamilyTempSensorConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _getAllByDeviceInApplicationIdConsumer = new EventingBasicConsumer(_model);
            _getAllResolutionsConsumer = new EventingBasicConsumer(_model);
            _setUnitMeasurementConsumer = new EventingBasicConsumer(_model);
            _setResolutionConsumer = new EventingBasicConsumer(_model);
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
                  queue: DSFamilyTempSensorConstants.GetAllResolutionsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetUnitMeasurementQueueName
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
            _setUnitMeasurementConsumer.Received += SetUnitMeasurementReceived;
            _setLabelConsumer.Received += SetLabelReceived;

            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName, false, _getAllByDeviceInApplicationIdConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllResolutionsQueueName, false, _getAllResolutionsConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetUnitMeasurementQueueName, false, _setUnitMeasurementConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetLabelQueueName, false, _setLabelConsumer);            
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
            var domain = _componentContext.Resolve<ISensorDomain>();            
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
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
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
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensor, DSFamilyTempSensorSetResolutionCompletedModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
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

        private void SetUnitMeasurementReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetUnitMeasurementReceivedAsync(sender, e));
        }

        private async Task SetUnitMeasurementReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetUnitMeasurementRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<ISensorDomain>();
            var data = await domain.SetUnitMeasurement(message.Contract.DSFamilyTempSensorId, message.Contract.UnitMeasurementId);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var device = await domain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<Sensor, DSFamilyTempSensorSetUnitMeasurementCompletedModel>(data);            
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DSFamilyTempSensorConstants.SetUnitMeasurementViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetUnitMeasurementRequestContract, DSFamilyTempSensorSetUnitMeasurementRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetUnitMeasurementRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetUnitMeasurementIoTQueueName);
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

            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(message.Contract.DSFamilyTempSensorId);

            //Enviando para View
            var viewModel = new DSFamilyTempSensorSetLabelCompletedModel { DeviceId = device.DeviceBaseId };
            Mapper.Map(data, viewModel);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DSFamilyTempSensorConstants.SetLabelViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }
                
        #endregion
    }
}
