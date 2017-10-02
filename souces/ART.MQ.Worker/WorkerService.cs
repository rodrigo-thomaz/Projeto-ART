using ART.MQ.Worker.Consumers;
using RabbitMQ.Client;

namespace ART.MQ.Worker
{
    public class WorkerService
    {
        #region private fields

        private readonly IConnection _connection;
        private readonly DSFamilyTempSensorConsumer _dsFamilyTempSensorConsumer;

        #endregion

        #region constructors

        public WorkerService(IConnection connection, DSFamilyTempSensorConsumer dsFamilyTempSensorConsumer)
        {
            _connection = connection;
            _dsFamilyTempSensorConsumer = dsFamilyTempSensorConsumer;
        }

        #endregion

        #region public voids

        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            _connection.Close(30);
            return true;
        }         

        #endregion
    }
}
