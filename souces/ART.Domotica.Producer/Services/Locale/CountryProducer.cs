using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces.Locale;
using ART.Domotica.Constant.Locale;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services.Locale
{
    public class CountryProducer : ProducerBase, ICountryProducer
    {
        #region constructors

        public CountryProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(CountryConstants.GetAllQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(CountryConstants.GetAllQueueName);
        }

        #endregion
    }
}