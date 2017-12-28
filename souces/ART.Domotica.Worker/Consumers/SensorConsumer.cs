using ART.Domotica.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Worker.IConsumers;
using System.Collections.Generic;
using Autofac;
using AutoMapper;
using ART.Domotica.Repository.Entities;
using ART.Domotica.Model;
using ART.Infra.CrossCutting.Logging;
using ART.Domotica.Contract;
using ART.Domotica.IoTContract;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Worker.Consumers
{
    public class SensorConsumer : ConsumerBase, ISensorConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _getAllByApplicationIdConsumer;
        private readonly EventingBasicConsumer _getAllByDeviceInApplicationIdConsumer;
        private readonly EventingBasicConsumer _setLabelConsumer;

        #endregion

        #region constructors

        public SensorConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _getAllByApplicationIdConsumer = new EventingBasicConsumer(_model);
            _getAllByDeviceInApplicationIdConsumer = new EventingBasicConsumer(_model);
            _setLabelConsumer = new EventingBasicConsumer(_model);

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(SensorConstants.GetAllByApplicationIdQueueName);
            BasicQueueDeclare(SensorConstants.SetLabelQueueName);
            BasicQueueDeclare(SensorConstants.GetAllByDeviceInApplicationIdIoTQueueName);            

            _model.QueueBind(
                  queue: SensorConstants.GetAllByDeviceInApplicationIdIoTQueueName
                , exchange: "amq.topic"
                , routingKey: GetApplicationRoutingKeyForAllIoT(SensorConstants.GetAllByDeviceInApplicationIdIoTQueueName)
                , arguments: CreateBasicArguments());

            _getAllByApplicationIdConsumer.Received += GetAllByApplicationIdReceived;
            _getAllByDeviceInApplicationIdConsumer.Received += GetAllByDeviceInApplicationIdReceived;
            _setLabelConsumer.Received += SetLabelReceived;

            _model.BasicConsume(SensorConstants.GetAllByApplicationIdQueueName, false, _getAllByApplicationIdConsumer);
            _model.BasicConsume(SensorConstants.GetAllByDeviceInApplicationIdIoTQueueName, false, _getAllByDeviceInApplicationIdConsumer);
            _model.BasicConsume(SensorConstants.SetLabelQueueName, false, _setLabelConsumer);
        }

        public void GetAllByApplicationIdReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllByApplicationIdReceivedAsync(sender, e));
        }

        public async Task GetAllByApplicationIdReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract>(e.Body);

            var applicationUserDomain = _componentContext.Resolve<IApplicationUserDomain>();
            var applicationUser = await applicationUserDomain.GetByKey(message.ApplicationUserId);

            var domain = _componentContext.Resolve<ISensorDomain>();
            var data = await domain.GetAllByApplicationId(applicationUser.ApplicationId);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<List<Sensor>, List<SensorGetModel>>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForView(applicationMQ.Topic, message.WebUITopic, SensorConstants.GetAllByApplicationIdCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }


        private void GetAllByDeviceInApplicationIdReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllByDeviceInApplicationIdReceivedAsync(sender, e));
        }

        private async Task GetAllByDeviceInApplicationIdReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var requestContract = SerializationHelpers.DeserializeJsonBufferToType<IoTRequestContract>(e.Body);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByDeviceId(requestContract.DeviceId);

            var domain = _componentContext.Resolve<ISensorDomain>();
            var data = await domain.GetAllByDeviceInApplicationId(applicationMQ.Id, requestContract.DeviceId, requestContract.DeviceDatasheetId);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(requestContract.DeviceId, requestContract.DeviceDatasheetId);

            var exchange = "amq.topic";

            //Enviando para o Iot
            var iotContract = Mapper.Map<List<Sensor>, List<SensorGetAllByDeviceInApplicationIdResponseIoTContract>>(data);
            var deviceMessage = new MessageIoTContract<List<SensorGetAllByDeviceInApplicationIdResponseIoTContract>>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorConstants.GetAllByDeviceInApplicationIdCompletedIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetLabelReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetLabelReceivedAsync(sender, e));
        }

        public async Task SetLabelReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorSetLabelRequestContract>>(e.Body);
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var data = await sensorDomain.SetLabel(message.Contract.SensorId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.Label);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Enviando para View
            var viewModel = Mapper.Map<Sensor, SensorSetLabelModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorConstants.SetLabelViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
