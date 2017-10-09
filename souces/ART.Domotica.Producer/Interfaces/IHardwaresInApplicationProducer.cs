namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;
    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IHardwaresInApplicationProducer
    {
        #region Methods

        Task GetList(AuthenticatedMessageContract message);
        Task SearchPin(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message);
        Task InsertHardware(AuthenticatedMessageContract<HardwaresInApplicationPinContract> message);

        #endregion Methods
    }
}