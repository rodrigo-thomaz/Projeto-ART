namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceWiFiProducer
    {
        #region Methods

        Task SetHostName(AuthenticatedMessageContract<DeviceWiFiSetHostNameRequestContract> message);

        #endregion Methods
    }
}