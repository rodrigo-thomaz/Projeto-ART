using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Contract;
using System;
using RabbitMQ.Client.MessagePatterns;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services
{
    public class ESPDeviceProducer : ProducerBase, IESPDeviceProducer
    {
        #region Fields

        private readonly IMQSettings _mqSettings;

        #endregion Fields

        #region constructors

        public ESPDeviceProducer(IConnection connection, IMQSettings mqSettings) : base(connection)
        {
            _mqSettings = mqSettings;
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.GetAllQueueName, null, payload);
            });
        }

        public async Task GetListInApplication(AuthenticatedMessageContract message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.GetListInApplicationQueueName, null, payload);
            });
        }        

        public async Task GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.GetByPinQueueName, null, payload);
            });
        }

        public async Task InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.InsertInApplicationQueueName, null, payload);
            });
        }

        public async Task DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.DeleteFromApplicationQueueName, null, payload);
            });
        }

        public async Task<ESPDeviceGetConfigurationsRPCResponseContract> GetConfigurationsRPC(ESPDeviceGetConfigurationsRPCRequestContract message)
        {
            return await Task.Run(() =>
            {
                var rpcClient = new SimpleRpcClient(_model, ESPDeviceConstants.GetConfigurationsRPCQueueName);
                rpcClient.TimeoutMilliseconds = _mqSettings.RpcClientTimeOutMilliSeconds;
                var body = SerializationHelpers.SerializeToJsonBufferAsync(message);
                rpcClient.TimedOut += (sender, e) =>
                {
                    throw new TimeoutException("Worker time out");
                };
                rpcClient.Disconnected += (sender, e) =>
                {
                    rpcClient.Close();
                    throw new Exception("Worker disconected");
                };

                var bufferResult = rpcClient.Call(body);
                rpcClient.Close();
                var result = SerializationHelpers.DeserializeJsonBufferToType<ESPDeviceGetConfigurationsRPCResponseContract>(bufferResult);
                return result;
            });            
        }

        public async Task SetTimeZone(AuthenticatedMessageContract<ESPDeviceSetTimeZoneRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.SetTimeZoneQueueName, null, payload);
            });
        }

        public async Task SetUpdateIntervalInMilliSecond(AuthenticatedMessageContract<ESPDeviceSetUpdateIntervalInMilliSecondRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.SetUpdateIntervalInMilliSecondQueueName, null, payload);
            });
        }

        public async Task SetLabel(AuthenticatedMessageContract<ESPDeviceSetLabelRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ESPDeviceConstants.SetLabelQueueName, null, payload);
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
                queue: ESPDeviceConstants.SetTimeZoneQueueName
              , durable: false
              , exclusive: false
              , autoDelete: true
              , arguments: null);

            _model.QueueDeclare(
                queue: ESPDeviceConstants.SetUpdateIntervalInMilliSecondQueueName
              , durable: false
              , exclusive: false
              , autoDelete: true
              , arguments: null);

            _model.QueueDeclare(
               queue: ESPDeviceConstants.SetLabelQueueName
             , durable: false
             , exclusive: false
             , autoDelete: true
             , arguments: null);
        }        

        #endregion
    }
}