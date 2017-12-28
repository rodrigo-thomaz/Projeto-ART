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

    public class SensorInDeviceProducer : ProducerBase, ISensorInDeviceProducer
    {
        #region Constructors

        public SensorInDeviceProducer(IConnection connection)
            : base(connection)
        {
            Initialize();
        }        

        #endregion Constructors

        #region Methods        

        public async Task SetOrdination(AuthenticatedMessageContract<SensorInDeviceSetOrdinationRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorInDeviceConstants.SetOrdinationQueueName, null, payload);
            });
        }

        #endregion Methods

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
              queue: SensorInDeviceConstants.SetOrdinationQueueName
            , durable: false
            , exclusive: false
            , autoDelete: true
            , arguments: CreateBasicArguments());
        }

        #endregion
    }
}