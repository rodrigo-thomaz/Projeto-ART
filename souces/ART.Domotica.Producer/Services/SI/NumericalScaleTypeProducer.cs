using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Constant.SI;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.Producer.Services.SI
{
    public class NumericalScaleTypeProducer : ProducerBase, INumericalScaleTypeProducer
    {
        #region constructors

        public NumericalScaleTypeProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", NumericalScaleTypeConstants.GetAllQueueName, null, payload);
            });            
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: NumericalScaleTypeConstants.GetAllQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);            
        }

        #endregion
    }
}