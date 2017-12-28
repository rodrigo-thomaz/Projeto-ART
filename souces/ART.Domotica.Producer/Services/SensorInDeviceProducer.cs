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

    public class SensorInDeviceProducer : ProducerBase, ISensorInDeviceProducer
    {
        #region Constructors

        public SensorInDeviceProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }        

        #endregion Constructors

        #region Methods        

        public async Task SetOrdination(AuthenticatedMessageContract<SensorInDeviceSetOrdinationRequestContract> message)
        {
            await BasicPublish(SensorInDeviceConstants.SetOrdinationQueueName, message);
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