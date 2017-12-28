namespace ART.Infra.CrossCutting.MQ.Worker
{
    using System.Collections.Generic;

    using RabbitMQ.Client;

    public abstract class ConsumerBase
    {
        #region Fields

        protected readonly IConnection _connection;
        protected readonly IModel _model;
        protected readonly IMQSettings _mqSettings;

        #endregion Fields

        #region Constructors

        public ConsumerBase(IConnection connection, IMQSettings mqSettings)
        {
            _connection = connection;
            _model = _connection.CreateModel();
            _mqSettings = mqSettings;
        }

        #endregion Constructors

        #region Methods

        protected Dictionary<string, object> CreateBasicArguments()
        {
            var arguments = new Dictionary<string, object>();

            arguments.Add("x-expires", _mqSettings.QueueExpiresMilliSecondsSettingsKey);

            return arguments;
        }

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