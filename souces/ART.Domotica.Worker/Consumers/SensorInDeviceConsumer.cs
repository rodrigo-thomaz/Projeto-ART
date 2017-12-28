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
    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using global::AutoMapper;
    using ART.Infra.CrossCutting.MQ;

    public class SensorInDeviceConsumer : ConsumerBase, ISensorInDeviceConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _setOrdinationConsumer;

        #endregion Fields

        #region Constructors

        public SensorInDeviceConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _setOrdinationConsumer = new EventingBasicConsumer(_model);

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(SensorInDeviceConstants.SetOrdinationQueueName);

            _setOrdinationConsumer.Received += SetOrdinationReceived;

            _model.BasicConsume(SensorInDeviceConstants.SetOrdinationQueueName, false, _setOrdinationConsumer);
        }

        private void SetOrdinationReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetOrdinationReceivedAsync(sender, e));
        }

        public async Task SetOrdinationReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorInDeviceSetOrdinationRequestContract>>(e.Body);
            var sensorInDeviceDomain = _componentContext.Resolve<ISensorInDeviceDomain>();
            var data = await sensorInDeviceDomain.SetOrdination(message.Contract.DeviceSensorsId, message.Contract.DeviceDatasheetId, message.Contract.SensorId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.Ordination);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<SensorInDevice, SensorInDeviceSetOrdinationModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorInDeviceConstants.SetOrdinationViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        #endregion Methods
    }
}