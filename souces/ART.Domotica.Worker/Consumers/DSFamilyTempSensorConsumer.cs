using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
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

namespace ART.Domotica.Worker.Consumers
{
    public class DSFamilyTempSensorConsumer : ConsumerBase, IDSFamilyTempSensorConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllConsumer;
        private readonly EventingBasicConsumer _getListInApplicationConsumer;
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
            _getAllConsumer = new EventingBasicConsumer(_model);
            _getListInApplicationConsumer = new EventingBasicConsumer(_model);
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
                 queue: DSFamilyTempSensorConstants.GetAllQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                 queue: DSFamilyTempSensorConstants.GetListInApplicationQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);            

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
                queue: DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdQueueName
              , durable: false
              , exclusive: false
              , autoDelete: true
              , arguments: null);

            _model.QueueBind(
                  queue: DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdQueueName
                , exchange: "amq.topic"
                , routingKey: DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdQueueName
                , arguments: null);

            _getAllConsumer.Received += GetAllReceived;
            _getListInApplicationConsumer.Received += GetListInApplicationReceived;
            _getAllByDeviceInApplicationIdConsumer.Received += GetAllByDeviceInApplicationIdReceived;
            _getAllResolutionsConsumer.Received += GetAllResolutionsReceived;
            _setResolutionConsumer.Received += SetResolutionReceived;
            _setHighAlarmConsumer.Received += SetHighAlarmReceived;
            _setLowAlarmConsumer.Received += SetLowAlarmReceived;

            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllQueueName, false, _getAllConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.GetListInApplicationQueueName, false, _getListInApplicationConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdQueueName, false, _getAllByDeviceInApplicationIdConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllResolutionsQueueName, false, _getAllResolutionsConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetHighAlarmQueueName, false, _setHighAlarmConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetLowAlarmQueueName, false, _setLowAlarmConsumer);
        }        

        public void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        public async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.GetAll(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, DSFamilyTempSensorConstants.GetAllCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, buffer);

            _logger.DebugLeave();
        }

        public void GetListInApplicationReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetListInApplicationReceivedAsync(sender, e));
        }

        public async Task GetListInApplicationReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var data = await domain.GetListInApplication(message.ApplicationUserId);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, DSFamilyTempSensorConstants.GetListInApplicationCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, buffer);

            _logger.DebugLeave();
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
            var deviceMessage = new MessageIoTContract<List<DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseContract>>(DSFamilyTempSensorConstants.GetAllByDeviceInApplicationIdCompletedQueueName, data);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);            
            var queueName = GetDeviceQueueName(requestContract.DeviceId);
            _model.BasicPublish("", queueName, null, buffer);

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
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, DSFamilyTempSensorConstants.GetAllResolutionsCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, buffer);

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
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            await domain.SetResolution(message);
            await SendSetResolutionToDevice(message);

            _logger.DebugLeave();
        }

        public async Task SendSetResolutionToDevice(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract> message)
        {
            _logger.DebugEnter();

            var queueName = await GetQueueName(message.Contract.DSFamilyTempSensorId);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetResolutionContract>(DSFamilyTempSensorConstants.SetResolutionQueueName, message.Contract);
            var json = JsonConvert.SerializeObject(deviceMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("", queueName, null, buffer);

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
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            await domain.SetHighAlarm(message);
            var queueName = await GetQueueName(message.Contract.DSFamilyTempSensorId);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetHighAlarmContract>(DSFamilyTempSensorConstants.SetHighAlarmQueueName, message.Contract);
            var json = JsonConvert.SerializeObject(deviceMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("", queueName, null, buffer);

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
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmContract>>(e.Body);
            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            await domain.SetLowAlarm(message);
            var queueName = await GetQueueName(message.Contract.DSFamilyTempSensorId);
            var deviceMessage = new MessageIoTContract<DSFamilyTempSensorSetLowAlarmContract>(DSFamilyTempSensorConstants.SetLowAlarmQueueName, message.Contract);
            var json = JsonConvert.SerializeObject(deviceMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("", queueName, null, buffer);

            _logger.DebugLeave();
        }

        private async Task<string> GetQueueName(Guid dsFamilyTempSensorId)
        {
            _logger.DebugEnter();

            var domain = _componentContext.Resolve<IDSFamilyTempSensorDomain>();
            var entity = await domain.GetDeviceFromSensor(dsFamilyTempSensorId);
            var queueName = string.Format("mqtt-subscription-{0}qos0", entity.DeviceBaseId);
            return queueName;

            _logger.DebugLeave();
        }

        #endregion
    }
}
