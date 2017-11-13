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
using ART.Domotica.Contract;
using ART.Domotica.IoTContract;
using Autofac;

namespace ART.Domotica.Worker.Consumers
{
    public class TemperatureScaleConsumer : ConsumerBase, ITemperatureScaleConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllConsumer;
        private readonly EventingBasicConsumer _getAllForDeviceConsumer;

        private readonly IComponentContext _componentContext;

        #endregion

        #region constructors

        public TemperatureScaleConsumer(IConnection connection, IComponentContext componentContext) : base(connection)
        {
            _getAllConsumer = new EventingBasicConsumer(_model);
            _getAllForDeviceConsumer = new EventingBasicConsumer(_model);

            _componentContext = componentContext;

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
                  queue: TemperatureScaleConstants.GetAllForDeviceQueueName
                , durable: false
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueBind(
                  queue: TemperatureScaleConstants.GetAllForDeviceQueueName
                , exchange: "amq.topic"
                , routingKey: TemperatureScaleConstants.GetAllForDeviceQueueName
                , arguments: null);

            _getAllConsumer.Received += GetAllReceived;
            _getAllForDeviceConsumer.Received += GetAllForDeviceReceived;

            _model.BasicConsume(TemperatureScaleConstants.GetAllQueueName, false, _getAllConsumer);
            _model.BasicConsume(TemperatureScaleConstants.GetAllForDeviceQueueName, false, _getAllForDeviceConsumer);
        }

        public void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        public async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<ITemperatureScaleDomain>();
            var data = await domain.GetAll();
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, TemperatureScaleConstants.GetAllCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        public void GetAllForDeviceReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllForDeviceReceivedAsync(sender, e));
        }

        public async Task GetAllForDeviceReceivedAsync(object sender, BasicDeliverEventArgs e)
        {            
            _model.BasicAck(e.DeliveryTag, false);
            var domain = _componentContext.Resolve<ITemperatureScaleDomain>();
            var data = await domain.GetAllForDevice();
            var deviceMessage = new MessageIoTContract<List<TemperatureScaleGetAllForDeviceResponseContract>>(TemperatureScaleConstants.GetAllForDeviceCompletedQueueName, data);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<DeviceRequestContract>(e.Body);
            var queueName = GetDeviceQueueName(requestContract.HardwareId);
            _model.BasicPublish("", queueName, null, buffer);            
        }

        #endregion
    }
}
