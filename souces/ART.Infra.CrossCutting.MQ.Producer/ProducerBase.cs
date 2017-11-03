namespace ART.Infra.CrossCutting.MQ.Producer
{
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
    }
}