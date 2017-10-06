using RabbitMQ.Client;

namespace ART.Infra.CrossCutting.MQ.Worker
{
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
    }
}
