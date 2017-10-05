using ART.Domotica.Common.Contracts;
using ART.Domotica.Common.QueueNames;
using ART.Domotica.WebApi.IProducers;
using ART.Domotica.WebApi.Models;
using AutoMapper;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System;
using ART.Infra.CrossCutting.MQ;

namespace ART.Domotica.WebApi.Producers
{
    public class DSFamilyTempSensorProducer : IDSFamilyTempSensorProducer
    {
        #region private readonly fields

        private readonly IConnection _connection;
        private readonly IModel _model;
        private readonly IBasicProperties _basicProperties;

        #endregion

        #region constructors

        public DSFamilyTempSensorProducer(IConnection connection)
        {
            _connection = connection;
            _model = _connection.CreateModel();
            _basicProperties = _model.CreateBasicProperties();

            Initialize();
        }

        #endregion

        #region public voids

        public async Task GetAll(Guid applicationId, string session)
        {
            var contract = new DSFamilyTempSensorGetAllContract { ApplicationId = applicationId, Session = session };
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(contract);
            await Task.Run(() => _model.BasicPublish("", DSFamilyTempSensorQueueNames.GetAllQueueName, null, payload));
        }

        public async Task GetResolutions(string session)
        {
            var contract = new DSFamilyTempSensorGetResolutionsContract { Session = session };
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(contract);
            await Task.Run(() => _model.BasicPublish("", DSFamilyTempSensorQueueNames.GetResolutionsQueueName, null, payload));
        }

        public async Task SetResolution(DSFamilyTempSensorSetResolutionModel request)
        {
            var contract = Mapper.Map<DSFamilyTempSensorSetResolutionModel, DSFamilyTempSensorSetResolutionContract>(request);
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(contract);
            _model.BasicPublish("", DSFamilyTempSensorQueueNames.SetResolutionQueueName, null, payload);
        }

        public async Task SetHighAlarm(DSFamilyTempSensorSetHighAlarmModel request)
        {
            var contract = Mapper.Map<DSFamilyTempSensorSetHighAlarmModel, DSFamilyTempSensorSetHighAlarmContract>(request);
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(contract);
            _model.BasicPublish("", DSFamilyTempSensorQueueNames.SetHighAlarmQueueName, null, payload);
        }

        public async Task SetLowAlarm(DSFamilyTempSensorSetLowAlarmModel request)
        {
            var contract = Mapper.Map<DSFamilyTempSensorSetLowAlarmModel, DSFamilyTempSensorSetLowAlarmContract>(request);
            var payload = await SerializationHelpers.SerializeToJsonBufferAsync(contract);
            _model.BasicPublish("", DSFamilyTempSensorQueueNames.SetLowAlarmQueueName, null, payload);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: DSFamilyTempSensorQueueNames.GetAllQueueName
                , durable: false
                , exclusive: false
                , autoDelete: true
                , arguments: null);

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

            _basicProperties.Persistent = true;
        }        

        #endregion
    }
}