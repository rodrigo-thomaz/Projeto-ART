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
    using ART.Infra.CrossCutting.Utils;
    using Autofac;
    using global::AutoMapper;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Threading.Tasks;

    public class DeviceSerialConsumer : ConsumerBase, IDeviceSerialConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _setEnabledConsumer;

        #endregion Fields

        #region Constructors

        public DeviceSerialConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _setEnabledConsumer = new EventingBasicConsumer(_model);

            Initialize();            
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceSerialConstants.SetEnabledQueueName);

            _setEnabledConsumer.Received += SetEnabledReceived;

            _model.BasicConsume(DeviceSerialConstants.SetEnabledQueueName, false, _setEnabledConsumer);
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
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceSerialSetEnabledRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceSerialDomain>();
            var data = await domain.SetEnabled(message.Contract.DeviceSerialId, message.Contract.DeviceId, message.Contract.DeviceDatasheetId, message.Contract.Enabled);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceSerial, DeviceSerialSetEnabledModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceSerialConstants.SetEnabledViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(viewModel.DeviceId, viewModel.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.Enabled);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceSerialConstants.SetEnabledIoTQueueName);
            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Other
    }
}