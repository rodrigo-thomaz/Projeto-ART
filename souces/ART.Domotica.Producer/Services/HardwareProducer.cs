using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.Producer.Services
{
    public class HardwareProducer : ProducerBase, IHardwareProducer
    {
        #region constructors

        public HardwareProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        //public async Task Get(AuthenticatedMessageContract message)
        //{
        //    await Task.Run(() => 
        //    {
        //        var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
        //        _model.BasicPublish("", HardwareConstants.GetQueueName, null, payload);
        //    });            
        //}

        #endregion

        #region private voids

        private void Initialize()
        {
            //_model.QueueDeclare(
            //      queue: HardwareConstants.GetQueueName
            //    , durable: false
            //    , exclusive: false
            //    , autoDelete: true
            //    , arguments: null);

            _basicProperties.Persistent = true;
        }

        #endregion
    }
}