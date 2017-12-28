namespace ART.Infra.CrossCutting.MQ.Producer
{
    using System.Collections.Generic;

    using RabbitMQ.Client;

    public abstract class ProducerBase
    {
        #region Fields

        protected readonly IConnection _connection;
        protected readonly IModel _model;

        #endregion Fields

        #region Constructors

        public ProducerBase(IConnection connection)
        {
            _connection = connection;
            _model = _connection.CreateModel();
        }

        #endregion Constructors

        #region Methods

        protected Dictionary<string, object> CreateBasicArguments()
        {
            var arguments = new Dictionary<string, object>();

            arguments.Add("x-expires", 6000);

            return arguments;
        }

        #endregion Methods
    }
}