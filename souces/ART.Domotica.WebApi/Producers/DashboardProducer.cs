namespace ART.Domotica.WebApi.Producers
{
    using ART.Domotica.WebApi.IProducers;
    using ART.Infra.CrossCutting.MQ.WebApi;
    using RabbitMQ.Client;

    public class DashboardProducer : ProducerBase, IDashboardProducer
    {
        #region Constructors

        public DashboardProducer(IConnection connection)
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