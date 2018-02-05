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
    public class ESPDeviceProducer : ProducerBase, IESPDeviceProducer
    {
        #region constructors

        public ESPDeviceProducer(IConnection connection, IMQSettings mqSettings) 
            : base(connection, mqSettings)
        {
            
        }

        #endregion

        #region public voids

        public async Task GetAll(AuthenticatedMessageContract message)
        {
            await BasicPublish(ESPDeviceConstants.GetAllQueueName, message);
        }

        public async Task GetAllByApplicationId(AuthenticatedMessageContract message)
        {
            await BasicPublish(ESPDeviceConstants.GetAllByApplicationIdQueueName, message);
        }

        public async Task GetByPin(AuthenticatedMessageContract<ESPDeviceGetByPinRequestContract> message)
        {
            await BasicPublish(ESPDeviceConstants.GetByPinQueueName, message);
        }        

        public async Task SetLabel(AuthenticatedMessageContract<DeviceSetLabelRequestContract> message)
        {
            await BasicPublish(ESPDeviceConstants.SetLabelQueueName, message);
        }

        public async Task<ESPDeviceGetConfigurationsRPCResponseContract> GetConfigurationsRPC(ESPDeviceGetConfigurationsRPCRequestContract message)
        {
            return await BasicRPCPublish<ESPDeviceGetConfigurationsRPCResponseContract>(ESPDeviceConstants.GetConfigurationsRPCQueueName, message);
        }

        public async Task<ESPDeviceCheckForUpdatesRPCResponseContract> CheckForUpdatesRPC(ESPDeviceCheckForUpdatesRPCRequestContract message)
        {
            return await BasicRPCPublish<ESPDeviceCheckForUpdatesRPCResponseContract>(ESPDeviceConstants.CheckForUpdatesRPCQueueName, message);            
        }        

        #endregion
    }
}