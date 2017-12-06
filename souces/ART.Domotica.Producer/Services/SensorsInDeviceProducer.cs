namespace ART.Domotica.Producer.Services
{
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.Producer;

    using RabbitMQ.Client;

    public class SensorsInDeviceProducer : ProducerBase, ISensorsInDeviceProducer
    {
        #region Constructors

        public SensorsInDeviceProducer(IConnection connection)
            : base(connection)
        {
            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
        }

        #endregion Methods
    }
}