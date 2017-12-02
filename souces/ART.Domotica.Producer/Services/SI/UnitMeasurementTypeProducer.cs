using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Constant.SI;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.Producer.Services.SI
{
    public class UnitMeasurementTypeProducer : ProducerBase, IUnitMeasurementTypeProducer
    {
        #region constructors

        public UnitMeasurementTypeProducer(IConnection connection) : base(connection)
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
                _model.BasicPublish("", UnitMeasurementTypeConstants.GetAllQueueName, null, payload);
            });            
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: UnitMeasurementTypeConstants.GetAllQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);            
        }

        #endregion
    }
}