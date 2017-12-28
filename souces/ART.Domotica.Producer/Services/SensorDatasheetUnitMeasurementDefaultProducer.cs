using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services
{
    public class SensorDatasheetUnitMeasurementDefaultProducer : ProducerBase, ISensorDatasheetUnitMeasurementDefaultProducer
    {
        #region constructors

        public SensorDatasheetUnitMeasurementDefaultProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(SensorDatasheetUnitMeasurementDefaultConstants.GetAllQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(SensorDatasheetUnitMeasurementDefaultConstants.GetAllQueueName);
        }

        #endregion
    }
}