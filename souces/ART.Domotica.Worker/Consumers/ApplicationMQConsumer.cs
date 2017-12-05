using ART.Domotica.Worker.IConsumers;
using ART.Infra.CrossCutting.Logging;
using ART.Infra.CrossCutting.MQ.Worker;
using Autofac;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ART.Domotica.Worker.Consumers
{
    public class ApplicationMQConsumer : ConsumerBase, IApplicationMQConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getRPCConsumer;
        private readonly IComponentContext _componentContext;
        private readonly ILogger _logger;
        
        #endregion

        #region constructors

        public ApplicationMQConsumer(IConnection connection, ILogger logger, IComponentContext componentContext) : base(connection)
        {
            _componentContext = componentContext;
            _logger = logger;
            
            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
           
        }
        

        #endregion
    }
}
