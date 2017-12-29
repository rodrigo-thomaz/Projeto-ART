namespace ART.Infra.CrossCutting.MQ.Producer
{

    using RabbitMQ.Client;
    using System.Threading.Tasks;
    using ART.Infra.CrossCutting.Utils;
    using RabbitMQ.Client.MessagePatterns;

    public abstract class ProducerBase : MQBase
    {
        #region Constructors

        public ProducerBase(IConnection connection, IMQSettings mqSettings)
            :base(connection, mqSettings)
        {

        }

        #endregion Constructors

        #region Methods

        protected async Task BasicPublish(string queueName, object message)
        {
            await Task.Run(() =>
            {
                using (var model = _connection.CreateModel())
                {
                    var queueDeclare = BasicQueueDeclare(model, queueName);

                    if (queueDeclare.ConsumerCount == 0)
                    {
                        throw new NoConsumersFoundException();
                    }

                    var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);

                    model.BasicPublish("", queueName, null, payload);
                }
            });
        }

        protected async Task<T> BasicRPCPublish<T>(string queueName, object message)
            where T : class
        {
            return await Task.Run(() =>
            {
                byte[] bufferResult;

                using (var model = _connection.CreateModel())
                {
                    var queueDeclare = BasicQueueDeclare(model, queueName);

                    if (queueDeclare.ConsumerCount == 0)
                    {
                        throw new NoConsumersFoundException();
                    }

                    var rpcClient = new SimpleRpcClient(model, queueName);

                    var body = SerializationHelpers.SerializeToJsonBufferAsync(message);

                    rpcClient.TimedOut += (sender, e) =>
                    {
                        throw new RPCTimeoutException();
                    };

                    bufferResult = rpcClient.Call(body);

                    rpcClient.Close();                    
                }

                var result = SerializationHelpers.DeserializeJsonBufferToType<T>(bufferResult);

                return result;

            });
        }

        private QueueDeclareOk BasicQueueDeclare(IModel model, string queueName)
        {
            return model.QueueDeclare(
                  queue: queueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: CreateBasicArguments());
        }

        #endregion Methods
    }
}