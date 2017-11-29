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

        public async Task SetAlarmOn(AuthenticatedMessageContract<SensorTriggerSetAlarmOnRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTriggerConstants.SetAlarmOnQueueName, null, payload);
            });
        }

        public async Task SetAlarmCelsius(AuthenticatedMessageContract<SensorTriggerSetAlarmCelsiusRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTriggerConstants.SetAlarmCelsiusQueueName, null, payload);
            });
        }

        public async Task SetAlarmBuzzerOn(AuthenticatedMessageContract<SensorTriggerSetAlarmBuzzerOnRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTriggerConstants.SetAlarmBuzzerOnQueueName, null, payload);
            });                        
        }

        #endregion

        #region private voids

        private void Initialize()
        {            
            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetAlarmOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetAlarmCelsiusQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetAlarmBuzzerOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);
        }

        #endregion
    }
}