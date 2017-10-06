namespace ART.Domotica.WebApi.Producers
{
    using ART.Domotica.WebApi.IProducers;
    using ART.Infra.CrossCutting.MQ.WebApi;
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