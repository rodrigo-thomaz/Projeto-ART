namespace ART.Infra.CrossCutting.MQ.Worker
{
    using RabbitMQ.Client;

    public abstract class ConsumerBase : MQBase
    {
        #region Constructors

        public ConsumerBase(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
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

        #endregion Methods
    }
}