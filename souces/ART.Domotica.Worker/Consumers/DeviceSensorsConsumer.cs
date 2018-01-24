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
    using System.Collections.Generic;

    public class DeviceSensorsConsumer : ConsumerBase, IDeviceSensorsConsumer
    {
        #region Fields
        
        private readonly EventingBasicConsumer _setPublishIntervalInMilliSecondsConsumer;
        private readonly EventingBasicConsumer _getFullByDeviceInApplicationIdConsumer;

        #endregion Fields

        #region Constructors

        public DeviceSensorsConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {            
            _setPublishIntervalInMilliSecondsConsumer = new EventingBasicConsumer(_model);
            _getFullByDeviceInApplicationIdConsumer = new EventingBasicConsumer(_model);

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceSensorsConstants.SetPublishIntervalInMilliSecondsQueueName);
            BasicQueueDeclare(DeviceSensorsConstants.GetFullByDeviceInApplicationIdIoTQueueName);

            _model.QueueBind(
                  queue: DeviceSensorsConstants.GetFullByDeviceInApplicationIdIoTQueueName
                , exchange: "amq.topic"
                , routingKey: GetApplicationRoutingKeyForAllIoT(DeviceSensorsConstants.GetFullByDeviceInApplicationIdIoTQueueName)
                , arguments: CreateBasicArguments());

            _setPublishIntervalInMilliSecondsConsumer.Received += SetPublishIntervalInMilliSecondsReceived;
            _getFullByDeviceInApplicationIdConsumer.Received += GetFullByDeviceInApplicationIdReceived;

            _model.BasicConsume(DeviceSensorsConstants.SetPublishIntervalInMilliSecondsQueueName, false, _setPublishIntervalInMilliSecondsConsumer);
            _model.BasicConsume(DeviceSensorsConstants.GetFullByDeviceInApplicationIdIoTQueueName, false, _getFullByDeviceInApplicationIdConsumer);
        }

        private void SetPublishIntervalInMilliSecondsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetPublishIntervalInMilliSecondsReceivedAsync(sender, e));
        }

        public async Task SetPublishIntervalInMilliSecondsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceSensorsSetPublishIntervalInMilliSecondsRequestContract>>(e.Body);
            var deviceSensorsDomain = _componentContext.Resolve<IDeviceSensorsDomain>();
            var data = await deviceSensorsDomain.SetPublishIntervalInMilliSeconds(message.Contract.DeviceSensorsId, message.Contract.DeviceDatasheetId, message.Contract.PublishIntervalInMilliSeconds);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceSensors, DeviceSensorsSetPublishIntervalInMilliSecondsModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceSensorsConstants.SetPublishIntervalInMilliSecondsViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<int>(data.PublishIntervalInMilliSeconds);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceSensorsConstants.SetPublishIntervalInMilliSecondsIoTQueueName);

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

            var domain = _componentContext.Resolve<IDeviceSensorsDomain>();
            var data = await domain.GetFullByDeviceInApplicationId(applicationMQ.Id, requestContract.DeviceId, requestContract.DeviceDatasheetId);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(requestContract.DeviceId, requestContract.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceSensors, DeviceSensorsGetResponseIoTContract>(data);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceSensorsConstants.GetFullByDeviceInApplicationIdCompletedIoTQueueName);
            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Methods
    }
}