namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceInApplicationProducer
    {
        #region Methods

        Task Insert(AuthenticatedMessageContract<DeviceInApplicationInsertRequestContract> message);

        Task Remove(AuthenticatedMessageContract<DeviceInApplicationRemoveRequestContract> message);

        #endregion Methods
    }
}