namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceDebugProducer
    {
        #region Methods

        Task SetRemoteEnabled(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message);

        Task SetResetCmdEnabled(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message);

        Task SetSerialEnabled(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message);

        Task SetShowColors(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message);

        Task SetShowDebugLevel(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message);

        Task SetShowProfiler(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message);

        Task SetShowTime(AuthenticatedMessageContract<DeviceDebugSetValueRequestContract> message);

        #endregion Methods
    }
}