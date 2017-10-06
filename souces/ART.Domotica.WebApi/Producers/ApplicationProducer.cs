using ART.Domotica.WebApi.IProducers;
using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using ART.Infra.CrossCutting.MQ;
using ART.Domotica.Constant;

namespace ART.Domotica.WebApi.Producers
{
    public class ApplicationProducer : ProducerBase, IApplicationProducer
    {
        #region constructors

        public ApplicationProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task Get(Guid applicationUserId)
        {
            //var payload = await SerializationHelpers.SerializeToJsonBufferAsync(contract);
            //await Task.Run(() => _model.BasicPublish("", DSFamilyTempSensorConstants.GetAllQueueName, null, payload));
        } 

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: ApplicationConstants.GetQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _basicProperties.Persistent = true;
        }

        #endregion
    }
}