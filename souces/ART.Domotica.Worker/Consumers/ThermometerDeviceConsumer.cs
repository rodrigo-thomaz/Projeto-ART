namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Domain.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Worker;
    using ART.Infra.CrossCutting.Utils;
    using log4net;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class ThermometerDeviceConsumer : ConsumerBase
    {
        #region Fields

        private readonly EventingBasicConsumer _getListConsumer;
        private readonly IThermometerDeviceDomain _thermometerDeviceDomain;

        #endregion Fields

        #region Constructors

        public ThermometerDeviceConsumer(IConnection connection, ILog log, IThermometerDeviceDomain thermometerDeviceDomain)
            : base(connection, log)
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

        private void GetListReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetListReceivedAsync(sender, e));
        }
        private async Task GetListReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[{0}] {1}", ThermometerDeviceConstants.GetListAdminQueueName, Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var data = await _thermometerDeviceDomain.GetList(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ThermometerDeviceConstants.GetListCompletedAdminQueueName);
            Console.WriteLine("[{0}] {1}", ThermometerDeviceConstants.GetListCompletedAdminQueueName, Encoding.UTF8.GetString(buffer));
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion Other
    }
}