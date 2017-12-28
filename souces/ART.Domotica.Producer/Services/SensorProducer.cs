using ART.Domotica.Contract;
using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;

namespace ART.Domotica.Producer.Services
{
    public class SensorProducer : ProducerBase, ISensorProducer
    {
        #region constructors

        public SensorProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAllByApplicationId(AuthenticatedMessageContract message)
        {
            await BasicPublish(SensorConstants.GetAllByApplicationIdQueueName, message);
        }

        public async Task SetLabel(AuthenticatedMessageContract<SensorSetLabelRequestContract> message)
        {
            await BasicPublish(SensorConstants.SetLabelQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: SensorConstants.GetAllByApplicationIdQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                 queue: SensorConstants.SetLabelQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: CreateBasicArguments());
        }

        #endregion
    }
}