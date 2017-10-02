using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ART.MQ.Worker
{
    public class WorkerService
    {
        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly EventingBasicConsumer _setResolutionConsumer;
        private readonly EventingBasicConsumer _setHighAlarmConsumer;
        private readonly EventingBasicConsumer _setLowAlarmConsumer;

        public WorkerService(IConnection connection)
        {
            _connection = connection;

            _model = _connection.CreateModel();

            _model.QueueDeclare(
                  queue: "DSFamilyTempSensor.SetResolution"
                , durable: true
                , exclusive: false
                , autoDelete: false
                , arguments: null);

            _setResolutionConsumer = new EventingBasicConsumer(_model);
            _setHighAlarmConsumer = new EventingBasicConsumer(_model);
            _setLowAlarmConsumer = new EventingBasicConsumer(_model);

            _setResolutionConsumer.Received += SetResolutionReceived;
            _setHighAlarmConsumer.Received += SetHighAlarmReceived;
            _setLowAlarmConsumer.Received += SetLowAlarmReceived;

            _model.BasicConsume("DSFamilyTempSensor.SetResolution", false, _setResolutionConsumer);
        }        

        public bool Start()
        {   
            return true;
        }

        public bool Stop()
        {
            _connection.Close(30);
            return true;
        }

        private void SetResolutionReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            _model.BasicAck(e.DeliveryTag, false);
        }

        private void SetLowAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            _model.BasicAck(e.DeliveryTag, false);
        }

        private void SetHighAlarmReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            _model.BasicAck(e.DeliveryTag, false);
        }
    }
}
