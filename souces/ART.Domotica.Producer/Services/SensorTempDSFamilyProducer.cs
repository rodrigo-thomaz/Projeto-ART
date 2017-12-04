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
    public class SensorTempDSFamilyProducer : ProducerBase, ISensorTempDSFamilyProducer
    {
        #region constructors

        public SensorTempDSFamilyProducer(IConnection connection) : base(connection)
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
                _model.BasicPublish("", SensorTempDSFamilyConstants.GetAllResolutionsQueueName, null, payload);
            });            
        }

        public async Task SetResolution(AuthenticatedMessageContract<SensorTempDSFamilySetResolutionRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", SensorTempDSFamilyConstants.SetResolutionQueueName, null, payload);
            });
        }        

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: SensorTempDSFamilyConstants.GetAllResolutionsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTempDSFamilyConstants.SetResolutionQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);
        }

        #endregion
    }
}