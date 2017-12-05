using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.Producer.Services
{
    public class ApplicationMQProducer : ProducerBase, IApplicationMQProducer
    {       
        #region constructors

        public ApplicationMQProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids
                

        #endregion

        #region private voids

        private void Initialize()
        {

        }

        #endregion
    }
}