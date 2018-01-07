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
    public class DeviceDebugProducer : ProducerBase, IDeviceDebugProducer
    {
        #region constructors

        public DeviceDebugProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids    

        public async Task SetRemoteEnabled(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message)
        {
            await BasicPublish(DeviceDebugConstants.SetRemoteEnabledQueueName, message);
        }

        public async Task SetResetCmdEnabled(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message)
        {
            await BasicPublish(DeviceDebugConstants.SetResetCmdEnabledQueueName, message);
        }

        public async Task SetSerialEnabled(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message)
        {
            await BasicPublish(DeviceDebugConstants.SetSerialEnabledQueueName, message);
        }

        public async Task SetShowColors(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message)
        {
            await BasicPublish(DeviceDebugConstants.SetShowColorsQueueName, message);
        }

        public async Task SetShowDebugLevel(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message)
        {
            await BasicPublish(DeviceDebugConstants.SetShowDebugLevelQueueName, message);
        }

        public async Task SetShowProfiler(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message)
        {
            await BasicPublish(DeviceDebugConstants.SetShowProfilerQueueName, message);
        }

        public async Task SetShowTime(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message)
        {
            await BasicPublish(DeviceDebugConstants.SetShowTimeQueueName, message);
        }

        #endregion
    }
}