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

    public class DeviceSensorsProducer : ProducerBase, IDeviceSensorsProducer
    {
        #region Constructors

        public DeviceSensorsProducer(IConnection connection, IMQSettings mqSettings)
            : base(connection, mqSettings)
        {
            
        }

        #endregion Constructors

        #region public voids

        public async Task SetPublishIntervalInMilliSeconds(AuthenticatedMessageContract<DeviceSensorsSetPublishIntervalInMilliSecondsRequestContract> message)
        {
            await BasicPublish(DeviceSensorsConstants.SetPublishIntervalInMilliSecondsQueueName, message);
        }

        #endregion
    }
}