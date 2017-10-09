namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Domain.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Worker;
    using ART.Infra.CrossCutting.Utils;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class HardwareConsumer : ConsumerBase
    {
        #region Fields

        private readonly EventingBasicConsumer _getListConsumer;
        private readonly IHardwareDomain _hardwareDomain;

        #endregion Fields

        #region Constructors

        public HardwareConsumer(IConnection connection, IHardwareDomain hardwareDomain)
            : base(connection)
        {
            _getListConsumer = new EventingBasicConsumer(_model);

            _hardwareDomain = hardwareDomain;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            var queueName = HardwareConstants.GetListQueueName;

            _model.QueueDeclare(
                 queue: queueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _getListConsumer.Received += GetListReceived;

            _model.BasicConsume(queueName, false, _getListConsumer);
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
            Console.WriteLine("[{0}] {1}", HardwareConstants.GetListQueueName, Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var data = await _hardwareDomain.GetList(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, HardwareConstants.GetListCompletedQueueName);
            Console.WriteLine("[{0}] {1}", HardwareConstants.GetListCompletedQueueName, Encoding.UTF8.GetString(buffer));
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion Other
    }
}