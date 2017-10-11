namespace ART.Domotica.Worker
{
    using ART.Domotica.Worker.IConsumers;

    using RabbitMQ.Client;

    public class WorkerService
    {
        #region Fields

        private readonly IConnection _connection;

        #endregion Fields

        #region Constructors

        public WorkerService(
            IConnection connection
            , IApplicationConsumer applicationConsumer
            , IApplicationUserConsumer applicationUserConsumer
            , IDSFamilyTempSensorConsumer dsFamilyTempSensorConsumer
            , ITemperatureScaleConsumer temperatureScaleConsumer
            , IHardwaresInApplicationConsumer hardwaresInApplicationConsumer
            , IThermometerDeviceConsumer thermometerDeviceConsumer
            )
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