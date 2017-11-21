namespace ART.Infra.CrossCutting.MQ.Worker
{
    using System;

    using RabbitMQ.Client;

    public abstract class ConsumerBase
    {
        #region Fields

        protected readonly IConnection _connection;
        protected readonly IModel _model;

        #endregion Fields

        #region Constructors

        public ConsumerBase(IConnection connection)
        {
            _connection = connection;
            _model = _connection.CreateModel();
        }

        #endregion Constructors

        #region Methods

        protected string GetRoutingKeyForIoT(Guid hardwareId, string topic)
        {
            var routingKey = string.Format("ART.ESPDevice.{0}.{1}", hardwareId, topic);
            return routingKey;
        }

        protected string GetDeviceQueueName(Guid hardwareId)
        {
            var queueName = string.Format("mqtt-subscription-{0}qos0", hardwareId);
            return queueName;
        }

        #endregion Methods
    }
}