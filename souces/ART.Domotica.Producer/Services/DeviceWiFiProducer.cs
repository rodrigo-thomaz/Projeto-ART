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
    public class DeviceWiFiProducer : ProducerBase, IDeviceWiFiProducer
    {
        #region constructors

        public DeviceWiFiProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids        

        public async Task SetHostName(AuthenticatedMessageContract<DeviceWiFiSetHostNameRequestContract> message)
        {
            await BasicPublish(DeviceWiFiConstants.SetHostNameQueueName, message);
        }

        #endregion
    }
}