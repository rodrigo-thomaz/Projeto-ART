namespace ART.Domotica.Worker
{
    using ART.Domotica.Worker.IConsumers;

    using RabbitMQ.Client;

    public class WorkerService
    {
        #region Fields

        private readonly IApplicationConsumer _applicationConsumer;
        private readonly IApplicationUserConsumer _applicationUserConsumer;
        private readonly IConnection _connection;
        private readonly IDSFamilyTempSensorConsumer _dsFamilyTempSensorConsumer;
        private readonly IHardwaresInApplicationConsumer _hardwaresInApplicationConsumer;
        private readonly ITemperatureScaleConsumer _temperatureScaleConsumer;
        private readonly IThermometerDeviceConsumer _thermometerDeviceConsumer;

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
            _applicationConsumer = applicationConsumer;
            _applicationUserConsumer = applicationUserConsumer;
            _dsFamilyTempSensorConsumer = dsFamilyTempSensorConsumer;
            _hardwaresInApplicationConsumer = hardwaresInApplicationConsumer;
            _temperatureScaleConsumer = temperatureScaleConsumer;
            _thermometerDeviceConsumer = thermometerDeviceConsumer;
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