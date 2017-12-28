namespace ART.Infra.CrossCutting.MQ.Producer
{
    using System.Collections.Generic;

    using RabbitMQ.Client;
    using System.Threading.Tasks;
    using ART.Infra.CrossCutting.Utils;
    using RabbitMQ.Client.MessagePatterns;

    public abstract class ProducerBase
    {
        #region Fields

        protected readonly IConnection _connection;
        protected readonly IModel _model;
        private readonly IMQSettings _mqSettings;

        #endregion Fields

        #region Constructors

        public ProducerBase(IConnection connection, IMQSettings mqSettings)
        {
            _connection = connection;
            _model = _connection.CreateModel();
            _mqSettings = mqSettings;
        }

        #endregion Constructors

        #region Methods

        protected async Task BasicPublish(string queueName, object message)
        {
            await Task.Run(() =>
            {
                var consumerCount = _model.ConsumerCount(queueName);

                if (consumerCount == 0)
                {
                    throw new NoConsumersFoundException();
                }

                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);

                _model.BasicPublish("", queueName, null, payload);
            });
        }

        protected async Task<T> BasicRPCPublish<T>(string queueName, object message)
            where T : class
        {
            return await Task.Run(() =>
            {
                var consumerCount = _model.ConsumerCount(queueName);

                if (consumerCount == 0)
                {
                    throw new NoConsumersFoundException();
                }

                var rpcClient = new SimpleRpcClient(_model, queueName)
                {
                    TimeoutMilliseconds = _mqSettings.RpcClientTimeOutMilliSeconds
                };

                var body = SerializationHelpers.SerializeToJsonBufferAsync(message);

                rpcClient.TimedOut += (sender, e) =>
                {
                    throw new RPCTimeoutException();
                };

                var bufferResult = rpcClient.Call(body);

                rpcClient.Close();

                var result = SerializationHelpers.DeserializeJsonBufferToType<T>(bufferResult);

                return result;
            });
        }

        protected Dictionary<string, object> CreateBasicArguments()
        {
            var arguments = new Dictionary<string, object>();

            arguments.Add("x-expires", 6000);

            return arguments;
        }

        #endregion Methods
    }
}