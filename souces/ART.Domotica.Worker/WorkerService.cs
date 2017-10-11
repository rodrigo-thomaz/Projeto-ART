namespace ART.Domotica.Worker
{
    using RabbitMQ.Client;

    public class WorkerService
    {
        #region Fields

        private readonly IConnection _connection;

        #endregion Fields

        #region Constructors

        public WorkerService(IConnection connection)
        {
            _connection = connection;
        }

        #endregion Constructors

        #region Methods

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            _connection.Close(30);
            log4net.LogManager.Shutdown();
            return true;
        }

        #endregion Methods
    }
}