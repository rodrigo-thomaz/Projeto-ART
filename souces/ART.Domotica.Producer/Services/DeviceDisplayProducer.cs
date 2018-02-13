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
    public class DeviceDisplayProducer : ProducerBase, IDeviceDisplayProducer
    {
        #region constructors

        public DeviceDisplayProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids    

        public async Task SetEnabled(AuthenticatedMessageContract<DeviceDisplaySetValueRequestContract> message)
        {
            await BasicPublish(DeviceDisplayConstants.SetEnabledQueueName, message);
        }

        #endregion
    }
}