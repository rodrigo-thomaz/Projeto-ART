using ART.Domotica.Contract;
using RabbitMQ.Client;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Domotica.Producer.Interfaces;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.Utils;

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
        
        public async Task SetLabel(AuthenticatedMessageContract<HardwareSetLabelRequestContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", HardwareConstants.SetLabelQueueName, null, payload);
            });
        }

        #endregion

        #region private voids

        private void Initialize()
        {           
            _model.QueueDeclare(
                 queue: HardwareConstants.SetLabelQueueName
               , durable: true
               , exclusive: false
               , autoDelete: false
               , arguments: null);
        }

        #endregion
    }
}