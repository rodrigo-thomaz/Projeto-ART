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
        private readonly EventingBasicConsumer _setScaleConsumer;
        private readonly EventingBasicConsumer _setResolutionConsumer;
        private readonly EventingBasicConsumer _setLabelConsumer;
        private readonly EventingBasicConsumer _setAlarmOnConsumer;
        private readonly EventingBasicConsumer _setAlarmCelsiusConsumer;
        private readonly EventingBasicConsumer _setAlarmBuzzerOnConsumer;
        private readonly EventingBasicConsumer _setChartLimiterCelsiusConsumer;
        
        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public DSFamilyTempSensorConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _getAllByDeviceInApplicationIdConsumer = new EventingBasicConsumer(_model);
            _getAllResolutionsConsumer = new EventingBasicConsumer(_model);
            _setScaleConsumer = new EventingBasicConsumer(_model);
            _setResolutionConsumer = new EventingBasicConsumer(_model);
            _setLabelConsumer = new EventingBasicConsumer(_model);
            _setAlarmOnConsumer = new EventingBasicConsumer(_model);
            _setAlarmCelsiusConsumer = new EventingBasicConsumer(_model);
            _setAlarmBuzzerOnConsumer = new EventingBasicConsumer(_model);
            _setChartLimiterCelsiusConsumer = new EventingBasicConsumer(_model);

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
                  queue: DSFamilyTempSensorConstants.SetScaleQueueName
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
                  queue: DSFamilyTempSensorConstants.SetChartLimiterCelsiusQueueName
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
            _setScaleConsumer.Received += SetScaleReceived;
            _setLabelConsumer.Received += SetLabelReceived;
            _setAlarmOnConsumer.Received += SetAlarmOnReceived;
            _setAlarmCelsiusConsumer.Received += SetAlarmCelsiusReceived;
            _setAlarmBuzzerOnConsumer.Received += SetAlarmBuzzerOnReceived;
            _setChartLimiterCelsiusConsumer.Received += SetChartLimiterCelsiusReceived;

            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName, false, _getAllByDeviceInApplicationIdConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllResolutionsQueueName, false, _getAllResolutionsConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetScaleQueueName, false, _setScaleConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetLabelQueueName, false, _setLabelConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetAlarmOnQueueName, false, _setAlarmOnConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetAlarmCelsiusQueueName, false, _setAlarmCelsiusConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetAlarmBuzzerOnQueueName, false, _setAlarmBuzzerOnConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetChartLimiterCelsiusQueueName, false, _setChartLimiterCelsiusConsumer);
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

            var espDeviceDomain = _componentContext.Resolve<IESPDeviceDomain>();
            var applicationMQ = await espDeviceDomain.GetApplicationMQ(requestContract.DeviceId);
            var deviceMQ = await espDeviceDomain.GetDeviceMQ(requestContract.DeviceId);

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
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<DSFamilyTempSensorResolution>, List<DSFamilyTempSensorResolutionDetailModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.SouceMQSession, DSFamilyTempSensorConstants.GetAllResolutionsViewCompletedQueueName);
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
            var data = await domain.SetResolution(message);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensor, DSFamilyTempSensorSetResolutionCompletedModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.SouceMQSession, DSFamilyTempSensorConstants.SetResolutionViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var espDeviceDomain = _componentContext.Resolve<IESPDeviceDomain>();
            var deviceMQ = await espDeviceDomain.GetDeviceMQ(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetResolutionRequestContract, DSFamilyTempSensorSetResolutionRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetResolutionRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetResolutionIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        private void SetScaleReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetScaleReceivedAsync(sender, e));
        }

        private async Task SetScaleReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetScaleRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.SetScale(message);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensor, DSFamilyTempSensorSetScaleCompletedModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.SouceMQSession, DSFamilyTempSensorConstants.SetScaleViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var espDeviceDomain = _componentContext.Resolve<IESPDeviceDomain>();
            var deviceMQ = await espDeviceDomain.GetDeviceMQ(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetScaleRequestContract, DSFamilyTempSensorSetScaleRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetScaleRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetScaleIoTQueueName);
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
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.SetLabel(message);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensorSetLabelRequestContract, DSFamilyTempSensorSetLabelCompletedModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.SouceMQSession, DSFamilyTempSensorConstants.SetLabelViewCompletedQueueName);
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
            var data = await domain.SetAlarmOn(message);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensorSetAlarmOnRequestContract, DSFamilyTempSensorSetAlarmOnCompletedModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.SouceMQSession, DSFamilyTempSensorConstants.SetAlarmOnViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var espDeviceDomain = _componentContext.Resolve<IESPDeviceDomain>();
            var deviceMQ = await espDeviceDomain.GetDeviceMQ(viewModel.DeviceId);

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
            var data = await domain.SetAlarmCelsius(message);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensorSetAlarmCelsiusRequestContract, DSFamilyTempSensorSetAlarmCelsiusCompletedModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.SouceMQSession, DSFamilyTempSensorConstants.SetAlarmCelsiusViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var espDeviceDomain = _componentContext.Resolve<IESPDeviceDomain>();
            var deviceMQ = await espDeviceDomain.GetDeviceMQ(viewModel.DeviceId);

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
            var data = await domain.SetAlarmBuzzerOn(message);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract, DSFamilyTempSensorSetAlarmBuzzerOnCompletedModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.SouceMQSession, DSFamilyTempSensorConstants.SetAlarmBuzzerOnViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var espDeviceDomain = _componentContext.Resolve<IESPDeviceDomain>();
            var deviceMQ = await espDeviceDomain.GetDeviceMQ(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract, DSFamilyTempSensorSetAlarmBuzzerOnRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetAlarmBuzzerOnRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetAlarmBuzzerOnIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetChartLimiterCelsiusReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetChartLimiterCelsiusReceivedAsync(sender, e));
        }

        public async Task SetChartLimiterCelsiusReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetChartLimiterCelsiusRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.SetChartLimiterCelsius(message);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<DSFamilyTempSensorSetChartLimiterCelsiusRequestContract, DSFamilyTempSensorSetChartLimiterCelsiusCompletedModel>(message.Contract);
            viewModel.DeviceId = data.SensorsInDevice.Single().DeviceBaseId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.SouceMQSession, DSFamilyTempSensorConstants.SetChartLimiterCelsiusViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var espDeviceDomain = _componentContext.Resolve<IESPDeviceDomain>();
            var deviceMQ = await espDeviceDomain.GetDeviceMQ(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DSFamilyTempSensorSetChartLimiterCelsiusRequestContract, DSFamilyTempSensorSetChartLimiterCelsiusRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetChartLimiterCelsiusRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);            
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DSFamilyTempSensorConstants.SetChartLimiterCelsiusIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
