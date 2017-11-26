using ART.Domotica.Constant;
using ART.Domotica.Contract;
using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Worker.IConsumers;
using ART.Infra.CrossCutting.Logging;
using ART.Infra.CrossCutting.MQ;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using Autofac;
using AutoMapper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationMQConsumer : ConsumerBase, IApplicationMQConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getRPCConsumer;
        private readonly IComponentContext _componentContext;
        private readonly ILogger _logger;
        private readonly IMQSettings _mqSettings;

        #endregion

        #region constructors

        public ApplicationMQConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings) : base(connection)
        {
            _getRPCConsumer = new EventingBasicConsumer(_model);

            _componentContext = componentContext;
            _logger = logger;
            _mqSettings = mqSettings;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            var queueName = ApplicationMQConstants.GetRPCQueueName;

            _model.QueueDeclare(
                 queue: queueName
               , durable: false
               , exclusive: false
               , autoDelete: false
               , arguments: null);

            _getRPCConsumer.Received += GetRPCReceived;

            _model.BasicConsume(queueName, false, _getRPCConsumer);
        }

        private void GetRPCReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetRPCReceivedAsync(sender, e));
        }

        private async Task GetRPCReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IApplicationMQDomain>();
            var data = await domain.GetById(requestContract);
            
            //Enviando para View
            var responseContract = Mapper.Map<ApplicationMQ, ApplicationMQGetRPCResponseContract>(data);

            responseContract.WebUITopic = RandonHelper.RandomString(10);

            var responseBuffer = SerializationHelpers.SerializeToJsonBufferAsync(responseContract);

            _model.BasicQos(0, 1, false);

            var props = e.BasicProperties;

            var replyProps = _model.CreateBasicProperties();

            replyProps.CorrelationId = props.CorrelationId;

            _model.BasicPublish(
                exchange: "",
                routingKey: props.ReplyTo,
                basicProperties: replyProps,
                body: responseBuffer);

            _model.BasicAck(e.DeliveryTag, false);

            _logger.DebugLeave();
        }        

        #endregion
    }
}
