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
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DeviceDebugConsumer : ConsumerBase, IDeviceDebugConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _getByKeyConsumer;
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
            _getByKeyConsumer = new EventingBasicConsumer(_model);
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
            BasicQueueDeclare(DeviceDebugConstants.GetByKeyIoTQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetRemoteEnabledQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetSerialEnabledQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetResetCmdEnabledQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetShowDebugLevelQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetShowTimeQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetShowProfilerQueueName);
            BasicQueueDeclare(DeviceDebugConstants.SetShowColorsQueueName);

            _model.QueueBind(
                 queue: DeviceDebugConstants.GetByKeyIoTQueueName
               , exchange: "amq.topic"
               , routingKey: GetApplicationRoutingKeyForAllIoT(DeviceDebugConstants.GetByKeyIoTQueueName)
               , arguments: CreateBasicArguments());

            _getByKeyConsumer.Received += GetByKeyReceived;
            _setRemoteEnabledConsumer.Received += SetRemoteEnabledReceived;
            _setSerialEnabledConsumer.Received += SetSerialEnabledReceived;
            _setResetCmdEnabledConsumer.Received += SetResetCmdEnabledReceived;
            _setShowDebugLevelConsumer.Received += SetShowDebugLevelReceived;
            _setShowTimeConsumer.Received += SetShowTimeReceived;
            _setShowProfilerConsumer.Received += SetShowProfilerReceived;
            _setShowColorsConsumer.Received += SetShowColorsReceived;

            _model.BasicConsume(DeviceDebugConstants.GetByKeyIoTQueueName, false, _getByKeyConsumer);
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

        public void GetByKeyReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetByKeyReceivedAsync(sender, e));
        }

        public async Task GetByKeyReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<IoTRequestContract>(e.Body);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByDeviceId(requestContract.DeviceId);

            var domain = _componentContext.Resolve<IDeviceDebugDomain>();
            var data = await domain.GetByKey(applicationMQ.Id, requestContract.DeviceTypeId, requestContract.DeviceDatasheetId, requestContract.DeviceId);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(requestContract.DeviceTypeId, requestContract.DeviceDatasheetId, requestContract.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<DeviceDebug, DeviceDebugGetResponseIoTContract>(data);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.GetByKeyCompletedIoTQueueName);
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
            var data = await domain.SetRemoteEnabled(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetRemoteEnabledViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.RemoteEnabled);
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
            var data = await domain.SetSerialEnabled(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetSerialEnabledViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.SerialEnabled);
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
            var data = await domain.SetResetCmdEnabled(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetResetCmdEnabledViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.ResetCmdEnabled);
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
            var data = await domain.SetShowDebugLevel(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetShowDebugLevelViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.ShowDebugLevel);
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
            var data = await domain.SetShowTime(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetShowTimeViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.ShowTime);
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
            var data = await domain.SetShowProfiler(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetShowProfilerViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.ShowProfiler);
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
            var data = await domain.SetShowColors(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Value);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>(message.Contract);
            viewModel.DeviceId = data.Id;
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceDebugConstants.SetShowColorsViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<bool>(data.ShowColors);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceDebugConstants.SetShowColorsIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Other
    }
}