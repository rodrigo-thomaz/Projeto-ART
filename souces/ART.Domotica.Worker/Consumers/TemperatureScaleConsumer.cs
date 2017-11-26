using ART.Domotica.Domain.Interfaces;
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
using AutoMapper;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Model;
using ART.Infra.CrossCutting.Logging;

namespace ART.Domotica.Worker.Consumers
{
    public class TemperatureScaleConsumer : ConsumerBase, ITemperatureScaleConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllConsumer;
        private readonly EventingBasicConsumer _getAllForIoTConsumer;

        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public TemperatureScaleConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _getAllConsumer = new EventingBasicConsumer(_model);
            _getAllForIoTConsumer = new EventingBasicConsumer(_model);

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
                  queue: TemperatureScaleConstants.GetAllQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);            

            _model.QueueDeclare(
                  queue: TemperatureScaleConstants.GetAllForIoTQueueName
                , durable: false
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueBind(
                  queue: TemperatureScaleConstants.GetAllForIoTQueueName
                , exchange: "amq.topic"
                , routingKey: GetApplicationRoutingKeyForAllIoT(TemperatureScaleConstants.GetAllForIoTQueueName)
                , arguments: null);

            _getAllConsumer.Received += GetAllReceived;
            _getAllForIoTConsumer.Received += GetAllForIoTReceived;

            _model.BasicConsume(TemperatureScaleConstants.GetAllQueueName, false, _getAllConsumer);
            _model.BasicConsume(TemperatureScaleConstants.GetAllForIoTQueueName, false, _getAllForIoTConsumer);
        }

        public void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        public async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<ITemperatureScaleDomain>();
            var data = await domain.GetAll();

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.Get(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleDetailModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, TemperatureScaleConstants.GetAllCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        public void GetAllForIoTReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllForIoTReceivedAsync(sender, e));
        }

        public async Task GetAllForIoTReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<IoTRequestContract>(e.Body);
            var temperatureScaleDomain = _componentContext.Resolve<ITemperatureScaleDomain>();            
            var data = await temperatureScaleDomain.GetAll();

            var espDeviceDomain = _componentContext.Resolve<IESPDeviceDomain>();
            var applicationMQ = await espDeviceDomain.GetApplicationMQ(requestContract.DeviceId);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetById(requestContract.DeviceId);

            var exchange = "amq.topic";

            //Enviando para o Iot
            var iotContract = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleGetAllForIoTResponseContract>>(data);
            var deviceMessage = new MessageIoTContract<List<TemperatureScaleGetAllForIoTResponseContract>>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);            
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, TemperatureScaleConstants.GetAllForIoTCompletedQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
