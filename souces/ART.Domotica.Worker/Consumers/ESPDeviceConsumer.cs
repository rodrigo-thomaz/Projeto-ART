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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ESPDeviceConsumer : ConsumerBase, IESPDeviceConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _getAllConsumer;
        private readonly EventingBasicConsumer _getAllByApplicationIdConsumer;
        private readonly EventingBasicConsumer _getByPinConsumer;
        private readonly EventingBasicConsumer _getConfigurationsRPCConsumer;
        private readonly EventingBasicConsumer _checkForUpdatesRPCConsumer;
        private readonly EventingBasicConsumer _setLabelConsumer;

        private readonly ISettingManager _settingsManager;

        #endregion Fields

        #region Constructors

        public ESPDeviceConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, ISettingManager settingsManager, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _getAllConsumer = new EventingBasicConsumer(_model);
            _getAllByApplicationIdConsumer = new EventingBasicConsumer(_model);
            _getByPinConsumer = new EventingBasicConsumer(_model);
            _getConfigurationsRPCConsumer = new EventingBasicConsumer(_model);
            _checkForUpdatesRPCConsumer = new EventingBasicConsumer(_model);
            _setLabelConsumer = new EventingBasicConsumer(_model);

            _settingsManager = settingsManager;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(ESPDeviceConstants.GetAllQueueName);
            BasicQueueDeclare(ESPDeviceConstants.GetAllByApplicationIdQueueName);
            BasicQueueDeclare(ESPDeviceConstants.GetByPinQueueName);
            BasicQueueDeclare(ESPDeviceConstants.SetLabelQueueName);
            BasicQueueDeclare(ESPDeviceConstants.GetConfigurationsRPCQueueName);
            BasicQueueDeclare(ESPDeviceConstants.CheckForUpdatesRPCQueueName);

            _getAllConsumer.Received += GetAllReceived;
            _getAllByApplicationIdConsumer.Received += GetAllByApplicationIdReceived;
            _getByPinConsumer.Received += GetByPinReceived;
            _getConfigurationsRPCConsumer.Received += GetConfigurationsRPCReceived;
            _checkForUpdatesRPCConsumer.Received += CheckForUpdatesRPCReceived;
            _setLabelConsumer.Received += SetLabelReceived;

            _model.BasicConsume(ESPDeviceConstants.GetAllQueueName, false, _getAllConsumer);
            _model.BasicConsume(ESPDeviceConstants.GetAllByApplicationIdQueueName, false, _getAllByApplicationIdConsumer);
            _model.BasicConsume(ESPDeviceConstants.GetByPinQueueName, false, _getByPinConsumer);
            _model.BasicConsume(ESPDeviceConstants.GetConfigurationsRPCQueueName, false, _getConfigurationsRPCConsumer);
            _model.BasicConsume(ESPDeviceConstants.CheckForUpdatesRPCQueueName, false, _checkForUpdatesRPCConsumer);
            _model.BasicConsume(ESPDeviceConstants.SetLabelQueueName, false, _setLabelConsumer);
        }

        #endregion Methods

        #region private voids

        public void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        public async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IESPDeviceDomain>();
            var data = await domain.GetAll();

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<ESPDevice>, List<ESPDeviceAdminGetModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, ESPDeviceConstants.GetAllViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        public void GetAllByApplicationIdReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllByApplicationIdReceivedAsync(sender, e));
        }
        public async Task GetAllByApplicationIdReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);

            var applicationUserDomain = _componentContext.Resolve<IApplicationUserDomain>();
            var applicationUser = await applicationUserDomain.GetByKey(message.ApplicationUserId);

            var espDevicedomain = _componentContext.Resolve<IESPDeviceDomain>();
            var data = await espDevicedomain.GetAllByApplicationId(applicationUser.ApplicationId);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<ESPDevice>, List<ESPDeviceGetModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, ESPDeviceConstants.GetAllByApplicationIdViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        public void GetByPinReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetByPinReceivedAsync(sender, e));
        }

        public async Task GetByPinReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IESPDeviceDomain>();
            var data = await domain.GetByPin(message.Contract.Pin);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, ESPDeviceConstants.GetByPinViewCompletedQueueName);
            var viewModel = Mapper.Map<ESPDevice, ESPDeviceGetByPinModel>(data);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, buffer);

            _logger.DebugLeave();
        }        

        public void GetConfigurationsRPCReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetConfigurationsRPCReceivedAsync(sender, e));
        }

        public async Task GetConfigurationsRPCReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<ESPDeviceGetConfigurationsRPCRequestContract>(e.Body);
            var domain = _componentContext.Resolve<IESPDeviceDomain>();
            var data = await domain.GetConfigurations(requestContract.ChipId, requestContract.FlashChipId, requestContract.StationMacAddress, requestContract.SoftAPMacAddress);

            var applicationTopic = string.Empty;
            if (data.DevicesInApplication != null && data.DevicesInApplication.Any())
            {
                var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
                var applicationMQ = await applicationMQDomain.GetByDeviceId(data.Id);
                applicationTopic = applicationMQ.Topic;
            }

            var ntpHost = await _settingsManager.GetValueAsync<string>(SettingsConstants.NTPHostSettingsKey);
            var ntpPort = await _settingsManager.GetValueAsync<int>(SettingsConstants.NTPPortSettingsKey);

            var responseContract = new ESPDeviceGetConfigurationsRPCResponseContract
            {
                DeviceInApplication = new DeviceInApplicationDetailResponseContract
                {
                    ApplicationTopic = applicationTopic,
                },
                DeviceMQ = new DeviceMQDetailResponseContract
                {
                    Host = _mqSettings.BrokerHost,
                    Port = _mqSettings.BrokerPort,                    
                },
                DeviceNTP = new DeviceNTPDetailResponseContract
                {
                    Host = ntpHost,
                    Port = ntpPort,
                },
            };

            Mapper.Map(data, responseContract);

            //Enviando para o Producer

            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(responseContract);

            _model.BasicQos(0, 1, false);

            var props = e.BasicProperties;

            var replyProps = _model.CreateBasicProperties();

            replyProps.CorrelationId = props.CorrelationId;

            _model.BasicPublish(
                exchange: "",
                routingKey: props.ReplyTo,
                basicProperties: replyProps,
                body: buffer);

            _model.BasicAck(e.DeliveryTag, false);

            _logger.DebugLeave();
        }

        public void CheckForUpdatesRPCReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(CheckForUpdatesRPCReceivedAsync(sender, e));
        }

        public async Task CheckForUpdatesRPCReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<ESPDeviceCheckForUpdatesRPCRequestContract>(e.Body);
            var domain = _componentContext.Resolve<IDeviceBinaryDomain>();
            var binaryBuffer = await domain.CheckForUpdates(requestContract.StationMacAddress, requestContract.SoftAPMacAddress);

            var responseContract = new ESPDeviceCheckForUpdatesRPCResponseContract
            {
                Buffer = binaryBuffer,
            };
            
            //Enviando para o Producer

            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(responseContract);

            _model.BasicQos(0, 1, false);

            var props = e.BasicProperties;

            var replyProps = _model.CreateBasicProperties();

            replyProps.CorrelationId = props.CorrelationId;

            _model.BasicPublish(
                exchange: "",
                routingKey: props.ReplyTo,
                basicProperties: replyProps,
                body: buffer);

            _model.BasicAck(e.DeliveryTag, false);

            _logger.DebugLeave();
        }

        public void UpdatePins(DateTimeOffset nextFireTimeUtc)
        {
            Task.WaitAll(UpdatePinsAsync(nextFireTimeUtc));
        }

        private async Task UpdatePinsAsync(DateTimeOffset nextFireTimeUtc)
        {
            _logger.DebugEnter();

            var domain = _componentContext.Resolve<IESPDeviceDomain>();
            var data = await domain.UpdatePins();

            foreach (var item in data)
            {
                //Enviando para o IoT
                var contract = Mapper.Map<ESPDevice, ESPDeviceUpdatePinsResponseIoTContract>(item);
                var nextFireTimeInSeconds = nextFireTimeUtc.Subtract(DateTimeOffset.Now).TotalSeconds;
                contract.NextFireTimeInSeconds = nextFireTimeInSeconds;
                var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(contract);
                var routingKey = GetDeviceRoutingKeyForIoT(item.DeviceMQ.Topic, ESPDeviceConstants.UpdatePinIoTQueueName);
                _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);
            }

            _logger.DebugLeave();
        }

        public void SetLabelReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetLabelReceivedAsync(sender, e));
        }

        public async Task SetLabelReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceSetLabelRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDeviceBaseDomain>();
            var data = await domain.SetLabel(message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId, message.Contract.Label);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<DeviceBase, DeviceSetLabelModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, ESPDeviceConstants.SetLabelViewCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para o Iot
            var iotContract = new SetValueRequestIoTContract<string>(data.Label);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, ESPDeviceConstants.SetLabelIoTQueueName);

            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion Other
    }
}