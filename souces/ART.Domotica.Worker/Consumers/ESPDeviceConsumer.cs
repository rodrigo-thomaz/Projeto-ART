namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Contract;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Worker.Contracts;
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Worker;
    using ART.Infra.CrossCutting.Utils;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class ESPDeviceConsumer : ConsumerBase, IESPDeviceConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _getListInApplicationConsumer;        
        private readonly EventingBasicConsumer _getByPinConsumer;
        private readonly EventingBasicConsumer _insertInApplicationConsumer;
        private readonly EventingBasicConsumer _deleteFromApplicationConsumer;
        private readonly EventingBasicConsumer _getConfigurationsConsumer;
        private readonly EventingBasicConsumer _getInApplicationForDeviceConsumer;

        private readonly IESPDeviceDomain _espDeviceDomain;

        #endregion Fields

        #region Constructors

        public ESPDeviceConsumer(IConnection connection, IESPDeviceDomain espDeviceDomain)
            : base(connection)
        {
            _getListInApplicationConsumer = new EventingBasicConsumer(_model);
            _getByPinConsumer = new EventingBasicConsumer(_model);
            _insertInApplicationConsumer = new EventingBasicConsumer(_model);
            _deleteFromApplicationConsumer = new EventingBasicConsumer(_model);
            _getConfigurationsConsumer = new EventingBasicConsumer(_model);
            _getInApplicationForDeviceConsumer = new EventingBasicConsumer(_model);

            _espDeviceDomain = espDeviceDomain;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            _model.QueueDeclare(
                 queue: ESPDeviceConstants.GetListInApplicationQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.GetByPinQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.InsertInApplicationQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.DeleteFromApplicationQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.GetConfigurationsQueueName
               , durable: false
               , exclusive: false
               , autoDelete: false
               , arguments: null);

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.GetInApplicationForDeviceCompletedQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                  queue: ESPDeviceConstants.UpdatePinQueueName
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
                  queue: ESPDeviceConstants.GetInApplicationForDeviceQueueName
                , durable: false
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueBind(
                  queue: ESPDeviceConstants.GetInApplicationForDeviceQueueName
                , exchange: "amq.topic"
                , routingKey: ESPDeviceConstants.GetInApplicationForDeviceQueueName
                , arguments: null);

            _getListInApplicationConsumer.Received += GetListInApplicationReceived;
            _getByPinConsumer.Received += GetByPinReceived;
            _insertInApplicationConsumer.Received += InsertInApplicationReceived;
            _deleteFromApplicationConsumer.Received += DeleteFromApplicationReceived;
            _getConfigurationsConsumer.Received += GetConfigurationsReceived;
            _getInApplicationForDeviceConsumer.Received += GetInApplicationForDeviceReceived;

            _model.BasicConsume(ESPDeviceConstants.GetListInApplicationQueueName, false, _getListInApplicationConsumer);
            _model.BasicConsume(ESPDeviceConstants.GetByPinQueueName, false, _getByPinConsumer);
            _model.BasicConsume(ESPDeviceConstants.InsertInApplicationQueueName, false, _insertInApplicationConsumer);
            _model.BasicConsume(ESPDeviceConstants.DeleteFromApplicationQueueName, false, _deleteFromApplicationConsumer);
            _model.BasicConsume(ESPDeviceConstants.GetConfigurationsQueueName, false, _getConfigurationsConsumer);
            _model.BasicConsume(ESPDeviceConstants.GetInApplicationForDeviceQueueName, false, _getInApplicationForDeviceConsumer);
        }        

        #endregion Methods

        #region private voids

        public void GetListInApplicationReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetListInApplicationReceivedAsync(sender, e));
        }
        public async Task GetListInApplicationReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var data = await _espDeviceDomain.GetListInApplication(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ESPDeviceConstants.GetListInApplicationCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        public void GetByPinReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetByPinReceivedAsync(sender, e));
        }

        public async Task GetByPinReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract>>(e.Body);
            var data = await _espDeviceDomain.GetByPin(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ESPDeviceConstants.GetByPinCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        public void InsertInApplicationReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(InsertInApplicationReceivedAsync(sender, e));
        }

        public async Task InsertInApplicationReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract>>(e.Body);
            var data = await _espDeviceDomain.InsertInApplication(message);            
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ESPDeviceConstants.InsertInApplicationCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, null);

            //Enviando para o Device

            var deviceMessage = new DeviceMessageContract<ESPDeviceInsertInApplicationResponseContract>(ESPDeviceConstants.InsertInApplicationQueueName, data);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var queueName = GetDeviceQueueName(data.HardwareId);
            _model.BasicPublish("", queueName, null, deviceBuffer);
        }

        public void DeleteFromApplicationReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(DeleteFromApplicationReceivedAsync(sender, e));
        }

        public async Task DeleteFromApplicationReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract>>(e.Body);
            var data = await _espDeviceDomain.DeleteFromApplication(message);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ESPDeviceConstants.DeleteFromApplicationCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, null);

            //Enviando para o Device

            var deviceMessage = new DeviceMessageContract<ESPDeviceDeleteFromApplicationResponseContract>(ESPDeviceConstants.DeleteFromApplicationQueueName, data);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var queueName = GetDeviceQueueName(data.HardwareId);
            _model.BasicPublish("", queueName, null, deviceBuffer);
        }

        public void GetConfigurationsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetConfigurationsReceivedAsync(sender, e));
        }

        public async Task GetConfigurationsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<ESPDeviceGetConfigurationsRequestContract>(e.Body);
            var data = await _espDeviceDomain.GetConfigurations(requestContract);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);

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
        }

        public void UpdatePins(List<ESPDeviceUpdatePinsContract> contracts, double nextFireTimeInSeconds)
        {
            foreach (var contract in contracts)
            {
                contract.NextFireTimeInSeconds = nextFireTimeInSeconds;
                var queueName = GetDeviceQueueName(contract.HardwareId);
                var deviceMessage = new DeviceMessageContract<ESPDeviceUpdatePinsContract>(ESPDeviceConstants.UpdatePinQueueName, contract);
                var json = JsonConvert.SerializeObject(deviceMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                var buffer = Encoding.UTF8.GetBytes(json);
                _model.BasicPublish("", queueName, null, buffer);
            }
        }                

        private void GetInApplicationForDeviceReceived(object sender, BasicDeliverEventArgs e)
        {
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<ESPDeviceGetInApplicationForDeviceRequestContract>(e.Body);
            var data = new ESPDeviceGetInApplicationForDeviceResponseContract
            {
                HardwareId = Guid.NewGuid(),
                HardwareInApplicationId = Guid.NewGuid(),
            };
            var deviceMessage = new DeviceMessageContract<ESPDeviceGetInApplicationForDeviceResponseContract>(ESPDeviceConstants.GetInApplicationForDeviceCompletedQueueName, data);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var queueName = GetDeviceQueueName(Guid.NewGuid());
            _model.BasicPublish("", queueName, null, buffer);
        }

        #endregion Other
    }
}