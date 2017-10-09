namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;
    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IHardwaresInApplicationProducer
    {
        #region Methods

        Task GetList(AuthenticatedMessageContract message);
        Task SearchPin(AuthenticatedMessageContract<HardwaresInApplicationSearchPinContract> message);

        #endregion Methods
    }
}