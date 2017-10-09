namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Contract;
    using ART.Domotica.Domain.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Worker;
    using ART.Infra.CrossCutting.Utils;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public class HardwaresInApplicationConsumer : ConsumerBase
    {
        #region Fields

        private readonly EventingBasicConsumer _getListConsumer;
        private readonly EventingBasicConsumer _searchPinConsumer;
        private readonly IHardwaresInApplicationDomain _hardwaresInApplicationDomain;

        #endregion Fields

        #region Constructors

        public HardwaresInApplicationConsumer(IConnection connection, IHardwaresInApplicationDomain hardwaresInApplicationDomain)
            : base(connection)
        {
            _getListConsumer = new EventingBasicConsumer(_model);
            _searchPinConsumer = new EventingBasicConsumer(_model);

            _hardwaresInApplicationDomain = hardwaresInApplicationDomain;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            _model.QueueDeclare(
                 queue: HardwaresInApplicationConstants.GetListQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                 queue: HardwaresInApplicationConstants.SearchPinQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _getListConsumer.Received += GetListReceived;
            _searchPinConsumer.Received += SearchPinReceived;

            _model.BasicConsume(HardwaresInApplicationConstants.GetListQueueName, false, _getListConsumer);
            _model.BasicConsume(HardwaresInApplicationConstants.SearchPinQueueName, false, _searchPinConsumer);
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
            Console.WriteLine("[{0}] {1}", HardwaresInApplicationConstants.GetListQueueName, Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var data = await _hardwaresInApplicationDomain.GetList(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, HardwaresInApplicationConstants.GetListCompletedQueueName);
            Console.WriteLine("[{0}] {1}", HardwaresInApplicationConstants.GetListCompletedQueueName, Encoding.UTF8.GetString(buffer));
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        private void SearchPinReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SearchPinReceivedAsync(sender, e));
        }
        private async Task SearchPinReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[{0}] {1}", HardwaresInApplicationConstants.SearchPinQueueName, Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<HardwaresInApplicationSearchPinContract>>(e.Body);
            var data = await _hardwaresInApplicationDomain.SearchPin(message);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, HardwaresInApplicationConstants.SearchPinCompletedQueueName);
            Console.WriteLine("[{0}] {1}", HardwaresInApplicationConstants.SearchPinCompletedQueueName, Encoding.UTF8.GetString(buffer));
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion Other
    }
}