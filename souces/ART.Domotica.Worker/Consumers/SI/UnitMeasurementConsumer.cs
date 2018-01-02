using ART.Domotica.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using System.Collections.Generic;
using ART.Domotica.IoTContract;
using Autofac;
using AutoMapper;
using ART.Infra.CrossCutting.Logging;
using ART.Domotica.Repository.Entities.SI;
using ART.Domotica.Domain.Interfaces.SI;
using ART.Domotica.Constant.SI;
using ART.Domotica.Model.SI;
using ART.Domotica.Worker.IConsumers.SI;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Worker.Consumers.SI
{
    public class UnitMeasurementConsumer : ConsumerBase, IUnitMeasurementConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllConsumer;
        private readonly EventingBasicConsumer _getAllForIoTConsumer;

        #endregion

        #region constructors

        public UnitMeasurementConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _getAllConsumer = new EventingBasicConsumer(_model);
            _getAllForIoTConsumer = new EventingBasicConsumer(_model);

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(UnitMeasurementConstants.GetAllQueueName);
            BasicQueueDeclare(UnitMeasurementConstants.GetAllForIoTQueueName);

            _model.QueueBind(
                  queue: UnitMeasurementConstants.GetAllForIoTQueueName
                , exchange: "amq.topic"
                , routingKey: GetApplicationRoutingKeyForAllIoT(UnitMeasurementConstants.GetAllForIoTQueueName)
                , arguments: CreateBasicArguments());

            _getAllConsumer.Received += GetAllReceived;
            _getAllForIoTConsumer.Received += GetAllForIoTReceived;

            _model.BasicConsume(UnitMeasurementConstants.GetAllQueueName, false, _getAllConsumer);
            _model.BasicConsume(UnitMeasurementConstants.GetAllForIoTQueueName, false, _getAllForIoTConsumer);
        }

        public void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        public async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);
            var domain = _componentContext.Resolve<IUnitMeasurementDomain>();
            var data = await domain.GetAll();

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<UnitMeasurement>, List<UnitMeasurementGetModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, UnitMeasurementConstants.GetAllCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        public void GetAllForIoTReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllForIoTReceivedAsync(sender, e));
        }

        public async Task GetAllForIoTReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<IoTRequestContract>(e.Body);
            var unitMeasurementDomain = _componentContext.Resolve<IUnitMeasurementDomain>();            
            var data = await unitMeasurementDomain.GetAll();

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByDeviceId(requestContract.DeviceId);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(requestContract.DeviceId, requestContract.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<List<UnitMeasurement>, List<UnitMeasurementGetAllForIoTResponseContract>>(data);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);            
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, UnitMeasurementConstants.GetAllForIoTCompletedQueueName);
            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
