namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Contract;
    using ART.Domotica.Domain.Interfaces;
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

    public class DeviceDebugConsumer : ConsumerBase, IDeviceDebugConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _setActiveConsumer;

        #endregion Fields

        #region Constructors

        public DeviceDebugConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, ISettingManager settingsManager, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _setActiveConsumer = new EventingBasicConsumer(_model);

            Initialize();            
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceDebugConstants.SetActiveQueueName);

            _setActiveConsumer.Received += SetActiveReceived;

            _model.BasicConsume(DeviceDebugConstants.SetActiveQueueName, false, _setActiveConsumer);
        }

        #endregion Methods

        #region private voids        

        public void SetActiveReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetActiveReceivedAsync(sender, e));
        }

        public async Task SetActiveReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetActiveRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetActive(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Active);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetActiveRequestContract, DeviceDebugSetActiveModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetActiveViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        #endregion Other
    }
}