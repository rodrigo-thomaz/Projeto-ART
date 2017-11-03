using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Contract;
using System;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using RabbitMQ.Client.MessagePatterns;

namespace ART.Domotica.Producer.Services
{
    public class ESPDeviceProducer : ProducerBase, IESPDeviceProducer
    {
        #region Fields


        #endregion Fields

        #region constructors

        public ESPDeviceProducer(IConnection connection) : base(connection)
        {   
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetListInApplication(AuthenticatedMessageContract message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.GetListInApplicationQueueName, null, payload);
            });
        }        

        public async Task GetByPin(AuthenticatedMessageContract<ESPDevicePinContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.GetByPinQueueName, null, payload);
            });
        }

        public async Task InsertInApplication(AuthenticatedMessageContract<ESPDevicePinContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.InsertInApplicationQueueName, null, payload);
            });
        }

        public async Task DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.DeleteFromApplicationQueueName, null, payload);
            });
        }

        public async Task<ESPDeviceGetConfigurationsResponseContract> GetConfigurations(ESPDeviceGetConfigurationsRequestContract message)
        {
            //var rpcClient = new SimpleRpcClient(_model, ESPDeviceConstants.GetConfigurationsQueueName);
            //rpcClient.TimeoutMilliseconds = 5000;
            //var body = SerializationHelpers.SerializeToJsonBufferAsync(message);
            //rpcClient.TimedOut += (sender, e) => 
            //{

            //};
            //rpcClient.Disconnected += (sender, e) =>
            //{

            //};
            //var bufferResult = rpcClient.Call(body);
            //var result = SerializationHelpers.DeserializeJsonBufferToType<ESPDeviceGetConfigurationsResponseContract>(bufferResult);


            return await Task.Run(() =>
            {
                var correlationId = Guid.NewGuid().ToString();

                var basicProperties = _model.CreateBasicProperties();

                var replyQueueName = _model.QueueDeclare().QueueName;

                basicProperties.ReplyTo = replyQueueName;

                basicProperties.CorrelationId = correlationId;

                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);

                _model.BasicPublish(
                    exchange: "",
                    routingKey: ESPDeviceConstants.GetConfigurationsQueueName,
                    basicProperties: basicProperties,
                    body: payload);

                var getConfigurationsCompletedConsumer = new EventingBasicConsumer(_model);
                var respQueue = new BlockingCollection<ESPDeviceGetConfigurationsResponseContract>();

                getConfigurationsCompletedConsumer.Received += (model, e) =>
                {
                    var requestContract = SerializationHelpers.DeserializeJsonBufferToType<ESPDeviceGetConfigurationsResponseContract>(e.Body);
                    if (e.BasicProperties.CorrelationId == correlationId)
                    {
                        respQueue.Add(requestContract);
                    }
                };

                _model.BasicConsume(replyQueueName, true, getConfigurationsCompletedConsumer);

                return respQueue.Take();
            });            
        }        

        #endregion

        #region private voids

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
        }

        #endregion
    }
}