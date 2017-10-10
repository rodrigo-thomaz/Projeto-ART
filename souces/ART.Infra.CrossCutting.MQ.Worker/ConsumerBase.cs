namespace ART.Infra.CrossCutting.MQ.Worker
{
    using log4net;
    using RabbitMQ.Client;

    public abstract class ConsumerBase
    {
        #region Fields

        protected readonly IConnection _connection;
        protected readonly IModel _model;
        protected readonly ILog _log;

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