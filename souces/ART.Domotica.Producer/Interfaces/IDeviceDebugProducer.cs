namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceDebugProducer
    {
        #region Methods

        Task SetActive(AuthenticatedMessageContract<DeviceDebugSetActiveRequestContract> message);

        #endregion Methods
    }
}