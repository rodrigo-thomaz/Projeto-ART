using ART.Infra.CrossCutting.MQ;
using ART.Domotica.Common.QueueNames;
using ART.Domotica.WebApi.IProducers;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace ART.Domotica.WebApi.Producers
{
    public class TemperatureScaleProducer : ITemperatureScaleProducer
    {
        #region private readonly fields

        private readonly IConnection _connection;
        private readonly IModel _model;
        private readonly IBasicProperties _basicProperties;

        #endregion

        #region constructors

        public TemperatureScaleProducer(IConnection connection)
        {
            _connection = connection;
            _model = _connection.CreateModel();
            _basicProperties = _model.CreateBasicProperties();

            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetScales(string session)
        {
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(session);
            await Task.Run(() => _model.BasicPublish("", TemperatureScaleQueueNames.GetScalesQueueName, null, payload));
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: TemperatureScaleQueueNames.GetScalesQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _basicProperties.Persistent = true;
        }

        #endregion
    }
}