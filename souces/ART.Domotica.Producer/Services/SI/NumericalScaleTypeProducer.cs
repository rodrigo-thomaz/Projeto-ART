using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Constant.SI;
using ART.Domotica.Producer.Interfaces.SI;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services.SI
{
    public class NumericalScaleTypeProducer : ProducerBase, INumericalScaleTypeProducer
    {
        #region constructors

        public NumericalScaleTypeProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(NumericalScaleTypeConstants.GetAllQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(NumericalScaleTypeConstants.GetAllQueueName);
        }

        #endregion
    }
}