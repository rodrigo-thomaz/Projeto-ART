namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.Logging;
    using ART.Infra.CrossCutting.MQ.Worker;

    using Autofac;

    using RabbitMQ.Client;

    public class DeviceSensorsConsumer : ConsumerBase, IDeviceSensorsConsumer
    {
        #region Fields

        private readonly IComponentContext _componentContext;
        private readonly ILogger _logger;

        #endregion Fields

        #region Constructors

        public DeviceSensorsConsumer(IConnection connection, ILogger logger, IComponentContext componentContext)
            : base(connection)
        {
            _componentContext = componentContext;
            _logger = logger;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            _model.ExchangeDeclare(
                  exchange: "amq.topic"
                , type: ExchangeType.Topic
                , durable: true
                , autoDelete: false
                , arguments: null);
        }

        #endregion Methods
    }
}