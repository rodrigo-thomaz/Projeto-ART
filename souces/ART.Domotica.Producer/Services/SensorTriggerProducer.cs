using ART.Domotica.Contract;
using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.Utils;

namespace ART.Domotica.Producer.Services
{
    public class SensorTriggerProducer : ProducerBase, ISensorTriggerProducer
    {
        #region constructors

        public SensorTriggerProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids          

        public async Task Insert(AuthenticatedMessageContract<SensorTriggerInsertRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTriggerConstants.InsertQueueName, null, payload);
            });
        }

        public async Task Delete(AuthenticatedMessageContract<SensorTriggerDeleteRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTriggerConstants.DeleteQueueName, null, payload);
            });
        }

        public async Task SetTriggerOn(AuthenticatedMessageContract<SensorTriggerSetTriggerOnRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTriggerConstants.SetTriggerOnQueueName, null, payload);
            });
        }

        public async Task SetTriggerValue(AuthenticatedMessageContract<SensorTriggerSetTriggerValueRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTriggerConstants.SetTriggerValueQueueName, null, payload);
            });
        }

        public async Task SetBuzzerOn(AuthenticatedMessageContract<SensorTriggerSetBuzzerOnRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTriggerConstants.SetBuzzerOnQueueName, null, payload);
            });                        
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                 queue: SensorTriggerConstants.InsertQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                 queue: SensorTriggerConstants.DeleteQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetTriggerOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetTriggerValueQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetBuzzerOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);
        }

        #endregion
    }
}