using ART.Domotica.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using log4net;

namespace ART.Domotica.Worker.Consumers
{
    public class TemperatureScaleConsumer : ConsumerBase
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllConsumer;

        private readonly ITemperatureScaleDomain _temperatureScaleDomain;

        #endregion

        #region constructors

        public TemperatureScaleConsumer(IConnection connection, ILog log, ITemperatureScaleDomain temperatureScaleDomain) : base(connection, log)
        {
            _getAllConsumer = new EventingBasicConsumer(_model);

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

            _getAllConsumer.Received += GetAllReceived;

            _model.BasicConsume(TemperatureScaleConstants.GetAllQueueName, false, _getAllConsumer);
        }

        private void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        private async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[{0}] {1}", TemperatureScaleConstants.GetAllQueueName, Encoding.UTF8.GetString(e.Body));

            _model.BasicAck(e.DeliveryTag, false);

            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var data = await _temperatureScaleDomain.GetAll();
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(data);
            var exchange = "amq.topic";
            var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, TemperatureScaleConstants.GetAllCompletedQueueName);

            Console.WriteLine("[{0}] {1}", TemperatureScaleConstants.GetAllCompletedQueueName, Encoding.UTF8.GetString(buffer));

            _model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion
    }
}
