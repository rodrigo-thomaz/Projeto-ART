using ART.Domotica.Domain.Interfaces;
using ART.Domotica.Repository.Entities;
using ART.Infra.CrossCutting.MQ;
using ART.Domotica.Contract;
using ART.Domotica.Worker.Contracts;
using ART.Domotica.Worker.Models;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ART.Domotica.Constant;

namespace ART.Domotica.Worker.Consumers
{
    public class DSFamilyTempSensorConsumer
    {
        #region private fields

        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly EventingBasicConsumer _getAllConsumer;
        private readonly EventingBasicConsumer _getResolutionsConsumer;
        private readonly EventingBasicConsumer _setResolutionConsumer;
        private readonly EventingBasicConsumer _setHighAlarmConsumer;
        private readonly EventingBasicConsumer _setLowAlarmConsumer;

        private readonly IDSFamilyTempSensorDomain _dsFamilyTempSensorDomain;

        #endregion

        #region constructors

        public DSFamilyTempSensorConsumer(IConnection connection, IDSFamilyTempSensorDomain dsFamilyTempSensorDomain)
        {
            _connection = connection;

            _model = _connection.CreateModel();

            _getAllConsumer = new EventingBasicConsumer(_model);
            _getResolutionsConsumer = new EventingBasicConsumer(_model);
            _setResolutionConsumer = new EventingBasicConsumer(_model);
            _setHighAlarmConsumer = new EventingBasicConsumer(_model);
            _setLowAlarmConsumer = new EventingBasicConsumer(_model);

            _dsFamilyTempSensorDomain = dsFamilyTempSensorDomain;

            Initialize();
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                 queue: DSFamilyTempSensorConstants.GetAllQueueName
               , durable: false
               , exclusive: false
               , autoDelete: true
               , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.GetResolutionsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetResolutionQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetHighAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorConstants.SetLowAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _getAllConsumer.Received += GetAllReceived;
            _getResolutionsConsumer.Received += GetResolutionsReceived;
            _setResolutionConsumer.Received += SetResolutionReceived;
            _setHighAlarmConsumer.Received += SetHighAlarmReceived;
            _setLowAlarmConsumer.Received += SetLowAlarmReceived;

            _model.BasicConsume(DSFamilyTempSensorConstants.GetAllQueueName, false, _getAllConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.GetResolutionsQueueName, false, _getResolutionsConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetHighAlarmQueueName, false, _setHighAlarmConsumer);
            _model.BasicConsume(DSFamilyTempSensorConstants.SetLowAlarmQueueName, false, _setLowAlarmConsumer);
        }

        private void GetAllReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetAllReceivedAsync(sender, e));
        }

        private async Task GetAllReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.GetAllReceivedAsync] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            var contract = SerializationHelpers.DeserializeJsonBufferToType<DSFamilyTempSensorGetAllContract>(e.Body);

            var exchange = string.Format("{0}-{1}", contract.Session, "GetAllCompleted");

            var entities = await _dsFamilyTempSensorDomain.GetAll(contract.ApplicationId);
            Console.WriteLine("[DSFamilyTempSensorDomain.GetAll] Ok");

            var models = Mapper.Map<List<DSFamilyTempSensor>, List<DSFamilyTempSensorModel>>(entities);
            var json = JsonConvert.SerializeObject(models, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("amq.topic", exchange, null, buffer);
        }

        private void GetResolutionsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetResolutionsReceivedAsync(sender, e));            
        }

        private async Task GetResolutionsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.GetResolutionsReceivedAsync] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            var contract = SerializationHelpers.DeserializeJsonBufferToType<DSFamilyTempSensorGetResolutionsContract>(e.Body);

            var exchange = string.Format("{0}-{1}", contract.Session, "GetResolutionsCompleted");

            var entities = await _dsFamilyTempSensorDomain.GetResolutions();
            Console.WriteLine("[DSFamilyTempSensorDomain.GetResolutions] Ok");

            var models = Mapper.Map<List<DSFamilyTempSensorResolution>, List<DSFamilyTempSensorResolutionModel>>(entities);
            var json = JsonConvert.SerializeObject(models, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("amq.topic", exchange, null, buffer);
        }

        private void SetResolutionReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetResolutionReceivedAsync(sender, e));         
        }

        private async Task SetResolutionReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.SetResolutionReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            var contract = SerializationHelpers.DeserializeJsonBufferToType<DSFamilyTempSensorSetResolutionContract>(e.Body);

            await _dsFamilyTempSensorDomain.SetResolution(contract.DSFamilyTempSensorId, contract.DSFamilyTempSensorResolutionId);
            Console.WriteLine("[DSFamilyTempSensorDomain.SetResolution] Ok");

            var queueName = await GetQueueName(contract.DSFamilyTempSensorId);
            var deviceMessage = new DeviceMessageContract<DSFamilyTempSensorSetResolutionContract>(DSFamilyTempSensorConstants.SetResolutionQueueName, contract);
            var json = JsonConvert.SerializeObject(deviceMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("", queueName, null, buffer);
        }

        private void SetHighAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetHighAlarmReceivedAsync(sender, e));
        }

        private async Task SetHighAlarmReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.SetHighAlarmReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            var contract = SerializationHelpers.DeserializeJsonBufferToType<DSFamilyTempSensorSetHighAlarmContract>(e.Body);

            await _dsFamilyTempSensorDomain.SetHighAlarm(contract.DSFamilyTempSensorId, contract.HighAlarm);
            Console.WriteLine("[DSFamilyTempSensorDomain.SetHighAlarm] Ok");

            var queueName = await GetQueueName(contract.DSFamilyTempSensorId);
            var deviceMessage = new DeviceMessageContract<DSFamilyTempSensorSetHighAlarmContract>(DSFamilyTempSensorConstants.SetHighAlarmQueueName, contract);
            var json = JsonConvert.SerializeObject(deviceMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("", queueName, null, buffer);
        }

        private void SetLowAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(SetLowAlarmReceivedAsync(sender, e));
        }

        private async Task SetLowAlarmReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.SetLowAlarmReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            var contract = SerializationHelpers.DeserializeJsonBufferToType<DSFamilyTempSensorSetLowAlarmContract>(e.Body);

            await _dsFamilyTempSensorDomain.SetLowAlarm(contract.DSFamilyTempSensorId, contract.LowAlarm);
            Console.WriteLine("[DSFamilyTempSensorDomain.SetLowAlarm] Ok");

            var queueName = await GetQueueName(contract.DSFamilyTempSensorId);
            var deviceMessage = new DeviceMessageContract<DSFamilyTempSensorSetLowAlarmContract>(DSFamilyTempSensorConstants.SetLowAlarmQueueName, contract);
            var json = JsonConvert.SerializeObject(deviceMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var buffer = Encoding.UTF8.GetBytes(json);
            _model.BasicPublish("", queueName, null, buffer);
        }        

        private async Task<string> GetQueueName(Guid dsFamilyTempSensorId)
        {
            var entity = await _dsFamilyTempSensorDomain.GetDeviceFromSensor(dsFamilyTempSensorId);
            var queueName = string.Format("mqtt-subscription-{0}qos0", entity.DeviceBaseId);
            return queueName;
        }

        #endregion
    }
}
