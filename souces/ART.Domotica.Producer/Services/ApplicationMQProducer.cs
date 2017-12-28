namespace ART.Domotica.Producer.Services
{
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ;
    using ART.Infra.CrossCutting.MQ.Producer;

    using RabbitMQ.Client;

    public class ApplicationMQProducer : ProducerBase, IApplicationMQProducer
    {
        #region Constructors

        public ApplicationMQProducer(IConnection connection, IMQSettings mqSettings)
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