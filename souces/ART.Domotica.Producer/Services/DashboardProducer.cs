namespace ART.Domotica.Producer.Services
{
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.Producer;

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
        }

        #endregion Methods
    }
}