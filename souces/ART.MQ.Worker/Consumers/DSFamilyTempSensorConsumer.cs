using ART.Data.Domain.Interfaces;
using ART.MQ.Common.Contracts;
using ART.MQ.Common.QueueNames;
using ART.MQ.Worker.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

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

        private readonly IDSFamilyTempSensorDomain _dsFamilyTempSensorDomain;

        #endregion

        #region constructors

        public DSFamilyTempSensorConsumer(IConnection connection, IDSFamilyTempSensorDomain dsFamilyTempSensorDomain)
        {
            _connection = connection;

            _model = _connection.CreateModel();

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
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.SetResolutionReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetResolutionContract>(e.Body);
            Task.WaitAll(_dsFamilyTempSensorDomain.SetResolution(contract.DSFamilyTempSensorId, contract.DSFamilyTempSensorResolutionId));
            Console.WriteLine("[DSFamilyTempSensorDomain.SetResolution] Ok");
        }

        private void SetHighAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.SetHighAlarmReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetHighAlarmContract>(e.Body);
            Task.WaitAll(_dsFamilyTempSensorDomain.SetHighAlarm(contract.DSFamilyTempSensorId, contract.HighAlarm));
            Console.WriteLine("[DSFamilyTempSensorDomain.SetHighAlarm] Ok");
        }        

        private void SetLowAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("[DSFamilyTempSensorConsumer.SetLowAlarmReceived] {0}", Encoding.UTF8.GetString(e.Body));
            _model.BasicAck(e.DeliveryTag, false);
            var contract = DeserializationHelpers.Deserialize<DSFamilyTempSensorSetLowAlarmContract>(e.Body);
            Task.WaitAll(_dsFamilyTempSensorDomain.SetLowAlarm(contract.DSFamilyTempSensorId, contract.LowAlarm));
            Console.WriteLine("[DSFamilyTempSensorDomain.SetLowAlarm] Ok");
        }        

        #endregion
    }
}
