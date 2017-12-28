using ART.Domotica.Contract;
using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services
{
    public class SensorTriggerProducer : ProducerBase, ISensorTriggerProducer
    {
        #region constructors

        public SensorTriggerProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }

        #endregion

        #region public voids          

        public async Task Insert(AuthenticatedMessageContract<SensorTriggerInsertRequestContract> message)
        {
            await BasicPublish(SensorTriggerConstants.InsertQueueName, message);
        }

        public async Task Delete(AuthenticatedMessageContract<SensorTriggerDeleteRequestContract> message)
        {
            await BasicPublish(SensorTriggerConstants.DeleteQueueName, message);
        }

        public async Task SetTriggerOn(AuthenticatedMessageContract<SensorTriggerSetTriggerOnRequestContract> message)
        {
            await BasicPublish(SensorTriggerConstants.SetTriggerOnQueueName, message);
        }

        public async Task SetTriggerValue(AuthenticatedMessageContract<SensorTriggerSetTriggerValueRequestContract> message)
        {
            await BasicPublish(SensorTriggerConstants.SetTriggerValueQueueName, message);
        }

        public async Task SetBuzzerOn(AuthenticatedMessageContract<SensorTriggerSetBuzzerOnRequestContract> message)
        {
            await BasicPublish(SensorTriggerConstants.SetBuzzerOnQueueName, message);
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
               , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                 queue: SensorTriggerConstants.DeleteQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetTriggerOnQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetTriggerValueQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                  queue: SensorTriggerConstants.SetBuzzerOnQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: CreateBasicArguments());
        }

        #endregion
    }
}