using ART.Domotica.Contract;
using ART.Domotica.WebApi.IProducers;
using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.WebApi;

namespace ART.Domotica.WebApi.Producers
{
    public class DSFamilyTempSensorProducer : ProducerBase, IDSFamilyTempSensorProducer
    {
        #region constructors

        public DSFamilyTempSensorProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(message);
            _model.BasicPublish("", DSFamilyTempSensorConstants.GetAllQueueName, null, payload);
        }

        public async Task GetResolutions(AuthenticatedMessageContract message)
        {
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(message);
            _model.BasicPublish("", DSFamilyTempSensorConstants.GetResolutionsQueueName, null, payload);
        }

        public async Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract> message)
        {
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(message);
            _model.BasicPublish("", DSFamilyTempSensorConstants.SetResolutionQueueName, null, payload);
        }

        public async Task SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmContract> message)
        {
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(message);
            _model.BasicPublish("", DSFamilyTempSensorConstants.SetHighAlarmQueueName, null, payload);
        }

        public async Task SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmContract> message)
        {
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(message);
            _model.BasicPublish("", DSFamilyTempSensorConstants.SetLowAlarmQueueName, null, payload);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.GetAllQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.GetResolutionsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetResolutionQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetHighAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetLowAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _basicProperties.Persistent = true;
        }        

        #endregion
    }
}