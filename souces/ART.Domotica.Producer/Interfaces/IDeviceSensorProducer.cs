namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceSensorProducer
    {
        #region Methods

        Task SetPublishIntervalInMilliSeconds(AuthenticatedMessageContract<DeviceSetIntervalInMilliSecondsRequestContract> message);

        Task SetReadIntervalInMilliSeconds(AuthenticatedMessageContract<DeviceSetIntervalInMilliSecondsRequestContract> message);

        #endregion Methods
    }
}