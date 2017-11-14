using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Contract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Worker.IConsumers;
using System.Collections.Generic;
using ART.Domotica.IoTContract;
using Autofac;
using ART.Infra.CrossCutting.Logging;
using AutoMapper;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Model;

namespace ART.Domotica.Worker.Consumers
{
    public class DSFamilyTempSensorConsumer : ConsumerBase, IDSFamilyTempSensorConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllByDeviceInApplicationIdConsumer;
        private readonly EventingBasicConsumer _getAllResolutionsConsumer;
        private readonly EventingBasicConsumer _setResolutionConsumer;
        private readonly EventingBasicConsumer _setHighAlarmConsumer;
        private readonly EventingBasicConsumer _setLowAlarmConsumer;

        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public DSFamilyTempSensorConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _getAllByDeviceInApplicationIdConsumer = new EventingBasicConsumer(_model);
            _getAllResolutionsConsumer = new EventingBasicConsumer(_model);
            _setResolutionConsumer = new EventingBasicConsumer(_model);
            _setHighAlarmConsumer = new EventingBasicConsumer(_model);
            _setLowAlarmConsumer = new EventingBasicConsumer(_model);

            _componentContext = componentContext;

            _logger = logger;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.GetAllResolutionsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetResolutionQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetHighAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetLowAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.ExchangeDeclare(
                 exchange: "amq.topic"
               , type: ExchangeType.Topic
               , durable: true
               , autoDelete: false
               , arguments: null);

            _model.QueueDeclare(
                queue: DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName
              , durable: false
              , exclusive: false
              , autoDelete: true
              , arguments: null);

            _model.QueueBind(
                  queue: DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName
                , exchange: "amq.topic"
                , routingKey: DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName
                , arguments: null);

            _getAllByDeviceInApplicationIdConsumer.Received += GetAllByDeviceInApplicationIdReceived;
            _getAllResolutionsConsumer.Received += GetAllResolutionsReceived;
            _setResolutionConsumer.Received += SetResolutionReceived;
            _setHighAlarmConsumer.Received += SetHighAlarmReceived;
            _setLowAlarmConsumer.Received += SetLowAlarmReceived;

            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdIoTQueueName, false, _getAllByDeviceInApplicationIdConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllResolutionsQueueName, false, _getAllResolutionsConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetHighAlarmQueueName, false, _setHighAlarmConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetLowAlarmQueueName, false, _setLowAlarmConsumer);
        }         

        private void GetAllByDeviceInApplicationIdReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllByDeviceInApplicationIdReceivedAsync(sender, e));
        }

        private async Task GetAllByDeviceInApplicationIdReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<IoTRequestContract>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.GetAllByDeviceInApplicationId(requestContract.DeviceInApplicationId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseIoTContract>>(data);
            var deviceMessage = new MessageIoTContract<List<DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseIoTContract>>(DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdCompletedIoTQueueName, iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);            
            var queueName = GetDeviceQueueName(requestContract.DeviceId);
            _model.BasicPublish("", queueName, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void GetAllResolutionsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllResolutionsReceivedAsync(sender, e));            
        }

        public async Task GetAllResolutionsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.GetAllResolutions();

            //Enviando para View
            var viewModel = Mapper.Map<List<DSFamilyTempSensorResolution>, List<DSFamilyTempSensorResolutionDetailModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, DSFamilyTempSensorConstants.GetAllResolutionsViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        public void SetResolutionReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetResolutionReceivedAsync(sender, e));         
        }

        public async Task SetResolutionReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            await domain.SetResolution(message);

            //Enviando para View
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, DSFamilyTempSensorConstants.SetResolutionViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, null);

            //Enviando para o Iot
            var queueName = await GetQueueName(message.Contract.DSFamilyTempSensorId);
            var iotContract = Mapper.Map<DSFamilyTempSensorSetResolutionRequestContract, DSFamilyTempSensorSetResolutionRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetResolutionRequestIoTContract>(DSFamilyTempSensorConstants.SetResolutionIoTQueueName, iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            _model.BasicPublish("", queueName, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetHighAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetHighAlarmReceivedAsync(sender, e));
        }

        public async Task SetHighAlarmReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            await domain.SetHighAlarm(message);

            //Enviando para View
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, DSFamilyTempSensorConstants.SetHighAlarmViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, null);

            //Enviando para o Iot
            var queueName = await GetQueueName(message.Contract.DSFamilyTempSensorId);
            var iotContract = Mapper.Map<DSFamilyTempSensorSetHighAlarmRequestContract, DSFamilyTempSensorSetHighAlarmRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetHighAlarmRequestIoTContract>(DSFamilyTempSensorConstants.SetHighAlarmIoTQueueName, iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            _model.BasicPublish("", queueName, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetLowAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetLowAlarmReceivedAsync(sender, e));
        }

        public async Task SetLowAlarmReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            await domain.SetLowAlarm(message);

            //Enviando para View
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, DSFamilyTempSensorConstants.SetLowAlarmViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, null);

            //Enviando para o Iot
            var queueName = await GetQueueName(message.Contract.DSFamilyTempSensorId);
            var iotContract = Mapper.Map<DSFamilyTempSensorSetLowAlarmRequestContract, DSFamilyTempSensorSetLowAlarmRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetLowAlarmRequestIoTContract>(DSFamilyTempSensorConstants.SetLowAlarmIoTQueueName, iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            _model.BasicPublish("", queueName, null, deviceBuffer);

            _logger.DebugLeave();
        }

        private async Task<string> GetQueueName(Guid dsFamilyTempSensorId)
        {
            _logger.DebugEnter();

            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var entity = await domain.GetDeviceFromSensor(dsFamilyTempSensorId);
            var queueName = string.Format("mqtt-subscription-{0}qos0", entity.DeviceBaseId);

            _logger.DebugLeave();

            return queueName;            
        }

        #endregion
    }
}
