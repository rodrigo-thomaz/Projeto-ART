using ART.Domotica.Contract;
using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services
{
    public class SensorUnitMeasurementScaleProducer : ProducerBase, ISensorUnitMeasurementScaleProducer
    {
        #region constructors

        public SensorUnitMeasurementScaleProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }

        #endregion

        #region public voids          

        public async Task SetRange(AuthenticatedMessageContract<SensorUnitMeasurementScaleSetValueRequestContract> message)
        {
            await BasicPublish(SensorUnitMeasurementScaleConstants.SetRangeQueueName, message);
        }

        public async Task SetChartLimiter(AuthenticatedMessageContract<SensorUnitMeasurementScaleSetValueRequestContract> message)
        {
            await BasicPublish(SensorUnitMeasurementScaleConstants.SetChartLimiterQueueName, message);
        }

        public async Task SetUnitMeasurementNumericalScaleTypeCountry(AuthenticatedMessageContract<SensorUnitMeasurementScaleSetUnitMeasurementNumericalScaleTypeCountryRequestContract> message)
        {
            await BasicPublish(SensorUnitMeasurementScaleConstants.SetUnitMeasurementNumericalScaleTypeCountryQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(SensorUnitMeasurementScaleConstants.SetRangeQueueName);
            BasicQueueDeclare(SensorUnitMeasurementScaleConstants.SetChartLimiterQueueName);
            BasicQueueDeclare(SensorUnitMeasurementScaleConstants.SetUnitMeasurementNumericalScaleTypeCountryQueueName);
        }

        #endregion
    }
}