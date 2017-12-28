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

    public class DeviceNTPConsumer : ConsumerBase, IDeviceNTPConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _setTimeZoneConsumer;
        private readonly EventingBasicConsumer _setUpdateIntervalInMilliSecondConsumer;

        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion Fields

        #region Constructors

        public DeviceNTPConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, ISettingManager settingsManager, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            _setTimeZoneConsumer = new EventingBasicConsumer(_model);
            _setUpdateIntervalInMilliSecondConsumer = new EventingBasicConsumer(_model);

            _componentContext = componentContext;

            _logger = logger;

            Initialize();            
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            _model.ExchangeDeclare(
                  exchange: "amq.topic"
                , type: ExchangeType.Topic
                , durable: true
                , autoDelete: false
                , arguments: null);

            BasicQueueDeclare(DeviceNTPConstants.SetTimeZoneQueueName);
            BasicQueueDeclare(DeviceNTPConstants.SetUpdateIntervalInMilliSecondQueueName);

            _setTimeZoneConsumer.Received += SetTimeZoneReceived;
            _setUpdateIntervalInMilliSecondConsumer.Received += SetUpdateIntervalInMilliSecondReceived;

            _model.BasicConsume(DeviceNTPConstants.SetTimeZoneQueueName, false, _setTimeZoneConsumer);
            _model.BasicConsume(DeviceNTPConstants.SetUpdateIntervalInMilliSecondQueueName, false, _setUpdateIntervalInMilliSecondConsumer);
        }

        #endregion Methods

        #region private voids        

        public void SetTimeZoneReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetTimeZoneReceivedAsync(sender, e));
        }

        public async Task SetTimeZoneReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceNTPSetTimeZoneRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceNTPDomain>();
            var data = await domain.SetTimeZone(message.Contract.DeviceNTPId, message.Contract.DeviceDatasheetId, message.Contract.TimeZoneId);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceNTPSetTimeZoneRequestContract, DeviceNTPSetTimeZoneModel>(message.Contract);
            viewModel.DeviceNTPId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceNTPConstants.SetTimeZoneViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(viewModel.DeviceNTPId, viewModel.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceNTP, DeviceNTPSetUtcTimeOffsetInSecondRequestIoTContract>(data);
            var deviceMessage = new MessageIoTContract<DeviceNTPSetUtcTimeOffsetInSecondRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceNTPConstants.SetUtcTimeOffsetInSecondIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetUpdateIntervalInMilliSecondReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetUpdateIntervalInMilliSecondReceivedAsync(sender, e));
        }

        public async Task SetUpdateIntervalInMilliSecondReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceNTPDomain>();
            var data = await domain.SetUpdateIntervalInMilliSecond(message.Contract.DeviceNTPId, message.Contract.DeviceDatasheetId, message.Contract.UpdateIntervalInMilliSecond);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract, DeviceNTPSetUpdateIntervalInMilliSecondModel>(message.Contract);
            viewModel.DeviceNTPId = data.Id;
            viewModel.DeviceDatasheetId = data.DeviceDatasheetId;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceNTPConstants.SetUpdateIntervalInMilliSecondViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(viewModel.DeviceNTPId, viewModel.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract, DeviceNTPSetUpdateIntervalInMilliSecondRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DeviceNTPSetUpdateIntervalInMilliSecondRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceNTPConstants.SetUpdateIntervalInMilliSecondIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Other
    }
}