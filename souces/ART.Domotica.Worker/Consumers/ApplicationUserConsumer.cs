using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Security.Common.Contracts;
using ART.Security.Common.QueueNames;
using AutoMapper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationUserConsumer
    {
        #region private fields

        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly EventingBasicConsumer _registerUserConsumer;

        private readonly IApplicationUserDomain _applicationUserDomain;

        #endregion

        #region constructors

        public ApplicationUserConsumer(IConnection connection, IApplicationUserDomain applicationUserDomain)
        {
            _connection = connection;

            _model = _connection.CreateModel();

            _registerUserConsumer = new EventingBasicConsumer(_model);

            _applicationUserDomain = applicationUserDomain;

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

            _registerUserConsumer.Received += RegisterUserReceived;

            _model.BasicConsume(queueName, false, _registerUserConsumer);
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

            var contract = SerializationHelpers.DeserializeJsonBufferToType<ApplicationUserContract>(e.Body);
            var entity = Mapper.Map<ApplicationUserContract, ApplicationUser>(contract);
            await _applicationUserDomain.RegisterUser(entity);
            Console.WriteLine("[ApplicationUserDomain.RegisterUser] Ok");
        }

        #endregion
    }
}
