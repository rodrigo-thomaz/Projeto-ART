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

    public class DeviceDisplayConsumer : ConsumerBase, IDeviceDisplayConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _setEnabledConsumer;

        #endregion Fields

        #region Constructors

        public DeviceDisplayConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, ISettingManager settingsManager, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _setEnabledConsumer = new EventingBasicConsumer(_model);

            Initialize();            
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceDisplayConstants.SetEnabledQueueName);

            _setEnabledConsumer.Received += SetEnabledReceived;

            _model.BasicConsume(DeviceDisplayConstants.SetEnabledQueueName, false, _setEnabledConsumer);
        }

        #endregion Methods

        #region private voids 

        public void SetEnabledReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetEnabledReceivedAsync(sender, e));
        }

        public async Task SetEnabledReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDisplaySetValueRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDisplayDomain>();
            var data = await domain.SetEnabled(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDisplaySetValueRequestContract, DeviceDisplaySetValueModel>(message.Contract);
            viewModel.DeviceId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDisplayConstants.SetEnabledViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.Enabled);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDisplayConstants.SetEnabledIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Other
    }
}