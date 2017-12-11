namespace ART.Domotica.Producer.Services
{
    using System.Threading.Tasks;
    using ART.Domotica.Contract;
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Producer;

    using RabbitMQ.Client;
    using ART.Infra.CrossCutting.Utils;
    using ART.Domotica.Constant;

    public class DeviceSensorsProducer : ProducerBase, IDeviceSensorsProducer
    {
        #region Constructors

        public DeviceSensorsProducer(IConnection connection)
            : base(connection)
        {
            Initialize();
        }

        #endregion Constructors

        #region public voids

        public async Task SetPublishIntervalInSeconds(AuthenticatedMessageContract<DeviceSensorsSetPublishIntervalInSecondsRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DeviceSensorsConstants.SetPublishIntervalInSecondsQueueName, null, payload);
            });
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: DeviceSensorsConstants.SetPublishIntervalInSecondsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);
        }

        #endregion Methods
    }
}