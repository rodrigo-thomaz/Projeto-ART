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
    using ART.Domotica.IoTContract;

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
            var data = await sensorInDeviceDomain.SetOrdination(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.SensorId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.Ordination);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<SensorInDevice, SensorInDeviceSetOrdinationModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorInDeviceConstants.SetOrdinationViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorInDevice, SetOrdinationRequestIoTContract>(data);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorInDeviceConstants.SetOrdinationIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Methods
    }
}