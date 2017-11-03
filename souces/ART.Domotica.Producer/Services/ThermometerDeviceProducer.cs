namespace ART.Domotica.Producer.Services
{
    using ART.Domotica.Constant;
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Producer;
    using ART.Infra.CrossCutting.Utils;
    using RabbitMQ.Client;
    using System.Threading.Tasks;

    public class ThermometerDeviceProducer : ProducerBase, IThermometerDeviceProducer
    {
        #region Constructors

        public ThermometerDeviceProducer(IConnection connection)
            : base(connection)
        {
            Initialize();
        }

        #endregion Constructors

        #region public voids

        public async Task GetList(AuthenticatedMessageContract message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ThermometerDeviceConstants.GetListAdminQueueName, null, payload);
            });
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: ThermometerDeviceConstants.GetListAdminQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);
        }

        #endregion Methods
    }
}