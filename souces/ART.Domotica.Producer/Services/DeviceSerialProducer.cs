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
    public class DeviceSerialProducer : ProducerBase, IDeviceSerialProducer
    {
        #region constructors

        public DeviceSerialProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids        

        public async Task SetEnabled(AuthenticatedMessageContract<DeviceSerialSetEnabledRequestContract> message)
        {
            await BasicPublish(DeviceSerialConstants.SetEnabledQueueName, message);
        }

        public async Task SetPin(AuthenticatedMessageContract<DeviceSerialSetPinRequestContract> message)
        {
            await BasicPublish(DeviceSerialConstants.SetPinQueueName, message);
        }

        #endregion
    }
}