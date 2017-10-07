namespace ART.Domotica.Producer.Services
{
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.Producer;

    using RabbitMQ.Client;

    public class ThermometerDeviceProducer : ProducerBase, IThermometerDeviceProducer
    {
        #region Constructors

        public ThermometerDeviceProducer(IConnection connection)
            : base(connection)
        {
            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            _basicProperties.Persistent = true;
        }

        #endregion Methods
    }
}