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
    public class DeviceInApplicationProducer : ProducerBase, IDeviceInApplicationProducer
    {
        #region constructors

        public DeviceInApplicationProducer(IConnection connection, IMQSettings mqSettings) 
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids        

        public async Task Insert(AuthenticatedMessageContract<DeviceInApplicationInsertRequestContract> message)
        {
            await BasicPublish(DeviceInApplicationConstants.InsertQueueName, message);
        }

        public async Task Remove(AuthenticatedMessageContract<DeviceInApplicationRemoveRequestContract> message)
        {
            await BasicPublish(DeviceInApplicationConstants.RemoveQueueName, message);
        }

        #endregion
    }
}