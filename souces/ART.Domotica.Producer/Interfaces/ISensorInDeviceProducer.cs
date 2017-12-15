namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorInDeviceProducer
    {
        #region Methods

        Task SetOrdination(AuthenticatedMessageContract<SensorInDeviceSetOrdinationRequestContract> message);

        #endregion Methods
    }
}