using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
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
    public class ApplicationUserConsumer : ConsumerBase
    {
        #region private fields
        
        private readonly EventingBasicConsumer _registerUserConsumer;

        private readonly IApplicationUserDomain _applicationUserDomain;

        #endregion

        #region constructors

        public ApplicationUserConsumer(IConnection connection, IApplicationUserDomain applicationUserDomain) : base(connection)
        {
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
            Console.WriteLine("[{0}] {1}", ApplicationUserQueueName.RegisterUserQueueName, Encoding.UTF8.GetString(e.Body));

            _model.BasicAck(e.DeliveryTag, false);

            var contract = SerializationHelpers.DeserializeJsonBufferToType<RegisterUserContract>(e.Body);
            await _applicationUserDomain.RegisterUser(contract);

            Console.WriteLine("[{0}] Ok", ApplicationUserQueueName.RegisterUserCompletedQueueName);
        }

        #endregion
    }
}
