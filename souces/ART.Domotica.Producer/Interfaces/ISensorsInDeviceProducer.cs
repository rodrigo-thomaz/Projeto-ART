namespace ART.Domotica.Producer.Interfaces
{
    using System.Threading.Tasks;

    using ART.Infra.CrossCutting.MQ.Contract;

    public interface ISensorsInDeviceProducer
    {
        #region Methods

        Task GetAllByApplicationId(AuthenticatedMessageContract message);

        #endregion Methods
    }
}