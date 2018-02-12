namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.Logging;
    using ART.Infra.CrossCutting.MQ.Worker;

    using Autofac;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Threading.Tasks;
    using ART.Infra.CrossCutting.Utils;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Domotica.Domain.Interfaces;
    using global::AutoMapper;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Model;
    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ;
    using ART.Domotica.IoTContract;

    public class DeviceSensorConsumer : ConsumerBase, IDeviceSensorConsumer
    {
        #region Fields
        
        private readonly EventingBasicConsumer _setReadIntervalInMilliSecondsConsumer;
        private readonly EventingBasicConsumer _setPublishIntervalInMilliSecondsConsumer;
        private readonly EventingBasicConsumer _getFullByDeviceInApplicationIdConsumer;

        #endregion Fields

        #region Constructors

        public DeviceSensorConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _setReadIntervalInMilliSecondsConsumer = new EventingBasicConsumer(_model);
            _setPublishIntervalInMilliSecondsConsumer = new EventingBasicConsumer(_model);
            _getFullByDeviceInApplicationIdConsumer = new EventingBasicConsumer(_model);

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceSensorConstants.SetReadIntervalInMilliSecondsQueueName);
            BasicQueueDeclare(DeviceSensorConstants.SetPublishIntervalInMilliSecondsQueueName);
            BasicQueueDeclare(DeviceSensorConstants.GetFullByDeviceInApplicationIdIoTQueueName);

            _model.QueueBind(
                  queue: DeviceSensorConstants.GetFullByDeviceInApplicationIdIoTQueueName
                , exchange: "amq.topic"
                , routingKey: GetApplicationRoutingKeyForAllIoT(DeviceSensorConstants.GetFullByDeviceInApplicationIdIoTQueueName)
                , arguments: CreateBasicArguments());

            _setReadIntervalInMilliSecondsConsumer.Received += SetReadIntervalInMilliSecondsReceived;
            _setPublishIntervalInMilliSecondsConsumer.Received += SetPublishIntervalInMilliSecondsReceived;
            _getFullByDeviceInApplicationIdConsumer.Received += GetFullByDeviceInApplicationIdReceived;

            _model.BasicConsume(DeviceSensorConstants.SetReadIntervalInMilliSecondsQueueName, false, _setReadIntervalInMilliSecondsConsumer);
            _model.BasicConsume(DeviceSensorConstants.SetPublishIntervalInMilliSecondsQueueName, false, _setPublishIntervalInMilliSecondsConsumer);
            _model.BasicConsume(DeviceSensorConstants.GetFullByDeviceInApplicationIdIoTQueueName, false, _getFullByDeviceInApplicationIdConsumer);
        }

        private void SetPublishIntervalInMilliSecondsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetPublishIntervalInMilliSecondsReceivedAsync(sender, e));
        }

        public async Task SetPublishIntervalInMilliSecondsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceSetIntervalInMilliSecondsRequestContract>>(e.Body);
            var deviceSensorDomain = _componentContext.Resolve<IDeviceSensorDomain>();
            var data = await deviceSensorDomain.SetPublishIntervalInMilliSeconds(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.IntervalInMilliSeconds);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceSensor, DeviceSetPublishIntervalInMilliSecondsModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceSensorConstants.SetPublishIntervalInMilliSecondsViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<long>(data.PublishIntervalInMilliSeconds);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceSensorConstants.SetPublishIntervalInMilliSecondsIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        private void SetReadIntervalInMilliSecondsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetReadIntervalInMilliSecondsReceivedAsync(sender, e));
        }

        public async Task SetReadIntervalInMilliSecondsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceSetIntervalInMilliSecondsRequestContract>>(e.Body);
            var deviceSensorDomain = _componentContext.Resolve<IDeviceSensorDomain>();
            var data = await deviceSensorDomain.SetReadIntervalInMilliSeconds(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.IntervalInMilliSeconds);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceSensor, DeviceSensorSetReadIntervalInMilliSecondsModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceSensorConstants.SetReadIntervalInMilliSecondsViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<long>(data.ReadIntervalInMilliSeconds);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceSensorConstants.SetReadIntervalInMilliSecondsIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        private void GetFullByDeviceInApplicationIdReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetFullByDeviceInApplicationIdReceivedAsync(sender, e));
        }

        private async Task GetFullByDeviceInApplicationIdReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<IoTRequestContract>(e.Body);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByDeviceId(requestContract.DeviceId);

            var domain = _componentContext.Resolve<IDeviceSensorDomain>();
            var data = await domain.GetFullByDeviceInApplicationId(applicationMQ.Id, requestContract.DeviceTypeId, requestContract.DeviceDatasheetId, requestContract.DeviceId);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(requestContract.DeviceTypeId, requestContract.DeviceDatasheetId, requestContract.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceSensor, DeviceSensorGetResponseIoTContract>(data);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceSensorConstants.GetFullByDeviceInApplicationIdCompletedIoTQueueName);
            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Methods
    }
}