using ART.Domotica.Contract;
using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.Utils;

namespace ART.Domotica.Producer.Services
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

        public async Task GetAllResolutions(AuthenticatedMessageContract message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DSFamilyTempSensorConstants.GetAllResolutionsQueueName, null, payload);
            });            
        }

        public async Task SetScale(AuthenticatedMessageContract<DSFamilyTempSensorSetScaleRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DSFamilyTempSensorConstants.SetScaleQueueName, null, payload);
            });            
        }

        public async Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DSFamilyTempSensorConstants.SetResolutionQueueName, null, payload);
            });
        }

        public async Task SetAlarmOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmOnRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DSFamilyTempSensorConstants.SetAlarmOnQueueName, null, payload);
            });
        }

        public async Task SetAlarmValue(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmValueRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DSFamilyTempSensorConstants.SetAlarmValueQueueName, null, payload);
            });
        }

        public async Task SetAlarmBuzzerOn(AuthenticatedMessageContract<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DSFamilyTempSensorConstants.SetAlarmBuzzerOnQueueName, null, payload);
            });                        
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.GetAllResolutionsQueueName
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
                 queue: DSFamilyTempSensorConstants.SetScaleQueueName
               , durable: true
               , exclusive: false
               , autoDelete: false
               , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetAlarmOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetAlarmValueQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetAlarmBuzzerOnQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);          
        }        

        #endregion
    }
}