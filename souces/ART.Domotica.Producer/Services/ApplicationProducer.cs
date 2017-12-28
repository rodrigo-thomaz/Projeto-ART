using System.Threading.Tasks;
using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services
{
    public class ApplicationProducer : ProducerBase, IApplicationProducer
    {
        #region constructors

        public ApplicationProducer(IConnection connection, IMQSettings mqSettings) 
            : base(connection, mqSettings)
        {            
            Initialize();
        }

        #endregion

        #region public voids

        public async Task<ApplicationGetRPCResponseContract> GetRPC(AuthenticatedMessageContract message)
        {
            return await BasicRPCPublish<ApplicationGetRPCResponseContract>(ApplicationConstants.GetRPCQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(ApplicationConstants.GetRPCQueueName);
        }

        #endregion
    }
}