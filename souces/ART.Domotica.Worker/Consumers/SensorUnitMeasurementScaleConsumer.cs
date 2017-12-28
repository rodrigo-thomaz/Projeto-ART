using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Contract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using ART.Domotica.Constant;
using ART.Infra.CrossCutting.MQ.Contract;
using ART.Infra.CrossCutting.MQ.Worker;
using ART.Infra.CrossCutting.Utils;
using ART.Domotica.Worker.IConsumers;
using ART.Domotica.IoTContract;
using Autofac;
using ART.Infra.CrossCutting.Logging;
using AutoMapper;
using ART.Domotica.Model;
using ART.Domotica.Repository.Entities;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.Worker.Consumers
{
    public class SensorUnitMeasurementScaleConsumer : ConsumerBase, ISensorUnitMeasurementScaleConsumer
    {
        #region private fields

        private readonly EventingBasicConsumer _setRangeConsumer;
        private readonly EventingBasicConsumer _setChartLimiterConsumer;
        private readonly EventingBasicConsumer _setUnitMeasurementNumericalScaleTypeCountryConsumer;

        private readonly IComponentContext _componentContext;

        private readonly ILogger _logger;

        #endregion

        #region constructors

        public SensorUnitMeasurementScaleConsumer(IConnection connection, ILogger logger, IComponentContext componentContext, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            _setRangeConsumer = new EventingBasicConsumer(_model);
            _setChartLimiterConsumer = new EventingBasicConsumer(_model);
            _setUnitMeasurementNumericalScaleTypeCountryConsumer = new EventingBasicConsumer(_model);

            _componentContext = componentContext;

            _logger = logger;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            BasicQueueDeclare(SensorUnitMeasurementScaleConstants.SetRangeQueueName);
            BasicQueueDeclare(SensorUnitMeasurementScaleConstants.SetChartLimiterQueueName);
            BasicQueueDeclare(SensorUnitMeasurementScaleConstants.SetUnitMeasurementNumericalScaleTypeCountryQueueName);
            
            _setRangeConsumer.Received += SetRangeReceived;
            _setChartLimiterConsumer.Received += SetChartLimiterReceived;
            _setUnitMeasurementNumericalScaleTypeCountryConsumer.Received += SetUnitMeasurementNumericalScaleTypeCountryReceived;

            _model.BasicConsume(SensorUnitMeasurementScaleConstants.SetRangeQueueName, false, _setRangeConsumer);
            _model.BasicConsume(SensorUnitMeasurementScaleConstants.SetChartLimiterQueueName, false, _setChartLimiterConsumer);
            _model.BasicConsume(SensorUnitMeasurementScaleConstants.SetUnitMeasurementNumericalScaleTypeCountryQueueName, false, _setUnitMeasurementNumericalScaleTypeCountryConsumer);
        }

        public void SetRangeReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetRangeReceivedAsync(sender, e));
        }

        public async Task SetRangeReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorUnitMeasurementScaleSetValueRequestContract>>(e.Body);
            var sensorUnitMeasurementScaleDomain = _componentContext.Resolve<ISensorUnitMeasurementScaleDomain>();
            await sensorUnitMeasurementScaleDomain.SetRange(message.Contract.SensorUnitMeasurementScaleId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.Position, message.Contract.Value);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(message.Contract.SensorUnitMeasurementScaleId);

            //Enviando para View
            var viewModel = Mapper.Map<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueModel>(message.Contract);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);            
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorUnitMeasurementScaleConstants.SetRangeViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(device.DeviceSensorsId, device.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorUnitMeasurementScaleSetValueRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);            
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorUnitMeasurementScaleConstants.SetRangeIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetChartLimiterReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetChartLimiterReceivedAsync(sender, e));
        }

        public async Task SetChartLimiterReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorUnitMeasurementScaleSetValueRequestContract>>(e.Body);
            var sensorUnitMeasurementScaleDomain = _componentContext.Resolve<ISensorUnitMeasurementScaleDomain>();
            await sensorUnitMeasurementScaleDomain.SetChartLimiter(message.Contract.SensorUnitMeasurementScaleId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.Position, message.Contract.Value);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(message.Contract.SensorUnitMeasurementScaleId);

            //Enviando para View
            var viewModel = Mapper.Map<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueModel>(message.Contract);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorUnitMeasurementScaleConstants.SetChartLimiterViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            var deviceMQDomain = _componentContext.Resolve<IDeviceMQDomain>();
            var deviceMQ = await deviceMQDomain.GetByKey(device.DeviceSensorsId, device.DeviceDatasheetId);

            //Enviando para o Iot
            var iotContract = Mapper.Map<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueRequestIoTContract>(message.Contract);
            var deviceMessage = new MessageIoTContract<SensorUnitMeasurementScaleSetValueRequestIoTContract>(iotContract);
            var deviceBuffer = SerializationHelpers.SerializeToJsonBufferAsync(deviceMessage);
            var routingKey = GetApplicationRoutingKeyForIoT(applicationMQ.Topic, deviceMQ.Topic, SensorUnitMeasurementScaleConstants.SetChartLimiterIoTQueueName);
            _model.BasicPublish(exchange, routingKey, null, deviceBuffer);

            _logger.DebugLeave();
        }

        public void SetUnitMeasurementNumericalScaleTypeCountryReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetUnitMeasurementNumericalScaleTypeCountryReceivedAsync(sender, e));
        }

        public async Task SetUnitMeasurementNumericalScaleTypeCountryReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            _logger.DebugEnter();

            _model.BasicAck(e.DeliveryTag, false);
            var message = SerializationHelpers.DeserializeJsonBufferToType<AuthenticatedMessageContract<SensorUnitMeasurementScaleSetUnitMeasurementNumericalScaleTypeCountryRequestContract>>(e.Body);
            var sensorUnitMeasurementScaleDomain = _componentContext.Resolve<ISensorUnitMeasurementScaleDomain>();
            var data = await sensorUnitMeasurementScaleDomain.SetUnitMeasurementNumericalScaleTypeCountry(message.Contract.SensorUnitMeasurementScaleId, message.Contract.SensorDatasheetId, message.Contract.SensorTypeId, message.Contract.UnitMeasurementId, message.Contract.UnitMeasurementTypeId, message.Contract.NumericalScalePrefixId, message.Contract.NumericalScaleTypeId, message.Contract.CountryId);

            var exchange = "amq.topic";

            var applicationMQDomain = _componentContext.Resolve<IApplicationMQDomain>();
            var applicationMQ = await applicationMQDomain.GetByApplicationUserId(message);

            //Load device into context
            var sensorDomain = _componentContext.Resolve<ISensorDomain>();
            var device = await sensorDomain.GetDeviceFromSensor(message.Contract.SensorUnitMeasurementScaleId);

            //Enviando para View
            var viewModel = Mapper.Map<SensorUnitMeasurementScale, SensorUnitMeasurementScaleSetUnitMeasurementNumericalScaleTypeCountryModel>(data);
            var viewBuffer = SerializationHelpers.SerializeToJsonBufferAsync(viewModel, true);
            var rountingKey = GetInApplicationRoutingKeyForAllView(applicationMQ.Topic, SensorUnitMeasurementScaleConstants.SetUnitMeasurementNumericalScaleTypeCountryViewCompletedQueueName);
            _model.BasicPublish(exchange, rountingKey, null, viewBuffer);

            _logger.DebugLeave();
        }

        #endregion
    }
}
