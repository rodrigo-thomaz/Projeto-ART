namespace ART.Domotica.Producer.Services
{
    using System.Threading.Tasks;
    using ART.Domotica.Contract;
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Producer;

    using RabbitMQ.Client;
    using ART.Domotica.Constant;
    using ART.Infra.CrossCutting.MQ;

    public class DeviceSensorsProducer : ProducerBase, IDeviceSensorsProducer
    {
        #region Constructors

        public DeviceSensorsProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }

        #endregion Constructors

        #region public voids

        public async Task SetPublishIntervalInSeconds(AuthenticatedMessageContract<DeviceSensorsSetPublishIntervalInSecondsRequestContract> message)
        {
            await BasicPublish(DeviceSensorsConstants.SetPublishIntervalInSecondsQueueName, message);
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
                , arguments: CreateBasicArguments());
        }

        #endregion Methods
    }
}