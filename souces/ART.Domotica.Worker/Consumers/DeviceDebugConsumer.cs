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

    public class DeviceDebugConsumer : ConsumerBase, IDeviceDebugConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _setTelnetTCPPortConsumer;
        private readonly EventingBasicConsumer _setRemoteEnabledConsumer;
        private readonly EventingBasicConsumer _setSerialEnabledConsumer;
        private readonly EventingBasicConsumer _setResetCmdEnabledConsumer;
        private readonly EventingBasicConsumer _setShowDebugLevelConsumer;
        private readonly EventingBasicConsumer _setShowTimeConsumer;
        private readonly EventingBasicConsumer _setShowProfilerConsumer;
        private readonly EventingBasicConsumer _setShowColorsConsumer;

        #endregion Fields

        #region Constructors

        public DeviceDebugConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, ISettingManager settingsManager, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _setTelnetTCPPortConsumer = new EventingBasicConsumer(_model);
            _setRemoteEnabledConsumer = new EventingBasicConsumer(_model);
            _setSerialEnabledConsumer = new EventingBasicConsumer(_model);
            _setResetCmdEnabledConsumer = new EventingBasicConsumer(_model);
            _setShowDebugLevelConsumer = new EventingBasicConsumer(_model);
            _setShowTimeConsumer = new EventingBasicConsumer(_model);
            _setShowProfilerConsumer = new EventingBasicConsumer(_model);
            _setShowColorsConsumer = new EventingBasicConsumer(_model);

            Initialize();            
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceDebugConstants.SetTelnetTCPPortQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetRemoteEnabledQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetSerialEnabledQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetResetCmdEnabledQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetShowDebugLevelQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetShowTimeQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetShowProfilerQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetShowColorsQueueName);

            _setTelnetTCPPortConsumer.Received += SetTelnetTCPPortReceived;
            _setRemoteEnabledConsumer.Received += SetRemoteEnabledReceived;
            _setSerialEnabledConsumer.Received += SetSerialEnabledReceived;
            _setResetCmdEnabledConsumer.Received += SetResetCmdEnabledReceived;
            _setShowDebugLevelConsumer.Received += SetShowDebugLevelReceived;
            _setShowTimeConsumer.Received += SetShowTimeReceived;
            _setShowProfilerConsumer.Received += SetShowProfilerReceived;
            _setShowColorsConsumer.Received += SetShowColorsReceived;

            _model.BasicConsume(DeviceDebugConstants.SetTelnetTCPPortQueueName, false, _setTelnetTCPPortConsumer);
            _model.BasicConsume(DeviceDebugConstants.SetRemoteEnabledQueueName, false, _setRemoteEnabledConsumer);
            _model.BasicConsume(DeviceDebugConstants.SetSerialEnabledQueueName, false, _setSerialEnabledConsumer);
            _model.BasicConsume(DeviceDebugConstants.SetResetCmdEnabledQueueName, false, _setResetCmdEnabledConsumer);
            _model.BasicConsume(DeviceDebugConstants.SetShowDebugLevelQueueName, false, _setShowDebugLevelConsumer);
            _model.BasicConsume(DeviceDebugConstants.SetShowTimeQueueName, false, _setShowTimeConsumer);
            _model.BasicConsume(DeviceDebugConstants.SetShowProfilerQueueName, false, _setShowProfilerConsumer);
            _model.BasicConsume(DeviceDebugConstants.SetShowColorsQueueName, false, _setShowColorsConsumer);
        }

        #endregion Methods

        #region private voids        

        public void SetTelnetTCPPortReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetTelnetTCPPortReceivedAsync(sender, e));
        }

        public async Task SetTelnetTCPPortReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetTelnetTCPPortRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetTelnetTCPPort(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetTelnetTCPPortRequestContract, DeviceDebugSetTelnetTCPPortModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetTelnetTCPPortViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebugSetTelnetTCPPortRequestContract, DeviceDebugSetTelnetTCPPortRequestIoTContract>(message.Contract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetTelnetTCPPortIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetRemoteEnabledReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetRemoteEnabledReceivedAsync(sender, e));
        }

        public async Task SetRemoteEnabledReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetValueRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetRemoteEnabled(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetRemoteEnabledViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueRequestIoTContract>(message.Contract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetRemoteEnabledIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetSerialEnabledReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetSerialEnabledReceivedAsync(sender, e));
        }

        public async Task SetSerialEnabledReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetValueRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetSerialEnabled(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetSerialEnabledViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueRequestIoTContract>(message.Contract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetSerialEnabledIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetResetCmdEnabledReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetResetCmdEnabledReceivedAsync(sender, e));
        }

        public async Task SetResetCmdEnabledReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetValueRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetResetCmdEnabled(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetResetCmdEnabledViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueRequestIoTContract>(message.Contract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetResetCmdEnabledIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetShowDebugLevelReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetShowDebugLevelReceivedAsync(sender, e));
        }

        public async Task SetShowDebugLevelReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetValueRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetShowDebugLevel(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetShowDebugLevelViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueRequestIoTContract>(message.Contract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetShowDebugLevelIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetShowTimeReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetShowTimeReceivedAsync(sender, e));
        }

        public async Task SetShowTimeReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetValueRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetShowTime(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetShowTimeViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueRequestIoTContract>(message.Contract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetShowTimeIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetShowProfilerReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetShowProfilerReceivedAsync(sender, e));
        }

        public async Task SetShowProfilerReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetValueRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetShowProfiler(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetShowProfilerViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueRequestIoTContract>(message.Contract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetShowProfilerIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetShowColorsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetShowColorsReceivedAsync(sender, e));
        }

        public async Task SetShowColorsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceDebugSetValueRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.SetShowColors(message.Contract.DeviceDebugId, message.Contract.DeviceDatasheetId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceDebugId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetShowColorsViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.Id, data.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueRequestIoTContract>(message.Contract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetShowColorsIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Other
    }
}