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
            _model.QueueDeclare(
                  queue: TemperatureScaleConstants.GetAllQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.ExchangeDeclare(
                  exchange: "amq.topic"
                , type: ExchangeType.Topic
                , durable: true
                , autoDelete: false
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
                , routingKey: TemperatureScaleConstants.GetAllForIoTQueueName
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

            //Enviando para View
            var viewModel = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleGetAllModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, TemperatureScaleConstants.GetAllCompletedQueueName);
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
            var domain = _componentContext.Resolve<ITemperatureScaleDomain>();
            var data = await domain.GetAll();

            //Enviando para o Iot
            var iotContract = Mapper.Map<List<TemperatureScale>, List<TemperatureScaleGetAllForIoTResponseContract>>(data);
            var deviceMessage = new MessageIoTContract<List<TemperatureScaleGetAllForIoTResponseContract>>(TemperatureScaleConstants.GetAllForIoTCompletedQueueName, iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<IoTRequestContract>(e.Body);
            var queueName = GetDeviceQueueName(requestContract.HardwareId);
            _model.BasicPublish("", queueName, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
