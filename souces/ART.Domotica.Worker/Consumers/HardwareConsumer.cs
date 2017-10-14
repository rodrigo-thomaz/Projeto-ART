namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.Model;
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.MQ.Worker;
    using ART.Infra.CrossCutting.Utils;
    using RabbitMQ.Client;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HardwareConsumer : ConsumerBase, IHardwareConsumer
    {
        #region Fields


        #endregion Fields

        #region Constructors

        public HardwareConsumer(IConnection connection)
            : base(connection)
        {
            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            //_model.QueueDeclare(
            //     queue: HardwaresInApplicationConstants.GetListQueueName
            //   , durable: false
            //   , exclusive: false
            //   , autoDelete: true
            //   , arguments: null);
        }

        #endregion Methods

        #region private voids

        public async Task UpdatePinsAsync(List<HardwareUpdatePinsModel> models)
        {
            var buffer = SerializationHelpers.SerializeToJsonBufferAsync(models);
            //var exchange = "amq.topic";
            //var rountingKey = string.Format("{0}-{1}", message.SouceMQSession, HardwaresInApplicationConstants.GetListCompletedQueueName);
            //_model.BasicPublish(exchange, rountingKey, null, buffer);
        }

        #endregion Other
    }
}