namespace ART.Domotica.Producer.Services
{
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ;
    using ART.Infra.CrossCutting.MQ.Producer;

    using RabbitMQ.Client;

    public class ApplicationUserProducer : ProducerBase, IApplicationUserProducer
    {
        #region Constructors

        public ApplicationUserProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
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