namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.Logging;
    using ART.Infra.CrossCutting.MQ;
    using ART.Infra.CrossCutting.MQ.Worker;

    using Autofac;

    using RabbitMQ.Client;

    public class ApplicationMQConsumer : ConsumerBase, IApplicationMQConsumer
    {
        #region Fields

        private readonly IComponentContext _componentContext;
        private readonly ILogger _logger;

        #endregion Fields

        #region Constructors

        public ApplicationMQConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            _componentContext = componentContext;
            _logger = logger;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
        }

        #endregion Methods
    }
}