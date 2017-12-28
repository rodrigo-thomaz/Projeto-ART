using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Infra.CrossCutting.MQ.Contract;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Domotica.Contract;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Producer.Services
{
    public class DeviceNTPProducer : ProducerBase, IDeviceNTPProducer
    {
        #region constructors

        public DeviceNTPProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            Initialize();
        }

        #endregion

        #region public voids        

        public async Task SetTimeZone(AuthenticatedMessageContract<DeviceNTPSetTimeZoneRequestContract> message)
        {
            await BasicPublish(DeviceNTPConstants.SetTimeZoneQueueName, message);
        }

        public async Task SetUpdateIntervalInMilliSecond(AuthenticatedMessageContract<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract> message)
        {
            await BasicPublish(DeviceNTPConstants.SetUpdateIntervalInMilliSecondQueueName, message);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(DeviceNTPConstants.SetTimeZoneQueueName);
            BasicQueueDeclare(DeviceNTPConstants.SetUpdateIntervalInMilliSecondQueueName);
        }        

        #endregion
    }
}