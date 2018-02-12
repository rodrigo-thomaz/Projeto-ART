namespace ART.Domotica.Producer.Services
{
    using System.Threading.Tasks;
    using ART.Domotica.Contract;
    using ART.Domotica.Producer.Interfaces;
    using ART.Infra.CrossCutting.MQ.Contract;
    using ART.Infra.CrossCutting.MQ.Producer;

    using RabbitMQ.Client;
    using ART.Domotica.Constant;
    using ART.Infra.CrossCutting.MQ;

    public class DeviceSensorProducer : ProducerBase, IDeviceSensorProducer
    {
        #region Constructors

        public DeviceSensorProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion Constructors

        #region public voids

        public async Task SetReadIntervalInMilliSeconds(AuthenticatedMessageContract<DeviceSetIntervalInMilliSecondsRequestContract> message)
        {
            await BasicPublish(DeviceSensorConstants.SetReadIntervalInMilliSecondsQueueName, message);
        }

        public async Task SetPublishIntervalInMilliSeconds(AuthenticatedMessageContract<DeviceSetIntervalInMilliSecondsRequestContract> message)
        {
            await BasicPublish(DeviceSensorConstants.SetPublishIntervalInMilliSecondsQueueName, message);
        }

        #endregion
    }
}