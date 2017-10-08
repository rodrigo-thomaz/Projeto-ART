namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IHardwareProducer
    {
        #region Methods

        Task GetList(AuthenticatedMessageContract message);

        #endregion Methods
    }
}