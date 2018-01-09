namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Contract;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
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

        #endregion Fields

        #region Constructors

        public DeviceWiFiConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, ISettingManager settingsManager, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _setHostNameConsumer = new EventingBasicConsumer(_model);

            Initialize();            
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceWiFiConstants.SetHostNameQueueName);

            _setHostNameConsumer.Received += SetHostNameReceived;

            _model.BasicConsume(DeviceWiFiConstants.SetHostNameQueueName, false, _setHostNameConsumer);
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

        #endregion Other
    }
}