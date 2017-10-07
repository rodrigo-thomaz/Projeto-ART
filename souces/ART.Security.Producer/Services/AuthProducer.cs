using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Producer;
using ART.Security.Constant;
using ART.Security.Contract;
using ART.Security.Producer.Interfaces;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace ART.Security.Producer.Services
{
    public class AuthProducer : ProducerBase, IAuthProducer
    {
        #region constructors

        public AuthProducer(IConnection connection) : base(connection)
        {
            Initialize();
        }

        #endregion

        #region public voids

        public async Task RegisterUser(NoAuthenticatedMessageContract<RegisterUserContract> message)
        {
            await Task.Run(() =>
            {
                var payload = SerializationHelpers.SerializeToJsonBufferAsync(message);
                _model.BasicPublish("", ApplicationUserQueueName.RegisterUserQueueName, null, payload);
            });
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: ApplicationUserQueueName.RegisterUserQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _basicProperties.Persistent = true;
        }

        #endregion
    }
}