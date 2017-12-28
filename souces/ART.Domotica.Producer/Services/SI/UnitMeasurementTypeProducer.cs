using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Constant.SI;
using ART.Domotica.Producer.Interfaces.SI;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services.SI
{
    public class UnitMeasurementTypeProducer : ProducerBase, IUnitMeasurementTypeProducer
    {
        #region constructors

        public UnitMeasurementTypeProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(UnitMeasurementTypeConstants.GetAllQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(UnitMeasurementTypeConstants.GetAllQueueName);
        }

        #endregion
    }
}