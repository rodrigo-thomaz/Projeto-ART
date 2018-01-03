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

        public async Task SetActive(AuthenticatedMessageContract<DeviceDebugSetActiveRequestContract> message)
        {
            await BasicPublish(DeviceDebugConstants.SetActiveQueueName, message);
        }

        #endregion
    }
}