using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces.Locale;
using ART.Domotica.Constant.Locale;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services.Locale
{
    public class ContinentProducer : ProducerBase, IContinentProducer
    {
        #region constructors

        public ContinentProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(ContinentConstants.GetAllQueueName, message);
        }

        #endregion
    }
}