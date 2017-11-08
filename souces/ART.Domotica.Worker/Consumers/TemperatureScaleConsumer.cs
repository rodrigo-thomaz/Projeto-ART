using ART.Domotica.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Worker.IConsumers;
using ART.Domotica.Worker.Contracts;
using System.Collections.Generic;
using ART.Domotica.Contract;

namespace ART.Domotica.Worker.Consumers
{
    public class TemperatureScaleConsumer : ConsumerBase, ITemperatureScaleConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllConsumer;
        private readonly EventingBasicConsumer _getAllForDeviceConsumer;

        private readonly ITemperatureScaleDomain _temperatureScaleDomain;

        #endregion

        #region constructors

        public TemperatureScaleConsumer(IConnection connection, ITemperatureScaleDomain temperatureScaleDomain) : base(connection)
        {
            _getAllConsumer = new EventingBasicConsumer(_model);
            _getAllForDeviceConsumer = new EventingBasicConsumer(_model);

            _temperatureScaleDomain = temperatureScaleDomain;

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
            var data = await _temperatureScaleDomain.GetAll();
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
            var data = await _temperatureScaleDomain.GetAllForDevice();
            var deviceMessage = new DeviceMessageContract<List<TemperatureScaleGetAllForDeviceResponseContract>>(TemperatureScaleConstants.GetAllForDeviceCompletedQueueName, data);
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<DeviceRequestContract>(e.Body);
            var queueName = GetDeviceQueueName(requestContract.HardwareId);
            _model.BasicPublish("", queueName, null, buffer);            
        }

        #endregion
    }
}
