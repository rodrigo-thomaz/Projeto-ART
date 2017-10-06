using ART.Domotica.WebApi.IProducers;
using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.WebApi.Producers
{
    public class ApplicationUserProducer : ProducerBase, IApplicationUserProducer
    {
        #region constructors

        public ApplicationUserProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids
                

        #endregion

        #region private voids

        private void Initialize()
        {
            _basicProperties.Persistent = true;
        }

        #endregion
    }
}