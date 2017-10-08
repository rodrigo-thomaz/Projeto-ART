namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Infra.CrossCutting.MQ.Worker;

    using RabbitMQ.Client;

    public class HardwaresInApplicationConsumer : ConsumerBase
    {
        #region Fields

        //private readonly EventingBasicConsumer _getConsumer;
        private readonly IApplicationDomain _applicationDomain;

        #endregion Fields

        #region Constructors

        public HardwaresInApplicationConsumer(IConnection connection, IApplicationDomain applicationDomain)
            : base(connection)
        {
            //_getConsumer = new EventingBasicConsumer(_model);

            _applicationDomain = applicationDomain;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            var queueName = "";// ApplicationConstants.GetQueueName;

            _model.QueueDeclare(
                 queue: queueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            //_getConsumer.Received += GetReceived;

            //_model.BasicConsume(queueName, false, _getConsumer);
        }

        #endregion Methods

        #region Other

        //private void GetReceived(object sender, BasicDeliverEventArgs e)
        //{
        //    Task.WaitAll(GetReceivedAsync(sender, e));
        //}
        //private async Task GetReceivedAsync(object sender, BasicDeliverEventArgs e)
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("[{0}] {1}", ApplicationConstants.GetQueueName, Encoding.UTF8.GetString(e.Body));
        //    _model.BasicAck(e.DeliveryTag, false);
        //    var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
        //    var data = await _applicationDomain.Get(message);
        //    var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
        //    var exchange = "amq.topic";
        //    var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, ApplicationConstants.GetCompletedQueueName);
        //    Console.WriteLine("[{0}] {1}", ApplicationConstants.GetCompletedQueueName, Encoding.UTF8.GetString(buffer));
        //    _model.BasicPublish(exchange, rountingKey, null, buffer);
        //}

        #endregion Other
    }
}