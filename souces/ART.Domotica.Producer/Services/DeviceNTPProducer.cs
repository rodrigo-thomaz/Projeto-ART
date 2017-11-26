using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Contract;

namespace ART.Domotica.Producer.Services
{
    public class DeviceNTPProducer : ProducerBase, IDeviceNTPProducer
    {
        #region constructors

        public DeviceNTPProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids        

        public async Task SetTimeZone(AuthenticatedMessageContract<DeviceNTPSetTimeZoneRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DeviceNTPConstants.SetTimeZoneQueueName, null, payload);
            });
        }

        public async Task SetUpdateIntervalInMilliSecond(AuthenticatedMessageContract<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", DeviceNTPConstants.SetUpdateIntervalInMilliSecondQueueName, null, payload);
            });
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                queue: DeviceNTPConstants.SetTimeZoneQueueName
              , durable: false
              , exclusive: false
              , autoDelete: true
              , arguments: null);

            _model.QueueDeclare(
                queue: DeviceNTPConstants.SetUpdateIntervalInMilliSecondQueueName
              , durable: false
              , exclusive: false
              , autoDelete: true
              , arguments: null);
        }        

        #endregion
    }
}