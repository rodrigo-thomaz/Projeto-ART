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
            Initialize();
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

        public async Task InsertInApplication(AuthenticatedMessageContract<ESPDeviceInsertInApplicationRequestContract> message)
        {
            await BasicPublish(ESPDeviceConstants.InsertInApplicationQueueName, message);
        }

        public async Task DeleteFromApplication(AuthenticatedMessageContract<ESPDeviceDeleteFromApplicationRequestContract> message)
        {
            await BasicPublish(ESPDeviceConstants.DeleteFromApplicationQueueName, message);
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

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: ESPDeviceConstants.GetAllByApplicationIdQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.GetByPinQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.InsertInApplicationQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.DeleteFromApplicationQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: CreateBasicArguments());

            _model.QueueDeclare(
               queue: ESPDeviceConstants.SetLabelQueueName
             , durable: false
             , exclusive: false
             , autoDelete: true
             , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                queue: ESPDeviceConstants.GetConfigurationsRPCQueueName
              , durable: false
              , exclusive: false
              , autoDelete: true
              , arguments: CreateBasicArguments());

            _model.QueueDeclare(
                 queue: ESPDeviceConstants.CheckForUpdatesRPCQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: CreateBasicArguments());
        }        

        #endregion
    }
}