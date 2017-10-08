using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Threading.Tasks;
using ART.Domotica.Constant;

namespace ART.Domotica.Producer.Services
{
    public class HardwareProducer : ProducerBase, IHardwareProducer
    {
        #region constructors

        public HardwareProducer(IConnection connection) : base(connection)
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
                _model.BasicPublish("", HardwareConstants.GetListQueueName, null, payload);
            });
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: HardwareConstants.GetListQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _basicProperties.Persistent = true;
        }

        #endregion
    }
}