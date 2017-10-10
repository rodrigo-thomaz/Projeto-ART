namespace ART.Infra.CrossCutting.MQ.Worker
{
    using log4net;

    using RabbitMQ.Client;

    public abstract class ConsumerBase
    {
        #region Fields

        protected readonly IConnection _connection;
        protected readonly ILog _log;
        protected readonly IModel _model;

        #endregion Fields

        #region Constructors

        public ConsumerBase(IConnection connection, ILog log)
        {
            _connection = connection;
            _model = _connection.CreateModel();
            _log = log;
        }

        #endregion Constructors
    }
}