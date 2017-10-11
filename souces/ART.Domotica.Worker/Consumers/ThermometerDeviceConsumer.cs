namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Worker;
    using ART.Infra.CrossCutting.Utils;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class ThermometerDeviceConsumer : ConsumerBase, IThermometerDeviceConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _getListConsumer;
        private readonly IThermometerDeviceDomain _thermometerDeviceDomain;

        #endregion Fields

        #region Constructors

        public ThermometerDeviceConsumer(IConnection connection, IThermometerDeviceDomain thermometerDeviceDomain)
            : base(connection)
        {
            _getListConsumer = new EventingBasicConsumer(_model);

            _thermometerDeviceDomain = thermometerDeviceDomain;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            _model.QueueDeclare(
                 queue: ThermometerDeviceConstants.GetListAdminQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _getListConsumer.Received += GetListReceived;

            _model.BasicConsume(ThermometerDeviceConstants.GetListAdminQueueName, false, _getListConsumer);
        }

        #endregion Methods

        #region private voids

        public void GetListReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetListReceivedAsync(sender, e));
        }
        public async Task GetListReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var data = await _thermometerDeviceDomain.GetList(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ThermometerDeviceConstants.GetListCompletedAdminQueueName);
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion Other
    }
}