namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDeviceSensorsProducer
    {
        #region Methods

        Task GetAllByApplicationId(AuthenticatedMessageContract message);

        #endregion Methods
    }
}