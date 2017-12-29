using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Constant.Globalization;
using ART.Domotica.Producer.Interfaces.Globalization;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services.Globalization
{
    public class TimeZoneProducer : ProducerBase, ITimeZoneProducer
    {
        #region constructors

        public TimeZoneProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(TimeZoneConstants.GetAllQueueName, message);
        }

        #endregion
    }
}