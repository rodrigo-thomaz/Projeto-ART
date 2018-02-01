namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Contract;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.Logging;
    using ART.Infra.CrossCutting.MQ;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Worker;
    using ART.Infra.CrossCutting.Setting;
    using ART.Infra.CrossCutting.Utils;
    using Autofac;
    using global::AutoMapper;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Threading.Tasks;

    public class DeviceWiFiConsumer : ConsumerBase, IDeviceWiFiConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _setHostNameConsumer;
        private readonly EventingBasicConsumer _setPublishIntervalInMilliSecondsConsumer;

        #endregion Fields

        #region Constructors

        public DeviceWiFiConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, ISettingManager settingsManager, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _setHostNameConsumer = new EventingBasicConsumer(_model);
            _setPublishIntervalInMilliSecondsConsumer = new EventingBasicConsumer(_model);

            Initialize();            
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceWiFiConstants.SetHostNameQueueName);
            BasicQueueDeclare(DeviceWiFiConstants.SetPublishIntervalInMilliSecondsQueueName);

            _setHostNameConsumer.Received += SetHostNameReceived;
            _setPublishIntervalInMilliSecondsConsumer.Received += SetPublishIntervalInMilliSecondsReceived;

            _model.BasicConsume(DeviceWiFiConstants.SetHostNameQueueName, false, _setHostNameConsumer);
            _model.BasicConsume(DeviceWiFiConstants.SetPublishIntervalInMilliSecondsQueueName, false, _setPublishIntervalInMilliSecondsConsumer);
        }

        #endregion Methods

        #region private voids        

        public void SetHostNameReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetHostNameReceivedAsync(sender, e));
        }

        public async Task SetHostNameReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceWiFiSetHostNameRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceWiFiDomain>();
            var data = await domain.SetHostName(message.Contract.DeviceWiFiId, message.Contract.DeviceDatasheetId, message.Contract.HostName);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceWiFiSetHostNameRequestContract, DeviceWiFiSetHostNameModel>(message.Contract);
            viewModel.DeviceWiFiId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceWiFiConstants.SetHostNameViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<string>(data.HostName);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceWiFiConstants.SetHostNameIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
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
            var deviceWiFiDomain = _componentContext.Resolve<IDeviceWiFiDomain>();
            var data = await deviceWiFiDomain.SetPublishIntervalInMilliSeconds(message.Contract.DeviceId, message.Contract.DeviceDatasheetId, message.Contract.IntervalInMilliSeconds);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceWiFi, DeviceSetPublishIntervalInMilliSecondsModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceWiFiConstants.SetPublishIntervalInMilliSecondsViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<int>(data.PublishIntervalInMilliSeconds);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceWiFiConstants.SetPublishIntervalInMilliSecondsIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Other
    }
}