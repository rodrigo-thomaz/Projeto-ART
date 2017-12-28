using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Constant.SI;
using ART.Domotica.Producer.Interfaces.SI;

namespace ART.Domotica.Producer.Services.SI
{
    public class NumericalScaleTypeCountryProducer : ProducerBase, INumericalScaleTypeCountryProducer
    {
        #region constructors

        public NumericalScaleTypeCountryProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(NumericalScaleTypeCountryConstants.GetAllQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: NumericalScaleTypeCountryConstants.GetAllQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: CreateBasicArguments());            
        }

        #endregion
    }
}