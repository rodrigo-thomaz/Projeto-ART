using ART.Data.Domain.Interfaces;
using ART.Data.Repository.Entities;
using ART.MQ.Common.Contracts;
using ART.MQ.Common.QueueNames;
using ART.MQ.Worker.Contracts;
using ART.MQ.Worker.Helpers;
using ART.MQ.Worker.Models;
using Autofac;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ART.MQ.Worker.Consumers
{
    public class DSFamilyTempSensorConsumer
    {
        #region private fields

        private readonly IComponentContext _context;

        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly EventingBasicConsumer _getResolutionsConsumer;
        private readonly EventingBasicConsumer _setResolutionConsumer;
        private readonly EventingBasicConsumer _setHighAlarmConsumer;
        private readonly EventingBasicConsumer _setLowAlarmConsumer;

        private readonly IDSFamilyTempSensorDomain _dsFamilyTempSensorDomain;

        #endregion

        #region constructors

        public DSFamilyTempSensorConsumer(IComponentContext context, IConnection connection, IDSFamilyTempSensorDomain dsFamilyTempSensorDomain)
        {
            _context = context;

            _connection = connection;

            _model = _connection.CreateModel();

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
                  queue: DSFamilyTempSensorQueueNames.GetResolutionsQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorQueueNames.SetResolutionQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorQueueNames.SetHighAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorQueueNames.SetLowAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _getResolutionsConsumer.Received += GetResolutionsReceived;
            _setResolutionConsumer.Received += SetResolutionReceived;
            _setHighAlarmConsumer.Received += SetHighAlarmReceived;
            _setLowAlarmConsumer.Received += SetLowAlarmReceived;

            _model.BasicConsume(DSFamilyTempSensorQueueNames.GetResolutionsQueueName, false, _getResolutionsConsumer);
            _model.BasicConsume(DSFamilyTempSensorQueueNames.SetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(DSFamilyTempSensorQueueNames.SetHighAlarmQueueName, false, _setHighAlarmConsumer);
            _model.BasicConsume(DSFamilyTempSensorQueueNames.SetLowAlarmQueueName, false, _setLowAlarmConsumer);
        }

        private void GetResolutionsReceived(object sender, BasicDeliverEventArgs e)
        {
            Task.WaitAll(GetResolutionsReceivedAsync(sender, e));            
        }

        private async Task GetResolutionsReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.GetResolutionsReceivedAsync] ");
            _model.BasicAck(e.DeliveryTag, false);

            var session = DeserializationHelpers.Deserialize<string>(e.Body);
            var exchange = string.Format("{0}-{1}", session, "GetResolutionsCompleted");

            //var dsFamilyTempSensorDomain = _context.Resolve<IDSFamilyTempSensorDomain>();
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
            //Console.WriteLine("[DSFamilyTempSensorConsumer.SetResolutionReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetResolutionContract>(e.Body);

            await _dsFamilyTempSensorDomain.SetResolution(contract.DSFamilyTempSensorId, contract.DSFamilyTempSensorResolutionId);
            Console.WriteLine("[DSFamilyTempSensorDomain.SetResolution] Ok");

            var queueName = await GetQueueName(contract.DSFamilyTempSensorId);
            var deviceMessage = new DeviceMessageContract<DSFamilyTempSensorSetResolutionContract>(DSFamilyTempSensorQueueNames.SetResolutionQueueName, contract);
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
            //Console.WriteLine("[DSFamilyTempSensorConsumer.SetHighAlarmReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetHighAlarmContract>(e.Body);

            await _dsFamilyTempSensorDomain.SetHighAlarm(contract.DSFamilyTempSensorId, contract.HighAlarm);
            Console.WriteLine("[DSFamilyTempSensorDomain.SetHighAlarm] Ok");

            var queueName = await GetQueueName(contract.DSFamilyTempSensorId);
            var deviceMessage = new DeviceMessageContract<DSFamilyTempSensorSetHighAlarmContract>(DSFamilyTempSensorQueueNames.SetHighAlarmQueueName, contract);
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
            //Console.WriteLine("[DSFamilyTempSensorConsumer.SetLowAlarmReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);

            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetLowAlarmContract>(e.Body);

            await _dsFamilyTempSensorDomain.SetLowAlarm(contract.DSFamilyTempSensorId, contract.LowAlarm);
            Console.WriteLine("[DSFamilyTempSensorDomain.SetLowAlarm] Ok");

            var queueName = await GetQueueName(contract.DSFamilyTempSensorId);
            var deviceMessage = new DeviceMessageContract<DSFamilyTempSensorSetLowAlarmContract>(DSFamilyTempSensorQueueNames.SetLowAlarmQueueName, contract);
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
