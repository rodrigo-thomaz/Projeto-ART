namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IHardwaresInApplicationProducer
    {
        #region Methods

        Task DeleteHardware(AuthenticatedMessageContract<HardwaresInApplicationDeleteHardwareContract> message);

        Task GetList(AuthenticatedMessageContract message);

        Task InsertHardware(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message);

        Task SearchPin(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message);

        #endregion Methods
    }
}