namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceSerialProducer
    {
        #region Methods

        Task SetEnabled(AuthenticatedMessageContract<DeviceSerialSetEnabledRequestContract> message);

        #endregion Methods
    }
}