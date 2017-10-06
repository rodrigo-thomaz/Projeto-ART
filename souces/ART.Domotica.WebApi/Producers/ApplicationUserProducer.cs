namespace ART.Domotica.WebApi.Producers
{
    using ART.Domotica.WebApi.IProducers;
    using ART.Infra.CrossCutting.MQ;

    using RabbitMQ.Client;

    public class ApplicationUserProducer : ProducerBase, IApplicationUserProducer
    {
        #region Constructors

        public ApplicationUserProducer(IConnection connection)
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