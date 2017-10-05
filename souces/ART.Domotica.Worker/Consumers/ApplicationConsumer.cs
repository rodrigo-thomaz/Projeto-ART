using ART.Domotica.Domain.Interfaces;
using ART.Security.Common.QueueNames;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationConsumer
    {
        #region private fields

        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly EventingBasicConsumer _getAllConsumer;

        private readonly IApplicationDomain _applicationDomain;

        #endregion

        #region constructors

        public ApplicationConsumer(IConnection connection, IApplicationDomain applicationDomain)
        {
            _connection = connection;

            _model = _connection.CreateModel();

            _getAllConsumer = new EventingBasicConsumer(_model);

            _applicationDomain = applicationDomain;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            var queueName = ApplicationUserQueueName.RegisterUserQueueName;

            _model.QueueDeclare(
                 queue: queueName
               , durable: true
               , exclusive: false
               , autoDelete: false
               , arguments: null);

            _getAllConsumer.Received += RegisterUserReceived;

            _model.BasicConsume(queueName, false, _getAllConsumer);
        }

        private void RegisterUserReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(RegisterUserAsync(sender, e));
        }

        private async Task RegisterUserAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[ApplicationUserConsumer.RegisterUserAsync] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            //var contract = SerializationHelpers.DeserializeJsonBufferToType<ApplicationUserContract>(e.Body);
            //var entity = Mapper.Map<ApplicationUserContract, ApplicationUser>(contract);
            //await _applicationDomain.RegisterUser(entity);
            //Console.WriteLine("[ApplicationUserDomain.RegisterUser] Ok");
        }

        #endregion
    }
}
