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
        private readonly EventingBasicConsumer _insertHardwareConsumer;
        private readonly EventingBasicConsumer _deleteHardwareConsumer;

        private readonly IHardwaresInApplicationDomain _hardwaresInApplicationDomain;

        #endregion Fields

        #region Constructors

        public HardwaresInApplicationConsumer(IConnection connection, IHardwaresInApplicationDomain hardwaresInApplicationDomain)
            : base(connection)
        {
            _getListConsumer = new EventingBasicConsumer(_model);
            _searchPinConsumer = new EventingBasicConsumer(_model);
            _insertHardwareConsumer = new EventingBasicConsumer(_model);
            _deleteHardwareConsumer = new EventingBasicConsumer(_model);

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

            _model.QueueDeclare(
                 queue: HardwaresInApplicationConstants.InsertHardwareQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                 queue: HardwaresInApplicationConstants.DeleteHardwareQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _getListConsumer.Received += GetListReceived;
            _searchPinConsumer.Received += SearchPinReceived;
            _insertHardwareConsumer.Received += InsertHardwareReceived;
            _deleteHardwareConsumer.Received += DeleteHardwareReceived;

            _model.BasicConsume(HardwaresInApplicationConstants.GetListQueueName, false, _getListConsumer);
            _model.BasicConsume(HardwaresInApplicationConstants.SearchPinQueueName, false, _searchPinConsumer);
            _model.BasicConsume(HardwaresInApplicationConstants.InsertHardwareQueueName, false, _insertHardwareConsumer);
            _model.BasicConsume(HardwaresInApplicationConstants.DeleteHardwareQueueName, false, _deleteHardwareConsumer);
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

            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<HardwaresInApplicationPinContract>>(e.Body);
            var data = await _hardwaresInApplicationDomain.SearchPin(message);

            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, HardwaresInApplicationConstants.SearchPinCompletedQueueName);
            Console.WriteLine("[{0}] {1}", HardwaresInApplicationConstants.SearchPinCompletedQueueName, Encoding.UTF8.GetString(buffer));
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        private void InsertHardwareReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(InsertHardwareReceivedAsync(sender, e));
        }

        private async Task InsertHardwareReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[{0}] {1}", HardwaresInApplicationConstants.InsertHardwareQueueName, Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<HardwaresInApplicationPinContract>>(e.Body);
            await _hardwaresInApplicationDomain.InsertHardware(message);            
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, HardwaresInApplicationConstants.InsertHardwareCompletedQueueName);
            Console.WriteLine("[{0}] Ok", HardwaresInApplicationConstants.InsertHardwareCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, null);
        }

        private void DeleteHardwareReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(DeleteHardwareReceivedAsync(sender, e));
        }

        private async Task DeleteHardwareReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[{0}] {1}", HardwaresInApplicationConstants.DeleteHardwareQueueName, Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<HardwaresInApplicationDeleteHardwareContract>>(e.Body);
            await _hardwaresInApplicationDomain.DeleteHardware(message);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, HardwaresInApplicationConstants.DeleteHardwareCompletedQueueName);
            Console.WriteLine("[{0}] Ok", HardwaresInApplicationConstants.DeleteHardwareCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, null);
        }

        #endregion Other
    }
}