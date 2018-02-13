namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceDisplayProducer
    {
        #region Methods

        Task SetEnabled(AuthenticatedMessageContract<DeviceDisplaySetValueRequestContract> message);

        #endregion Methods
    }
}