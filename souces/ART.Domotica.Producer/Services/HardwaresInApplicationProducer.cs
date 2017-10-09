using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Contract;

namespace ART.Domotica.Producer.Services
{
    public class HardwaresInApplicationProducer : ProducerBase, IHardwaresInApplicationProducer
    {
        #region constructors

        public HardwaresInApplicationProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetList(AuthenticatedMessageContract message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", HardwaresInApplicationConstants.GetListQueueName, null, payload);
            });
        }

        public async Task SearchPin(AuthenticatedMessageContract<HardwaresInApplicationSearchPinContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", HardwaresInApplicationConstants.SearchPinQueueName, null, payload);
            });
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: HardwaresInApplicationConstants.GetListQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                 queue: HardwaresInApplicationConstants.SearchPinQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _basicProperties.Persistent = true;
        }

        #endregion
    }
}