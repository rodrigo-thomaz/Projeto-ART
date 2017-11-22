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

        protected string GetRoutingKeyForAllIoT(string topic)
        {
            var routingKey = string.Format("ART.Device.*.{0}", topic);
            return routingKey;
        }

        protected string GetRoutingKeyForIoT(string deviceTopic, string topic)
        {
            var routingKey = string.Format("ART.Device.{0}.{1}", deviceTopic, topic);
            return routingKey;
        }

        #endregion Methods
    }
}