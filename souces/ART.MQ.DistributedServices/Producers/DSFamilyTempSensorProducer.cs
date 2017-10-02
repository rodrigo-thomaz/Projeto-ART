using ART.MQ.Common.Contracts;
using ART.MQ.Common.QueueNames;
using ART.MQ.DistributedServices.Helpers;
using ART.MQ.DistributedServices.IProducers;
using ART.MQ.DistributedServices.Models;
using AutoMapper;
using RabbitMQ.Client;

namespace ART.MQ.DistributedServices.Producers
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

        public void SetResolution(DSFamilyTempSensorSetResolutionModel request)
        {
            var contract = Mapper.Map<DSFamilyTempSensorSetResolutionModel, DSFamilyTempSensorSetResolutionContract>(request);
            byte[] payload = SerializationHelpers.SerialiseIntoBinary(contract);
            _model.BasicPublish("", DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetResolutionQueueName, null, payload);
        }

        public void SetHighAlarm(DSFamilyTempSensorSetHighAlarmModel request)
        {
            var contract = Mapper.Map<DSFamilyTempSensorSetHighAlarmModel, DSFamilyTempSensorSetHighAlarmContract>(request);
            byte[] payload = SerializationHelpers.SerialiseIntoBinary(contract);
            _model.BasicPublish("", DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetHighAlarmQueueName, null, payload);
        }

        public void SetLowAlarm(DSFamilyTempSensorSetLowAlarmModel request)
        {
            var contract = Mapper.Map<DSFamilyTempSensorSetLowAlarmModel, DSFamilyTempSensorSetLowAlarmContract>(request);
            byte[] payload = SerializationHelpers.SerialiseIntoBinary(contract);
            _model.BasicPublish("", DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetLowAlarmQueueName, null, payload);
        }

        #endregion

        #region private voids

        private void Initialize()
        {
            _model.QueueDeclare(
                  queue: DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetResolutionQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetHighAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _model.QueueDeclare(
                  queue: DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetLowAlarmQueueName
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _basicProperties.Persistent = true;
        }

        #endregion
    }
}