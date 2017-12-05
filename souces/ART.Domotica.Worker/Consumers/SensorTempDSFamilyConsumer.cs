using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Contract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Worker.IConsumers;
using System.Collections.Generic;
using ART.Domotica.IoTContract;
using Autofac;
using ART.Infra.CrossCutting.Logging;
using AutoMapper;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Model;

namespace ART.Domotica.Worker.Consumers
{
    public class SensorTempDSFamilyConsumer : ConsumerBase, ISensorTempDSFamilyConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllResolutionsConsumer;
        private readonly EventingBasicConsumer _setResolutionConsumer;        
        
        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public SensorTempDSFamilyConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _getAllResolutionsConsumer = new EventingBasicConsumer(_model);
            _setResolutionConsumer = new EventingBasicConsumer(_model);
            
            _componentContext = componentContext;

            _logger = logger;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.ExchangeDeclare(
                 exchange: "amq.topic"
               , type: ExchangeType.Topic
               , durable: true
               , autoDelete: false
               , arguments: null);

            _model.QueueDeclare(
                  queue: SensorTempDSFamilyConstants.GetAllResolutionsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                 queue: SensorTempDSFamilyConstants.SetResolutionQueueName
               , durable: true
               , exclusive: false
               , autoDelete: false
               , arguments: null);

            _getAllResolutionsConsumer.Received += GetAllResolutionsReceived;
            _setResolutionConsumer.Received += SetResolutionReceived;
                        
            _model.BasicConsume(SensorTempDSFamilyConstants.SetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(SensorTempDSFamilyConstants.GetAllResolutionsQueueName, false, _getAllResolutionsConsumer);
            
        }

        public void GetAllResolutionsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllResolutionsReceivedAsync(sender, e));
        }

        public async Task GetAllResolutionsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<ISensorTempDSFamilyDomain>();
            var data = await domain.GetAllResolutions();

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<SensorTempDSFamilyResolution>, List<SensorTempDSFamilyResolutionGetModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, SensorTempDSFamilyConstants.GetAllResolutionsViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        public void SetResolutionReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetResolutionReceivedAsync(sender, e));
        }

        public async Task SetResolutionReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorTempDSFamilySetResolutionRequestContract>>(e.Body);
            var domain = _componentContext.Resolve<ISensorTempDSFamilyDomain>();
            var data = await domain.SetResolution(message.Contract.SensorTempDSFamilyId, message.Contract.SensorTempDSFamilyResolutionId);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(data.Id);

            //Enviando para View
            var viewModel = Mapper.Map<SensorTempDSFamily, SensorTempDSFamilySetResolutionModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorTempDSFamilyConstants.SetResolutionViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(viewModel.DeviceId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorTempDSFamilySetResolutionRequestContract, SensorTempDSFamilySetResolutionRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorTempDSFamilySetResolutionRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorTempDSFamilyConstants.SetResolutionIoTQueueName);

            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }
                
        #endregion
    }
}
