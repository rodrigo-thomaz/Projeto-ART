namespace ART.Domotica.Worker.Consumers
{
    using ART.Domotica.Constant;
    using ART.Domotica.Contract;
    using ART.Domotica.Domain.Interfaces;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Worker.IConsumers;
    using ART.Infra.CrossCutting.Logging;
    using ART.Infra.CrossCutting.MQ;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Worker;
    using ART.Infra.CrossCutting.Utils;
    using Autofac;
    using global::AutoMapper;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DeviceInApplicationConsumer : ConsumerBase, IDeviceInApplicationConsumer
    {
        #region Fields

        private readonly EventingBasicConsumer _insertConsumer;
        private readonly EventingBasicConsumer _removeConsumer;

        #endregion Fields

        #region Constructors

        public DeviceInApplicationConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings, logger, componentContext)
        {
            _insertConsumer = new EventingBasicConsumer(_model);
            _removeConsumer = new EventingBasicConsumer(_model);

            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            BasicQueueDeclare(DeviceInApplicationConstants.InsertQueueName);
            BasicQueueDeclare(DeviceInApplicationConstants.RemoveQueueName);

            _insertConsumer.Received += InsertReceived;
            _removeConsumer.Received += RemoveReceived;

            _model.BasicConsume(DeviceInApplicationConstants.InsertQueueName, false, _insertConsumer);
            _model.BasicConsume(DeviceInApplicationConstants.RemoveQueueName, false, _removeConsumer);
         }

        #endregion Methods

        #region private voids       

        public void InsertReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(InsertReceivedAsync(sender, e));
        }

        public async Task InsertReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceInApplicationInsertRequestContract>>(e.Body);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            var domain = _componentContext.Resolve<IDeviceInApplicationDomain>();
            var data = await domain.Insert(applicationMQ.Id, message.ApplicationUserId, message.Contract.Pin);

            //Enviando para View
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceInApplicationConstants.InsertViewCompletedQueueName);
            var viewModel = Mapper.Map<ESPDevice, ESPDeviceGetModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            //Enviando sensores para View
            var sensorRountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorConstants.InsertInApplicationViewCompletedQueueName);
            var sensorViewModel = Mapper.Map<IEnumerable<Sensor>, IEnumerable<SensorGetModel>>(data.DeviceSensor.SensorInDevice.Select(x => x.Sensor));
            var sensorViewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(sensorViewModel, true);
            _model.BasicPublish(defaultExchangeTopic, sensorRountingKey, null, sensorViewBuffer);

            //Enviando para o Iot
            var iotContract = Mapper.Map<ESPDevice, DeviceInApplicationInsertResponseIoTContract>(data);
            iotContract.ApplicationTopic = applicationMQ.Topic;
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(iotContract);
            var routingKey = GetDeviceRoutingKeyForIoT(data.DeviceMQ.Topic, DeviceInApplicationConstants.InsertIoTQueueName);
            _model.BasicPublish(defaultExchangeTopic, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void RemoveReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(RemoveReceivedAsync(sender, e));
        }

        public async Task RemoveReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<DeviceInApplicationRemoveRequestContract>>(e.Body);

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            var domain = _componentContext.Resolve<IDeviceInApplicationDomain>();
            var data = await domain.Remove(applicationMQ.Id, message.Contract.DeviceTypeId, message.Contract.DeviceDatasheetId, message.Contract.DeviceId);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(data.DeviceTypeId, data.DeviceDatasheetId, data.Id);

            //Enviando para View
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, DeviceInApplicationConstants.RemoveViewCompletedQueueName);
            var viewModel = Mapper.Map<DeviceBase, DeviceInApplicationRemoveModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            _model.BasicPublish(defaultExchangeTopic, rountingKey, null, viewBuffer);

            //Enviando sensores para View
            var sensorRountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorConstants.DeleteFromApplicationViewCompletedQueueName);
            var sensorViewModel = Mapper.Map<IEnumerable<Sensor>, IEnumerable<SensorGetModel>>(data.DeviceSensor.SensorInDevice.Select(x => x.Sensor));
            var sensorViewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(sensorViewModel, true);
            _model.BasicPublish(defaultExchangeTopic, sensorRountingKey, null, sensorViewBuffer);

            //Enviando para o IoT
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, DeviceInApplicationConstants.RemoveIoTQueueName);
            _model.BasicPublish(defaultExchangeTopic, routingKey, null, null);

            _logger.DebugLeave();
        }        

        #endregion Other
    }
}