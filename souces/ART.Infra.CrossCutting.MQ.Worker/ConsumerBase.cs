namespace ART.Infra.CrossCutting.MQ.Worker
{
    using ART.Infra.CrossCutting.Logging;

    using Autofac;

    using RabbitMQ.Client;

    public abstract class ConsumerBase : MQBase
    {
        #region Fields

        protected readonly IComponentContext _componentContext;
        protected readonly ILogger _logger;

        #endregion Fields

        #region Constructors

        public ConsumerBase(IConnection connection, IMQSettings mqSettings, ILogger logger, IComponentContext componentContext)
            : base(connection, mqSettings)
        {
            _componentContext = componentContext;
            _logger = logger;

            Initialize();
        }

        #endregion Constructors

        #region Methods

        protected string GetApplicationRoutingKeyForAllIoT(string topic)
        {
            var routingKey = string.Format("ART.Application.*.Device.*.{0}", topic);
            return routingKey;
        }

        protected string GetApplicationRoutingKeyForIoT(string applicationTopic, string deviceTopic, string topic)
        {
            var routingKey = string.Format("ART.Application.{0}.Device.{1}.{2}", applicationTopic, deviceTopic, topic);
            return routingKey;
        }

        protected string GetDeviceRoutingKeyForIoT(string deviceTopic, string topic)
        {
            var routingKey = string.Format("ART.Device.{0}.{1}", deviceTopic, topic);
            return routingKey;
        }

        protected string GetInApplicationRoutingKeyForAllView(string applicationTopic, string topic)
        {
            var routingKey = string.Format("ART.Application.{0}.WebUI.{1}", applicationTopic, topic);
            return routingKey;
        }

        protected string GetInApplicationRoutingKeyForView(string applicationTopic, string webUITopic, string topic)
        {
            var routingKey = string.Format("ART.Application.{0}.WebUI.{1}.{2}", applicationTopic, webUITopic, topic);
            return routingKey;
        }

        protected string GetNotInApplicationRoutingKeyForView(string webUITopic, string topic)
        {
            var routingKey = string.Format("ART.WebUI.{0}.{1}", webUITopic, topic);
            return routingKey;
        }

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