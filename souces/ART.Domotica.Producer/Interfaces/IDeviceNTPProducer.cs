namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceNTPProducer
    {
        #region Methods

        Task SetTimeZone(AuthenticatedMessageContract<DeviceNTPSetTimeZoneRequestContract> message);

        Task SetUpdateIntervalInMilliSecond(AuthenticatedMessageContract<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract> message);

        #endregion Methods
    }
}