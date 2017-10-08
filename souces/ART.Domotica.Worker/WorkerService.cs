namespace ART.Domotica.Worker
{
    using ART.Domotica.Worker.Consumers;

    using RabbitMQ.Client;

    public class WorkerService
    {
        #region Fields

        private readonly ApplicationConsumer _applicationConsumer;
        private readonly ApplicationUserConsumer _applicationUserConsumer;
        private readonly IConnection _connection;
        private readonly DSFamilyTempSensorConsumer _dsFamilyTempSensorConsumer;
        private readonly HardwareConsumer _hardwareConsumer;
        private readonly HardwaresInApplicationConsumer _hardwaresInApplicationConsumer;
        private readonly TemperatureScaleConsumer _temperatureScaleConsumer;

        #endregion Fields

        #region Constructors

        public WorkerService(
            IConnection connection
            , ApplicationConsumer applicationConsumer
            , ApplicationUserConsumer applicationUserConsumer
            , DSFamilyTempSensorConsumer dsFamilyTempSensorConsumer
            , TemperatureScaleConsumer temperatureScaleConsumer
            , HardwareConsumer hardwareConsumer
            , HardwaresInApplicationConsumer hardwaresInApplicationConsumer
            )
        {
            _connection = connection;
            _applicationConsumer = applicationConsumer;
            _applicationUserConsumer = applicationUserConsumer;
            _dsFamilyTempSensorConsumer = dsFamilyTempSensorConsumer;
            _hardwareConsumer = hardwareConsumer;
            _hardwaresInApplicationConsumer = hardwaresInApplicationConsumer;
            _temperatureScaleConsumer = temperatureScaleConsumer;
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
            return true;
        }

        #endregion Methods
    }
}