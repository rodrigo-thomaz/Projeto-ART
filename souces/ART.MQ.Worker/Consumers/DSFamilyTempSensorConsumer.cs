using ART.MQ.Common.Contracts;
using ART.MQ.Common.QueueNames;
using ART.MQ.Worker.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ART.MQ.Worker.Consumers
{
    public class DSFamilyTempSensorConsumer
    {
        #region private fields

        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly EventingBasicConsumer _setResolutionConsumer;
        private readonly EventingBasicConsumer _setHighAlarmConsumer;
        private readonly EventingBasicConsumer _setLowAlarmConsumer;

        #endregion

        #region constructors

        public DSFamilyTempSensorConsumer(IConnection connection)
        {
            _connection = connection;

            _model = _connection.CreateModel();

            _setResolutionConsumer = new EventingBasicConsumer(_model);
            _setHighAlarmConsumer = new EventingBasicConsumer(_model);
            _setLowAlarmConsumer = new EventingBasicConsumer(_model);

            Initialize();
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

            _setResolutionConsumer.Received += SetResolutionReceived;
            _setHighAlarmConsumer.Received += SetHighAlarmReceived;
            _setLowAlarmConsumer.Received += SetLowAlarmReceived;

            _model.BasicConsume(DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetResolutionQueueName, false, _setResolutionConsumer);
            _model.BasicConsume(DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetHighAlarmQueueName, false, _setHighAlarmConsumer);
            _model.BasicConsume(DSFamilyTempSensorQueueNames.DSFamilyTempSensorSetLowAlarmQueueName, false, _setLowAlarmConsumer);
        }

        private void SetResolutionReceived(object sender, BasicDeliverEventArgs e)
        {
            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetResolutionContract>(e.Body);
            _model.BasicAck(e.DeliveryTag, false);
        }

        private void SetHighAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetHighAlarmContract>(e.Body);
            _model.BasicAck(e.DeliveryTag, false);
        }        

        private void SetLowAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetLowAlarmContract>(e.Body);
            _model.BasicAck(e.DeliveryTag, false);
        }        

        #endregion
    }
}
